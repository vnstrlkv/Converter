using Converter.ViewModel;
using System;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;


// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Converter.View
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ExtendedSplash : Page
    {
        internal Frame rootFrame;
        internal ExchangeRateVM ViewModel;
        public ExtendedSplash(SplashScreen splashScreen, bool loadState)
        {
            this.InitializeComponent();            
            rootFrame = new Frame();

            LoadViewModel();  //пока загружается курс валют показываем загрузочный экран
                     
        }

        async void LoadViewModel()
        {
           Task<ExchangeRateVM> task = ExchangeRateVM.Create(); 
           ViewModel = await task;
           rootFrame.Navigate(typeof(MainPage), ViewModel); // как только загрузка прошла переходим на MainPage
           Window.Current.Content = rootFrame;
        }
    }
}
