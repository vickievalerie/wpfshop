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

    public partial class Customers_Window : Window
    {

        XElement customers = XElement.Load("../../../xml-files/customers.xml");
        public Customers_Window()
        {
            InitializeComponent();
            this.Loaded += Window_Loaded;

            var result = customers.Descendants("Customer").Select(x => new
            {
                Код_Покупателя = x.Element("Id").Value,
                ФИО = x.Element("Name").Value,
                Телефон = x.Element("Phone").Value,
                Email = x.Element("Mail").Value,
                Статус = x.Element("Status").Value
            });

            DTUsers.ItemsSource = result;
        }

        private void Button_Reload(object sender, RoutedEventArgs e)
        {
            var result = customers.Descendants("Customer").Select(x => new
            {
                Код_Покупателя = x.Element("Id").Value,
                ФИО = x.Element("Name").Value,
                Телефон = x.Element("Phone").Value,
                Email = x.Element("Mail").Value,
                Статус = x.Element("Status").Value
            });

            /// Заполняю табличку данными. Колонки будут названы названием переменной, так как в xaml я поставила AutoGenerateColumns="True"
            DTUsers.ItemsSource = result;
        }

        /// Функция добавления данных
        private void Button_Add(object sender, RoutedEventArgs e)
        {
            bool wrongId = false;

            foreach (var customer in customers.Elements("Customer"))
            {
                if (customer.Element("Id").Value == Id.Text)
                {
                    wrongId = true;
                    MessageBox.Show("Покупатель с таким id уже есть!");
                    break;
                }
            }

            if (Id.Text != null & Name.Text != null & Phone.Text != null & Mail.Text != null & Status.Text != null & !wrongId)
            {
                var newCustomer = new XElement("Customer",
                    new XElement("Id", Id.Text),
                    new XElement("Name", Name.Text),
                    new XElement("Phone", Phone.Text),
                    new XElement("Mail", Mail.Text),
                    new XElement("Status", Status.Text)
                );

                customers.Add(newCustomer);
                customers.Save("../../../xml-files/customers.xml");

                MessageBox.Show("Успешно!");
                Id.Text = "Код Водителя";
                Name.Text = "Фамилия";
                Phone.Text = "Имя";
                Mail.Text = "Отчество";
                Status.Text = "Стаж";
            } else
            {
                MessageBox.Show("Ошибка!");
            };
            
        }

        private void Button_Rename(object sender, RoutedEventArgs e)
        {

            IEnumerable<XElement> dr = from customer in customers.Elements("Customer")
                                       where customer.Element("Id").Value == Id.Text
                                       select customer;

            dr.First().Element("Name").Value = Name.Text;
            dr.First().Element("Phone").Value = Phone.Text;
            dr.First().Element("Mail").Value = Mail.Text;
            dr.First().Element("Status").Value = Status.Text;

            customers.Save("../../../xml-files/customers.xml");

            MessageBox.Show("Успешно!");
            Id.Text = "Код Водителя";
            Name.Text = "Фамилия";
            Phone.Text = "Имя";
            Mail.Text = "Отчество";
            Status.Text = "Стаж";
        }

        private void Id_TextChanged(object sender, TextChangedEventArgs e)
        {

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

        private void Status_TextChanged(object sender, TextChangedEventArgs e)
        {

        }
    }
}
