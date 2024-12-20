using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client_ADBD.ViewModels
{
    internal class VM_PostPage:VM_Base
    {

        private Visibility _mainGridVisibility;

        public event PropertyChangedEventHandler PropertyChanged;

        public Visibility MainGridVisibility
        {
            get => _mainGridVisibility;
            set
            {
                if (_mainGridVisibility != value)
                {
                    _mainGridVisibility = value;
                    OnPropertyChange(nameof(MainGridVisibility));
                }
            }
        }

    }
}
