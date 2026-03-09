using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Runtime.Remoting.Contexts;

namespace ShopContent_Ohschepkov.ViewModel
{
    public class VMCategorys : INotifyPropertyChanged
    {
        public ObservableCollection<Context.CategorysContext> Categorys { get; set; }

        /// <summary> Конструктор модели представления </summary>
        public VMCategorys()
        {
            Categorys = Context.CategorysContext.AllCategorys();
        }

        /// <summary> Событие изменения свойства </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary> Метод сообщающий системе об изменении свойств </summary>
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}