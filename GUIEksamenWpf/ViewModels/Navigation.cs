using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Prism.Mvvm;
using Syncfusion.UI.Xaml.Utility;

namespace GUIEksamenWpf.ViewModels
{
    class NavigationViewModel : INotifyPropertyChanged

    {

        public ICommand EmployeeCommand { get; set; }

        public ICommand DepartmentCommand { get; set; }

        private object selectedViewModel;

        public object SelectedViewModel

        {

            get { return selectedViewModel; }

            set { selectedViewModel = value; OnPropertyChanged("SelectedViewModel"); }

        }



        public NavigationViewModel()

        {

            EmployeeCommand = new BaseCommand(OpenEmployeeView);

            DepartmentCommand = new BaseCommand(OpenDepartmentView);

        }

        private void OpenEmployeeView(object obj)

        {

            SelectedViewModel = new EmployeeViewModel();

        }

        private void OpenDepartmentView(object obj)

        {

            SelectedViewModel = new DepartmentViewModel();

        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propName)

        {

            if (PropertyChanged != null)

            {

                PropertyChanged(this, new PropertyChangedEventArgs(propName));

            }

        }

    }
}
