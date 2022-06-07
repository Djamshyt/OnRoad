using ModernWpf.Controls.Primitives;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace OnRoad.Windows
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		public MainWindow()
		{
			InitializeComponent();
		}

		private void LoginButtonClick(object sender, RoutedEventArgs e)
		{
			if (DbUtils.Authorization(LoginBox.Text, PasswordBox.Password) == false)
			{
				//FlyoutBase.ShowAttachedFlyout((FrameworkElement)sender);
				MessageBox.Show("Проверьте правильность введенных данных.");
			}
			else
			{
				Employee user = DbUtils.Db.Employee.Single(x => x.Login == LoginBox.Text);
				AdminWindow Aw = new AdminWindow(user);
				this.Close();
				Aw.Show();
			}

		}

		private void RegistrationButtonClick(object sender, RoutedEventArgs e)
		{
			MessageBox.Show("Для регистрации нового пользователя обратитесь к адмиистратору.");
		}

		private void ExitClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
