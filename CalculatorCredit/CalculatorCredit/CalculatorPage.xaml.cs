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
    public partial class CalculatorPage : ContentPage
    {
        public CalculatorPage(string surname)
        {
            InitializeComponent();
            // устанавливаем заголовок страницы с фамилией пользователя
            Title = $"Калькулятор: {surname}";
            // задаем максимальное и начальное значение ползунка
            RateSlider.Maximum = 30;
            RateSlider.Value = 20;
            // устанавливаем минимальное значение ползунка
            RateSlider.Minimum = 1;
            // выбираем первый тип платежа по умолчанию
            PaymentTypePicker.SelectedIndex = 0;
        }

        // обработчик изменения значения ползунка процентной ставки
        private void SliderValueChanged(object sender, ValueChangedEventArgs e)
        {
            RateLabel.Text = $"{Math.Round(e.NewValue)}%";
            CalculateLoan(null, null);
        }

        // основной метод расчета кредита
        private void CalculateLoan(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(AmountEntry.Text) || string.IsNullOrEmpty(PeriodEntry.Text))
            {
                ClearResults();
                return;
            }

            if (!double.TryParse(AmountEntry.Text, out double creditAmount) ||
                !int.TryParse(PeriodEntry.Text, out int monthsCount))
            {
                return;
            }

            double annualRate = Math.Round(RateSlider.Value);
            double monthlyRate = annualRate / 12 / 100;

            if (PaymentTypePicker.SelectedIndex == 0)
            {
                MonthlyPaymentLabel.IsVisible = true;
                double monthlyPayment = creditAmount * (monthlyRate * Math.Pow(1 + monthlyRate, monthsCount)) /
                                        (Math.Pow(1 + monthlyRate, monthsCount) - 1);

                if (double.IsNaN(monthlyPayment) || double.IsInfinity(monthlyPayment)) return;

                double totalSum = monthlyPayment * monthsCount;
                double overpayment = totalSum - creditAmount;

                MonthlyPaymentLabel.Text = $"Ежемесячный платеж: {monthlyPayment:F2}";
                TotalSumLabel.Text = $"Общая сумма: {totalSum:F2}";
                OverpaymentLabel.Text = $"Переплата: {overpayment:F2}";
            }
            else
            {
                MonthlyPaymentLabel.IsVisible = false;
                double totalSum = 0;
                double remainingCredit = creditAmount;
                double mainDebtPayment = creditAmount / monthsCount;

                for (int i = 0; i < monthsCount; i++)
                {
                    double percentPayment = remainingCredit * monthlyRate;
                    double currentPayment = mainDebtPayment + percentPayment;
                    totalSum += currentPayment;
                    remainingCredit -= mainDebtPayment;
                }

                double overpayment = totalSum - creditAmount;
                TotalSumLabel.Text = $"Общая сумма: {totalSum:F2}";
                OverpaymentLabel.Text = $"Переплата: {overpayment:F2}";
            }
        }

        // очистка полей с результатами
        private void ClearResults()
        {
            MonthlyPaymentLabel.Text = "Ежемесячный платеж: ";
            TotalSumLabel.Text = "Общая сумма: ";
            OverpaymentLabel.Text = "Переплата: ";
        }

        // переход на 3 экран (RatesPage) с передачей максимального значения слайдера
        private async void OnGoToRatesClicked(object sender, EventArgs e)
        {
            double maxRateValue = RateSlider.Maximum; // максимальное значение = 30
            await Navigation.PushAsync(new RatesPage(maxRateValue));
        }
    }
}