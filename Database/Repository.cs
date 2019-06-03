using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
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



        #region Location
        public ObservableCollection<Location> GetAllLocations()
        {
            using (var context = new AppDbContext(_options))
            {
                return new ObservableCollection<Location>(context.Locations.ToList());
            }
        }

        public void InsertLocation(Location location)
        {
            using (var context = new AppDbContext(_options))
            {
               context.Locations.Add(location);
               context.SaveChanges();
            }
        }

        public void InsertLocationRange(ObservableCollection<Location> locations)
        {
            using (var context = new AppDbContext(_options))
            {
                context.Locations.AddRange(locations);
                context.SaveChanges();
               
            }
        }

        public void DeleteLocation(Location location)
        {
            using (var context = new AppDbContext(_options))
            {
                context.Locations.Remove(location);
                context.SaveChanges();

            }
        }


        #endregion

        #region Sensor
        public ObservableCollection<Sensor> GetAllSensors()
        {
            using (var context = new AppDbContext(_options))
            {
                return new ObservableCollection<Sensor>(context.Sensors.ToList());
            }
        }

        public void InsertSensor(Sensor sensor)
        {
            using (var context = new AppDbContext(_options))
            {
                context.Sensors.Add(sensor);
                context.SaveChanges();
            }

        }

        public void InsertSensorRange(ObservableCollection<Sensor> sensors)
        {
            using (var context = new AppDbContext(_options))
            {
                context.Sensors.AddRange(sensors);
                context.SaveChanges();
            }
        }


        public ObservableCollection<Sensor> GetSensorsByLocationId(int id)
        {
            using (var context = new AppDbContext(_options))
            {
                return new ObservableCollection<Sensor>(context.Sensors.Where(x => x.LocationId == id).ToList());
            }
        }


        #endregion

        #region TreeTypeToMeasure
        public ObservableCollection<TreeTypeToMeasure> GetAllTreeTypeToMeasure()
        {
            using (var context = new AppDbContext(_options))
            {
                return new ObservableCollection<TreeTypeToMeasure>(context.TreeTypesToMeasure.ToList());
            }
        }

        public async Task<ObservableCollection<TreeTypeToMeasure>> GetAllTreesForLocation(int locationId)
        {
            using (var context = new AppDbContext(_options))
            {
                return new ObservableCollection<TreeTypeToMeasure>(await context.TreeTypesToMeasure.Where(x=>x.LocationId==locationId).ToListAsync());
            }
        }



        public void InsertTreeTypeToMeasure(TreeTypeToMeasure tt)
        {
            using (var context = new AppDbContext(_options))
            {
                context.TreeTypesToMeasure.Add(tt);
                context.SaveChanges();
            }
        }

        public void InsertTreeTypeToMeasureRange(ObservableCollection<TreeTypeToMeasure> trees)
        {
            using (var context = new AppDbContext(_options))
            {
                context.TreeTypesToMeasure.AddRange(trees);
                context.SaveChanges();
            }
        }

        #endregion
    }
}
