namespace EBev.API.Extension
{
    public static class Startup
    {
        public static IServiceCollection ConfigureServices(this IServiceCollection services, WebApplicationBuilder configuration)
        {
            services.AddDbContext<EBevDbContext>(options => options.UseSqlServer(configuration.Configuration.GetConnectionString(AppConstant.APP_CONNECTIONSTRING)));
            services.AddControllers();
            services.AddEndpointsApiExplorer();

            services
                .AddOptions()
                .AddHttpContextAccessor()
                .AddSwagger()
                .AddHttpClient()
                .AddCors(o => o.AddPolicy(AppConstant.APP_Policy, builder =>
                {
                    builder.AllowAnyOrigin()
                           .AllowAnyMethod()
                           .AllowAnyHeader();
                }))
                .AddConfigurationOptions()
                .AddInfrastructureServices()
                .AddRepositoryServices()
                .AddSingleton<IConfiguration>(configuration.Configuration)
                .AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
                .AddSingleton<IContextHelper, ContextHelper>()
                .AddScoped<IUnitOfWorkRead, UnitOfWorkRead<EBevDbContext>>()
                .AddScoped<IUnitOfWorkWrite, UnitOfWorkWrite<EBevDbContext>>()
                .AddMapper()
                .AddLogging()
                .AddTransient<UnhandledExceptionHandlerMiddleware>()
                .ConfigureAuthentication(configuration.Configuration);

            return services;
        }

        #region Configure
        public static void Configure(this WebApplication app, WebApplicationBuilder configuration)
        {
            SeedData(app, configuration.Configuration);
            app
            .UseRouting()
            .UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader())
            .UseAuthentication()
            .UseAuthorization()
            .UseHttpLogging()
            .UseMiddleware<UnhandledExceptionHandlerMiddleware>()
            .UseSwagger()
            .UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", AppConstant.SWAGGER_SUPPORT_API);
                c.DefaultModelsExpandDepth(2);
                c.DefaultModelExpandDepth(2);
                c.DefaultModelsExpandDepth(-1);
                c.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
            })
            .UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", context =>
                {
                    context.Response.Redirect("/swagger/index.html", permanent: false);
                    return Task.CompletedTask;
                });
            });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
        #endregion

        #region JWT Bearer Authentication
        public static void ConfigureAuthentication(this IServiceCollection services, IConfiguration configuration)
        {

            var appSettingsSection = configuration.GetSection("JwtSettingsOptions");
            services.Configure<JwtSettingsOptions>(appSettingsSection);
            var appSettings = appSettingsSection.Get<JwtSettingsOptions>();
            services.AddSingleton(appSettings);

            #region JWT Authentication

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(appSettings.Secret)),
                ValidateIssuer = false,
                ValidateAudience = false,
                RequireExpirationTime = false,
                ValidateLifetime = true,
                ClockSkew = System.TimeSpan.Zero
            };
            services.AddSingleton(tokenValidationParameters);
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.SaveToken = true;
                x.TokenValidationParameters = tokenValidationParameters;
            });
            #endregion

            services.AddRouting(Option => Option.LowercaseUrls = true);

            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-XSRF-TOKEN";
            });
        }
        #endregion

        #region Swagger
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = AppConstant.SWAGGER_SUPPORT_DOC,
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact() { Email = AppConstant.SWAGGER_SUPPORT_EMAIL },
                    License = new Microsoft.OpenApi.Models.OpenApiLicense() { Name = AppConstant.SWAGGER_SUPPORT_LICENSE }
                });

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                     {
                           new OpenApiSecurityScheme
                             {
                                 Reference = new OpenApiReference
                                 {
                                     Type = ReferenceType.SecurityScheme,
                                     Id = "Bearer"
                                 }
                             },
                             new string[] {}
                     }
                });

                var currentAssembly = Assembly.GetExecutingAssembly();
                var xmlDocs = currentAssembly.GetReferencedAssemblies()
                .Union(new AssemblyName[] { currentAssembly.GetName() })
                .Select(a => Path.Combine(Path.GetDirectoryName(currentAssembly.Location), $"{a.Name}.xml"))
                .Where(f => File.Exists(f)).ToArray();
                Array.ForEach(xmlDocs, (d) =>
                {
                    c.IncludeXmlComments(d);
                });
            });
            return services;
        }
        #endregion

        #region AutoMapper
        public static IServiceCollection AddMapper(this IServiceCollection services)
        {
            var config = new AutoMapper.MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new AutoMapperConfiguration());
            });
            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);
            return services;
        }
        #endregion

        #region Seed Data
        private static void SeedData(this IApplicationBuilder app, ConfigurationManager configuration)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetRequiredService<EBevDbContext>();
                bool isCreated = context.Database.EnsureCreated();
                if (isCreated && !context.Person.Any())
                {
                    //Seeds(context, configuration);

                    Person person = new Person()
                    {
                        Name = "Admin",
                        ShortDescription = "Admin",
                        Description = "Admin",
                        CreatedOn = DateTime.Now,
                        CreatedBy = 9999,
                        ImageUrl = string.Empty,
                        IsActive = true
                    };
                    context.Person.AddRange(person);
                    context.SaveChanges();
                }
            }
        }

        private static void Seeds(EBevDbContext context, IConfiguration Configuration)
        {
            if (context.Person.ToList().Count == 0)
            {
                string baseDir = AppDomain.CurrentDomain.BaseDirectory.Replace("\\bin", string.Empty).Replace("\\Debug", string.Empty).Replace("\\net6.0", string.Empty);
                if (!string.IsNullOrEmpty(baseDir))
                {
                    string seedDataPath = Convert.ToString(Configuration["ServiceUrls:SeedDataURI"]);
                    if (!string.IsNullOrEmpty(seedDataPath))
                    {
                        string baseDirPath = baseDir + seedDataPath;
                        var sqlfiles = Directory.GetFiles(baseDirPath, "*.sql").ToList();
                        if (sqlfiles != null && sqlfiles.Count > 0)
                        {
                            foreach (var file in sqlfiles)
                            {
                                context.Database.ExecuteSqlRaw(File.ReadAllText(file));
                            }
                        }
                    }
                }
            }
        }
        #endregion
    }
}
