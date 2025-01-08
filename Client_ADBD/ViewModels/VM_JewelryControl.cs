using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client_ADBD.Models;

namespace Client_ADBD.ViewModels
{
    internal class VM_JewelryControl : VM_Base
    {
        public VM_JewelryControl() { }
        public VM_JewelryControl(Jewelry_ j)
        {
            Brand= j.Brand;
            Type=GetType2(j.Type);  
            Weight = j.Weight;
          
            Year=j.CreationYear;
        }
        private bool _isValid=true;

        private string _brand;
        private string _type;
        private decimal _weight;
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

        private string GetType2(string value)
        {
            switch (value)
            {
                case "cercei":
                    return "cercei";
                case "lantisor":
                    return "lănțișor";
                case "inel":
                    return "inel";
                case "bratara":
                    return "brățară";
                case "colier":
                    return "colier";
                case "brosa":
                    return "broșă";
                default:
                    return string.Empty;
            }
        }
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

    }
}
