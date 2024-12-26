using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client_ADBD.Models;

namespace Client_ADBD.ViewModels
{
    internal class VM_WatchPostControl:VM_Base
    {
        private string _mechanism;
        private string _type;
        private decimal _diameter;

        public VM_WatchPostControl(Watch_ w)
        {
            _mechanism = w.Mechanism;
            _type = w.Type;
            _diameter = w.Diameter;
        }
        public string Mechanism
        {
            get => _mechanism;
            set
            {
                _mechanism = value;
                OnPropertyChange(nameof(Mechanism));
            }
        }

        public string Type
        {
            get => _type;
            set
            {
                _type = value;
                OnPropertyChange(nameof(Type));
            }
        }

        public decimal Diameter
        {
            get => _diameter;
            set
            {
                _diameter = value;
                OnPropertyChange(nameof(Diameter));
            }
        }
    }
}
