using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Xml.Serialization;
using Database;
using Database.Models;
using GUIEksamenWpf.ViewModels;
using GUIEksamenWpf.Views;
using Microsoft.Win32;
using Prism.Commands;
using Prism.Mvvm;


namespace GUIEksamenWpf
{
    class MainWindowViewModel:BindableBase
    {

        private Repository _repository;
        private AddLocationWindow locationWindow = null;
        private AddLocationViewModel locationViewModel = null;
        private string fileName;
        

        public MainWindowViewModel()
        {
            _repository = new Repository();
            _repository.CreateDB();
            CurrentSearchString = "";

        }




        #region SearchProå

        private string currentSearchString;

        public string CurrentSearchString
        {
            get { return currentSearchString; }
            set
            {
                SetProperty(ref currentSearchString, value);
                CurrentLocations = new ObservableCollection<Location>(Locations.Where(x=>x.Name.StartsWith(CurrentSearchString)));
                RaisePropertyChanged("CurrentLocations");
                    
            }
        }
        private ObservableCollection<Location> currentLocations;
        public ObservableCollection<Location> CurrentLocations {
            get { return currentLocations; }
            set { SetProperty(ref currentLocations, value); }

        }

        #endregion


        #region LocationProp

        public ObservableCollection<Location> Locations
        {
            get
            {
              
                    var locations = _repository.GetAllLocations();
                    return locations;
                


            }
        }
    

        Location currentLocation = null;

        public Location CurrentLocation
        {
            get { return currentLocation; }
            set
            {
                SetProperty(ref currentLocation, value);
                RaisePropertyChanged("Trees");
            }
        }

        int currentLocationIndex = -1;
        public int CurrentLocationIndex
        {
            get { return currentLocationIndex; }
            set
            {
                SetProperty(ref currentLocationIndex, value);
            }
        }

        #endregion


        #region TreesProp
        public ObservableCollection<TreeTypeToMeasure> Trees
        {
            get
            {
                if (CurrentLocation != null)
                {
                    var trees = _repository.GetAllTreesForLocation(CurrentLocation.Id);
                    return trees.Result;
                }

                return null;

            }
        }

        TreeTypeToMeasure currentTrees = null;

        public TreeTypeToMeasure CurrentTrees
        {
            get { return currentTrees; }
            set
            {
                SetProperty(ref currentTrees, value);
            }
        }

        int currentTreesIndex = -1;
        public int CurrentTreesIndex
        {
            get { return currentTreesIndex; }
            set
            {
                SetProperty(ref currentTreesIndex, value);
            }
        }


        #endregion

        #region LocationModeless

        ICommand _addNewLocationCommand;

        public ICommand AddNewLocationCommand
        {
            get
            {
                return _addNewLocationCommand ?? (_addNewLocationCommand = new DelegateCommand(() =>
                {
                    if (locationWindow != null)
                    {
                        locationWindow.Focus();
                    }
                    else
                    {
                        Location newLocation = new Location();
                        ObservableCollection<TreeTypeToMeasure> newTreeTypeToMeasures = new ObservableCollection<TreeTypeToMeasure>();
                        
                        locationViewModel = new AddLocationViewModel(newLocation, newTreeTypeToMeasures);
                        locationWindow = new AddLocationWindow
                        {
                            DataContext = locationViewModel,
                            Owner = Application.Current.MainWindow.Owner
                        };

                        locationViewModel.Apply += new EventHandler(addModelApply);
                        locationViewModel.Close += new EventHandler(addModelClose);
                        locationWindow.Closed += new EventHandler(addModelClose);
                        locationWindow.Show();

                    }


                }));
            }
        }

        private void addModelApply(object sender, EventArgs e)
        {

            _repository.InsertLocation(locationViewModel.NewLocation);
            foreach (var tree in locationViewModel.NewTreeTypeToMeasures)
            {
                tree.LocationId = locationViewModel.NewLocation.Id;
                _repository.InsertTreeTypeToMeasure(tree);
            }

           
            RaisePropertyChanged("Locations");
            RaisePropertyChanged("Trees");
            CurrentSearchString = "";
            locationWindow.Close();

        }

        private void addModelClose(object sender, EventArgs e)
        {
            locationViewModel.Apply -= new EventHandler(addModelApply);
            locationViewModel.Close -= new EventHandler(addModelClose);
            locationWindow.Closed -= new EventHandler(addModelClose);
            locationWindow = null;
            locationViewModel = null;
            Application.Current.MainWindow.Focus();
        }

        #endregion


        #region Save_Open

        ICommand _closeAppCommand;
        public ICommand CloseAppCommand
        {
            get
            {
                return _closeAppCommand ?? (_closeAppCommand = new DelegateCommand(() =>
                {
                    App.Current.MainWindow.Close();
                }));
            }
        }

