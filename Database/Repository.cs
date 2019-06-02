using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;

namespace Database
{


  

    public class Repository
    {

        private DbContextOptions<AppDbContext> _options;
        public Repository()
        {
            _options = new DbContextOptionsBuilder<AppDbContext>()
                .UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=EksamenDB;Trusted_Connection=true;MultipleActiveResultSets=true")
                .Options;
        }

        public bool CreateDB()
        {
            using (var context = new AppDbContext(_options))
            {
                if (true && (context.GetService<IDatabaseCreator>() as RelationalDatabaseCreator).Exists())
                    return false;

                //context.Database.EnsureDeleted();
                return context.Database.EnsureCreated();
            }
        }
    }
}
