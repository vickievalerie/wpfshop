using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace WPFShop
{
    public partial class Orders_Window : Window
    {

        XElement orders = XElement.Load("../../../xml-files/orders.xml");
        XElement customers = XElement.Load("../../../xml-files/customers.xml");
        XElement goods = XElement.Load("../../../xml-files/goods.xml");

        bool newCustomer = false;
        public Orders_Window()
        {
            InitializeComponent();
            this.Button_Reload(null, null);

            var customerId = customers.Descendants("Customer").Select(x =>
            x.Element("Id").Value + " (" + x.Element("Name").Value + ")");

            CustomerId.ItemsSource = customerId;

            var goodId = goods.Descendants("Good").Select(x =>
            x.Element("Id").Value + " (" + x.Element("Name").Value + ")");
            GoodId.ItemsSource = goodId;

            DateStart.SelectedDate = DateTime.Now;
            OrderId.Text = ((from x in orders.Elements("Order") select int.Parse(x.Element("OrderId").Value)).Max()+1).ToString();

        }
        
        private string GetID(string s)
        {
            return s.Substring(0, s.IndexOf('(') - 1);
        }

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            if (GoodId.SelectedItem != null & CustomerId.SelectedItem != null & DateStart.Text != string.Empty & Amount.Text != string.Empty)
            {
                var OId = GetID(GoodId.SelectedItem.ToString());
                ///var Storage = (from x in goods.Elements("Good") where x.Element("Id").Value == OId select x.Element("Amount").Value).FirstOrDefault("0");
                var goodElement = Utils.GetXMLElement(goods, "Good", "Id", OId);
                var Storage = goodElement.Element("Amount").Value;
                var Price = goodElement.Element("Price").Value;

                var CustId = GetID(CustomerId.SelectedItem.ToString());
                var customerElement = Utils.GetXMLElement(customers, "Customer", "Id", CustId);
                var Total = int.Parse(Amount.Text) * int.Parse(Price);
                if (int.Parse(customerElement.Element("Status").Value) >= 5000)
                {
                    Total = (int)(Total * 0.8);
                }

                if (int.Parse(Amount.Text)>int.Parse(Storage))
                {
                    MessageBox.Show("Такого количества товара нет на складе!");
                    return;
                }
                var newOrder = new XElement("Order",
                    new XElement("GoodId", OId),
                    new XElement("Amount", Amount.Text),
                    new XElement("CustomerId", GetID(CustomerId.Text)),
                    new XElement("OrderId", OrderId.Text),
                    new XElement("Date", DateStart.Text),
                    new XElement("Total", Total.ToString())
                );

                orders.Add(newOrder);
                orders.Save("../../../xml-files/orders.xml");

                goodElement.Element("Amount").Value = (int.Parse(goodElement.Element("Amount").Value) - int.Parse(Amount.Text)).ToString();
                goods.Save("../../../xml-files/goods.xml");

                
                customerElement.Element("Status").Value = (int.Parse(customerElement.Element("Status").Value) + Total).ToString();
                customers.Save("../../../xml-files/customers.xml");


                MessageBox.Show("Выполнено");
                GoodId.SelectedIndex=-1;
                Amount.Text = "";
                this.Button_Reload(null, null);
            }
        }

        private void Button_Reload(object sender, RoutedEventArgs e)
        {

            var result = orders.Descendants("Order").Select(x => new
            {

                Код_Товара = x.Element("GoodId").Value,
                Количество_Товара = x.Element("Amount").Value,
                Код_Клиента = x.Element("CustomerId").Value,
                Код_Заказа = x.Element("OrderId").Value,
                Дата = x.Element("Date").Value,
                Сумма = x.Element("Total").Value
            });

            DTOrders.ItemsSource = result;
        }
    }
}
