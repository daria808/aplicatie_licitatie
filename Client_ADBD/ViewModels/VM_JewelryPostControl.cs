using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client_ADBD.Models;

namespace Client_ADBD.ViewModels
{
    internal class VM_JewelryPostControl : VM_Base
    {
        private string _type;
        private decimal _weight;
        private int _creationYear;


        public VM_JewelryPostControl(Jewelry_ j)
        {
            _type = j.Type;
            _weight = j.Weight;
            _creationYear = j.CreationYear;

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

        public decimal Weight
        {
            get => _weight;
            set
            {
                if (_weight != value)
                {
                    _weight = value;
                    OnPropertyChange(nameof(Weight));
                }
            }
        }

        public int CreationYear
        {
            get => _creationYear;
            set
            {
                if (_creationYear != value)
                {
                    _creationYear = value;
                    OnPropertyChange(nameof(CreationYear));
                }
            }
        }
    }
}
