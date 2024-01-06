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

namespace CargoAppWpf
{
    /// <summary>
    /// Логика взаимодействия для Ways_Window.xaml
    /// </summary>
    public partial class Ways_Window : Window
    {

        XElement ways = XElement.Load("../../../xml-files/ways.xml");
        public Ways_Window()
        {
            InitializeComponent();

            /// Беру из каждого Driver следующие данные.
            var result = ways.Descendants("Way").Select(x => new
            {
                Код_Маршрута = x.Element("Id").Value,
                Название = x.Element("Name").Value,
                Дальность = x.Element("Distance").Value,
                Дней_в_пути = x.Element("Days").Value,
                Оплата = x.Element("Salary").Value
            });

            /// Заполняю табличку данными. Колонки будут названы названием переменной, так как в xaml я поставила AutoGenerateColumns="True"
            DTUsers.ItemsSource = result;
        }

        /// Функция обновления данных
        private void Button_Reload(object sender, RoutedEventArgs e)
        {
            /// Беру из каждого Driver следующие данные.
            var result = ways.Descendants("Way").Select(x => new
            {
                Код_Маршрута = x.Element("Id").Value,
                Название = x.Element("Name").Value,
                Дальность = x.Element("Distance").Value,
                Дней_в_пути = x.Element("Days").Value,
                Оплата = x.Element("Salary").Value
            });

            /// Заполняю табличку данными. Колонки будут названы названием переменной, так как в xaml я поставила AutoGenerateColumns="True"
            DTUsers.ItemsSource = result;
        }

        /// Функция добавления данных
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            bool wrongId = false;

            foreach (var driver in ways.Elements("Way"))
            {
                if (driver.Element("Id").Value == Id.Text)
                {
                    wrongId = true;
                    MessageBox.Show("Маршрут с таким id уже есть!");
                    break;
                }
            }

            if (Id.Text != null & Name.Text != null & Distance.Text != null & Days.Text != null & Salary.Text != null & !wrongId)
            {
                var newWay = new XElement("Way",
                    new XElement("Id", Id.Text),
                    new XElement("Name", Name.Text),
                    new XElement("Distance", Distance.Text),
                    new XElement("Days", Days.Text),
                    new XElement("Salary", Salary.Text)
                );

                ways.Add(newWay);
                ways.Save("../../../xml-files/ways.xml");

                MessageBox.Show("Успешно!");
                Id.Text = "Код Маршрута";
                Name.Text = "Название маршрута";
                Distance.Text = "Дальность, км";
                Days.Text = "Дней в пути";
                Salary.Text = "Оплата, руб";
            }
            else
            {
                MessageBox.Show("Ошибка!");
            };

        }

        private void Button_Rename(object sender, RoutedEventArgs e)
        {

            IEnumerable<XElement> w = from way in ways.Elements("Way")
                                       where way.Element("Id").Value == Id.Text
                                       select way;

            w.First().Element("Name").Value = Name.Text;
            w.First().Element("Distance").Value = Distance.Text;
            w.First().Element("Days").Value = Days.Text;
            w.First().Element("Salary").Value = Salary.Text;

            ways.Save("../../../xml-files/ways.xml");

            MessageBox.Show("Успешно!");
            Id.Text = "Код Маршрута";
            Name.Text = "Название маршрута";
            Distance.Text = "Дальность, км";
            Days.Text = "Дней в пути";
            Salary.Text = "Оплата, руб";
        }
    }
}

