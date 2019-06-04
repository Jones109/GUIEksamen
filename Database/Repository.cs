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




        public void SeedData()
        {
            using (var context = new AppDbContext(_options))
            {
                
                Location l1 = new Location
                {
                    City = "Aarhus",
                    Name = "Århus Bypark",
                    Street = "AarhusVej",
                    ZipCode = 8200,
                    StreetNumber = "12a"

                };
                Location l2 = new Location
                {
                    City = "Aarhus",
                    Name = "Skov",
                    Street = "SkovVej",
                    ZipCode = 8200,
                    StreetNumber = "20"

                };

                Location l3 = new Location
                {
                    City = "Holstebro",
                    Name = "Den Røde Plads",
                    Street = "Nørregade",
                    ZipCode = 7500,
                    StreetNumber = "10"

                };
                Location l4 = new Location
                {
                    City = "Holstebro",
                    Name = "Holstebro anlæg",
                    Street = "Hulvejen",
                    ZipCode = 7500,
                    StreetNumber = "12a"

                };
                Location l5 = new Location
                {
                    City = "Holstebro",
                    Name = "VestrePlantage",
                    Street = "HerningVej",
                    ZipCode = 7500,
                    StreetNumber = "20"

                };

                context.Locations.Add(l1);
                context.Locations.Add(l2);
                context.Locations.Add(l3);
                context.Locations.Add(l4);
                context.Locations.Add(l5);
                context.SaveChanges();
                TreeTypeToMeasure t1= new TreeTypeToMeasure
                {
                    Count = 1,
                    TreeType = "Eg",
                    LocationId = l1.Id
                };
                TreeTypeToMeasure t2 = new TreeTypeToMeasure
                {
                    Count = 2,
                    TreeType = "Bøg",
                    LocationId = l1.Id
                };
                TreeTypeToMeasure t3 = new TreeTypeToMeasure
                {
                    Count = 3,
                    TreeType = "Eg",
                    LocationId = l2.Id
                };
                TreeTypeToMeasure t4 = new TreeTypeToMeasure
                {
                    Count = 4,
                    TreeType = "Bøg",
                    LocationId = l2.Id
                };
                TreeTypeToMeasure t5 = new TreeTypeToMeasure
                {
                    Count = 5,
                    TreeType = "Eg",
                    LocationId = l3.Id
                };
                TreeTypeToMeasure t6 = new TreeTypeToMeasure
                {
                    Count = 6,
                    TreeType = "Bøg",
                    LocationId = l3.Id
                };
                TreeTypeToMeasure t7 = new TreeTypeToMeasure
                {
                    Count = 7,
                    TreeType = "Eg",
                    LocationId = l4.Id
                };
                TreeTypeToMeasure t8 = new TreeTypeToMeasure
                {
                    Count = 8,
                    TreeType = "Bøg",
                    LocationId = l4.Id
                };
                TreeTypeToMeasure t9 = new TreeTypeToMeasure
                {
                    Count = 9,
                    TreeType = "Eg",
                    LocationId = l5.Id
                };
                TreeTypeToMeasure t10 = new TreeTypeToMeasure
                {
                    Count = 10,
                    TreeType = "Bøg",
                    LocationId = l5.Id
                };

                context.TreeTypesToMeasure.Add(t1);
                context.TreeTypesToMeasure.Add(t2);
                context.TreeTypesToMeasure.Add(t3);
                context.TreeTypesToMeasure.Add(t4);
                context.TreeTypesToMeasure.Add(t5);
                context.TreeTypesToMeasure.Add(t6);
                context.TreeTypesToMeasure.Add(t7);
                context.TreeTypesToMeasure.Add(t8);
                context.TreeTypesToMeasure.Add(t9);
                context.TreeTypesToMeasure.Add(t10);
                context.SaveChanges();
                Sensor s1 = new Sensor
                {
                    GpsLat = 1,
                    GpsLon = 1,
                    SensorId = "1111111111111111",
                    TreeType = "Eg",
                    LocationId = l1.Id
                };
                Sensor s2 = new Sensor
                {
                    GpsLat = 1,
                    GpsLon = 1,
                    SensorId = "1111111111111112",
                    TreeType = "Bøg",
                    LocationId = l1.Id
                };

                Sensor s3 = new Sensor
                {
                    GpsLat = 1,
                    GpsLon = 1,
                    SensorId = "1111111111111113",
                    TreeType = "Eg",
                    LocationId = l2.Id
                };

                Sensor s4 = new Sensor
                {
                    GpsLat = 1,
                    GpsLon = 1,
                    SensorId = "1111111111111114",
                    TreeType = "Bøg",
                    LocationId = l2.Id
                };

                Sensor s5 = new Sensor
                {
                    GpsLat = 1,
                    GpsLon = 1,
                    SensorId = "1111111111111115",
                    TreeType = "Eg",
                    LocationId = l3.Id
                };

                Sensor s6 = new Sensor
                {
                    GpsLat = 1,
                    GpsLon = 1,
                    SensorId = "1111111111111116",
                    TreeType = "Bøg",
                    LocationId = l3.Id
                };

                Sensor s7 = new Sensor
                {
                    GpsLat = 1,
                    GpsLon = 1,
                    SensorId = "1111111111111117",
                    TreeType = "Eg",
                    LocationId = l4.Id
                };

                Sensor s8 = new Sensor
                {
                    GpsLat = 1,
                    GpsLon = 1,
                    SensorId = "1111111111111118",
                    TreeType = "Bøg",
                    LocationId = l4.Id
                };

                Sensor s9 = new Sensor
                {
                    GpsLat = 1,
                    GpsLon = 1,
                    SensorId = "1111111111111119",
                    TreeType = "Eg",
                    LocationId = l5.Id
                };

                Sensor s10 = new Sensor
                {
                    GpsLat = 1,
                    GpsLon = 1,
                    SensorId = "1111111111111110",
                    TreeType = "Bøg",
                    LocationId = l5.Id
                };


                context.Sensors.Add(s1);
                context.Sensors.Add(s2);
                context.Sensors.Add(s3);
                context.Sensors.Add(s4);
                context.Sensors.Add(s5);
                context.Sensors.Add(s6);
                context.Sensors.Add(s7);
                context.Sensors.Add(s8);
                context.Sensors.Add(s9);
                context.Sensors.Add(s10);
                context.SaveChanges();


            }
        }
    }
}
