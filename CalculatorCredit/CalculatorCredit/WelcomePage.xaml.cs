using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CalculatorCredit
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class WelcomePage : ContentPage
    {
        public WelcomePage()
        {
            InitializeComponent();
        }

        private async void SignInClicked(object sender, EventArgs e)
        {
            // Проверка на заполнение полей
            if (string.IsNullOrWhiteSpace(SurnameEntry.Text))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите вашу Фамилию (Username)", "OK");
                return;
            }

            if (string.IsNullOrWhiteSpace(PasswordEntry.Text))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите ваш пароль", "OK");
                return;
            }

            // Переход на CarouselPage с тремя экранами
            var carouselPage = new MainCarouselPage(SurnameEntry.Text);
            await Navigation.PushAsync(carouselPage);
        }
    }
}