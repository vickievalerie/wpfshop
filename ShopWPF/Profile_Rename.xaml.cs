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
    /// Логика взаимодействия для Profile_Rename.xaml
    /// </summary>
    public partial class Profile_Rename : Window
    {

        private IEnumerable<XElement> userLogIn;
        public Profile_Rename(IEnumerable<XElement> userLog)
        {

            userLogIn = userLog;
            InitializeComponent();

            /// Подставляю значения пользователя в ячейки
            FirstName.Text = userLog.First().Element("FirstName").Value;
            Name.Text = userLog.First().Element("Name").Value;
            LastName.Text = userLog.First().Element("LastName").Value;
            Age.Text = userLog.First().Element("Age").Value;
            Work.Text = userLog.First().Element("Work").Value;
            Mail.Text = userLog.First().Element("Mail").Value;
            Telephone.Text = userLog.First().Element("Telephone").Value;
        }


        private void Button_Rename(object sender, RoutedEventArgs e)
        {
            /// Подключаю файлик
            XElement users = XElement.Load("../../../xml-files/users.xml");

            /// Ищу юзера с данным id в файлике xml
            IEnumerable<XElement> rename = from user in users.Elements("User")
                                           where user.Element("Id").Value == userLogIn.First().Element("Id").Value
                                           select user;

            /// Проверяю, чтобы ни одна строка не была пустой и нам не выдалась ошибка
            if (Name.Text != null & FirstName.Text != null & LastName.Text != null & Age.Text != null & Work.Text != null & Telephone.Text != null & Mail.Text != null) {
                
                /// Меняю значение каждого элемента на то, что пользователь напечатал
                rename.First().Element("Name").Value = Name.Text; 
                rename.First().Element("FirstName").Value = FirstName.Text;
                rename.First().Element("LastName").Value = LastName.Text;
                rename.First().Element("Age").Value = Age.Text;
                rename.First().Element("Work").Value = Work.Text;
                rename.First().Element("Mail").Value = Mail.Text;
                rename.First().Element("Telephone").Value = Telephone.Text;

                /// Сохраняю файлик
                users.Save("../../../xml-files/users.xml");

                /// Закрываю окошко
                this.Close();
            } else
            {
                MessageBox.Show("Упс! Что-то пошло не так!");
            }

        }

        private void Age_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void LastName_TextChanged(object sender, TextChangedEventArgs e)
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
    }
}
