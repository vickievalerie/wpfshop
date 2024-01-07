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
    /// Логика взаимодействия для Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        private IEnumerable<XElement> userLog; /// Обозначаю переменную, в которой буду хранить данные пользователя
        public Window1(IEnumerable<XElement> userLogIn) /// Получаю userLogIn с авторизации
        {
            InitializeComponent();

            userLog = userLogIn; /// Перекладываю локальную переменную в глобальную

            /// userLogIn — это тип IEnumerable, т.е. неисчисляемый (по типу массива). Поэтому я методом First() беру первый элемент оттуда — элемент xml-файлика User, в котором ищу Имя зашедшего
            string nameUser = userLogIn.First().Element("FirstName").Value; /// Беру имя пользователя

            /// Подключаю файлик
            XElement users = XElement.Load("../../../xml-files/users.xml");
        }

        /// Дополнительная фича, которая отвечает за то, чтобы напоминать пользователю заполнить информацию о себе полностью

        private void Button_Profile(object sender, RoutedEventArgs e)
        {
            Profile profile = new Profile(userLog);
            profile.Show();
        }

        private void Button_Users(object sender, RoutedEventArgs e)
        {
            Users_Window usersWindow = new Users_Window();
            usersWindow.Show();
        }

        private void Button_Drivers(object sender, RoutedEventArgs e)
        {
            Customers_Window driversWindow = new Customers_Window();
            driversWindow.Show();
        }

        private void Button_Ways(object sender, RoutedEventArgs e)
        {
            Goods_Window waysWindow = new Goods_Window();
            waysWindow.Show();
        }

        private void Button_Work(object sender, RoutedEventArgs e)
        {
            Orders_Window workWindow = new Orders_Window();
            workWindow.Show();
        }

        private void Button_Statistics(object sender, RoutedEventArgs e)
        {

        }

        private void Button_Generate(object sender, RoutedEventArgs e)
        {
            XElement orders = XElement.Load("../../../xml-files/orders.xml");
            XElement customers = XElement.Load("../../../xml-files/customers.xml");
            XElement goods = XElement.Load("../../../xml-files/goods.xml");

            var custIds = (from z in customers.Elements("Customer") select z.Element("Id").Value).ToArray();
            var goodIds = (from z in goods.Elements("Good") select z.Element("Id").Value).ToArray();

            for (int i = 0; i < 365 * 5; i++)
            {
                var d = new DateTime(2020, 1, 1).AddDays(i);


                var OId = Utils.Pick(goodIds);
                ///var Storage = (from x in goods.Elements("Good") where x.Element("Id").Value == OId select x.Element("Amount").Value).FirstOrDefault("0");
                var goodElement = Utils.GetXMLElement(goods, "Good", "Id", OId);
                var Price = goodElement.Element("Price").Value;

                var CustId = Utils.Pick(custIds);
                var Amount = Utils.random.Next(100) + 1;
                var customerElement = Utils.GetXMLElement(customers, "Customer", "Id", CustId);
                var Total = Amount * int.Parse(Price);
                if (int.Parse(customerElement.Element("Status").Value) >= 5000)
                {
                    Total = (int)(Total * 0.8);
                }


                var newOrder = new XElement("Order",
                    new XElement("GoodId", OId),
                    new XElement("Amount", Amount.ToString()),
                    new XElement("CustomerId", CustId),
                    new XElement("OrderId", i.ToString()),
                    new XElement("Date", d.ToString()),
                    new XElement("Total", Total.ToString())
                );

                orders.Add(newOrder);
                customerElement.Element("Status").Value = (int.Parse(customerElement.Element("Status").Value) + Total).ToString();
            }

            customers.Save("../../../xml-files/customers.xml");
            orders.Save("../../../xml-files/orders.xml");
            goods.Save("../../../xml-files/goods.xml");

            MessageBox.Show("Выполнено");

        }
    }
}
