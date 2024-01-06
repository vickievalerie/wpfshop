using System;
using System.Collections.Generic;
using System.Diagnostics;
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
    /// Логика взаимодействия для Ways_Window.xaml
    /// </summary>
    public partial class Goods_Window : Window
    {

        XElement goods = XElement.Load("../../../xml-files/goods.xml");
        public Goods_Window()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;

            var result = goods.Descendants("Good").Select(x => new
            {
                Код_Товара = x.Element("Id").Value,
                Название = x.Element("Name").Value,
                Количество = x.Element("Amount").Value,
                Ед_изм = x.Element("Measure").Value,
                Цена = x.Element("Price").Value
            });

            /// Заполняю табличку данными. Колонки будут названы названием переменной, так как в xaml я поставила AutoGenerateColumns="True"
            DTUsers.ItemsSource = result;
        }

        /// Функция обновления данных
        private void Button_Reload(object sender, RoutedEventArgs e)
        {
            var result = goods.Descendants("Good").Select(x => new
            {
                Код_Товара = x.Element("Id").Value,
                Название = x.Element("Name").Value,
                Количество = x.Element("Amount").Value,
                Ед_изм = x.Element("Measure").Value,
                Цена = x.Element("Price").Value
            });

            /// Заполняю табличку данными. Колонки будут названы названием переменной, так как в xaml я поставила AutoGenerateColumns="True"
            DTUsers.ItemsSource = result;
        }

        /// Функция добавления данных
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            bool wrongId = false;

            foreach (var item in goods.Elements("Good"))
            {
                if (item.Element("Id").Value == Id.Text)
                {
                    wrongId = true;
                    MessageBox.Show("Товар с таким id уже есть");
                    break;
                }
            }

            if (Id.Text != null & Name.Text != null & Amount.Text != null & Measure.Text != null & Price.Text != null & !wrongId)
            {
                var newGood = new XElement("Good",
                    new XElement("Id", Id.Text),
                    new XElement("Name", Name.Text),
                    new XElement("Amount", Amount.Text),
                    new XElement("Measure", Measure.Text),
                    new XElement("Price", Price.Text)
                );

                goods.Add(newGood);
                goods.Save("../../../xml-files/goods.xml");

                MessageBox.Show("Выполнено");
                Id.Text = "Код Товара";
                Name.Text = "Название";
                Amount.Text = "Количество на складе";
                Measure.Text = "Единицы измерения";
                Price.Text = "Цена";
            }
            else
            {
                MessageBox.Show("Ошибка!");
            };

            Button_Reload(null, null);

        }

        private void Button_Rename(object sender, RoutedEventArgs e)
        {

            IEnumerable<XElement> w = from item in goods.Elements("Good")
                                       where item.Element("Id").Value == Id.Text
                                       select item;

            w.First().Element("Name").Value = Name.Text;
            w.First().Element("Amount").Value = Amount.Text;
            w.First().Element("Measure").Value = Measure.Text;
            w.First().Element("Price").Value = Price.Text;

            goods.Save("../../../xml-files/goods.xml");

            MessageBox.Show("Успешно!");
            Id.Text = "";
            Name.Text = "";
            Amount.Text = "";
            Measure.Text = "";
            Price.Text = "";

            Button_Reload(null, null);
        }


        private void BTN_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;
            tb.Text = string.Empty;
            tb.GotFocus -= BTN_GotFocus;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            foreach (var x in Utils.FindVisualChildren<TextBox>(this))
            {
                x.GotFocus += this.BTN_GotFocus;
            }
        }

        private void DTUsers_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var a = (from item in goods.Elements("Good") select item).ToArray();
            var i = DTUsers.SelectedIndex;
            if (i == -1) return;
            var x = a[i];

            Id.Text = x.Element("Id").Value;
            Name.Text = x.Element("Name").Value;
            Amount.Text = x.Element("Amount").Value;
            Measure.Text = x.Element("Measure").Value;
            Price.Text = x.Element("Price").Value;

        }
    }
}

