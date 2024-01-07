using System.Configuration;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.Generic;
using System.Xml.Linq;
using System.Xml;
using System.Linq;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace WPFShop
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Login : Window
    {
        public Login()
        {
            InitializeComponent();
            this.Loaded += this.Window_Loaded;

            // Auto-login to simplify debug
            /*LoginTextBox.Text="v";
            PasswordTextBox.Text= "1";
            Button_Click_Send(null, null);*/

        }

        private void Button_Click_Send(object sender, RoutedEventArgs e)
        {
            /// Получаю данные того, что написал пользователь в наши TextBox
            var login = LoginTextBox.Text;
            var password = PasswordTextBox.Text;

            var wrong = false; /// Переменная, для входа

            /// Подключаю файлик
            XElement users = XElement.Load("../../../xml-files/users.xml");


            /// Делаю переменную, в которой будут лежать данные о пользователе, если логин и пароль правильные
            IEnumerable<XElement> userLogIn = from user in users.Elements("User")
                                              where user.Element("Login").Value == login && user.Element("Password").Value == password
                                              select user;

            /// Обработка исключения, если userLogIn окажется пустым
            try
            {
                var xelement = userLogIn.First(); /// Берём 1 элемент из массива (то, что прошло). Если ничего не прошло, вылезает исключение
                wrong = true; /// Если исключение не вылезло, значит всё ок
            }
            catch (InvalidOperationException)
            {
                wrong = false; /// Выполнится, если вылезло исключение и переменная останется false
            }
            finally /// По окончании проверки проверяем, будет ли осуществлён вход
            {
                if (wrong)
                {
                    /// public IEnumerable<XElement> userLog = userLogIn;
                    Window1 mainWin = new Window1(userLogIn);  /// Передаю в главное окно данные человека, который зашёл
                    mainWin.Show();
                    this.Close();

                    
    }
                else
                {
                    MessageBox.Show("Неправильный логин или пароль");
                }
            }
        }

        /// Функция перехода на окошко регистрации
        private void Button_Click1(object sender, RoutedEventArgs e)
        {
            var register = new Registration();
            register.Show();
            this.Close();
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