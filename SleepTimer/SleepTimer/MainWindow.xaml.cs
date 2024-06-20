using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
namespace SleepTimer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            CountingLabel.Visibility = Visibility.Hidden;
            SetTimer.Visibility = Visibility.Hidden;
            ApplySet.Visibility = Visibility.Hidden;
            GuideButton.Visibility = Visibility.Hidden;
            FormatType.Visibility = Visibility.Hidden;
            StartButton.IsEnabled = false;
        }
        private int increment = 0;
        private int kd = 0;
        private DispatcherTimer timer;
        private void StartButton_Click(object sender, RoutedEventArgs e)
        {
            timer = new DispatcherTimer();
            timer.Tick += new EventHandler(timerTick);
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.Start();
            StartButton.IsEnabled = false;
            ThirtyMinutes.IsEnabled = false;
            OneHour.IsEnabled = false;
            TwoHours.IsEnabled = false;
            CustomValue.IsEnabled = false;
        }
        #region GPT
        private async void ShowNotificationAsync()
        {
            if (Math.Abs(Convert.ToInt32(HoursLabel.Content) - Convert.ToInt32(HoursLabelRestrict.Content)) == 0 && Math.Abs(Convert.ToInt32(MinutesLabel.Content) - Convert.ToInt32(MinutesLabelRestrict.Content)) == 5 && kd == 0)
            {
                kd++;
                await Task.Delay(5); // Подождать 
                MessageBox.Show("До окончания осталось 5 минут!\nНачинайте заканчивать все дела в игре!", "Уведомление", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
            }
        }
        protected override void OnClosing(System.ComponentModel.CancelEventArgs e)
        {
            var result = MessageBox.Show("Вы действительно хотите закрыть приложение?", "Подтверждение", MessageBoxButton.YesNo, MessageBoxImage.Question);

            if (result == MessageBoxResult.No)
            {
                e.Cancel = true; // Отмена закрытия приложения
            }
            else if (result == MessageBoxResult.Yes)
            {
                Autorization aut = new Autorization();
                aut.ShowDialog();
            }
            else
            {
                // Здесь можно добавить код для сохранения данных или других действий перед закрытием
            }

            base.OnClosing(e);
        }
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool LockWorkStation();

        private void LockComputer()
        {
            LockWorkStation();
        }
        #endregion
        private void timerTick(object sender, EventArgs e)
        {
            ShowNotificationAsync();
            if ((Convert.ToInt32(HoursLabel.Content) == Convert.ToInt32(HoursLabelRestrict.Content)) && (Convert.ToInt32(MinutesLabel.Content) == Convert.ToInt32(MinutesLabelRestrict.Content)) && (Convert.ToInt32(SecondsLabel.Content) == Convert.ToInt32(SecondsLabelRestrict.Content)))
            {
                MessageBox.Show("Время игры закончилось! Нажми ОК", "GG", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK, MessageBoxOptions.ServiceNotification);
                timer.Stop(); // Остановка таймера
                HoursLabel.Content = "00";
                MinutesLabel.Content = "00";
                SecondsLabel.Content = "00";
                StartButton.IsEnabled = true; // Возможно, вы захотите снова включить кнопку старта
                ThirtyMinutes.IsEnabled = true;
                OneHour.IsEnabled = true;
                TwoHours.IsEnabled = true;
                CustomValue.IsEnabled = true;
                increment = 0; // Сброс счетчика
                kd = 0; // Сброс счетчика
                LockComputer();
                return; // Выход из метода, чтобы не продолжать увеличение времени
            }

            increment++;
            
            if (increment <= 9)
            {
                SecondsLabel.Content = $"0{increment}";
            }
            else
                SecondsLabel.Content = increment.ToString();
            string sec = (string)SecondsLabel.Content;
            string min = (string)MinutesLabel.Content;
            //string hour = (string)HoursLabel.Content;
            int boofer = 0;
            if (sec == "59")
            {
                increment = 0;
                boofer = Convert.ToInt32(MinutesLabel.Content);
                boofer += 1;
                if (boofer <= 9)
                {
                    MinutesLabel.Content = $"0{boofer}";
                }
                else
                    MinutesLabel.Content = boofer.ToString();
                if (min == "59")
                {
                    increment = 0;
                    MinutesLabel.Content = "00";
                    boofer = Convert.ToInt32(HoursLabel.Content);
                    boofer += 1;
                    if (boofer <= 9)
                    {
                        HoursLabel.Content = $"0{boofer}";
                    }
                    else
                        HoursLabel.Content = boofer.ToString();
                }
            }
        }

        private void Additionals_Click(object sender, RoutedEventArgs e)
        {
            MultiplyTable m = new MultiplyTable();
            m.Show();
        }

        private void ApplySet_Click(object sender, RoutedEventArgs e)
        {
            HoursLabelRestrict.Content = HourSet.Text;
            MinutesLabelRestrict.Content = MinSet.Text;
            SecondsLabelRestrict.Content = SecSet.Text;

            CustomValue.IsEnabled = true;
            CountingLabel.Visibility = Visibility.Hidden;
            SetTimer.Visibility = Visibility.Hidden;
            ApplySet.Visibility = Visibility.Hidden;
            GuideButton.Visibility = Visibility.Hidden;
            FormatType.Visibility = Visibility.Hidden;

            StartButton.IsEnabled = true;
        }
        #region Solutions
        private void ThirtyMinutes_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = true;
            CustomValue.IsEnabled = true;
            CountingLabel.Visibility = Visibility.Hidden;
            SetTimer.Visibility = Visibility.Hidden;
            ApplySet.Visibility = Visibility.Hidden;
            GuideButton.Visibility = Visibility.Hidden;
            FormatType.Visibility = Visibility.Hidden;

            ThirtyMinutes.IsEnabled = false;
            OneHour.IsEnabled = true;
            TwoHours.IsEnabled = true;

            HoursLabelRestrict.Content = "00";
            MinutesLabelRestrict.Content = "36";
            SecondsLabelRestrict.Content = "59";
        }

        private void OneHour_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = true;
            CustomValue.IsEnabled = true;
            CountingLabel.Visibility = Visibility.Hidden;
            SetTimer.Visibility = Visibility.Hidden;
            ApplySet.Visibility = Visibility.Hidden;
            GuideButton.Visibility = Visibility.Hidden;
            FormatType.Visibility = Visibility.Hidden;

            ThirtyMinutes.IsEnabled = true;
            OneHour.IsEnabled = false;
            TwoHours.IsEnabled = true;

            HoursLabelRestrict.Content = "01";
            MinutesLabelRestrict.Content = "06";
            SecondsLabelRestrict.Content = "59";
        }

        private void TwoHours_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = true;
            CustomValue.IsEnabled = true;
            CountingLabel.Visibility = Visibility.Hidden;
            SetTimer.Visibility = Visibility.Hidden;
            ApplySet.Visibility = Visibility.Hidden;
            GuideButton.Visibility = Visibility.Hidden;
            FormatType.Visibility = Visibility.Hidden;

            ThirtyMinutes.IsEnabled = true;
            OneHour.IsEnabled = true;
            TwoHours.IsEnabled = false;

            HoursLabelRestrict.Content = "02";
            MinutesLabelRestrict.Content = "06";
            SecondsLabelRestrict.Content = "59";
        }

        private void CustomValue_Click(object sender, RoutedEventArgs e)
        {
            StartButton.IsEnabled = false;

            HoursLabelRestrict.Content = "00";
            MinutesLabelRestrict.Content = "00";
            SecondsLabelRestrict.Content = "00";

            ThirtyMinutes.IsEnabled = true;
            OneHour.IsEnabled = true;
            TwoHours.IsEnabled = true;
            CustomValue.IsEnabled = false;
            CountingLabel.Visibility = Visibility.Visible;
            SetTimer.Visibility = Visibility.Visible;
            ApplySet.Visibility = Visibility.Visible;
            GuideButton.Visibility = Visibility.Visible;
            FormatType.Visibility = Visibility.Visible;
        }
        #endregion

        private void GuideButton_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Вводить свои значения нужно в формате часы/минуты/секунды\nК примеру, нужно ввести 1 час, тогда это должно выглядеть так:\n01:06:59, т.к. нужно оставить промежуток времени в 5 минут для активации уведомления о скором ококнчании оставшегося времени для игры\n------------------------------------------\nТаким же способом вводятся и остальные значения:\nПолчаса: 00:36:59\n2 часа: 02:06:59\n5 часов: 05:06:59\nИ так далее", "Гайд на ввод значений", MessageBoxButton.OK, MessageBoxImage.Information, MessageBoxResult.OK);
        }
    }
}
