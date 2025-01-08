using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client_ADBD.Models;

namespace Client_ADBD.ViewModels
{
    internal class VM_SculptureControl:VM_Base
    {

        public VM_SculptureControl() { }
        public VM_SculptureControl(Sculpture_ s) 
        {
            Artist = s.Artist;  
            Material = s.Material;
            Height=s.Length;
            Length=s.Width;
            Depth = s.Depth;
           
        }

        private bool _isValid=true;

        private string _artist;
        private string _material;
        private decimal _height;
        private decimal _length;
        private decimal _depth;

        public bool IsValid
        {
            get => _isValid;
            set
            {
                _isValid = value;
                OnPropertyChange(nameof(IsValid));
            }
        }

        public string Artist
        {
            get => _artist;
            set
            {
                if (_artist != value)
                {
                    _artist = value;
                    OnPropertyChange(nameof(Artist));
                }
            }
        }

        public string Material
        {
            get => _material;
            set
            {

                _material = value;
                Material2=GetMaterial(value);
                OnPropertyChange(nameof(Material));
               
            }
        }

        public string Material2;

        private string GetMaterial(string material)
        {
            switch (material)
            {
                case "marmură":
                    return "marmura";
                case "granit":
                    return "granit";
                case "calcar":
                    return "calcar";
                case "bronz":
                    return "bronz";
                case "fier":
                    return "fier";
                case "oțel":
                    return "otel";
                case "lut":
                    return "lut";
                case "sticlă":
                    return "sticla";
                default:
                    return string.Empty;
            }
        }
        public decimal Height
        {
            get => _height;
            set
            {

                _height = value;
                OnPropertyChange(nameof(Height));
            }
        }

        public decimal Length
        {
            get => _length;
            set
            {

                _length = value;
                OnPropertyChange(nameof(Length));
            }
        }

        public decimal Depth
        {
            get => _depth;
            set
            {
                _depth = value;
                OnPropertyChange(nameof(Depth));
            }
        }
    }
}
