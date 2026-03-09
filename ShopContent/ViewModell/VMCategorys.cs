using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShopContent.ViewModell
{
    public class VMCategorys : INotifyPropertyChanged
    {
        private ObservableCollection<Context.CategorysContext> _categories;
        public ObservableCollection<Context.CategorysContext> Categories
        {
            get { return _categories; }
            set
            {
                _categories = value;
                OnPropertyChanged("Categories");
            }
        }

        public VMCategorys()
        {
            Categories = Context.CategorysContext.AllCategories();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}