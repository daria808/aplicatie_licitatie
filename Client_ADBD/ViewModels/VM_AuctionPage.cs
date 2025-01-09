using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Client_ADBD.Helpers;
using Client_ADBD.Models;
using Client_ADBD.Views;
using Client_ADBD.Views.UserControl;
using CommunityToolkit.Mvvm.Input;

namespace Client_ADBD.ViewModels
{
    internal class VM_AuctionPage:VM_Base
    {
        private int _currentPage = 1;

        //private Auction_ _auction;
        private Visibility _ownerAdminVisibility;
        private string _auctionTitle;
        private string _auctionDescription;
        //private string _auctionShortDescription;
        private bool _isFullDescriptionVisible = false;
        private DateTime _auctionDate;
        private string _auctionLocation;
        private int _auctionLotCount;
        private int _auctionNumber;
        private string _selectedSortType;
        private string _selectedSortStatus;
        private int _selectedLotPerPage;
        private string _auctionImagePath;
        private string _usernameOwner;
        private ObservableCollection<PostControl> _lotsPerPage;
        //private ObservableCollection<AuctionLot> _lots;

        private List<int> _postsLotNumbers;
        private int _maxLotNumber;

        public int MaxLotNumber
        {
            get => _maxLotNumber;
            set
            {
                _maxLotNumber = value;
                OnPropertyChange(nameof(MaxLotNumber));
            }
        }
        
        private decimal _totalEarnings;
        private double _rate;


        private Visibility _fromResults;
        public Visibility FromResults
        {
            get { return _fromResults; }
            set
            {
                _fromResults = value;
                OnPropertyChange(nameof(FromResults));
            }
        }

        public decimal TotalEarnings
        {
            get => _totalEarnings;
            set
            {
                _totalEarnings = value;
                OnPropertyChange(nameof(TotalEarnings));
            }
        }


        public double Rate
        {
            get => _rate;
            set
            {
                _rate = value;
                OnPropertyChange(nameof(Rate));
            }
        }

        private int _selectedLotNumber;
        private string _lotNumberError;

        private string _auctionType;
        public ICommand GoToMainPageCommand { get; }
        public ICommand GoToAddPostCommand { get; }

        public ICommand ToggleDescriptionCommand { get; }
        public ICommand GoToPageDetailsCommand {  get; }

        public ICommand PreviousPageCommand {  get; }
        public ICommand NextPageCommand { get; }
        public ICommand GoToModifyPageCommand {  get; }

        public ICommand DeleteAuctionCommand { get; }
        public VM_AuctionPage() {
        
        }

        public int SelectedLotNumber
        {
            get => _selectedLotNumber;
            set
        {
                _selectedLotNumber = value;
                
                if(!_postsLotNumbers.Contains(value))
                {
                    LotNumberError = "Număr lot invalid";
                  
                }
                else
                {
                    LotNumberError = string.Empty;
                    GoToChosenLotPage(_selectedLotNumber);
                }

                OnPropertyChange(nameof(SelectedLotNumber));
            }
        }

        public Visibility OwnerAdminVisibility
        {
            get => _ownerAdminVisibility;
            set
            {   _ownerAdminVisibility = value;
                OnPropertyChange(nameof(OwnerAdminVisibility));
            }
        }

        private Visibility IsCurrentUserOwner(string username)
        {
            if(CurrentUser.User._username == username) 
                return Visibility.Visible;
            else 
                return Visibility.Hidden;
        }

        public string LotNumberError
        {
            get => _lotNumberError;
            set
            {
                _lotNumberError = value;
                OnPropertyChange(nameof(LotNumberError));
            }
        }

        private void GoToChosenLotPage(int lotNumber)
        {

           int id= DatabaseManager.GetPostIdByLot(lotNumber, _auctionNumber);
            Post_ p = DatabaseManager.GetPostDetails(id);

            var mainWindow = App.Current.Windows
                    .OfType<MainWindow>()
                    .FirstOrDefault();
            var frame = mainWindow?.FindName("MainFrame") as Frame;


            if (frame != null)
            {
                frame.Navigate(new PostPage(p));
            }

        }


        public string ButtonText
        {
            get
            {
                if (string.IsNullOrEmpty(ShortDescription) || string.IsNullOrEmpty(FullDescription))
                {
                    return string.Empty;
                }
                if (FullDescription.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).Length <= 15)
                {
                    return string.Empty;
                }
                return IsFullDescriptionVisible ? "Ascunde" : "Citește mai mult";
            }
        }

        public bool IsFullDescriptionVisible
        {
            get => _isFullDescriptionVisible;
            set
            {
                if (_isFullDescriptionVisible != value)
                {
                    _isFullDescriptionVisible = value;
                    OnPropertyChange(nameof(IsFullDescriptionVisible));
                    OnPropertyChange(nameof(ButtonText)); // Actualizează textul butonului
                    OnPropertyChange(nameof(DescriptionText)); // Actualizează descrierea
                }
            }
        }


