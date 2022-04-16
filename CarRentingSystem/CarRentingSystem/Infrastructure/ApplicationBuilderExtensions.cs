using CarRentingSystem.Data;
using CarRentingSystem.Data.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading.Tasks;
using static CarRentingSystem.WebConstants;

namespace CarRentingSystem.Infrastructure
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder PrepareDatabase(
            this IApplicationBuilder app)
        {
            using var scopedServices = app.ApplicationServices.CreateScope();
            var serviceProvider = scopedServices.ServiceProvider;

            var data = serviceProvider.GetRequiredService<CarRentingDbContext>();

            data.Database.Migrate();

            SeedCategories(data);
            SeedAdministrator(serviceProvider);

            return app;
        }

        private static void SeedCategories(CarRentingDbContext data)
        {
            //if (data.Categories.Any())
            //{
            //    return;
            //}

            AddOrUpdateCategories(new[]
            {
                new Category{ Name = "Mini", CategoryUrl = "http://cdn.shopify.com/s/files/1/1359/2999/articles/mini-car-01.jpg?v=1623075972"},
                new Category{ Name = "Economy", CategoryUrl = "https://webnews.bg/uploads/images/27/5127/545127/768x432.jpg?_=1630586714"},
                new Category{ Name = "Midsize", CategoryUrl = "https://cdn4.focus.bg/fakti/photos/big/b72/kakvo-novo-v-novia-volkswagen-passat-1.jpg"},
                new Category{ Name = "Large", CategoryUrl = "https://cdn.audi.bg/media/Model_Gallery_DetailImage_Image_XSmall_Component/56883-545951_65550-image-xsmall/dh-702-b05066/74be1c65/1627886580/audi-galerie-2-oe.jpg"},
                new Category{ Name = "SUV", CategoryUrl = "https://carsopedia.com/files/generations/BMW-E53-969.jpg"},
                new Category{ Name = "Vans", CategoryUrl = "https://images.ams.bg/images/galleries/112208/volkswagen-sharan-1460821996_big.jpg"},
                new Category{ Name = "Luxury", CategoryUrl = "https://cdn1.mecum.com/auctions/ln1117/ln1117-319035/images/01-1505747371357@2x.jpg?1510798753000"},
            }, data);

            data.SaveChanges();
        }

        private static void AddOrUpdateCategories(Category[] categories, CarRentingDbContext data)
        {
            foreach (var category in categories)
            {
                var selectedCategory = data.Categories.FirstOrDefault(c => c.Name == category.Name);

                if (selectedCategory != null)
                {
                    selectedCategory.CategoryUrl = category.CategoryUrl;
                }
                else
                {
                    data.Categories.Add(category);
                }
            }

            data.SaveChanges();
        }

        private static void SeedAdministrator(IServiceProvider services)
        {
            var userManager = services.GetRequiredService<UserManager<User>>();
            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var dealerManager = 

            Task.Run(async () =>
            {
                if (await roleManager.RoleExistsAsync(AdministratorRoleName))
                {
                    return;
                }

                var role = new IdentityRole { Name = AdministratorRoleName };

                await roleManager.CreateAsync(role);

                string adminEmail = "admin@abv.bg";
                string adminPassword = "admin123";

                var user = new User()
                {
                    Email = adminEmail,
                    UserName = adminEmail,
                    FullName = "Admin"
                };

                await userManager.CreateAsync(user, adminPassword);

                await userManager.AddToRoleAsync(user, role.Name);
            })
            .GetAwaiter()
            .GetResult();
        }
    }
}
