using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Client_ADBD.ViewModels
{
    internal class VM_StatisticsControler : VM_Base
    {
        public int Id;
        private DateTime _endTime;
        private string _imagePath;
        private string _name;
        private string _location;

        public decimal Total {  get; set; }
        public double Procent { get; set; }

        public DateTime EndTime
        {
            get => _endTime;
            set
            {
                _endTime = value;
                OnPropertyChange(nameof(EndTime));
            }
        }

        public string ImagePath
        {
            get => _imagePath;
            set
            {
                _imagePath = value;
                OnPropertyChange(nameof(ImagePath));
            }
        }

        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChange(nameof(Name));
            }
        }

        public string Location
        {
            get => _location;
            set
            {
                _location = value;
                OnPropertyChange(nameof(Location));
            }
        }


        public VM_StatisticsControler(int id)
        {
            Id = id;

            var databaseManager = new DatabaseManager();
            Total = databaseManager.GetTotalBidsForAuction(id);
            Procent = databaseManager.GetSoldPercentage(id);
        }
    }
}
