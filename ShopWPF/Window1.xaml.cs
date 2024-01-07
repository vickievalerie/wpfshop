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
        private void Button_Later(object sender, RoutedEventArgs e)
        {
            /// Скрывает элементы от пользователя при нажатии на кнопку «Позже»
            ImageLater.Opacity = 0;
            TextLater.Opacity = 0;
            ButtonLater.Opacity = 0;
        }

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
    }
}
