using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Navigation;
using Converter.ViewModel;
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
