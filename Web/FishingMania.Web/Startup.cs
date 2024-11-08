namespace FishingMania.Web
{
    using System;
    using System.Configuration;
    using System.Reflection;
    using CloudinaryDotNet;
    using FishingMania.Data;
    using FishingMania.Data.Common;
    using FishingMania.Data.Common.Repositories;
    using FishingMania.Data.Models;
    using FishingMania.Data.Repositories;
    using FishingMania.Data.Seeding;
    using FishingMania.Services.Data.Contracts;
    using FishingMania.Services.Data.Services;
    using FishingMania.Services.Mapping;
    using FishingMania.Services.Messaging;
    using FishingMania.Web.Infrastructure.ModelBinders;
    using FishingMania.Web.ViewModels;

    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;

    public class Startup
    {
        private readonly IConfiguration configuration;
        private readonly IWebHostEnvironment env;

        public Startup(IConfiguration configuration, IWebHostEnvironment env)
        {
            this.configuration = configuration;
            this.env = env;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(
                options => options.UseSqlServer(this.configuration.GetConnectionString("DefaultConnection")));

            services.AddDefaultIdentity<ApplicationUser>(IdentityOptionsProvider.GetIdentityOptions)
                .AddRoles<ApplicationRole>().AddEntityFrameworkStores<ApplicationDbContext>();

            services.Configure<CookiePolicyOptions>(
                options =>
                    {
                        options.CheckConsentNeeded = context => true;
                        options.MinimumSameSitePolicy = SameSiteMode.None;
                    });

            services.AddControllersWithViews(
                options =>
                    {
                        options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    }).AddRazorRuntimeCompilation()
                    .AddMvcOptions(options => options.ModelBinderProviders.Insert(0, new DecimalModelBinderProvider()));

            services.AddRazorPages();
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddSingleton(this.configuration);

            // Data repositories
            services.AddScoped(typeof(IDeletableEntityRepository<>), typeof(EfDeletableEntityRepository<>));
            services.AddScoped(typeof(IRepository<>), typeof(EfRepository<>));
            services.AddScoped<IDbQueryRunner, DbQueryRunner>();

            // Application services

            string mailJetApiKey = null, mailJetSecretKey = null;

            if (this.env.IsDevelopment())
            {
                mailJetApiKey = this.configuration["Mailjet:ApiKey"];
                mailJetSecretKey = this.configuration["Mailjet:SecretKey"];
            }
            else if (this.env.IsProduction())
            {
                mailJetApiKey = Environment.GetEnvironmentVariable("Mailjet:ApiKey");
                mailJetSecretKey = Environment.GetEnvironmentVariable("Mailjet:SecretKey");
            }

            services.AddTransient<IEmailSender>(x => new MultiJetEmailSender(mailJetApiKey, mailJetSecretKey));
            services.AddTransient<IFishingSpotService, FishingSpotService>();
            services.AddTransient<ICatchesService, CatchesService>();
            services.AddTransient<IFishSpeciesService, FishSpeciesService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IImageService, ImageService>();
            services.AddTransient<ICommentService, CommentService>();
            services.AddTransient<IVotesService, VotesService>();
            services.AddTransient<IProductsService, ProductsService>();
            services.AddTransient<IProductCategoryService, ProductCategoryService>();
            services.AddTransient<ICartsService, CartsService>();
            services.AddTransient<IOrdersService, OrdersService>();
            services.AddTransient<IReportsService, ReportsService>();

            // Cloudinary service
            var cloudinarySettings = this.configuration.GetSection("CloudinarySettings");

            var cloudinaryCloudName = cloudinarySettings["CloudName"];
            var cloudinaryApiKey = cloudinarySettings["ApiKey"];
            var cloudinaryApiSecret = cloudinarySettings["ApiSecret"];

            Account account = new Account(cloudinaryCloudName, cloudinaryApiKey, cloudinaryApiSecret);

            Cloudinary cloudinary = new Cloudinary(account);
            services.AddSingleton(cloudinary);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            AutoMapperConfig.RegisterMappings(typeof(ErrorViewModel).GetTypeInfo().Assembly);

            // Seed data on application startup
            using (var serviceScope = app.ApplicationServices.CreateScope())
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                dbContext.Database.Migrate();
                new ApplicationDbContextSeeder().SeedAsync(dbContext, serviceScope.ServiceProvider).GetAwaiter().GetResult();
            }

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithRedirects("/Home/Error?statusCode={0}");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(
                endpoints =>
                    {
                        endpoints.MapControllerRoute("areaRoute", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                        endpoints.MapRazorPages();
                    });
        }
    }
}