        public string AuctionDescription
        {
            get => _auctionDescription;
            set
            {
                if (_auctionDescription != value)
                {
                    _auctionDescription = value;
                    OnPropertyChange(nameof(AuctionDescription));
                    OnPropertyChange(nameof(ShortDescription)); 
                    OnPropertyChange(nameof(FullDescription));  
                }
            }
        }

        public string FullDescription => AuctionDescription;
        public string ShortDescription => GetFirstNWords(AuctionDescription, 15);
        private string GetFirstNWords(string description, int numberOfWords)
        {
            if (description == null)
            {
                return string.Empty;
            }

            var words = description.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
            return string.Join(" ", words.Take(numberOfWords))+" ...";
                
        }

        public string DescriptionText => IsFullDescriptionVisible ? FullDescription : ShortDescription;

        public void SetPostsPreview(List<PostPreview> posts)
        { 
            //if (_vmPostsPreview == null || DisplayedPosts.Count() == 0)
            //{
                _vmPostsPreview = new ObservableCollection<VM_PostControl>(

                    posts.Select(p => new VM_PostControl
                    {
                        Id = p.postId,
                        Name = p.postName,
                        ImagePath = p.imagePath,
                        StartPrice = p.startPrice,
                        Status = p.postStatus,
                        Author = p.artistName
                    }
                    ));
           // }

            Posts = new ObservableCollection<PostPreview>(posts);

            _currentPage = 1;
            UpdateDisplayedPosts();
        }
        
        public VM_AuctionPage(Auction_ auction,bool fromResults=false, bool statistics = false)
        {

            TotalEarnings = DatabaseManager.GetTotalBidsForAuction(auction.auctionNumber);
            Rate = DatabaseManager.GetSoldPercentage(auction.auctionNumber);

            _postsLotNumbers=DatabaseManager.GetPostLotsNumber(auction.id);
            if (_postsLotNumbers.Count!=0)
            {
                MaxLotNumber = _postsLotNumbers.Max();
            }
            else
            {
                MaxLotNumber=0;
            }

            SelectedLotPerPage = 6;
            AuctionTitle = auction.name;
            AuctionDescription =auction.description;
            AuctionDate =auction.startTime;
            AuctionLocation =auction.location;
            AuctionImagePath=auction.imagePath;
            AuctionType=auction.auctionType;
            AuctionNumber=auction.auctionNumber;
            _usernameOwner = auction.usernameOwner;
            OwnerAdminVisibility=IsCurrentUserOwner(_usernameOwner);

            SetPostsPreview(DatabaseManager.GetPostPreview(AuctionNumber));

            if(fromResults==false)
            {
                FromResults=Visibility.Hidden;
            }
            else
            {
                FromResults=Visibility.Visible;
            }
 
            AuctionLotCount = Posts.Count();

            GoToAddPostCommand=new RelayCommand(GoToAddPost);
            if (statistics == true)
            {
                GoToMainPageCommand = new RelayCommand(GoToStatistics);
            }
            else
            {
                GoToMainPageCommand = new RelayCommand(GoToMainPage);

            }


            ToggleDescriptionCommand = new RelayCommand(ToggleDescription);
            GoToPageDetailsCommand = new RelayCommand<int>(GoToChosenLotPage);
            PreviousPageCommand = new RelayCommand(PreviousPage);
            NextPageCommand = new RelayCommand(NextPage);
            GoToModifyPageCommand = new RelayCommand(GoToModifyPage);
            DeleteAuctionCommand = new RelayCommand(ShowMessageBox);
        }

