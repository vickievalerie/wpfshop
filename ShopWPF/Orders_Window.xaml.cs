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
    /// Логика взаимодействия для Work_Window.xaml
    /// </summary>
    public partial class Orders_Window : Window
    {

        XElement works = XElement.Load("../../../xml-files/works.xml");
        XElement customers = XElement.Load("../../../xml-files/customers.xml");
        XElement goods = XElement.Load("../../../xml-files/goods.xml");

        bool newCustomer = false;
        public Orders_Window()
        {
            InitializeComponent();

           /* var result = works.Descendants("Work").Select(x => new
            {
                Код_Маршрута = x.Element("GoodId").Value,
                Код_Водителя = x.Element("Customers").Elements("CustomerId").Count<XElement>() == 2 ? 
                x.Element("Customers").Elements("CustomerId").First().Value + "\n" +
                x.Element("Customers").Elements("CustomerId").Last().Value : x.Element("Customers").Elements("CustomerId").First().Value,
                Дата_Отправки = x.Element("DateStart").Value,
                Дата_Возвращения = x.Element("DateBack").Value,
                Премия = x.Element("Prize").Value
            });

            /// Заполняю табличку данными. Колонки будут названы названием переменной, так как в xaml я поставила AutoGenerateColumns="True"
            DTUsers.ItemsSource = result;*/

            var customerId = customers.Descendants("Customer").Select(x => 
            x.Element("Id").Value + " (" + x.Element("Name").Value + ")");

            CustomerId.ItemsSource = customerId;

            var goodId = goods.Descendants("Good").Select(x =>
            x.Element("Id").Value + " (" + x.Element("Name").Value + ")");
            GoodId.ItemsSource = goodId;
        }

/*        private void Button_New(object sender, RoutedEventArgs e)
        {
            ButtonNew.Visibility = Visibility.Collapsed;
            ButtonCancel.Visibility = Visibility.Visible;
            CustomerId2.Visibility = Visibility.Visible;

            newCustomer = true;

        }

        private void Button_Cancel(object sender, RoutedEventArgs e)
        {
            ButtonNew.Visibility = Visibility.Visible;
            ButtonCancel.Visibility = Visibility.Collapsed;
            CustomerId2.Visibility = Visibility.Collapsed;

            newCustomer = false;
        }*/

        private void Button_Add(object sender, RoutedEventArgs e)
        {
            /*if (GoodId.SelectedItem != null & CustomerId.SelectedItem != null & DateStart.Text != null & !newCustomer)
            {
                var customerTimeWork = from dr in customers.Elements("Customer")
                                     where CustomerId.SelectedItem.ToString().Substring(0, 8) == dr.Element("Id").Value
                                     select (float)dr.Element("TimeWork");

                var goodsalary = from w in goods.Elements("Good")
                                where GoodId.SelectedItem.ToString().Substring(0, 8) == w.Element("Id").Value
                                select (float)w.Element("Salary");

                float prize = goodsalary.First() * (customerTimeWork.First() / 100 + 1);

                var time = from t in goods.Elements("Good")
                           where GoodId.SelectedItem.ToString().Substring(0, 8) == t.Element("Id").Value
                           select (float)t.Element("Days");

                TimeSpan dayTimeRoad = new TimeSpan((int)time.First(), 0, 0, 1);

                bool wrongDate = false;
                DateTime? dt = DateStart.SelectedDate;
                try
                {
                    var newWork = new XElement("Work",
                        new XElement("GoodId", GoodId.SelectedItem.ToString().Substring(0, 8)),
                        new XElement("Customers",
                            new XElement("CustomerId", CustomerId.SelectedItem.ToString().Substring(0, 8))),
                        new XElement("DateStart", DateStart.ToString().Substring(0, 10)),
                        new XElement("DateBack", (dt + dayTimeRoad).ToString().Substring(0, 10)),
                        new XElement("Prize", prize));
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Выберите дату!");
                    wrongDate = true;
                }
                finally
                {
                    if (!wrongDate)
                    {
                        var newWork = new XElement("Work",
                            new XElement("GoodId", GoodId.SelectedItem.ToString().Substring(0, 8)),
                            new XElement("Customers",
                                new XElement("CustomerId", CustomerId.SelectedItem.ToString().Substring(0, 8))),
                            new XElement("DateStart", DateStart.ToString().Substring(0, 10)),
                            new XElement("DateBack", (dt + dayTimeRoad).ToString().Substring(0, 10)),
                            new XElement("Prize", prize));

                        works.Add(newWork);
                        works.Save("../../../xml-files/works.xml");
                        MessageBox.Show("Успешно!");
                    }
                }
            }
            else if ((GoodId.SelectedItem != null & CustomerId.SelectedItem != null & DateStart.Text != null & newCustomer))
            {
                var customerTimeWork = from dr in customers.Elements("Customer")
                                     where CustomerId.SelectedItem.ToString().Substring(0, 8) == dr.Element("Id").Value
                                     select (float)dr.Element("TimeWork");
            

                var goodsalary = from w in goods.Elements("Good")
                                where GoodId.SelectedItem.ToString().Substring(0, 8) == w.Element("Id").Value
                                select (float)w.Element("Salary");

                float prize = goodsalary.First() * (customerTimeWork.First() / 100 + 1 + customerTimeWork2.First() / 100) * 2;

                var time = from t in goods.Elements("Good")
                           where GoodId.SelectedItem.ToString().Substring(0, 8) == t.Element("Id").Value
                           select (float)t.Element("Days");

                TimeSpan dayTimeRoad = new TimeSpan((int)time.First(), 0, 0, 1);

                bool wrongDate = false;
                DateTime? dt = DateStart.SelectedDate;
                try
                {
                    var newWork = new XElement("Work",
                        new XElement("GoodId", GoodId.SelectedItem.ToString().Substring(0, 8)),
                        new XElement("Customers",
                            new XElement("CustomerId", CustomerId.SelectedItem.ToString().Substring(0, 8)),
                            new XElement("CustomerId", CustomerId2.SelectedItem.ToString().Substring(0, 8))),
                        new XElement("DateStart", DateStart.ToString().Substring(0, 10)),
                        new XElement("DateBack", (dt + dayTimeRoad).ToString().Substring(0, 10)),
                        new XElement("Prize", prize));
                }
                catch (ArgumentOutOfRangeException)
                {
                    MessageBox.Show("Выберите дату!");
                    wrongDate = true;
                }
                finally
                {
                    if (!wrongDate)
                    {
                        var newWork = new XElement("Work",
                            new XElement("GoodId", GoodId.SelectedItem.ToString().Substring(0, 8)),
                            new XElement("Customers",
                                new XElement("CustomerId", CustomerId.SelectedItem.ToString().Substring(0, 8)),
                                new XElement("CustomerId", CustomerId2.SelectedItem.ToString().Substring(0, 8))),
                            new XElement("DateStart", DateStart.ToString().Substring(0, 10)),
                            new XElement("DateBack", (dt + dayTimeRoad).ToString().Substring(0, 10)),
                            new XElement("Prize", prize));

                        works.Add(newWork);
                        works.Save("../../../xml-files/works.xml");
                        MessageBox.Show("Успешно!");
                    }
                }
            }
            else { MessageBox.Show("Не все ячейки заполнены!"); }; */
        }

        private void Button_Reload(object sender, RoutedEventArgs e)
        {
            /*var result = works.Descendants("Work").Select(x => new
            {
                Код_Маршрута = x.Element("GoodId").Value,
                Код_Водителя = x.Element("Customers").Elements("CustomerId").Count<XElement>() == 2 ?
                x.Element("Customers").Elements("CustomerId").First().Value + "\n" +
                x.Element("Customers").Elements("CustomerId").Last().Value : x.Element("Customers").Elements("CustomerId").First().Value,
                Дата_Отправки = x.Element("DateStart").Value,
                Дата_Возвращения = x.Element("DateBack").Value,
                Премия = x.Element("Prize").Value
            });

            /// Заполняю табличку данными. Колонки будут названы названием переменной, так как в xaml я поставила AutoGenerateColumns="True"
            DTUsers.ItemsSource = result;*/
        }
    }
}
