using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using ShopContent.Classes;
using ShopContent.Modell;

namespace ShopContent.Context
{
    public class ItemsContext : Items
    {
        public ItemsContext(bool save = false)
        {
            if (save) Save(true);
            Caregory = new Categorys();
        }
        public static ObservableCollection<ItemsContext> AllItems()
        {
            ObservableCollection<ItemsContext> allItems = new ObservableCollection<ItemsContext>();
            ObservableCollection<CategorysContext> allCategorys = CategorysContext.AllCategorys();
            SqlConnection connection;
            SqlDataReader dataItems = Connection.Query("SELECT * FROM [dbo].[Items]", out connection);
            while (dataItems.Read())
            {
                allItems.Add(new ItemsContext()
                {
                    Id = dataItems.GetInt32(0),
                    Name = dataItems.GetString(1),
                    Price = dataItems.GetDouble(2),
                    Description = dataItems.GetString(3),
                    Category = dataItems.IsDBNull(4) ?
                    null :
                    allCategorys.Where(x => x.Id == dataItems.GetInt32(4)).First()
                });
            }
            Connection.CloseConnection(connection);
            return allItems;
        }
        public void Save(bool save = false)
        {
            SqlConnection connection;

            if (New)
            {
                SqlDataReader dataItems = Connection.Query("INSERT INTO " +
                    "[dbo].[Items](" +
                    "Name, " +
                    "Price, " +
                    "Description)" +
                    "OUTPUT Inserted.Id" +
                    $"VALUES(" +
                    $"N '{this.Name}', " +
                    $"{this.Price}, " +
                    $"N '{this.Description}',) ", out connection);
                dataItems.Read();
                this.Id = dataItems.GetInt32(0);
            }
            else
            {
                Connection.Query("UPDATE [dbo].[Items] " +
                    "SET " +
                    $"Name = N'{this.Name}', " +
                     $"Price = {this.Price}, " +
                     $"Description = N'{this.Description}', " +
                     $"IdCategory = {this.Category.Id} " +
                   "WHERE" +
                   $"Id = {this.Id}", out connection);
            }
            Connection.CloseConnection(connection);
            MainWindow.init.frame.Navigate(MainWindow.init.Main);
        }
        public void Delete()
        {
            SqlConnection connection;
            Connection.Query("DELET FROM [dbo].[Items" +
                "WHERE" +
                $"Id = {this.Id}", out connection);
            Connection.CloseConnection(connection);
        }
        public RelayCommand Onsave
        {
            get
            {
                return new RelayCommand(object =>
                {
                    Category = CategorysContext.AllCategorys().where(x => x.Id == this.Category.Id).First();

                    Save();
                });
            }
        }
        public RelayCommand OnDelete
        {
            get
            {
                return new RelayCommand(object =>
                {
                    Delete();
                    (MainWindow.init.Main.DataContext as ViewModell.VMItems).Items.Remove(this);
                });
            }
        }
    }
}
