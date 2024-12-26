using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client_ADBD.Models;


namespace Client_ADBD.ViewModels
{
    internal class VM_SculpturePostControl:VM_Base
    {
        private string _material;
        private decimal _length;
        private decimal _width;
        private decimal _depth;

        public VM_SculpturePostControl(Sculpture_ s)
        {
            _material = s.Material;
            _length = s.Length;
            _width = s.Width;
            _depth = s.Depth;
        }

        public string Material
        {
            get => _material;
            set
            {
                _material = value;
                OnPropertyChange(nameof(Material));
            }
        }

        public decimal Length
        {
            get => _length;
            set
            {
                _length = value;
                OnPropertyChange(nameof(Length));
                OnPropertyChange(nameof(Size)); // Actualizăm Size când Length se schimbă
            }
        }

        public decimal Width
        {
            get => _width;
            set
            {
                _width = value;
                OnPropertyChange(nameof(Width));
                OnPropertyChange(nameof(Size)); // Actualizăm Size când Width se schimbă
            }   
        }

        public decimal Depth
        {
            get => _depth;
            set
            {
                _depth = value;
                OnPropertyChange(nameof(Depth));
                OnPropertyChange(nameof(Size)); // Actualizăm Size când Depth se schimbă
            }
        }

        public string Size => $"{Length} x {Width} x {Depth} cm";
    }
}
