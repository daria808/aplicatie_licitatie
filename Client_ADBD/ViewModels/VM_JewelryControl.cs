using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.ViewModels
{
    internal class VM_JewelryControl : VM_Base
    {
        private bool _isValid=true;

        private string _brand;
        private string _type;
        private decimal _weight;
        private string _material;
        private int _year;

        public int Year
        {
            get { return _year; } 
            set { 
                _year = value;
                OnPropertyChange(nameof(Year));
            }
        }

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
            get { return _brand; } 
            set {
                _brand = value; 
                OnPropertyChange(nameof(Brand));
            } 
        }

        public string Type
        {
            get { return _type; }
            set
            {
                _type = value;
                Type2 = GetType(value);
                OnPropertyChange(nameof(Type));
            }
        }

        public string Type2;

        private string GetType(string value)
        {
            switch (value)
            {
                case "cercei":
                    return "cercei";
                case "lănțișor":
                    return "lantisor";
                case "inel":
                    return "inel";
                case "brățară":
                    return "bratara";
                case "colier":
                    return "colier";
                case "broșă":
                    return "brosa";
                default:
                    return string.Empty;
            } 
        }

        public decimal Weight
        {
            get { return _weight; }
            set
            {
                _weight = value;
                OnPropertyChange(nameof(Weight));
            }
        }

        public string Material
        {
            get { return _material; }
            set
            {
                _material = value;
                OnPropertyChange(nameof(Material));
            }
        }

    }
}
