using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace WPFShop
{
    /// <summary>
    /// Логика взаимодействия для Drivers_Window.xaml
    /// </summary>
    public partial class Customers_Window : Window
    {

        XElement drivers = XElement.Load("../../../xml-files/drivers.xml");
        public Customers_Window()
        {
            InitializeComponent();

            /// Беру из каждого Driver следующие данные.
            var result = drivers.Descendants("Driver").Select(x => new
            {
                Код_Водителя = x.Element("Id").Value,
                Фамилия = x.Element("Name").Value,
                Имя = x.Element("FirstName").Value,
                Отчество = x.Element("LastName").Value,
                Стаж = x.Element("TimeWork").Value
            });

            /// Заполняю табличку данными. Колонки будут названы названием переменной, так как в xaml я поставила AutoGenerateColumns="True"
            DTUsers.ItemsSource = result;
        }

        /// Функция обновления данных
        private void Button_Reload(object sender, RoutedEventArgs e)
        {
            /// Беру из каждого Driver следующие данные.
            var result = drivers.Descendants("Driver").Select(x => new
            {
                Код_Водителя = x.Element("Id").Value,
                Фамилия = x.Element("Name").Value,
                Имя = x.Element("FirstName").Value,
                Отчество = x.Element("LastName").Value,
                Стаж = x.Element("TimeWork").Value
            });

            /// Заполняю табличку данными. Колонки будут названы названием переменной, так как в xaml я поставила AutoGenerateColumns="True"
            DTUsers.ItemsSource = result;
        }

        /// Функция добавления данных
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            bool wrongId = false;

            foreach (var driver in drivers.Elements("Driver"))
            {
                if (driver.Element("Id").Value == Id.Text)
                {
                    wrongId = true;
                    MessageBox.Show("Водитель с таким id уже есть!");
                    break;
                }
            }

            if (Id.Text != null & Name.Text != null & FirstName.Text != null & LastName.Text != null & TimeWork.Text != null & !wrongId)
            {
                var newDriver = new XElement("Driver",
                    new XElement("Id", Id.Text),
                    new XElement("Name", Name.Text),
                    new XElement("FirstName", FirstName.Text),
                    new XElement("LastName", LastName.Text),
                    new XElement("TimeWork", TimeWork.Text)
                );

                drivers.Add(newDriver);
                drivers.Save("../../../xml-files/drivers.xml");

                MessageBox.Show("Успешно!");
                Id.Text = "Код Водителя";
                Name.Text = "Фамилия";
                FirstName.Text = "Имя";
                LastName.Text = "Отчество";
                TimeWork.Text = "Стаж";
            } else
            {
                MessageBox.Show("Ошибка!");
            };
            
        }

        private void Button_Rename(object sender, RoutedEventArgs e)
        {

            IEnumerable<XElement> dr = from driver in drivers.Elements("Driver")
                                       where driver.Element("Id").Value == Id.Text
                                       select driver;

            dr.First().Element("Name").Value = Name.Text;
            dr.First().Element("FirstName").Value = FirstName.Text;
            dr.First().Element("LastName").Value = LastName.Text;
            dr.First().Element("TimeWork").Value = TimeWork.Text;

            drivers.Save("../../../xml-files/drivers.xml");

            MessageBox.Show("Успешно!");
            Id.Text = "Код Водителя";
            Name.Text = "Фамилия";
            FirstName.Text = "Имя";
            LastName.Text = "Отчество";
            TimeWork.Text = "Стаж";
        }
    }
}
