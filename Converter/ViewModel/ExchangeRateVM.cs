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
        private CBRDaily allValutes; //тут хранятся все курсы валют
        private Valute leftValute; // левая валюта
        private Valute rightValute; // правая валюта
        private double leftMoney; // 
        private double rightMoney;
        private bool flag; // используется для исключения зацикливания и ошибки переполнения при подсчете курса
        private Valute selectedValute; // выбранная валюта
        private string leftOrRight; // указаталь не то, с какой стороны менять тип валюта
        public ICommand LeftClickCommand; // команда для смены левой валюты
        public ICommand RightClickCommand;// команда для сменя правой валюты
        public static async Task<ExchangeRateVM> Create() // Чтобы получить не блокировался интерфейс при получении курса валют, делаем асинхронный конструктор
        {
            var myClass = new ExchangeRateVM();
            await myClass.Initialize();
            return myClass;
        }
        private ExchangeRateVM()
        {
           
        }
        private async Task Initialize() // задаем первоначальные параметры
        {
            await Task.Delay(1000); //Ожидание, чтобы покрутился экран заставки
            allValutes = new CBRDaily();
            allValutes = await allValutes.GetValutes(); // загружаем курс валют
            flag = true; 
            leftOrRight = "";

            LeftClickCommand = new RelayCommand(() => // если происходит выбор смены левой валюты, то "указатель" на валюту меняем соответвественно
            {
                leftOrRight = "L";
            });
            RightClickCommand = new RelayCommand(() =>
            {

                leftOrRight = "R";
            });

            // первоначальное отображение
            leftValute = allValutes.Valute["RUB"]; 
            rightValute = allValutes.Valute["USD"];

        }


        public Valute LeftValute
        {
            get
            {
                return leftValute;
            }
            set
            {
                leftValute = value;
                OnPropertyChanged("LeftValute");
            }
        }

        public Valute RightValute
        {
            get
            {
                return rightValute;
            }
            set
            {
                rightValute = value;
                OnPropertyChanged("RightValute");
            }
        }

        public double LeftMoney
        {
            get {             
                return Math.Round(leftMoney,3);
                }

            set {              
                   // подсчет денег с левой стороны обмена валют
                    leftMoney = value; 
                    if (flag)
                    {
                        flag = false;
                        RightMoney = (LeftMoney) * (LeftValute.Value / LeftValute.Nominal) / (RightValute.Value / RightValute.Nominal);
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
                // подсчет денег с правой стороны обмена валют                      
                rightMoney = value;
                if (flag)
                {
                    flag = false;                   
                        
                    LeftMoney = (RightMoney) *  (RightValute.Value / RightValute.Nominal)/ (LeftValute.Value / LeftValute.Nominal);  
                    
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
                //логика выбора валюты
                selectedValute = value;  
                if(LeftOrRight=="L") 
                {
                    LeftValute = value;
                    LeftMoney=LeftMoney;
                }
                else if(LeftOrRight=="R")
                {
                    RightValute = value;
                    RightMoney = RightMoney;
                    
                }
                selectedValute = null; // обнуляем выбранную валюту, чтобы не было привязки при повторном открытии
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

        // реализация интерфейса INotifyPropertyChanged 
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
