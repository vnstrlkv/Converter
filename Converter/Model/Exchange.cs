using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Converter.Model
{
    public class Exchange : INotifyPropertyChanged
    {
        private string name;
        private double rate;
        private long nominal;
        private double amount;

        public string Name 
            {
            get { return name; }
            set {
                name = value;
                OnPropertyChanged("Name");
                }
            }

        public double Rate
        {
            get { return rate; }
            set
            {
                rate = value;
                OnPropertyChanged("Rate");
            }
        }

        public long Nominal
        {
            get { return nominal; }
            set
            {
                nominal = value;
                OnPropertyChanged("Nominal");
            }
        }

        public double Amount
        {
            get { return amount; }
            set
            {
                amount = value;
                OnPropertyChanged("Amount");
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

    }
}
