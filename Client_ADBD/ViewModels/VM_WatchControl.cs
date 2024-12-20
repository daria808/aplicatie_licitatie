using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.ViewModels
{
    internal class VM_WatchControl:VM_Base
    {
        private bool _isValid = true;

        private string _brand;
        private string _type;
        private int _diameter;
        private string _mechanism;

       
        public bool IsValid
        {
            get => _isValid;
            set
            {
                _isValid = value;
                OnPropertyChange(nameof(IsValid));
            }
        }

            
        public string Brand
        {
            get => _brand;
            set
            {
                if (_brand != value)
                {
                    _brand = value;
                    OnPropertyChange(nameof(Brand));
                }
            }
        }
        public string Type
        {
            get => _type;
            set
            {
                if (_type != value)
                {
                    _type = value;
                    OnPropertyChange(nameof(Type));
                }
            }
        }
        public int Diameter
        {
            get => _diameter;
            set
            {
                if (_diameter != value)
                {
                    _diameter = value;
                    OnPropertyChange(nameof(Diameter));
                }
            }
        }
        public string Mechanism
        {
            get => _mechanism;
            set
            {
                if (_mechanism != value)
                {
                    _mechanism = value;
                    OnPropertyChange(nameof(Mechanism));
                }
            }
        }
    }
}
