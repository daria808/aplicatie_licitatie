using Client_ADBD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.ViewModels
{
    internal class VM_PaintingControl:VM_Base
    {

        public VM_PaintingControl() { }
        public VM_PaintingControl(Painting_ p)
        {
            Artist=p.Artist;
            Year=p.CreationYear;
            Length=p.Length;
            Width=p.Width;
            Technique=p.Type;

        }

        private bool _isValid = true;

        private string _artist;
        private int _year;
        private decimal _length;
        private decimal _width;
        private string _technique;

        public string Technique2;

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
        { get { return _artist; }
            set 
            {
                _artist = value;

                OnPropertyChange(nameof(Artist));
            }
        }
        public int Year
        {
            get { return _year; }
            set
            {
                _year = value;
                OnPropertyChange(nameof(Year));
            }
        
        }

        public decimal Length
        {
            get { return _length; }
            set
            {
                _length = value;
                OnPropertyChange(nameof(Length));
            }
        }

        public decimal Width
        {
            get { return _width; }
            set
            {
                _width = value;
                OnPropertyChange(nameof(Width));
            }
        }

        public string Technique
        {
            get { return _technique; }
            set {
                _technique = value;
                Technique2 = GetTechinuque(value);
                OnPropertyChange(nameof(Technique));
            } 
        }

        private string GetTechinuque(string technique)
        {
            switch (technique)
            {
                case "pictură în ulei":
                    return "pictura in ulei";
                case "acuarelă":
                    return "acuarela";
                case "acrilic":
                    return "acrilic";
                case "frescă":
                    return "fresca";
                case "pastel":
                    return "pastel";
                default:
                    return string.Empty;
            }
        }

    }
}
