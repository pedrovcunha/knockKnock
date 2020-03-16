using System;
using System.IO;
using System.Linq;
using System.Reflection;
using knockKnock.API.Services;
using knockKnock.API.Services.Contracts;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.AspNetCore.Mvc.Versioning;

namespace knockKnock.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers(options =>
            {
                // Adds the following response types to all controllers.
                options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status400BadRequest));
                options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status406NotAcceptable));
                options.Filters.Add(new ProducesResponseTypeAttribute(StatusCodes.Status500InternalServerError));
                options.Filters.Add(new ProducesDefaultResponseTypeAttribute());

                options.ReturnHttpNotAcceptable = true;

                var jsonOutputFormatter = options.OutputFormatters
                    .OfType<SystemTextJsonOutputFormatter>().FirstOrDefault();

                if (jsonOutputFormatter != null)
                {
                    // remove text/json as it isn't the approved media type for working with JSON at API level
                    if (jsonOutputFormatter.SupportedMediaTypes.Contains("text/json"))
                    {
                        jsonOutputFormatter.SupportedMediaTypes.Remove("text/json");
                    }
                }
            }).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);

            services.AddResponseCaching();

            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var actionExecutingContext =
                        actionContext as Microsoft.AspNetCore.Mvc.Filters.ActionExecutingContext;

                    // if there are model-state errors & all keys were correctly found/parsed we're dealing with validation errors - Returns 422 Response.
                    if (actionContext.ModelState.ErrorCount > 0
                        && actionExecutingContext?.ActionArguments.Count == actionContext.ActionDescriptor.Parameters.Count)
                    {
                        return new UnprocessableEntityObjectResult(actionContext.ModelState);
                    }

                    // if one of the keys wasn't correctly found / couldn't be parsed
                    // we're dealing with null/unparseable input
                    return new BadRequestObjectResult(actionContext.ModelState);
                };
            });

            services.AddVersionedApiExplorer(setupAction =>
            {
                setupAction.GroupNameFormat = "'v'VV";
                setupAction.SubstituteApiVersionInUrl = true;
            });

            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IFibonacciService, FibonacciService>();
            services.AddScoped<IReverseWordService, ReverseWordService>();
            services.AddScoped<ITriangleTypeService, TriangleTypeService>();

            services.AddApiVersioning(setupAction =>
            {
                setupAction.AssumeDefaultVersionWhenUnspecified = true;
                setupAction.DefaultApiVersion = new ApiVersion(1,0);
                setupAction.ReportApiVersions = true;
                // setupAction.ApiVersionReader = new HeaderApiVersionReader("api-version");
            });

            var apiVersionDescriptionProvider =
                services.BuildServiceProvider().GetService<IApiVersionDescriptionProvider>();

            // Adds the Swagger Specification Document for each API version.
            services.AddSwaggerGen(setupAction =>
            {
                // Ensures that multiple swagger documentations will be generated with the GroupName as part of their name.
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    setupAction.SwaggerDoc($"KnockKnockOpenAPISpecification{description.GroupName}", 
                        new OpenApiInfo
                    {
                        Title = "KnockKnock API",
                        Version = description.ApiVersion.ToString(),
                        Description = "Through this API you can access the KnockKnock API Challenge.",
                        Contact = new OpenApiContact()
                        {
                            Email = "pedrovcunha87@gmail.com",
                            Name = "Pedro Cunha",
                            Url = new Uri("https://www.linkedin.com/in/pedro-cunha-42052087/")
                        },
                        License = new OpenApiLicense()
                        {
                            Name = "MIT Licence",
                            Url = new Uri("https://opensource.org/licenses/MIT")
                        }
                    });
                }

                
                // Get the version associated with the action and compare to the document name, which includes the version, so we know what document to include.
                setupAction.DocInclusionPredicate((documentName, apiDescription) =>
                {
                    // Get the version. Explicit: via ApiVersion attribute | Implicit: default version
                    var actionApiVersionModel = apiDescription.ActionDescriptor
                        .GetApiVersionModel(ApiVersionMapping.Explicit | ApiVersionMapping.Implicit);

                    if (actionApiVersionModel == null)
                    {
                        return true;
                    }

                    if (actionApiVersionModel.DeclaredApiVersions.Any())
                    {
                        return actionApiVersionModel.DeclaredApiVersions.Any(v =>
                            $"KnockKnockOpenAPISpecificationv{v.ToString()}" == documentName);
                    }
                    return actionApiVersionModel.ImplementedApiVersions.Any(v =>
                        $"KnockKnockOpenAPISpecificationv{v.ToString()}" == documentName);
                });


                // As the File matches the assembly name, get name using reflection.
                // Project properties => Build => XML Documentation file checkbox. Input: Just the File name (in this case KnockKnock.API.xml).
                // This avoid conflicts when moving the project folder or files around.
                var xmlCommentFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlCommentFullPAth = Path.Combine(AppContext.BaseDirectory, xmlCommentFile);

                setupAction.IncludeXmlComments(xmlCommentFullPAth);
            });

            //services.AddHsts(options =>
            //{
            //    options.Preload = true;
            //    options.IncludeSubDomains = true;
            //    options.MaxAge = TimeSpan.FromDays(60);
            //    //options.ExcludedHosts.Add("example.com");
            //    //options.ExcludedHosts.Add("www.example.com");
            //});

            //services.AddHttpsRedirection(options =>
            //{
            //    options.RedirectStatusCode = StatusCodes.Status307TemporaryRedirect;
            //    options.HttpsPort = 5001;
            //});
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. For production scenarios, see https://aka.ms/aspnetcore-hsts.
                if (env.IsStaging() || env.IsProduction())
                {
                    app.UseExceptionHandler("/Error");
                    //app.UseHsts();
                }
                    
            }

            app.UseResponseCaching();

            //app.UseHttpsRedirection();

            // Adds Swashbuckle - Always after UseHttpsRedirection (Ensures that any call to a non-encrypted OpenAPI endpoint will be redirected to the encrypted version.
            app.UseSwagger();

            app.UseSwaggerUI(setupAction =>
            {
                 // Creates endpoints for each version passing through the groupName.
                foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
                {
                    setupAction.SwaggerEndpoint($"/swagger/" + 
                                                $"KnockKnockOpenAPISpecification{description.GroupName}/swagger.json",
                        description.GroupName.ToUpperInvariant());
                }


                // Setting RoutePrefix to empty string makes the documentation available at root.
                setupAction.RoutePrefix = "";
                setupAction.EnableDeepLinking();
                setupAction.DisplayOperationId();
            });

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
