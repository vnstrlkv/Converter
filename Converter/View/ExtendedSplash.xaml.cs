using Converter.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

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

            LoadViewModel();  
            // Set extended splash info on main page
            // Place the frame in the current Window
           
        }

        async void LoadViewModel()
        {
           Task<ExchangeRateVM> task = ExchangeRateVM.Create();
           ViewModel = await task;
           rootFrame.Navigate(typeof(MainPage), ViewModel);
           Window.Current.Content = rootFrame;
        }
    }
}
