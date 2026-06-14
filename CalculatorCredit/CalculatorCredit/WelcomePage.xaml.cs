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
            // проверка на заполнение полей
            if (string.IsNullOrWhiteSpace(SurnameEntry.Text))
            {
                await DisplayAlert("Ошибка", "Пожалуйста, введите вашу Фамилию (Username)", "ОК");
                return;
            }

            // переход на второй экран с передачей фамилии пользователя
            string userSurname = SurnameEntry.Text.Trim();
            await Navigation.PushAsync(new CalculatorPage(userSurname));
        }
    }
}