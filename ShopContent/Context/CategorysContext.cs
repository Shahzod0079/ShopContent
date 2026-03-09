using ShopContent.Modell;
using ShopContent_Oshchepkov.Classes;
using System.Collections.ObjectModel;
using System.Data.SqlClient;

namespace ShopContent.Context
{
    public class CategorysContext : Categorys
    {
        /// <summary> Метод загрузки данных из БД </summary>
        public static ObservableCollection<CategorysContext> AllCategories()
        {
            ObservableCollection<CategorysContext> allCategories = new ObservableCollection<CategorysContext>();
            SqlConnection connection;
            SqlDataReader dataCategories = Connection.Query("SELECT * FROM [dbo].[Categories]", out connection);

            while (dataCategories.Read())
            {
                allCategories.Add(new CategorysContext()
                {
                    Id = dataCategories.GetInt32(0),
                    Name = dataCategories.GetString(1)
                });
            }

            Connection.CloseConnection(connection);
            return allCategories;
        }
    }
}