using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Client_ADBD.Models;

namespace Client_ADBD.ViewModels
{
    internal class VM_BookControl:VM_Base
    {
        public VM_BookControl() { }
        public VM_BookControl(Book_ b)
        {
            Author = b.Author;
            BookCondition = b.Condition;
            Language = b.Language;
            Year=b.PublicationYear;
            NumberOfPage=b.PageNumber;
            PublishingHouse = b.PublishingHouse;
        }

        private bool _isValid = true;

        private string _author;
        private string _bookCondition;
        private string _language;
        private int _year;
        private int _numberOfPage;
        private string _publishingHouse;

        public string PublishingHouse
        {
            get
            {
                return _publishingHouse;
            }
            set
            {
                _publishingHouse = value;
                OnPropertyChange(nameof(PublishingHouse));
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
        public string Author
        {
            get { return _author; }
            set
            {
                _author = value;
                OnPropertyChange(nameof(Author));
            }
        }


        public string BookCondition2;

        private string GetCondition(string condition)
        {
            switch (condition)
            {
                case "nouă":
                    return "noua";
                case "foarte bună":
                    return "foarte buna";
                case "bună":
                    return "buna";
                case "deteriorată":
                    return "deteriorata";
                default:
                    return string.Empty;
            }
        }

        public string BookCondition
        {
            get { return _bookCondition; }
            set
            {
                _bookCondition = value;
                BookCondition2 = GetCondition(value);
                OnPropertyChange(nameof(BookCondition));
            }
        }

        public string Language
        {
            get { return _language; }
            set
            {
                _language = value;
                OnPropertyChange(nameof(Language));
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
        public int NumberOfPage
        {
            get { return _numberOfPage; }
            set
            {
                _numberOfPage = value;
                OnPropertyChange(nameof(NumberOfPage));
            }
        }
    }
}
