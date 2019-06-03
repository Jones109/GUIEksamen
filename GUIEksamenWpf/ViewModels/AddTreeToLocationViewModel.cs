using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Database.Models;
using Prism.Mvvm;

namespace GUIEksamenWpf.ViewModels
{
    class AddTreeToLocationViewModel:BindableBase
    {



        private TreeTypeToMeasure _treeTypeToMeasure;

        public AddTreeToLocationViewModel( TreeTypeToMeasure newTreeTypeToMeasure)
        {

            _treeTypeToMeasure = newTreeTypeToMeasure;
        }






        public TreeTypeToMeasure TreeTypeToMeasure
        {
            get { return _treeTypeToMeasure; }
            set
            {
                SetProperty(ref _treeTypeToMeasure, value);
            }
        }


    }
}
