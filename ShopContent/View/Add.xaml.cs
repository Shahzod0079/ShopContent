using System.Windows.Controls;

namespace ShopContent.View
{
    public partial class Add : Page
    {
        public Add(object Context)
        {
            InitializeComponent();

            DataContext = new
            {
                item = Context,
                categories = new ViewModell.VMCategorys()
            };
        }
    }
}