        private void GoToStatistics()
        {
            var mainWindow = App.Current.Windows
                .OfType<MainWindow>()
                .FirstOrDefault();
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_Statistics());
            }
        }

        private void ShowMessageBox()
        {
            var result = MessageBox.Show(
                     "Sunteți sigur că doriți să ștergeți această licitație?",
                     "Confirmare ștergere",
                     MessageBoxButton.YesNo,
                     MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                var rc=false;
                rc=DatabaseManager.DeleteAuction(AuctionNumber);
                if (rc == false)
                {
                    GoToMainPage();
                    MessageBox.Show("Licitație ștearsă cu succes!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Licitație nu a putut fi ștearsă!", "", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
        }

        private void GoToModifyPage()
        {

            var mainWindow = App.Current.Windows
                    .OfType<MainWindow>()
                    .FirstOrDefault();
            var frame = mainWindow?.FindName("MainFrame") as Frame;
            var a = DatabaseManager.GetAuctionByNumber(_auctionNumber);

            if (frame != null)
            {
                frame.Navigate(new ModifyAuctionPage(a));
            }

        }


        private void PreviousPage()
        {

            if(_currentPage >1)
            {
                _currentPage--;
                UpdateDisplayedPosts();
            }
        }

        private void NextPage()
        {
            if(_currentPage*_selectedLotPerPage<Posts.Count())
            {
                _currentPage++;
                UpdateDisplayedPosts();
            }
        }

        private ObservableCollection<PostPreview> _posts;
        public ObservableCollection<PostPreview> Posts
        {
            get { return _posts; }
            set
            {
                if (_posts != value)
                {
                    _posts = value;
                    OnPropertyChange(nameof(Posts));
                }
            }
        }
        private ObservableCollection<VM_PostControl> _vmPostsPreview;
        public ObservableCollection<VM_PostControl> VM_PostsPreview
        {
            get { return _vmPostsPreview; }
            set
            {
                if (_vmPostsPreview != value) {
                    _vmPostsPreview = value;
                    OnPropertyChange(nameof(VM_PostsPreview));
                }
            } 
        }

        private ObservableCollection<VM_PostControl> _displayedPosts;
        public ObservableCollection<VM_PostControl> DisplayedPosts
        {
            get { return _displayedPosts; }
            set
            {
                if (_displayedPosts != value)
                {
                    _displayedPosts = value;
                    OnPropertyChange(nameof(DisplayedPosts));
                }
            }
        }


        private void UpdateDisplayedPosts()
        {
            if (Posts == null || VM_PostsPreview == null) return;

            var pagePosts = Posts.Skip((_currentPage - 1) * SelectedLotPerPage).Take(SelectedLotPerPage);

            var visiblePosts = VM_PostsPreview
                                    .Where(vm => pagePosts.Any(p => p.postId == vm.Id))
                                    .ToList();

            DisplayedPosts = new ObservableCollection<VM_PostControl>(visiblePosts);

        }

        private void ToggleDescription()
        {
            IsFullDescriptionVisible = !IsFullDescriptionVisible;
        }

        private void GoToMainPage()
        {
     
            var mainWindow = App.Current.Windows
                .OfType<MainWindow>()
                .FirstOrDefault();
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new VM_MainPage());
            }
        }

        private void GoToAddPost()
        {
          
            var mainWindow = App.Current.Windows
             .OfType<MainWindow>()
             .FirstOrDefault();
            var frame = mainWindow?.FindName("MainFrame") as Frame;

            if (frame != null)
            {
                frame.Navigate(new AddPostPage(AuctionType,AuctionNumber));
            }
        }
 
        public string AuctionTitle
        {
            get { return _auctionTitle; }
            set { _auctionTitle = value; 
            OnPropertyChange(nameof(AuctionTitle)); }
        }
    
        public DateTime AuctionDate
        {
            get { return _auctionDate; }
            set { _auctionDate = value; 
            OnPropertyChange(nameof(AuctionDate)); }
        }


        public string AuctionLocation
        {
            get { return _auctionLocation; }
            set { _auctionLocation = value; 
            OnPropertyChange(nameof(AuctionLocation)); }
        }

        public int AuctionLotCount
        {
            get { return _auctionLotCount; }
            set { _auctionLotCount = value; 
            OnPropertyChange(nameof(AuctionLotCount)); }
        }
        public int AuctionNumber
        {
            get { return _auctionNumber; }
            set
            {
                _auctionNumber = value;
                OnPropertyChange(nameof(AuctionNumber));
            }
        }

        public string SelectedSortType
        {
            get
            {
                if (_selectedSortType == null || _selectedSortType=="TOATE")
                    return "default";
                return _selectedSortType;
            }
            set { _selectedSortType = value;
                SetPostsPreview(DatabaseManager.GetPostPreview(AuctionNumber, SelectedSortType, SelectedSortStatus));
                OnPropertyChange(nameof(SelectedSortType)); }
        }

        public string SelectedSortStatus
        {
            get {
                if (_selectedSortStatus == null || _selectedSortStatus=="TOATE")
                    return "default";
                return _selectedSortStatus; }
            set
            {
                _selectedSortStatus = value;
                SetPostsPreview(DatabaseManager.GetPostPreview(AuctionNumber, SelectedSortType, SelectedSortStatus));
                OnPropertyChange(nameof(SelectedSortStatus));
            }
        }

        public int SelectedLotPerPage
        {
            get {
                if (_selectedLotPerPage==0)
                    return 6;
                return _selectedLotPerPage; }
            set { _selectedLotPerPage = value;
            UpdateDisplayedPosts();
            OnPropertyChange(nameof(SelectedLotPerPage)); }
        }

        public string AuctionImagePath
        {
            get { return _auctionImagePath; }
            set
            {
                _auctionImagePath = value;
                OnPropertyChange(nameof(AuctionImagePath));
            }
        }

        public string AuctionType
        {
            get { return _auctionType; }
            set
            {
                _auctionType = value;
                OnPropertyChange(nameof(AuctionType));
            }
        }



    }

}
