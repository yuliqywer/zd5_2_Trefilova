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
    public partial class MainCarouselPage : CarouselPage
    {
        private string userName;
        private double sliderValue = 50;

        public MainCarouselPage(string username)
        {
            InitializeComponent();

            userName = username;

            ShowWelcomeMessage();
        }

        private void ShowWelcomeMessage()
        {
            if (Children.Count > 0)
            {
                var mainPage = (ContentPage)Children[0];
                var welcomeLabel = mainPage.FindByName<Label>("WelcomeUserLabel");

                if (welcomeLabel != null)
                {
                    welcomeLabel.Text = $"Добро пожаловать, {userName}!";
                }
            }
        }

        private double GetMaxSliderValue()
        {
            double maxValue = 0;

            if (Children.Count > 0)
            {
                var mainPage = (ContentPage)Children[0];

                var slider1 = mainPage.FindByName<Slider>("Slider1");
                var slider2 = mainPage.FindByName<Slider>("Slider2");
                var slider3 = mainPage.FindByName<Slider>("Slider3");
                var slider4 = mainPage.FindByName<Slider>("Slider4");

                if (slider1 != null && slider1.Value > maxValue)
                    maxValue = slider1.Value;
                if (slider2 != null && slider2.Value > maxValue)
                    maxValue = slider2.Value;
                if (slider3 != null && slider3.Value > maxValue)
                    maxValue = slider3.Value;
                if (slider4 != null && slider4.Value > maxValue)
                    maxValue = slider4.Value;
            }

            return maxValue;
        }

        private void GoToInfoClicked(object sender, EventArgs e)
        {
            UpdateInfoPage();
            CurrentPage = Children[1];
        }

        private void UpdateInfoPage()
        {
            if (Children.Count < 2) return;

            var infoPage = (ContentPage)Children[1];

            double maxSliderValue = GetMaxSliderValue();
            sliderValue = maxSliderValue;

            var userLabel = infoPage.FindByName<Label>("InfoUserLabel");
            var sliderLabel = infoPage.FindByName<Label>("InfoSliderValueLabel");
            var descriptionLabel = infoPage.FindByName<Label>("InfoDescriptionLabel");
            var staticButton = infoPage.FindByName<Button>("InfoStaticButton");

            if (userLabel != null)
                userLabel.Text = $"Пользователь: {userName}";

            if (sliderLabel != null)
                sliderLabel.Text = $"Максимальное значение: {maxSliderValue:F0}";

            if (descriptionLabel != null)
            {
                if (maxSliderValue < 30)
                    descriptionLabel.Text = "Низкий уровень - рекомендуется увеличить значение";
                else if (maxSliderValue < 70)
                    descriptionLabel.Text = "Средний уровень - оптимальное значение";
                else
                    descriptionLabel.Text = "Высокий уровень - отличный результат!";
            }

            if (staticButton != null)
            {
                staticButton.Text = $"STATIC {maxSliderValue:F0}%";
                staticButton.BackgroundColor = Color.FromHex("#4A90D9");
                staticButton.TextColor = Color.White;
                staticButton.CornerRadius = 8;
                staticButton.FontAttributes = FontAttributes.Bold;
            }
        }
    }
}