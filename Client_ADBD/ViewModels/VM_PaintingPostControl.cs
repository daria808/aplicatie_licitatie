using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client_ADBD.Models;

namespace Client_ADBD.ViewModels
{
    internal class VM_PaintingPostControl:VM_Base
    {

            private string _type;
            private int _creationYear;
            private decimal _length;
            private decimal _width;

        public VM_PaintingPostControl(Painting_ p)
        {
            _type=p.Type;
            _creationYear=p.CreationYear;
            _length=p.Length;
            _width=p.Width;
        }
            public string Type
            {
                get => _type;
                set
                {
                    _type = value;
                    OnPropertyChange(nameof(Type));
                    OnPropertyChange(nameof(Size)); // Actualizăm automat Size când Type se schimbă
                }
            }

            public int CreationYear
            {
                get => _creationYear;
                set
                {
                    _creationYear = value;
                    OnPropertyChange(nameof(CreationYear));
                }
            }

            public decimal Length
            {
                get => _length;
                set
                {
                    _length = value;
                    OnPropertyChange(nameof(Length));
                    OnPropertyChange(nameof(Size)); // Actualizăm automat Size când Length se schimbă
                }
            }

            public decimal Width
            {
                get => _width;
                set
                {
                    _width = value;
                    OnPropertyChange(nameof(Width));
                    OnPropertyChange(nameof(Size)); // Actualizăm automat Size când Width se schimbă
                }
            }

            public string Size => $"{Length} x {Width} cm";

        
        }
    }
