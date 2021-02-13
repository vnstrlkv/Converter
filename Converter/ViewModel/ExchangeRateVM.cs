using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converter.Model;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Converter.ViewModel
{
    public class ExchangeRateVM : INotifyPropertyChanged
    {
        private CBRDaily allValutes;
        private Exchange leftExchange;
        private Exchange rightExchange;
        private double leftMoney;
        private double rightMoney;
        private bool flag;

        public Exchange LeftExchange
        {
            get
            {
                return leftExchange;
            }
            set
            {
                leftExchange = value;
                OnPropertyChanged("LeftExchange");
            }
        }

        public Exchange RightExchange
        {
            get
            {
                return rightExchange;
            }
            set
            {
                rightExchange = value;
                OnPropertyChanged("RightExchange");
            }
        }

        public double LeftMoney
        {
            get {             
                return leftMoney;
                }

            set { 
                LeftExchange.Amount = value;
                leftMoney = value;
                if (rightExchange.Amount != 0)
                {
                    // leftMoney = LeftExchange.Amount;
                    RightMoney = (LeftExchange.Amount) * (LeftExchange.Rate * LeftExchange.Nominal) / (RightExchange.Rate * RightExchange.Nominal);
                }
                else LeftMoney = 0;
                OnPropertyChanged("LeftMoney");
                }
        }

        public double RightMoney
        {
            get
            {             
                return rightMoney;
            }
            set {
                RightExchange.Amount = value;
                rightMoney = value;
                if (leftExchange.Amount != 0)
                {
                   // LeftMoney = (LeftExchange.Amount) * (LeftExchange.Rate * LeftExchange.Nominal) / (RightExchange.Rate * RightExchange.Nominal);
                }
                else RightMoney = 0;               
                OnPropertyChanged("RightMoney"); 
                }
        }

        public ExchangeRateVM()
        {
            allValutes = new CBRDaily();
            allValutes = allValutes.GetValutes();
            
                        
            leftExchange = new Exchange { Amount = 1, Name = Valutes["RUB"].Name, Rate = Valutes["RUB"].Value, Nominal = Valutes["RUB"].Nominal };
            rightExchange = new Exchange { Amount = 1, Name = Valutes["USD"].Name, Rate = Valutes["USD"].Value, Nominal = Valutes["USD"].Nominal };
        }

        public Dictionary<string, Valute> Valutes
            {
                get { return allValutes.Valute; }            
            }



        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
