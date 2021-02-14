using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using Converter.ViewModel;
using Converter.View;

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x419

namespace Converter.View
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public ExchangeRateVM ViewModel;
        public MainPage()
        {
            this.InitializeComponent();
            this.ViewModel = new ExchangeRateVM();

        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                if(e.Parameter is ExchangeRateVM)
                ViewModel = (ExchangeRateVM)e.Parameter;
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
           this.Frame.Navigate(typeof(ExchangePage), ViewModel);           
        }


    }
}
