using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Converter.Model;
using Converter.Helpers;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

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
        private Valute selectedValute;
        private string leftOrRight;
        public ICommand LeftClickCommand;
        public ICommand RightClickCommand;

        public ExchangeRateVM()
        {
            allValutes = new CBRDaily();
            allValutes = allValutes.GetValutes();
            flag = true;
            leftOrRight = "";

            LeftClickCommand= new RelayCommand(() =>
            {
               
                leftOrRight = "L";
            });
            RightClickCommand = new RelayCommand(() =>
            {
                
                    leftOrRight = "R";
            });

            leftExchange = new Exchange { Amount = 1, Name = Valutes["RUB"].Name, Rate = Valutes["RUB"].Value, Nominal = Valutes["RUB"].Nominal };
            rightExchange = new Exchange { Amount = 1, Name = Valutes["USD"].Name, Rate = Valutes["USD"].Value, Nominal = Valutes["USD"].Nominal };


        }



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
                return Math.Round(leftMoney);
                }

            set {
              
                    LeftExchange.Amount = value;
                    leftMoney = value;
                    if (flag)
                    {
                        flag = false;
                        RightMoney = (LeftExchange.Amount) * (LeftExchange.Rate / LeftExchange.Nominal) / (RightExchange.Rate / RightExchange.Nominal);
                        flag = true;
                    }
                    OnPropertyChanged("LeftMoney");
                
            }
        }

        public double RightMoney
        {
            get
            {             
                return Math.Round(rightMoney,3);
            }
            set
            {
              
                   
                    RightExchange.Amount = value;
                    rightMoney = value;
                if (flag)
                {
                    flag = false;                   
                        
                    LeftMoney = (RightExchange.Amount) *  (RightExchange.Rate / RightExchange.Nominal)/ (LeftExchange.Rate / LeftExchange.Nominal);  
                    
                    flag = true;
                }
                OnPropertyChanged("RightMoney");
            }
        }

        public Valute SelectedValute
        {
            get
            {
                return selectedValute;
            }
            set
            {
                selectedValute = value;  
                if(LeftOrRight=="L")
                {
                    LeftExchange.Name = selectedValute.Name;
                    LeftExchange.Nominal = SelectedValute.Nominal;
                    LeftExchange.Rate = SelectedValute.Value;
                    LeftMoney=LeftMoney;
                }
                else if(LeftOrRight=="R")
                {
                    RightExchange.Name = selectedValute.Name;
                    RightExchange.Nominal = SelectedValute.Nominal;
                    RightExchange.Rate = SelectedValute.Value;
                    RightMoney = RightMoney;
                    
                }
                selectedValute = null;
            }
        }
        
        public Dictionary<string, Valute> Valutes
            {
                get { return allValutes.Valute; }            
            }

        public string LeftOrRight
        {
            get { return leftOrRight; }
        }
        

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
