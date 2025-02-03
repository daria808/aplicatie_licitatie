using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Linq;
using System.IO;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;



using Client_ADBD.Helpers;
using Client_ADBD.Models;
using Client_ADBD.Views;

namespace Client_ADBD
{
    internal class DatabaseManager
    {
        private static AuctionAppEntities _dbContext;
        public DatabaseManager()
        {
            _dbContext = new AuctionAppEntities();
        }

        public static string GetWatchTypeById(int id)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var watch = _dbContext.Watch_types.SingleOrDefault(p => p.id_watch_type ==id);

            return watch.type;
        }

        public static string GetWatchMechanismById(int id)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var watch = _dbContext.Watch_mechanisms.SingleOrDefault(p => p.id_mechanism == id);

            return watch.mechanism;
        }

        public static string GetJewelryTypeById(int id)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var j = _dbContext.Jewelry_types.SingleOrDefault(p => p.id_jewelry_type == id);

            return j.type;
        }

        public static void UpdateCommonPostDetails(int postId, decimal startPrice, decimal listPrice,int productId,string productName, string description,DateTime invDate,string[] ImagePaths)
        {

            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var post = _dbContext.Posts.SingleOrDefault(p => p.id_post == postId);

            if (post != null)
            {

                if (startPrice != post.start_price)
                    post.start_price = startPrice;


                if (listPrice != post.list_price)
                    post.list_price = listPrice;

            }

            var product = _dbContext.Products.SingleOrDefault(p => p.id_product == productId);

            if (product != null)
            {
                if(product.name!=productName)
                {
                    product.name = productName;
                }

                if (product.description != description)
                {
                    product.description = description;
                }

                if (product.inventory_date != invDate)
                {
                    product.inventory_date = invDate;
                }
            }

            var productImages = _dbContext.Product_images
              .Where(p => p.id_product == productId)
              .OrderBy(p => p.id_image) 
              .Take(3)
              .ToList();

            if (!string.IsNullOrEmpty(ImagePaths[0]))
            {
                if (productImages.Count > 0)
                {
                    productImages[0].image_path = ImagePaths[0];
                }
                else
                {
                    _dbContext.Product_images.InsertOnSubmit(new Product_image
                    {
                        id_product = productId,
                        image_path = ImagePaths[0]
                    });
                }
            }

            if (ImagePaths.Length>1 )
            {
               if(!string.IsNullOrEmpty(ImagePaths[1]))
               { 
                    if (productImages.Count > 1)
                    {

                        productImages[1].image_path = ImagePaths[1];
                    }
                    else
                    {
                        _dbContext.Product_images.InsertOnSubmit(new Product_image
                        {
                            id_product = productId,
                            image_path = ImagePaths[1]
                        });
                    }
                }
                else
                {
                    _dbContext.Product_images.DeleteOnSubmit(productImages[1]);
                }
            }

            if (ImagePaths.Length>2)
            {
                if(!string.IsNullOrEmpty(ImagePaths[2]))
                {
                    if (productImages.Count > 2)
                    {
                        productImages[2].image_path = ImagePaths[2];
                    }
                    else
                    {
                        _dbContext.Product_images.InsertOnSubmit(new Product_image
                        {
                            id_product = productId,
                            image_path = ImagePaths[2]
                        });
                    }
                }else
                {
                    _dbContext.Product_images.DeleteOnSubmit(productImages[2]);
                }
            }



            _dbContext.SubmitChanges();
        }

        public static List<Auction_> GetAuctionsByUserId(int userId, string statusFilter = "default", string sortFilter = "default")
        {
            // Creează o instanță de DataContext
            using (var context = new AuctionAppDataContext()) // Înlocuiește cu numele contextului tău de LINQ to SQL
            {
                var query = context.Auctions.Where(a => a.id_user == userId);

                // Maparea între obiectele Auction și Auction_
                var auctionList = query.Select(a => new Auction_
                {
                    id = a.id_auction,               // Mapare id
                    name = a.name,           // Mapare name
                    startTime = a.start_time, // Mapare start_time
                    endTime = a.end_time,    // Mapare end_time
                    location = a.location,   // Mapare location
                    imagePath = a.image_path, // Mapare imagePath
                    auctionNumber = a.auction_number // Mapare auctionNumber
                }).ToList(); // Execută și convertește la List<Auction_>

                return auctionList; // Returnează lista de Auction_
            }
        }

        public static void UpdateWatchPostDetails(int productId,decimal diameter,string manufacturer,string type,string mechanism)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var watch = _dbContext.Watches.SingleOrDefault(w => w.id_product == productId);

           
            if (watch.diameter!=diameter)
            {
                watch.diameter = diameter;
            }

            if (watch.manufacturer!=manufacturer)
            {
                watch.manufacturer = manufacturer;
            }

            int idType = GetIdWatchType(type);

            if (watch.id_type!=idType)
            {
                watch.id_type = idType;
            }

            int idMechanism = GetIdWatchMechanism(mechanism);

            if (watch.id_mechanism!=idMechanism)
            {
                watch.id_mechanism = idMechanism;

            }

            _dbContext.SubmitChanges();
            
        }

        public static void UpdateJewelryPostDetails(int productId, string brand, decimal weight, int creationYear, string type)
        {

            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var jewelry = _dbContext.Jewelries.SingleOrDefault(j => j.id_product == productId);

            if (jewelry.brand != brand)
            {
                jewelry.brand = brand;
            }

            if (jewelry.weight != weight)
            {
                jewelry.weight = weight;
            }

            if (jewelry.creation_year != creationYear)
            {
                jewelry.creation_year = creationYear;
            }

            int idType = GetJewelryIdType(type);

            if (jewelry.id_type != idType)
            {
                jewelry.id_type = idType;
            }

            _dbContext.SaveChanges();
        }
            
        public static void UpdateBookPostDetails(int productId,string author,int publicationYear,string publishingHouse,int pageNumber,string language,string condition)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var book = _dbContext.Books.SingleOrDefault(b => b.id_product == productId);


            if (book.author != author)
            {
                book.author = author;
            }

            if (book.publication_year != publicationYear)
            {
                book.publication_year =publicationYear;
            }

            if (book.publishing_house != publishingHouse)
            {
                book.publishing_house = publishingHouse;
            }

            if (book.page_number != pageNumber)
            {
                book.page_number = pageNumber;
            }

            if (book.book_language != language)
            {
                book.book_language = language;
            }

            int conditionId = GetBookConditionId(condition);

            if (conditionId != book.id_condition)
            {
                book.id_condition = conditionId;
            }

            _dbContext.SubmitChanges();

        }

        public static List<int> GetPostLotsNumber(int auction_id)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            return _dbContext.Posts.Where(p=>p.id_auction==auction_id).Select(p=>p.lot).ToList();
        }
        public static void UpdateSculpturePostDetails(int productId,string artist,decimal length,decimal width,decimal depth,string material)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var sculpture = _dbContext.Sculptures.FirstOrDefault(s => s.id_product ==productId);
            if (sculpture.artist != artist)
            {
                sculpture.artist = artist;
            }

            if (sculpture.length != length)
            {
                sculpture.length = length;
            }

            if (sculpture.width != width)
            {
                sculpture.width = width;
            }

            if (sculpture.depth != depth)
            {
                sculpture.depth = depth;
            }

            int materialId = GetSculptureMaterialId(material);

            if (materialId != sculpture.id_sculpture_material)
            {
                sculpture.id_sculpture_material = materialId;
            }

            _dbContext.SubmitChanges();

        }

        public static void UpdatePaintingPostDetails(int productId,string artist,decimal length,decimal width,string type,int creationYear)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }
            var painting = _dbContext.Paintings.FirstOrDefault(p => p.id_produs == productId);

     
            if (painting.artist != artist)
            {
                painting.artist =artist;
            }

            if (painting.creation_year != creationYear)
            {
                painting.creation_year = creationYear;
            }


            if (painting.length != length)
            {
                painting.length = length;
            }

            if (painting.width != width)
            {
                painting.width = width;
            }

            int typeId = GetPaintingIdType(type);

            if (painting.id_type != typeId)
            {
                painting.id_type = typeId;
            }

            _dbContext.SubmitChanges();
        }
        public static void UpdateAuction(Auction_ auctionToUpdate)
        {

            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var auction = _dbContext.Auctions.SingleOrDefault(a => a.auction_number == auctionToUpdate.auctionNumber);

            if (auction != null)
            {
                if (auctionToUpdate.name != auction.name)
                    auction.name = auctionToUpdate.name;

                if (auctionToUpdate.startTime != auction.start_time)
                    auction.start_time = auctionToUpdate.startTime;

                if (auctionToUpdate.endTime != auction.end_time)
                    auction.end_time = auctionToUpdate.endTime;

                if (auctionToUpdate.imagePath != auction.image_path)
                    auction.image_path = auctionToUpdate.imagePath;

                if (auctionToUpdate.description != auction.description)
                    auction.description = auctionToUpdate.description;

                if (auctionToUpdate.location != auction.location)
                    auction.location = auctionToUpdate.location;

                _dbContext.SubmitChanges();
            }
             
        }

        static public string GetUsernameById(int user_id)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var username = _dbContext.Users.Where(u=> u.id_user == user_id).FirstOrDefault().username;  
            return username;

        }

        static public string GetPostUser(int auction_number)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var username=(from u in _dbContext.Users
                          join a in _dbContext.Auctions on u.id_user equals a.id_user
                          where a.auction_number == auction_number
                          select u.username).FirstOrDefault();

            return username;

        }
        static public void PrintUsers()
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var users = from u in _dbContext.Users select u;
            foreach (var user in users)
            {
                Console.WriteLine(user.username);


            }
        }
        static public void AddUser(string firstName,string lastName,string username,string password,string email)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var salt = Hash.GenerateSalt();
            var hashPassword = Hash.HashPassword(password, salt);

            var newUser = new User
            {
                fisrt_name= firstName,
                last_name = lastName,
                username = username,
                salt = salt,
                password = hashPassword,
                email = email,
                created_at = DateTime.Now, 
                last_login = null,
                balance = 0.00M 
            };


            _dbContext.Users.Add(newUser);
            _dbContext.SubmitChanges();

            var role = new User_role
            {
                id_user = newUser.id_user,
                id_tip = 3,
                role_date = DateTime.Now
            };

            _dbContext.User_roles.InsertOnSubmit(role);
            _dbContext.SubmitChanges(); 

        }


        public int GetRoleIdByUsername(string username)
        {
            var item = _dbContext.Users.FirstOrDefault(u => u.username == username);
            if (item == null)
                throw new Exception("Utilizatorul nu există!");

            var role = _dbContext.User_roles.FirstOrDefault(r => r.id_user == item.id_user);
            if (role == null)
                throw new Exception("Nu există rol pentru acest utilizator!");

            return (int)role.id_tip;
        }

        public bool ChangeUserPassword(string username, string oldPassword, string newPassword)
        {
            var user = (from u in _dbContext.Users
                        where u.username == username
                        select new { u.username, u.password, u.salt }).FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            if (!Hash.VerifyPassword(oldPassword, user.password, user.salt))
            {
                return false;
            }

            if (Hash.VerifyPassword(newPassword, user.password, user.salt))
            {
                return false;
            }

            string newSalt = Hash.GenerateSalt();
            string newPasswordHash = Hash.HashPassword(newPassword, newSalt);


            var userToUpdate = _dbContext.Users.FirstOrDefault(u => u.username == username);
            if (userToUpdate != null)
            {
                userToUpdate.password = newPasswordHash;
                userToUpdate.salt = newSalt;

                _dbContext.SubmitChanges();
                return true;
            }

            return false;
        }


        public bool AdminChangeUserPassword(string username, string newPassword)
        {
            var user = (from u in _dbContext.Users
                        where u.username == username
                        select new { u.username, u.password, u.salt }).FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            if (Hash.VerifyPassword(newPassword, user.password, user.salt))
            {
                return false;
            }

            string newSalt = Hash.GenerateSalt();
            string newPasswordHash = Hash.HashPassword(newPassword, newSalt);


            var userToUpdate = _dbContext.Users.FirstOrDefault(u => u.username == username);
            if (userToUpdate != null)
            {
                userToUpdate.password = newPasswordHash;
                userToUpdate.salt = newSalt;

                _dbContext.SubmitChanges();
                return true;
            }

            return false;
        }


        static public bool VerifyUserCredentials(string username, string password)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var user = (from u in _dbContext.Users
                        where u.username == username
                        select new { u.username, u.password, u.salt }).FirstOrDefault();

            if (user == null)
            {
                return false;
            }

            return Hash.VerifyPassword(password, user.password, user.salt);
        }

        static public List<User_> GetUsers()
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var u = _dbContext.Users
                      .Select(u => new
                      {
                          u.id_user,
                          u.fisrt_name,
                          u.last_name,
                          u.username,
                          u.email,
                          u.created_at,
                          u.last_login,
                          u.balance
                      })
                      .ToList();

            List<User_> users = _dbContext.Users
                      .Select(u => new
                      {
                          u.id_user,
                          u.fisrt_name,
                          u.last_name,
                          u.username,
                          u.email,
                          u.created_at,
                          u.last_login,
                          u.balance
                      })
                      .ToList()
                      .Select(u => new User_(
                          id: u.id_user,
                          firstname: u.fisrt_name,
                          lastname: u.last_name,
                          username: u.username,
                          email: u.email,
                          date_created: u.created_at,
                          last_login: u.last_login,
                          balance: u.balance
                      ))
                      .ToList();

            return users;

        }

        static int GetAuctionIdType(string type)
        {

            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var idType = _dbContext.Auction_types
                          .Where(t => t.type_name == type)
                          .Select(t => t.id_auction_type)
                          .FirstOrDefault();

            return idType;
        }

        static public int GetAuctionNumber()
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var id=_dbContext.Auctions
                    .Max(t => (int?)t.auction_number) ?? 0;

            return id;
        }

        public static bool DeleteUserById(int userId)
        {
            try
            {
                using (var dbContext = new AuctionAppDataContext())
                {
                    var rolesToDelete = dbContext.User_roles.Where(ur => ur.id_user == userId);
                    if (rolesToDelete.Any())
                    {
                        dbContext.User_roles.DeleteAllOnSubmit(rolesToDelete);
                    }

                    var userInDb = dbContext.Users.FirstOrDefault(u => u.id_user == userId);
                    if (userInDb != null)
                    {
                        dbContext.Users.DeleteOnSubmit(userInDb);
                    }

                    dbContext.SubmitChanges();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Eroare la ștergerea utilizatorului și a relațiilor: {ex.Message}");
            }

            return false;
        }

        static public int GetUserIdByUsername(string username)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var id = _dbContext.Users
                     .Where(u => u.username == username) 
                     .Select(u => u.id_user)                 
                     .FirstOrDefault();

            return id;
        }


        static public void AddAuction(string auctionName,string auctionType,DateTime startDateTime,DateTime endDateTime,string imagePath,string description, string location,string usernameUser)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            int idType=GetAuctionIdType(auctionType);
            int idUser=GetUserIdByUsername(usernameUser);

            var newAuction = new Auction
            {
                name = auctionName,
                start_time = startDateTime,
                end_time = endDateTime,
                image_path = imagePath,
                location = location,
                description = description,
                id_auction_type = idType,
                auction_number=GetAuctionNumber()+1,
                id_user=idUser
              
            };

            _dbContext.Auctions.InsertOnSubmit(newAuction);
            _dbContext.SubmitChanges();

        }

        static public Auction_ GetAuctionByName(string auctionName)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var auction = _dbContext.Auctions
                         .FirstOrDefault(a => a.name == auctionName);

            if (auction == null)
            {
                throw new InvalidOperationException($"Auction with name '{auctionName}' not found.");
            }

            return new Auction_(auction);

        }

        static public Auction_ GetAuctionByNumber(int auctionNumber)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var auction = _dbContext.Auctions
                         .FirstOrDefault(a => a.auction_number == auctionNumber);

            if (auction == null)
            {
                throw new InvalidOperationException($"Auction with name '{auctionNumber}' not found.");
            }

            return new Auction_(auction);

        }

        static public  string GetPostStatusById(int postId)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var idStatus = _dbContext.Posts
                         .FirstOrDefault(p => p.id_post==postId).id_status;

            var status = _dbContext.Post_status.FirstOrDefault(s=> s.id_status==idStatus).status_name;

            return status;

        }


        static public string GetAuctionNameByPost(int ?auctionId)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var aName=_dbContext.Auctions.FirstOrDefault(a=>a.id_auction==auctionId).name;

            return aName;
           
        }

        public bool IsUserAdmin(string username)
        {
            using (var dbContext = new AuctionAppDataContext())
            {
                try
                {
                    var existingUser = dbContext.Users.FirstOrDefault(u => u.username == username);

                    if (existingUser == null)
                    {
                        throw new Exception("Utilizatorul nu a fost găsit în baza de date.");
                    }

                    var role = dbContext.User_roles.FirstOrDefault(r => r.id_user == existingUser.id_user);


                    if (role == null)
                    {
                        throw new Exception("Nu a fost găsit un rol asociat acestui utilizator.");
                    }

                    if (role.id_tip == 1)
                        return true;

                    return false;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }

        public int CountTotalUsers()
        {
            return _dbContext.Users.Count() - 1;
        }

        public int CountTotalBidders()
        {
            return _dbContext.User_roles.Count(ur => ur.id_tip == 2);
        }

        public int CountTotalClients()
        {
            return _dbContext.User_roles.Count(ur => ur.id_tip == 3);
        }
        public void UpdateUserRoleToBidder(string username)
        {
            // Creează un nou context pentru operația curentă
            using (var dbContext = new AuctionAppDataContext())
            {
                try
                {
                    // Găsește utilizatorul în baza de date
                    var existingUser = dbContext.Users.FirstOrDefault(u => u.username == username);

                    if (existingUser == null)
                    {
                        throw new Exception("Utilizatorul nu a fost găsit în baza de date.");
                    }

                    // Găsește rolul asociat utilizatorului
                    var role = dbContext.User_roles.FirstOrDefault(r => r.id_user == existingUser.id_user);

                    if (role == null)
                    {
                        throw new Exception("Nu a fost găsit un rol asociat acestui utilizator.");
                    }

                    // Schimbă tipul de rol al utilizatorului (2 = "Bidder")
                    role.id_tip = 2;

                    // Salvează modificările în baza de date
                    dbContext.SubmitChanges();
                }
                catch (Exception ex)
                {
                    // Aruncă o excepție cu detalii suplimentare
                    throw new Exception($"Eroare la actualizarea rolului utilizatorului: {ex.Message}");
                }
            }
        }

        public void UpdateUserRoleToClient(string username)
        {
            using (var dbContext = new AuctionAppDataContext())
            {
                try
                {
                    var existingUser = dbContext.Users.FirstOrDefault(u => u.username == username);

                    if (existingUser == null)
                    {
                        throw new Exception("Utilizatorul nu a fost găsit în baza de date.");
                    }

                    var role = dbContext.User_roles.FirstOrDefault(r => r.id_user == existingUser.id_user);

                    if (role == null)
                    {
                        throw new Exception("Nu a fost găsit un rol asociat acestui utilizator.");
                    }

                    role.id_tip = 3;

                    dbContext.SubmitChanges();
                }
                catch (Exception ex)
                {
                    throw new Exception($"Eroare la actualizarea rolului utilizatorului: {ex.Message}");
                }
            }
        }


        public void UpdateUser(string username, string firstName, string lastName, string email, decimal balance)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            try
            {
                // Găsește utilizatorul în baza de date
                var existingUser = _dbContext.Users.FirstOrDefault(u => u.username == username);

                if (existingUser == null)
                {
                    throw new Exception("Utilizatorul nu a fost găsit!");
                }

                // Actualizează câmpurile utilizatorului
                existingUser.fisrt_name = firstName;
                existingUser.last_name = lastName;
                existingUser.email = email;
                existingUser.balance = balance;

                // Salvează modificările
                _dbContext.SubmitChanges();

                //CurrentUser.User = new User_
                //{
                //    _id= existingUser.id_user,
                //    _firstName = firstName,
                //    _lastName = lastName,
                //    _email = email,
                //    _balance = balance,
                //    _username = username,
                //    _lastLogin = existingUser.last_login,
                //    _dateCreated=existingUser.created_at,
                    

                //};
                CurrentUser.User._balance = balance;
            }
            catch (Exception ex)
            {
                throw new Exception($"Eroare la actualizarea utilizatorului: {ex.Message}");
            }
        }

        static public string GetAuctionType(int idAuctionType)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            string type = _dbContext.Auction_types.Where(t => t.id_auction_type == idAuctionType).FirstOrDefault().type_name;

            return type;
        }

        static public List< Auction_> GetAuction(string auctionStatus, string filter)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var query = _dbContext.Auctions.AsQueryable();

            if (!string.Equals(auctionStatus, "default", StringComparison.OrdinalIgnoreCase))
            {
                query = query.Where(auction =>
                    auctionStatus.Equals("Licitații în curs", StringComparison.OrdinalIgnoreCase)
                        ? auction.start_time <= DateTime.Now && auction.end_time > DateTime.Now
                        : auctionStatus.Equals("Licitații viitoare", StringComparison.OrdinalIgnoreCase)
                            ? auction.start_time > DateTime.Now
                            : auctionStatus.Equals("Closed", StringComparison.OrdinalIgnoreCase)
                                ? auction.end_time <= DateTime.Now
                                : true);
            }

            if (string.Equals(filter, "Crescător", StringComparison.OrdinalIgnoreCase))
            {
                query = query.OrderBy(auction => auction.start_time);
            }
            else if (string.Equals(filter, "Descrescător", StringComparison.OrdinalIgnoreCase))
            {
                query = query.OrderByDescending(auction => auction.start_time);
            }

            // Mapare în DTO dacă `Auction_` este diferit de `Auction`
            return query
                .Select(auction => new Auction_
                {
                    id = auction.id_auction,
                    name = auction.name,
                    startTime = auction.start_time,
                    endTime = auction.end_time,
                    description = auction.description,
                    location = auction.location,
                    imagePath = auction.image_path,
                    auctionNumber = auction.auction_number, 
                    auctionType=GetAuctionType(auction.id_auction_type)
                    
                })
                .ToList();

        }

        static public int GetIdByUsername(string username)
        {
            var item = _dbContext.Users.FirstOrDefault(u => u.username == username);
            if (item == null)
                throw new Exception("Utilizatorul nu există!");

            return item.id_user;
        }
        static public int GetIdWatchMechanism(string mechanism)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var id = _dbContext.Watch_mechanisms.Where(p => p.mechanism == mechanism).FirstOrDefault().id_mechanism;

            return id;


        }

        static public int GetIdWatchType(string type)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var id = _dbContext.Watch_types.Where(p => p.type == type).FirstOrDefault().id_watch_type;

            return id;
        }

        static int GetAuctionIdByNumber(int number)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var id = _dbContext.Auctions.Where(p => p.auction_number == number).FirstOrDefault().id_auction;

            return id;
        }

        static public string GetProductNameById(int productId)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            string name = _dbContext.Products.Where(p => p.id_product == productId).FirstOrDefault().name;

            return name;
        }
        static public string GetManufacturerName(string auction_type,int productId)
        {

            switch (auction_type)
            {
                case "Ceasuri":
                    return _dbContext.Watches.Where(p => p.id_product == productId).FirstOrDefault().manufacturer??string.Empty;
                case "Bijuterii":
                    return _dbContext.Jewelries.Where(p=>p.id_product==productId).FirstOrDefault().brand ?? string.Empty;
                case "Carti":
                    return _dbContext.Books.Where(p => p.id_product == productId).FirstOrDefault().author ?? string.Empty;
                case "Sculpturi":
                    return _dbContext.Sculptures.Where(p => p.id_product == productId).FirstOrDefault().artist ?? string.Empty;
                case "Tablouri":
                    return _dbContext.Paintings.Where(p => p.id_produs == productId).FirstOrDefault().artist ?? string.Empty;
                default:
                    return string.Empty;

            }
        }

        static public int GetAuctionIdType(int auction_id)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var id = _dbContext.Auctions.Where(p => p.id_auction == auction_id).FirstOrDefault().id_auction_type;

            return id;
        }

        static public string GetFirstProductImagePath(int product_id)
        {
            if (_dbContext == null) {
                _dbContext = new AuctionAppDataContext();
            }

            var path = _dbContext.Product_images.Where(p => p.id_product == product_id).FirstOrDefault().image_path;

            return path;
        }
        static public List<PostPreview> GetPostPreview(int auctionNumber,string sortType="default",string postStatus="default")
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var auctionId = GetAuctionIdByNumber(auctionNumber);
            var auctionIdType = GetAuctionIdType(auctionId);
            var auctionType = GetAuctionType(auctionIdType);


            var query = from post in _dbContext.Posts
                        join pr in _dbContext.Products on post.id_product equals pr.id_product
                        join ps in _dbContext.Post_status on post.id_status equals ps.id_status
                        join a in _dbContext.Auctions on post.id_auction equals a.id_auction
                        where post.id_auction == auctionId
                        select new PostPreview
                        {
                            postId = post.id_post,
                            imagePath = GetFirstProductImagePath(pr.id_product),
                            postName = pr.name,
                            artistName = GetManufacturerName(auctionType, pr.id_product),
                            startPrice = post.start_price,
                            postStatus = ps.status_name
                        };

            if(postStatus !="default")
            {
                query = query.Where(post => post.postStatus.ToLower() == postStatus.ToLower());
            }

            switch(sortType)
            {
                case "PREȚ, ASCENDENT":
                    query = query.OrderBy(post => post.startPrice);
                
                    break;
                case "PREȚ, DESCENDENT":
                    query = query.OrderByDescending(post => post.startPrice);
                    break;
                default:
                    break;

            }

            return query.ToList();
        }

        static public bool DeletePost(int postId,string auctionType)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            //var postToDelete = _dbContext.Posts.SingleOrDefault(p => p.id_post== postId);

            var productToDelete = (from pr in _dbContext.Products
                                   join post in _dbContext.Posts on pr.id_product equals post.id_product
                                   where post.id_post == postId
                                    select pr).First();

            if (productToDelete != null)
            {
                _dbContext.Products.DeleteOnSubmit(productToDelete);
                _dbContext.SubmitChanges();
                return false;
            }

            //switch(auctionType)
            //{
                           
            //    case "Ceasuri":
            //        var w = _dbContext.Watches.Where(w => w.id_product == postToDelete.id_product).First();
            //        _dbContext.Watches.DeleteOnSubmit(w);
            //        break;

            //    case "Bijuterii":
            //        var j = _dbContext.Jewelries.Where(w => w.id_product == postToDelete.id_product).First();
            //        _dbContext.Jewelries.DeleteOnSubmit(j);
            //        _dbContext.SubmitChanges();
            //        break;
            //    case "Carti":
            //        var b = _dbContext.Books.Where(w => w.id_product == postToDelete.id_product).First();
            //        _dbContext.Books.DeleteOnSubmit(b);
            //        _dbContext.SubmitChanges();
            //        break;
            //    case "Sculpturi":
            //        var s = _dbContext.Sculptures.Where(w => w.id_product == postToDelete.id_product).First();
            //        _dbContext.Sculptures.DeleteOnSubmit(s);
            //        _dbContext.SubmitChanges();
            //        break;
            //    case "Tablouri":
            //        var p = _dbContext.Paintings.Where(w => w.id_produs == postToDelete.id_product).First();
            //        _dbContext.Paintings.DeleteOnSubmit(p);
            //        _dbContext.SubmitChanges();
            //        break;  
            //    default:
            //        break;
                    
            //}
            //if (postToDelete != null)
            //{
            //    _dbContext.Posts.DeleteOnSubmit(postToDelete);
            //    _dbContext.SubmitChanges();
            //    return false;
            //}

            return true;
        }

        static public bool DeleteAuction(int auctionNumber)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var auctionToDelete = _dbContext.Auctions.SingleOrDefault(a => a.auction_number == auctionNumber);

            if (auctionToDelete != null)
            {
                _dbContext.Auctions.DeleteOnSubmit(auctionToDelete);
                _dbContext.SubmitChanges();

                return false;
            }

            return true;
        }

        static public void AddWatchPost(int auctionNumber,decimal startPrice,decimal listPrice,DateTime creationTime,string[] imagePath,
            string productName,string description,DateTime inventoryDate,string mechanism,string type,decimal diameter,string manufacturer)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }


            try
            {
                using (var transaction = new TransactionScope())
                {
                    var newProduct = new Product
                    {
                        name = productName,
                        description = description,
                        inventory_date = inventoryDate

                    };

                    _dbContext.Products.InsertOnSubmit(newProduct);
                    _dbContext.SubmitChanges();

                    int productId = newProduct.id_product;

                    int idMechanism = GetIdWatchMechanism(mechanism);
                    int idType = GetIdWatchType(type);

                    var newWatch = new Watch
                    {
                        id_product = productId,  // Asociem detaliile cu produsul prin ID
                        id_mechanism = idMechanism,
                        id_type = idType,
                        diameter = diameter,
                        manufacturer = manufacturer
                    };

                    _dbContext.Watches.InsertOnSubmit(newWatch);
                    _dbContext.SubmitChanges();

                    int idAuction = GetAuctionIdByNumber(auctionNumber);
                    int lotNumber = GetNextLotNumber(idAuction);

                    var newPost = new Post
                    {
                        id_product = productId,
                        id_status = 2,
                        id_auction = idAuction,
                        start_price = startPrice,
                        list_price = listPrice,
                        created_at = creationTime,
                        lot=lotNumber,
                    };

                    _dbContext.Posts.InsertOnSubmit(newPost);
                    _dbContext.SubmitChanges();

                    var newImage = new Product_image
                    {
                        id_product = productId,
                        image_path = imagePath[0]
                    };

                    _dbContext.Product_images.InsertOnSubmit(newImage);
                    _dbContext.SubmitChanges();

                    if (!string.IsNullOrEmpty(imagePath[1]))
                    {
                        var newImage1 = new Product_image
                        {
                            id_product = productId,
                            image_path = imagePath[1]
                        };

                        _dbContext.Product_images.InsertOnSubmit(newImage1);
                        _dbContext.SubmitChanges();
                    }

                    if (!string.IsNullOrEmpty(imagePath[2]))
                    {
                        var newImage2 = new Product_image
                        {
                            id_product = productId,
                            image_path = imagePath[2]
                        };

                        _dbContext.Product_images.InsertOnSubmit(newImage2);
                        _dbContext.SubmitChanges();
                    }


                    transaction.Complete();



                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Eroare la adăugarea ceasului: {ex.Message}");
            }
        }
        static public int GetSculptureMaterialId(string material)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var id = _dbContext.Sculpture_materials.Where(p => p.material == material).FirstOrDefault().id_sculpture_material;

            return id;
        }
        static public void AddSculpturePost(int auctionNumber, decimal startPrice, decimal listPrice, DateTime creationTime, string[] imagePath,
          string productName, string description, DateTime inventoryDate, string artist,string material,decimal width,decimal length,decimal depth)
        {

            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }


            try
            {
                using (var transaction = new TransactionScope())
                {
                    var newProduct = new Product
                    {
                        name = productName,
                        description = description,
                        inventory_date = inventoryDate

                    };

                    _dbContext.Products.InsertOnSubmit(newProduct);
                    _dbContext.SubmitChanges();

                    int productId = newProduct.id_product;

                    int idMaterial = GetSculptureMaterialId(material);



                    var newSculpture = new Sculpture
                    {
                        id_product = productId,
                        id_sculpture_material = idMaterial,
                        artist = artist,
                        length = length,
                        depth = depth,
                        width = width
                    };

                    _dbContext.Sculptures.InsertOnSubmit(newSculpture);
                    _dbContext.SubmitChanges();

                    int idAuction = GetAuctionIdByNumber(auctionNumber);
                    int lotNumber = GetNextLotNumber(idAuction);

                    var newPost = new Post
                    {
                        id_product = productId,
                        id_status = 2,
                        id_auction = idAuction,
                        start_price = startPrice,
                        list_price = listPrice,
                        created_at = creationTime,
                        lot=lotNumber,
                   
                    };

                    _dbContext.Posts.InsertOnSubmit(newPost);
                    _dbContext.SubmitChanges();


                    var newImage = new Product_image
                    {
                        id_product = productId,
                        image_path = imagePath[0]
                    };

                    _dbContext.Product_images.InsertOnSubmit(newImage);
                    _dbContext.SubmitChanges();

                    if (!string.IsNullOrEmpty(imagePath[1]))
                    {
                        var newImage1 = new Product_image
                        {
                            id_product = productId,
                            image_path = imagePath[1]
                        };

                        _dbContext.Product_images.InsertOnSubmit(newImage1);
                        _dbContext.SubmitChanges();
                    }

                    if (!string.IsNullOrEmpty(imagePath[2]))
                    {
                        var newImage2 = new Product_image
                        {
                            id_product = productId,
                            image_path = imagePath[2]
                        };

                        _dbContext.Product_images.InsertOnSubmit(newImage2);
                        _dbContext.SubmitChanges();
                    }


                    //var newImage1 = new Product_image
                    //{
                    //    id_product = productId,
                    //    image_path = imagePath[0]
                    //};

                    //_dbContext.Product_images.InsertOnSubmit(newImage1);
                    //_dbContext.SubmitChanges();

                    //var newImage2 = new Product_image
                    //{
                    //    id_product = productId,
                    //    image_path = imagePath[0]
                    //};

                    //_dbContext.Product_images.InsertOnSubmit(newImage2);
                    //_dbContext.SubmitChanges();
                   transaction.Complete();


                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Eroare la adăugarea sculpturii: {ex.Message}");
            }
        }

        static public int GetNextLotNumber(int auction_id)
        {
            if(_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }
            int maxLot = 1; 

            var lots = _dbContext.Posts
                .Where(post => post.id_auction == auction_id)
                .Select(post => post.lot);

            if (lots.Any()) 
            {
                maxLot = lots.Max() + 1;
            }

            return maxLot;
        }
        static public int GetBookConditionId(string condition)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }
            var id = _dbContext.Book_conditions.Where(p => p.condition == condition).FirstOrDefault().id_book_condition;

            return id;
        }
        static public void AddBookPost(int auctionNumber, decimal startPrice, decimal listPrice, DateTime creationTime, string[] imagePath,
         string productName, string description, DateTime inventoryDate, string author, string condition, int year, string ph, int pageNr,string language)
        {

            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }


            try
            {
                using (var transaction = new TransactionScope())
                {
                    var newProduct = new Product
                    {
                        name = productName,
                        description = description,
                        inventory_date = inventoryDate

                    };

                    _dbContext.Products.InsertOnSubmit(newProduct);
                    _dbContext.SubmitChanges();

                    int productId = newProduct.id_product;
                    int idCondition = GetBookConditionId(condition);



                    var newBook = new Book
                    {
                        id_product = productId,
                        id_condition = idCondition,
                        author = author,
                        publication_year = year,
                        publishing_house = ph,
                        page_number = pageNr,
                        book_language = language
                    };

                    _dbContext.Books.InsertOnSubmit(newBook);
                    _dbContext.SubmitChanges();

                    int idAuction = GetAuctionIdByNumber(auctionNumber);
                    int lotNumber=GetNextLotNumber(idAuction);

                    var newPost = new Post
                    {
                        id_product = productId,
                        id_status = 2,
                        id_auction = idAuction,
                        start_price = startPrice,
                        list_price = listPrice,
                        created_at = creationTime,
                        lot=lotNumber,
                     
                    };

                    _dbContext.Posts.InsertOnSubmit(newPost);
                    _dbContext.SubmitChanges();

                    var newImage = new Product_image
                    {
                        id_product = productId,
                        image_path = imagePath[0]
                    };

                    _dbContext.Product_images.InsertOnSubmit(newImage);
                    _dbContext.SubmitChanges();

                    if (!string.IsNullOrEmpty(imagePath[1]))
                    {
                        var newImage1 = new Product_image
                        {
                            id_product = productId,
                            image_path = imagePath[1]
                        };


                        _dbContext.Product_images.InsertOnSubmit(newImage1);
                        _dbContext.SubmitChanges();
                    }

                    if (!string.IsNullOrEmpty(imagePath[2]))
                    {
                        var newImage2 = new Product_image
                        {
                            id_product = productId,
                            image_path = imagePath[2]
                        };

                        _dbContext.Product_images.InsertOnSubmit(newImage2);
                        _dbContext.SubmitChanges();
                    }

                    transaction.Complete();



                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Eroare la adăugarea sculpturii: {ex.Message}");
            }
        }

        static public Post_ GetPostDetails(int postId)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var auctionType = (from post in _dbContext.Posts
                               join ac in _dbContext.Auctions on post.id_auction equals ac.id_auction
                               join at in _dbContext.Auction_types on ac.id_auction_type equals at.id_auction_type
                               where post.id_post == postId
                               select at.type_name).FirstOrDefault();

            string[] imagePaths = _dbContext.Posts
                                .Where(post => post.id_post == postId) 
                                .Join(
                                    _dbContext.Product_images, 
                                    post => post.id_product,  
                                    img => img.id_product,
                                    (post, img) => img 
                                )
                                .OrderBy(img => img.id_image) 
                                .Take(3) 
                                .Select(img => img.image_path) 
                                .ToArray();

            string lastOfferUser = string.Empty;
            decimal lastOffer = DatabaseManager.GetPostLastOffer(postId, ref lastOfferUser);

            switch (auctionType)
            {
                case "Ceasuri":

                    var WatchPost = (from post in _dbContext.Posts
                                 join pr in _dbContext.Products on post.id_product equals pr.id_product
                                 join ps in _dbContext.Post_status on post.id_status equals ps.id_status
                                 join a in _dbContext.Auctions on post.id_auction equals a.id_auction
                                 join wch in _dbContext.Watches on pr.id_product equals wch.id_product
                                 join wm in _dbContext.Watch_mechanisms on wch.id_mechanism equals wm.id_mechanism
                                 join wt in _dbContext.Watch_types on wch.id_type equals wt.id_watch_type
                                 where post.id_post == postId
                                 select new Post_
                                 {
                                     postId = postId,
                                     productStatus = ps.status_name,
                                     auctionName = a.name,
                                     auctionNumber = a.auction_number,
                                     auctionType = auctionType,
                                     creationTime = post.created_at,
                                     lotNumber=post.lot,
                                     lastOffer=lastOffer,
                                     lastOfferUser=lastOfferUser,
                                     product = new Watch_(wm.mechanism, wch.diameter, wch.manufacturer, pr.id_product, pr.name, post.start_price,
                                                          post.list_price, imagePaths, pr.description, Helpers.Utilities.ConvertDateTimeNullToNotNull(pr.inventory_date),wt.type)

                                 }).FirstOrDefault();
                    return WatchPost;
                case "Bijuterii":
                    var JewwlryPost = (from post in _dbContext.Posts
                                       join pr in _dbContext.Products on post.id_product equals pr.id_product
                                       join ps in _dbContext.Post_status on post.id_status equals ps.id_status
                                       join a in _dbContext.Auctions on post.id_auction equals a.id_auction
                                       join jw in _dbContext.Jewelries on pr.id_product equals jw.id_product
                                       join jt in _dbContext.Jewelry_types on jw.id_type equals jt.id_jewelry_type
                                       where post.id_post == postId
                                       select new Post_
                                       {
                                           postId = postId,
                                           productStatus = ps.status_name,
                                           auctionName=a.name,
                                           auctionNumber = a.auction_number,
                                           auctionType = auctionType,
                                           creationTime = post.created_at,
                                           lotNumber = post.lot,
                                           lastOffer = lastOffer,
                                           lastOfferUser = lastOfferUser,
                                           product = new Jewelry_(pr.id_product, pr.name, pr.description, Helpers.Utilities.ConvertDateTimeNullToNotNull(pr.inventory_date), post.start_price,
                                           post.list_price, imagePaths, jt.type, jw.brand, jw.weight, jw.creation_year)
                                       }).FirstOrDefault();
                    return JewwlryPost;
                case "Carti":
                    var BookPost = (from post in _dbContext.Posts
                                    join pr in _dbContext.Products on post.id_product equals pr.id_product
                                    join ps in _dbContext.Post_status on post.id_status equals ps.id_status
                                    join a in _dbContext.Auctions on post.id_auction equals a.id_auction
                                    join bk in _dbContext.Books on pr.id_product equals bk.id_product
                                    join bc in _dbContext.Book_conditions on bk.id_condition equals bc.id_book_condition
                                    where post.id_post == postId
                                    select new Post_
                                    {
                                        postId = postId,
                                        productStatus = ps.status_name,
                                        auctionName = a.name,
                                        auctionNumber = a.auction_number,
                                        auctionType = auctionType,
                                        creationTime = post.created_at,
                                        lotNumber = post.lot,
                                        lastOffer = lastOffer,
                                        lastOfferUser = lastOfferUser,
                                        product = new Book_(pr.id_product, pr.name, pr.description, Helpers.Utilities.ConvertDateTimeNullToNotNull(pr.inventory_date), post.start_price, post.list_price,
                                        bc.condition, bk.author, bk.publication_year, bk.publishing_house, bk.page_number, bk.book_language, imagePaths)
                                    }).FirstOrDefault();
                    return BookPost;
                case "Sculpturi":
                    var SculpturePost = (from post in _dbContext.Posts
                                         join pr in _dbContext.Products on post.id_product equals pr.id_product
                                         join ps in _dbContext.Post_status on post.id_status equals ps.id_status
                                         join a in _dbContext.Auctions on post.id_auction equals a.id_auction
                                         join sc in _dbContext.Sculptures on pr.id_product equals sc.id_product
                                         join sm in _dbContext.Sculpture_materials on sc.id_sculpture_material equals sm.id_sculpture_material
                                         where post.id_post == postId
                                         select new Post_
                                         {
                                             postId = postId,
                                             productStatus = ps.status_name,
                                             auctionName = a.name,
                                             auctionNumber = a.auction_number,
                                             auctionType = auctionType,
                                             creationTime = post.created_at,
                                             lotNumber = post.lot,
                                             lastOffer = lastOffer,
                                             lastOfferUser = lastOfferUser,
                                             product = new Sculpture_(pr.id_product, pr.name, pr.description, Helpers.Utilities.ConvertDateTimeNullToNotNull(pr.inventory_date), post.start_price, post.list_price,
                                           imagePaths, sm.material, sc.artist, sc.length, sc.width, sc.depth)
                                         }).FirstOrDefault();
                    return SculpturePost;
                case "Tablouri":
                    var PaintingPost = (from post in _dbContext.Posts
                                        join pr in _dbContext.Products on post.id_product equals pr.id_product
                                        join ps in _dbContext.Post_status on post.id_status equals ps.id_status
                                        join a in _dbContext.Auctions on post.id_auction equals a.id_auction
                                        join pn in _dbContext.Paintings on pr.id_product equals pn.id_produs
                                        join pt in _dbContext.Painting_types on pn.id_type equals pt.id_painting_type
                                        where post.id_post == postId
                                        select new Post_
                                        {
                                            postId = postId,
                                            productStatus = ps.status_name,
                                            auctionName = a.name,
                                            auctionNumber = a.auction_number,
                                            auctionType = auctionType,
                                            lotNumber = post.lot,
                                            creationTime = post.created_at,
                                            lastOffer = lastOffer,
                                            lastOfferUser = lastOfferUser,
                                            product = new Painting_(pr.id_product, pr.name, pr.description, Helpers.Utilities.ConvertDateTimeNullToNotNull(pr.inventory_date), post.start_price, post.list_price,
                                            pt.type, pn.artist, pn.creation_year, pn.length, pn.width, imagePaths
                                           )
                                        }).FirstOrDefault();
                    return PaintingPost;
                default:
                    return null;
            }
        }

        static public int GetJewelryIdType(string type)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }
            var id = _dbContext.Jewelry_types.Where(p => p.type == type).FirstOrDefault().id_jewelry_type;

            return id;
        }
        static public void AddJewelryPost(int auctionNumber, decimal startPrice, decimal listPrice, DateTime creationTime, string[] imagePath,
         string productName, string description, DateTime inventoryDate,string type, string brand,decimal weight, int year)
        {

            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }


            try
            {
                using (var transaction = new TransactionScope())
                {
                    var newProduct = new Product
                    {
                        name = productName,
                        description = description,
                        inventory_date = inventoryDate

                    };

                    _dbContext.Products.InsertOnSubmit(newProduct);
                    _dbContext.SubmitChanges();

                    int productId = newProduct.id_product;
                    int idType = GetJewelryIdType(type);


                    var newJewelry = new Jewelry
                    {
                        id_product = productId,
                        id_type = idType,
                        brand = brand,
                        weight = weight,
                        creation_year=year
                    };

                    _dbContext.Jewelries.InsertOnSubmit(newJewelry);
                    _dbContext.SubmitChanges();

                    int idAuction = GetAuctionIdByNumber(auctionNumber);
                    int lotNumber = GetNextLotNumber(idAuction);

                    var newPost = new Post
                    {
                        id_product = productId,
                        id_status = 2,
                        id_auction = idAuction,
                        start_price = startPrice,
                        list_price = listPrice,
                        created_at = creationTime,
                        lot=lotNumber,
                       
                    };

                    _dbContext.Posts.InsertOnSubmit(newPost);
                    _dbContext.SubmitChanges();


                    var newImage = new Product_image
                    {
                        id_product = productId,
                        image_path = imagePath[0]
                    };

                    _dbContext.Product_images.InsertOnSubmit(newImage);
                    _dbContext.SubmitChanges();

                    if (!string.IsNullOrEmpty(imagePath[1]))
                    {
                        var newImage1 = new Product_image
                        {
                            id_product = productId,
                            image_path = imagePath[1]
                        };


                        _dbContext.Product_images.InsertOnSubmit(newImage1);
                        _dbContext.SubmitChanges();
                    }

                    if (!string.IsNullOrEmpty(imagePath[2]))
                    {
                        var newImage2 = new Product_image
                        {
                            id_product = productId,
                            image_path = imagePath[2]
                        };

                        _dbContext.Product_images.InsertOnSubmit(newImage2);
                        _dbContext.SubmitChanges();
                    }

                    transaction.Complete();

                
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Eroare la adăugarea sculpturii: {ex.Message}");
            }


        }

        static public int GetPostIdByLot(int lotNumber,int auctionNumber)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }
          
            var id=(from post in _dbContext.Posts
                    join a in _dbContext.Auctions on post.id_auction equals a.id_auction
                    where a.auction_number ==auctionNumber && post.lot ==lotNumber
                    select post.id_post).FirstOrDefault();

            return id;
        }

        static public int GetPaintingIdType(string type)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }
            var id = _dbContext.Painting_types.Where(p => p.type == type).FirstOrDefault().id_painting_type;

            return id;
        }

        public static decimal GetPostLastOffer(int postID, ref string lastOfferUser)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var lastOffer = _dbContext.Bids
                  .Where(b => b.id_post == postID)  
                  .OrderByDescending(b => b.bid_date)  
                  .FirstOrDefault();

            if (lastOffer == null)
            {
                lastOfferUser=string.Empty;
                return -1;
            }
            else
            {
                if (lastOffer.id_user != null)
                {
                    lastOfferUser = GetUsernameById(lastOffer.id_user ?? 0);
                }
                else {
                    lastOfferUser = string.Empty;
                }


                return lastOffer.bid_price;
            }
        }
        public static void AddBid(int postId, int idUsesr, decimal bidPrice)
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var newBid = new Bid
            {
                id_post = postId,
                id_user = idUsesr,
                bid_price = bidPrice,
                bid_date = DateTime.Now,
            };

            _dbContext.Bids.InsertOnSubmit(newBid);
            _dbContext.SubmitChanges();
        }
        
        static public void UpdatePostStatus()
        {
            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }

            var postsToUpdate = (from p in _dbContext.Posts
                                 join a in _dbContext.Auctions on p.id_auction equals a.id_auction
                                 join ps in _dbContext.Post_status on p.id_status equals ps.id_status
                                 where a.end_time < DateTime.Now && ps.status_name == "Neadjudecat"
                                 select p);

            foreach (var post in postsToUpdate)
            {
                string user = string.Empty;
                decimal bidOffer = DatabaseManager.GetPostLastOffer(post.id_post, ref user);

                if (bidOffer > 0)
                {
                    post.id_status = _dbContext.Post_status.Where(p => p.status_name == "Adjudecat").FirstOrDefault().id_status;
                    
                }

                if(!string.IsNullOrEmpty(user))
                {
                    var user2 = (from u in _dbContext.Users
                                 where u.username == user
                                 select u).FirstOrDefault();

                    if (user2 != null)
                    {
                        user2.balance -= bidOffer;
                    }
                    
                }
            }


            _dbContext.SubmitChanges();
        }

        static public void AddPaintingPost(int auctionNumber, decimal startPrice, decimal listPrice, DateTime creationTime, string[] imagePath,
       string productName, string description, DateTime inventoryDate, string type, string artist, int year,decimal length,decimal width)
        {

            if (_dbContext == null)
            {
                _dbContext = new AuctionAppDataContext();
            }


            try
            {
                using (var transaction = new TransactionScope())
                {
                    var newProduct = new Product
                    {
                        name = productName,
                        description = description,
                        inventory_date = inventoryDate

                    };

                    _dbContext.Products.InsertOnSubmit(newProduct);
                    _dbContext.SubmitChanges();

                    int productId = newProduct.id_product;
                    int idType = GetPaintingIdType(type);


                    var newPainting = new Painting
                    {
                        id_produs = productId,
                        id_type = idType,
                        artist = artist,             
                        creation_year = year,
                        length = length,
                        width = width
                    };

                    _dbContext.Paintings.InsertOnSubmit(newPainting);
                    _dbContext.SubmitChanges();

                    int idAuction = GetAuctionIdByNumber(auctionNumber);
                    int lotNumber = GetNextLotNumber(idAuction);

                    var newPost = new Post
                    {
                        id_product = productId,
                        id_status = 2,
                        id_auction = idAuction,
                        start_price = startPrice,
                        list_price = listPrice,
                        created_at = creationTime,
                        lot=lotNumber,
                    };

                    _dbContext.Posts.InsertOnSubmit(newPost);
                    _dbContext.SubmitChanges();

                    var newImage = new Product_image
                    {
                        id_product = productId,
                        image_path = imagePath[0]
                    };

                    _dbContext.Product_images.InsertOnSubmit(newImage);
                    _dbContext.SubmitChanges();

                    if (!string.IsNullOrEmpty(imagePath[1]))
                    {
                        var newImage1 = new Product_image
                        {
                            id_product = productId,
                            image_path = imagePath[1]
                        };


                        _dbContext.Product_images.InsertOnSubmit(newImage1);
                        _dbContext.SubmitChanges();
                    }

                    if (!string.IsNullOrEmpty(imagePath[2]))
                    {
                        var newImage2 = new Product_image
                        {
                            id_product = productId,
                            image_path = imagePath[2]
                        };

                        _dbContext.Product_images.InsertOnSubmit(newImage2);
                        _dbContext.SubmitChanges();
                    }

                    transaction.Complete();


                }
            }
            catch (Exception ex)
            {

                Console.WriteLine($"Eroare la adăugarea sculpturii: {ex.Message}");
            }


        }
    
    
        static public decimal GetTotalBidsForAuction(int nr)
        {
            using (var dbContext = new AuctionAppDataContext())
            {
                // Găsește id-ul licitației
                var auction = dbContext.Auctions
                    .FirstOrDefault(a => a.auction_number == nr);

                if (auction == null)
                    return 0; // Licitația nu există
                              // Găsește toate id-urile postărilor asociate licitației
                var postIds = dbContext.Posts
                    .Where(p => p.id_auction == auction.id_auction)
                    .Select(p => p.id_post)
                    .ToList();

                var verificare = dbContext.Bids.Where(b => postIds.Contains(b.id_post));
                if (!verificare.Any())  // Dacă nu există niciun element în verificare
                {
                    return 0;  // Dacă nu există intrări, returnăm 0
                }

                var total = dbContext.Bids
                    .Where(b => postIds.Contains(b.id_post) && b.bid_price != null)  // Verificăm dacă bid_price nu este null
                    .Sum(b => b.bid_price);  // Calculăm suma doar pentru valori valide


                return total;
            }

        }

        static public int GetTotalItemsInAuction(int auctionNumber)
        {
            using (var dbContext = new AuctionAppDataContext())
            {
                // Găsește id-ul licitației
                var auction = dbContext.Auctions
                    .FirstOrDefault(a => a.auction_number == auctionNumber);

                if (auction == null)
                    return 0; // Licitația nu există

                // Numără toate postările asociate licitației
                var totalItems = dbContext.Posts
                    .Count(p => p.id_auction == auction.id_auction);

                return totalItems;
            }
        }

        //static public int GetAuctionNumberById(int id)
        //{
        //    if (_dbContext == null)
        //    {
        //        _dbContext = new AuctionAppDataContext();
        //    }

        //    int auctionNumber = (from a in _dbContext.Auctions
        //                         where a.id_auction == id
        //                         select a
        //                       ).FirstOrDefault().auction_number;
        //    return auctionNumber;
        //}
        static public int GetSoldItemsInAuction(int auctionNumber)
        {
            using (var dbContext = new AuctionAppDataContext())
            {
                int soldItemsCount = 0;


              //  Obținem toate postările asociate licitației
              int auctionId=GetAuctionIdByNumber(auctionNumber);
               var posts = dbContext.Posts
                   .Where(p => p.id_auction == auctionId)  // Filtrăm postările care sunt asociate cu licitația
                   .ToList();


                foreach (var post in posts)
                {
                    string nume = string.Empty;

                    // Apelăm funcția GetPostLastOffer pentru fiecare postare
                    var res = GetPostLastOffer(post.id_post, ref nume);

                    // Dacă GetPostLastOffer returnează un rezultat valid (adică diferit de -1), considerăm postarea vândută
                    if (res != -1)
                    {
                        soldItemsCount++;  // Creștem contorul de postări vândute
                    }
                }

                return soldItemsCount;  // Returnăm numărul de postări vândute
            }
        }

        static public double GetSoldPercentage(int auctionNumber)
        {
            using (var dbContext = new AuctionAppDataContext())
            {
                // Găsește licitația asociată
                var auction = dbContext.Auctions
                    .FirstOrDefault(a => a.auction_number == auctionNumber);

                if (auction == null)
                    return 0; // Licitația nu există, procentul este 0

                // Obține numărul total de articole
                var totalItems = GetTotalItemsInAuction(auctionNumber);

                if (totalItems == 0)
                    return 0; // Dacă nu există articole, procentul este 0

                // Obține numărul articolelor vândute
                var soldItems = GetSoldItemsInAuction(auctionNumber);

                // Calculează procentul
                return (double)soldItems / totalItems * 100;
            }
        }

    }

}
