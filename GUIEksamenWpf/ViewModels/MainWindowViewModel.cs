using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database;
using Prism.Mvvm;

namespace GUIEksamenWpf.ViewModels
{
    class MainWindowViewModel:BindableBase
    {

        private Repository _repository;
        public MainWindowViewModel()
        {
            _repository = new Repository();
            _repository.CreateDB();
        }
    }
}
