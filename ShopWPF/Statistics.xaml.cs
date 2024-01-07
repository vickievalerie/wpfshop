using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.DataVisualization.Charting;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace ShopWPF
{

    public class Stat
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
    }

    /// <summary>
    /// Interaction logic for Statistics.xaml
    /// </summary>
    public partial class Statistics : Window
    {
        public Statistics()
        {
            InitializeComponent();

            this.Loaded += Statistics_Loaded;
        }

        private void Statistics_Loaded(object sender, RoutedEventArgs e)
        {
            XElement orders_xml = XElement.Load("../../../xml-files/orders.xml");
            XElement customers_xml = XElement.Load("../../../xml-files/customers.xml");
            XElement goods_xml = XElement.Load("../../../xml-files/goods.xml");

            var goods = (from z in goods_xml.Elements("Good")
                         select new Stat
                         {
                             Id = z.Element("Id").Value,
                             Name = z.Element("Name").Value,
                             Value = 0
                         }).ToArray();

            var custs = (from z in customers_xml.Elements("Customer")
                         select new Stat
                         {
                             Id = z.Element("Id").Value,
                             Name = z.Element("Name").Value,
                             Value = 0
                         }).ToArray();

            foreach (var x in orders_xml.Elements("Order"))
            {
                var GId = x.Element("GoodId").Value;
                foreach (var i in goods)
                {
                    if (i.Id == GId)
                    {
                        i.Value++;
                    }
                }

                var CId = x.Element("CustomerId").Value;
                foreach (var i in custs)
                {
                    if (i.Id == CId)
                    {
                        i.Value++;
                    }
                }
            }

            ((PieSeries)mcChart.Series[0]).ItemsSource = goods;
            ((ColumnSeries)mcChart2.Series[0]).ItemsSource = custs;
        }
    }
}
