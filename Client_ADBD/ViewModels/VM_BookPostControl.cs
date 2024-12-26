using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client_ADBD.Models;

namespace Client_ADBD.ViewModels
{
    internal class VM_BookPostControl:VM_Base
    {
        private int _publicationYear;
        private string _publisher;
        private string _language;
        private int _pageCount;

        public VM_BookPostControl(Book_ b)
        {
            _publicationYear=b.PublicationYear;
            _publisher = b.PublishingHouse;
            _language = b.Language;
            _pageCount=b.PageNumber;
        }

        public int PublicationYear
        {
            get => _publicationYear;
            set
            {
                _publicationYear = value;
                OnPropertyChange(nameof(PublicationYear));
            }
        }

        public string Publisher
        {
            get => _publisher;
            set
            {
                _publisher = value;
                OnPropertyChange(nameof(Publisher));
            }
        }

        public string Language
        {
            get => _language;
            set
            {
                _language = value;
                OnPropertyChange(nameof(Language));
            }
        }

        public int PageCount
        {
            get => _pageCount;
            set
            {
                _pageCount = value;
                OnPropertyChange(nameof(PageCount));
            }
        }

    }
}
