using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace Database.Models
{
    public class SaveData
    {
        private ObservableCollection<Location> locations;

        public ObservableCollection<Location> Locations
        {
            get { return locations; }
            set { locations = value; }
        }

        
        private ObservableCollection<Sensor> sensors;
        public ObservableCollection<Sensor> Sensors
        {
            get { return sensors;}
            set { sensors = value; }
        }

        private ObservableCollection<TreeTypeToMeasure> treeTypeToMeasures;

        public ObservableCollection<TreeTypeToMeasure> TreeTypeToMeasures {
            get { return treeTypeToMeasures; }
            set { treeTypeToMeasures = value; }
        }
        

    }
}
