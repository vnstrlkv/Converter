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

// Документацию по шаблону элемента "Пустая страница" см. по адресу https://go.microsoft.com/fwlink/?LinkId=234238

namespace Converter.View
{
    /// <summary>
    /// Пустая страница, которую можно использовать саму по себе или для перехода внутри фрейма.
    /// </summary>
    public sealed partial class ExchangePage : Page
    {
        public ExchangeRateVM ViewModel;
        public ExchangePage()
        {
            this.InitializeComponent();
        }



        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            if (e.Parameter != null)
            {
                ViewModel =(ExchangeRateVM) e.Parameter;
            }
        }

        private void ListView_ItemClick(object sender, ItemClickEventArgs e)
        {          
            this.Frame.Navigate(typeof(MainPage), ViewModel);
        }
    }
}
