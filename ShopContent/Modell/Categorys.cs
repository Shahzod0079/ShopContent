using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace ShopContent.Modell
{
    public class Categorys : INotifyPropertyChanged
    {
        private int id;

        public int Id
        {
            get { return id; }

            set
            {
                id = value;
                OnPropertyChanged("Id");
            }
        }
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                OnPropertyChanged("Name");
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
