using CarRentingSystem.Data;
using Microsoft.EntityFrameworkCore;
using System;

namespace CarRentingSystem.Test.Moqs
{
    public static class DatabaseMoq
    {
        public static CarRentingDbContext Instance
        {
            get
            {
                var dbContextOptions = new DbContextOptionsBuilder<CarRentingDbContext>()
                    .UseInMemoryDatabase(Guid.NewGuid().ToString())
                    .Options;

                return new CarRentingDbContext(dbContextOptions);
            }
        }
    }
}
