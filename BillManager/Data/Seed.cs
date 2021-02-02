using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BillManager.Data
{
    public static class Seed
    {
        public static void SeedDb(DbContext appDbContext)
        {
            if (appDbContext.Database.GetPendingMigrations().Any())
            {
                appDbContext.Database.Migrate();
            }
        }
    }
}
