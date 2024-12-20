using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using Client_ADBD.Models;
using Client_ADBD.Views;
using Client_ADBD.Views.UserControl;
using CommunityToolkit.Mvvm.Input;

namespace Client_ADBD.ViewModels
{
    internal class VM_AuctionPage:VM_Base
    {
        private int _currentPage = 1;

        private string _auctionTitle;
        private string _auctionDescription;
        //private string _auctionShortDescription;
        private bool _isFullDescriptionVisible = false;
        private DateTime _auctionDate;
        private string _auctionLocation;
        private int _auctionLotCount;
        private int _auctionNumber;
        private string _selectedSortType;
        private int _selectedLotPerPage;
        private string _auctionImagePath;
        private ObservableCollection<PostControl> _lotsPerPage;
        //private ObservableCollection<AuctionLot> _lots;

        private string _auctionType;
        public ICommand GoToMainPageCommand { get; }
        public ICommand GoToAddPostCommand { get; }

        public ICommand ToggleDescriptionCommand { get; }
        public VM_AuctionPage() { }

        public string ButtonText => IsFullDescriptionVisible ? "Ascunde" : "Citește mai mult";
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
            if (_vmPostsPreview == null || DisplayedPosts.Count() == 0)
            {
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
            }

            Posts = new ObservableCollection<PostPreview>(posts);

            _currentPage = 1;
            UpdateDisplayedPosts();
        }
        
        public VM_AuctionPage(Auction_ auction)
        {


            AuctionTitle = auction.name;
            AuctionDescription =auction.description;
            //_auctionDescription = auction.description;
            AuctionDate =auction.startTime;
            AuctionLocation =auction.location;
            AuctionImagePath=auction.imagePath;
            AuctionType=auction.auctionType;
            AuctionNumber=auction.auctionNumber;
            SelectedLotPerPage = 9;

            SetPostsPreview(DatabaseManager.GetPostPreview(AuctionNumber));
 
            AuctionLotCount = Posts.Count();

            GoToAddPostCommand=new RelayCommand(GoToAddPost);
            GoToMainPageCommand = new RelayCommand(GoToMainPage);
            ToggleDescriptionCommand = new RelayCommand(ToggleDescription);

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

        //private void ShowFullDescription()
        //{
        //    IsDescriptionExpanded = !IsDescriptionExpanded;
        //}

        //public string GetShortDescription(string fullDescription)
        //{
        //    var words = fullDescription.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        //    var shortDescription = string.Join(" ", words.Take(30));
        //    return shortDescription+"...";
        //}
        //public string ButtonText
        //{
        //    get
        //    {
        //        return IsFullDescriptionVisible ? "Ascunde" : "Citește mai mult";
        //    }
        //}
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
        //public string AuctionDescription
        //{
        //    get { return _auctionDescription; }
        //    set { _auctionDescription = value; 
        //    OnPropertyChange(nameof(AuctionDescription)); }
        //}
    
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
            get { return _selectedSortType; }
            set { _selectedSortType = value; 
            OnPropertyChange(nameof(SelectedSortType)); }
        }

        public int SelectedLotPerPage
        {
            get { return _selectedLotPerPage; }
            set { _selectedLotPerPage = value; 
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
