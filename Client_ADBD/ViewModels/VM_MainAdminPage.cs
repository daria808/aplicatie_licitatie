using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Client_ADBD.ViewModels
{
    internal class VM_MainAdminPage : VM_Base
    {
        private int _totalUsers;
        private int _totalBidders;
        private int _totalClients;
        private int _totalActiveAuctions;

        public int TotalUsers
        {
            get => _totalUsers;
            set
            {
                _totalUsers = value;
                OnPropertyChange(nameof(TotalUsers));
            }
        }

        public int TotalBidders
        {
            get => _totalBidders;
            set
            {
                _totalBidders = value;
                OnPropertyChange(nameof(TotalBidders));
            }
        }

        public int TotalClients
        {
            get => _totalClients;
            set
            {
                _totalClients = value;
                OnPropertyChange(nameof(TotalClients));
            }
        }

        public int TotalActiveAuctions
        {
            get => _totalActiveAuctions;
            set
            {
                _totalActiveAuctions = value;
                OnPropertyChange(nameof(TotalActiveAuctions));
            }
        }

        public VM_MainAdminPage()
        {
            LoadStatistics();
        }

        private void LoadStatistics()
        {
            TotalUsers = (new DatabaseManager()).CountTotalUsers();
            TotalBidders = (new DatabaseManager()).CountTotalBidders();
            TotalClients = (new DatabaseManager()).CountTotalClients();

        }
    }
}
