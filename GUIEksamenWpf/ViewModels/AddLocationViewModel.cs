using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using Database;
using Database.Models;
using GUIEksamenWpf.Views;
using Prism.Commands;
using Prism.Mvvm;

namespace GUIEksamenWpf.ViewModels
{
    class AddLocationViewModel:BindableBase
    {

        private Location _newLocation;
        private ObservableCollection<TreeTypeToMeasure> _newTreeTypeToMeasures;
        
        private Repository _repository;

        public AddLocationViewModel(Location newLocation, ObservableCollection<TreeTypeToMeasure> newTreeTypeToMeasures)
        {
            _newLocation = newLocation;
            _newTreeTypeToMeasures = newTreeTypeToMeasures;
            _repository = new Repository();
        }

        public Location NewLocation
        {
            get { return _newLocation; }
            set { SetProperty(ref _newLocation, value); }
        }

        public ObservableCollection<TreeTypeToMeasure> NewTreeTypeToMeasures
        {
            get
            {
                return _newTreeTypeToMeasures; 
                
            }
            set { SetProperty(ref _newTreeTypeToMeasures, value); }
        }

        public TreeTypeToMeasure NewTreeTypeToMeasure
        {
            get
            {
                return new TreeTypeToMeasure();

            }
            set { NewTreeTypeToMeasures.Add(value); }
        }


        public event EventHandler Apply;
        private ICommand _acceptCommand;
        public ICommand AcceptCommand
        {
            get
            {
                return _acceptCommand ?? (_acceptCommand = new DelegateCommand(() =>
                {
                    if (Apply != null)
                    {
                        Apply(this, EventArgs.Empty);
                    }

                }));
            }
        }


        public event EventHandler Close;
        private ICommand _closeCommand;
        public ICommand CloseCommand
        {
            get
            {
                return _closeCommand ?? (_closeCommand = new DelegateCommand(() =>
                {
                    if (Close != null)
                    {
                        Close(this, EventArgs.Empty);
                    }

                }));
            }
        }



        ICommand _addNewTreeToLocationCommand;

        public ICommand AddNewTreeToLocationCommand
        {
            get
            {
                return _addNewTreeToLocationCommand ?? (_addNewTreeToLocationCommand = new DelegateCommand(() =>
                {
                TreeTypeToMeasure createTreeTypeToMeasure = new TreeTypeToMeasure();
                var vm = new AddTreeToLocationViewModel(createTreeTypeToMeasure);
                    AddTreeToLocationWindow amw = new AddTreeToLocationWindow()
                    {
                        DataContext = vm,
                        Owner = Application.Current.MainWindow.Owner
                    };
                    if (amw.ShowDialog() == true)
                    {
                        _newTreeTypeToMeasures.Add(vm.TreeTypeToMeasure);
                        RaisePropertyChanged("NewTreeTypeToMeasures");
                    }
                }));
            }
        }

    }
}
