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
    /// Логика взаимодействия для Profile.xaml
    /// </summary>
    public partial class Profile : Window
    {

        public IEnumerable<XElement> userLog; /// Обозначаю переменную, в которой буду хранить данные пользователя

        public Profile(IEnumerable<XElement> userLogIn)
        {
            InitializeComponent();
            this.Loaded += this.Window_Loaded;

            userLog = userLogIn; /// Перекладываю локальную переменную в глобальную

            /// Подключаю файлик
            XElement users = XElement.Load("../../../xml-files/users.xml");

            /// Вывожу в личном кабинете информацию из человека, который авторирзовался (userLogIn)
            Name.Content = userLogIn.First().Element("Name").Value.ToUpper() + " " +
                userLogIn.First().Element("FirstName").Value + "\n" +
                userLogIn.First().Element("LastName").Value;
            Id.Content = userLogIn.First().Element("Id").Value;
            Login.Content = userLogIn.First().Element("Login").Value;
            Gender.Content = userLogIn.First().Element("Gender").Value;
            Date.Content = userLogIn.First().Element("Date").Value;
            Mail.Content = userLogIn.First().Element("Mail").Value;
            Telephone.Content = userLogIn.First().Element("Telephone").Value;
            Work.Content = userLogIn.First().Element("Work").Value;
            Age.Content = userLogIn.First().Element("Age").Value;
        }

        private void Button_Rename(object sender, RoutedEventArgs e)
        {
            /// Открываю окошкодля изменения данных
            Profile_Rename rename = new Profile_Rename(userLog);
            rename.Show();
        }

        private void Button_Reload(object sender, RoutedEventArgs e)
        {
            /// Подключаю файлик ещё раз
            XElement users = XElement.Load("../../../xml-files/users.xml");

            /// Линк запросом ищу обновлённый элемент по id, который никогда не меняется
            IEnumerable<XElement> userLogIn = from user in users.Elements("User")
                                              where userLog.First().Element("Id").Value == user.Element("Id").Value
                                              select user;

            /// Обновляю текстовую информацию
            Name.Content = userLogIn.First().Element("Name").Value.ToUpper() + " " +
                userLogIn.First().Element("FirstName").Value + "\n" +
                userLogIn.First().Element("LastName").Value;
            Id.Content = userLogIn.First().Element("Id").Value;
            Login.Content = userLogIn.First().Element("Login").Value;
            Gender.Content = userLogIn.First().Element("Gender").Value;
            Date.Content = userLogIn.First().Element("Date").Value;
            Mail.Content = userLogIn.First().Element("Mail").Value;
            Telephone.Content = userLogIn.First().Element("Telephone").Value;
            Work.Content = userLogIn.First().Element("Work").Value;
            Age.Content = userLogIn.First().Element("Age").Value;
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

    }
}
