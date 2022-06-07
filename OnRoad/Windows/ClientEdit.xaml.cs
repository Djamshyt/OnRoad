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

namespace OnRoad.Windows
{
	/// <summary>
	/// Логика взаимодействия для ClientEdit.xaml
	/// </summary>
	public partial class ClientEdit : Window
	{
		public Clients clnt { get; set; }
		public ClientEdit(Clients client)
		{
			InitializeComponent();
			DataContext = this;
			clnt = client;
			LoadCombo();
		}

		public void LoadCombo()
		{
			GenderBox.Items.Add("Гендер");
			foreach (Gender g in DbUtils.Db.Gender)
			{
				GenderBox.Items.Add(g.Gender1);
				if (GenderBox.Items.Count != 1)
				{
					GenderBox.SelectedIndex = clnt.IdGender;
				}
			}

		}
		private void SaveClick(object sender, RoutedEventArgs e)
		{
			if (clnt.Id == 0)
			{
				try
				{
					var gender = DbUtils.Db.Gender.Single(x => x.Gender1 == GenderBox.SelectedItem.ToString());
					clnt.IdGender = gender.Id;
					DbUtils.Db.Clients.Add(clnt);
					MessageBox.Show("Успешно");
				}
				catch (Exception)
				{
					MessageBox.Show("Неудалось добавить клиента, проверьте корректность введенных данных");
				}

			}
			DbUtils.Db.SaveChanges();
		}
		private void ExitClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
