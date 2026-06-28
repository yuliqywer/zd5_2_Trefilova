using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalculatorCredit
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            // Запускаем с WelcomePage (с навигацией)
            MainPage = new NavigationPage(new WelcomePage());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