        ICommand _SaveAsCommand;
        public ICommand SaveAsCommand
        {
            get { return _SaveAsCommand ?? (_SaveAsCommand = new DelegateCommand<string>(SaveAsCommand_Execute)); }
        }

        private void SaveAsCommand_Execute(string argFilename)
        {

            SaveFileDialog sfd = new SaveFileDialog();
            if (sfd.ShowDialog()!=null)
            {
                fileName = sfd.FileName;
            }

                SaveFileCommand_Execute();
            
        }

        ICommand _SaveCommand;
        public ICommand SaveCommand
        {
            get
            {
                return _SaveCommand ?? (_SaveCommand = new DelegateCommand(SaveFileCommand_Execute, SaveFileCommand_CanExecute)
                  .ObservesProperty(() => Locations.Count));
            }
        }

        private void SaveFileCommand_Execute()
        {
            SaveData saveData = new SaveData();
            saveData.Locations = _repository.GetAllLocations();
            saveData.Sensors = _repository.GetAllSensors();
            saveData.TreeTypeToMeasures = _repository.GetAllTreeTypeToMeasure();



            // Create an instance of the XmlSerializer class and specify the type of object to serialize.
            XmlSerializer serializer = new XmlSerializer(typeof(SaveData));
            TextWriter writer = new StreamWriter(fileName);
            // Serialize the savedata.
            serializer.Serialize(writer, saveData);
            writer.Close();
        }

        private bool SaveFileCommand_CanExecute()
        {
            return (fileName != "");
        }

        /*
        ICommand _NewFileCommand;
        public ICommand NewFileCommand
        {
            get { return _NewFileCommand ?? (_NewFileCommand = new DelegateCommand(NewFileCommand_Execute)); }
        }

        private void NewFileCommand_Execute()
        {
            MessageBoxResult res = MessageBox.Show("Any unsaved data will be lost. Are you sure you want to initiate a new file?", "Warning",
                MessageBoxButton.YesNo, MessageBoxImage.Question, MessageBoxResult.No);
            if (res == MessageBoxResult.Yes)
            {
                VarroEntries.Clear();
                EntryIds = new ObservableCollection<string>
                {
                    "1",
                    "2",
                    "3",
                    "4",
                    "5"
                };
                filterEntryIds.Clear();
                filterEntryIds.Add("All");
                filterEntryIds.AddRange(EntryIds);
                filename = "";
            }
        }
        */

        
        ICommand _OpenFileCommand;
        public ICommand OpenFileCommand
        {
            get { return _OpenFileCommand ?? (_OpenFileCommand = new DelegateCommand(OpenFileCommand_Execute)); }
        }

        private void OpenFileCommand_Execute()
        {
            OpenFileDialog ofd = new OpenFileDialog();
            if (ofd.ShowDialog() != null)
            {
                fileName = ofd.FileName;
            }

                var tempSaveData = new SaveData();

                // Create an instance of the XmlSerializer class and specify the type of object to deserialize.
                XmlSerializer serializer = new XmlSerializer(typeof(SaveData));

                try
                {
                    TextReader reader = new StreamReader(fileName);
                    // Deserialize all the saveData
                    tempSaveData = (SaveData)serializer.Deserialize(reader);
                    reader.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Unable to open file", MessageBoxButton.OK, MessageBoxImage.Error);
                }

            
                //Resetting id for all entities
                foreach (var location in tempSaveData.Locations)
                {
                    int tempId = location.Id;
                    location.Id = 0;
                    _repository.InsertLocation(location);
                    for (int i = 0; i < Math.Max(tempSaveData.Sensors.Count, tempSaveData.TreeTypeToMeasures.Count); i++)
                    {
                        

                        if (i < tempSaveData.Sensors.Count&&tempSaveData.Sensors[i].LocationId==tempId)
                        {
                            tempSaveData.Sensors[i].LocationId = location.Id;
                        }
                        if (i<tempSaveData.TreeTypeToMeasures.Count&&tempSaveData.TreeTypeToMeasures[i].LocationId==tempId)
                        {
                            tempSaveData.TreeTypeToMeasures[i].LocationId = location.Id;
                        }
                    }
                }

                foreach (var sensor in tempSaveData.Sensors)
                {
                    sensor.Id = 0;
                }
                _repository.InsertSensorRange(tempSaveData.Sensors);


                foreach (var tree in tempSaveData.TreeTypeToMeasures)
                {
                    tree.Id = 0;
                }
                _repository.InsertTreeTypeToMeasureRange(tempSaveData.TreeTypeToMeasures);
 

            RaisePropertyChanged("Trees");
            CurrentSearchString = "";
        }
        }

    

        #endregion

    }

