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
    public partial class RatesPage : ContentPage
    {
        private double _maxSliderValue; // поле для хранения максимального значения слайдера

        // конструктор принимает максимальное значение слайдера с CalculatorPage
        public RatesPage(double maxSliderValue)
        {
            InitializeComponent();
            _maxSliderValue = maxSliderValue;
        }

        // обработчик нажатия кнопки Static
        private async void OnStaticClicked(object sender, EventArgs e)
        {
            // показать максимальное значение из слайдера
            await DisplayAlert("Static", $"Максимальное значение слайдера: {_maxSliderValue}%", "OK");
        }
    }
}