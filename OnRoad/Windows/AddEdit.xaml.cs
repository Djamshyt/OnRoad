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
	/// Логика взаимодействия для AddEdit.xaml
	/// </summary>
	public partial class AddEdit : Window
	{
		public Employee user { get; set; }
		public AddEdit(Employee emp)
		{
			InitializeComponent();
			DataContext = this;
			user = emp;
			LoadCombo();
		}
		public void LoadCombo()
		{
			PositionBox.Items.Add("Должность");
			GenderBox.Items.Add("Гендер");
			foreach (Gender g in DbUtils.Db.Gender)
			{
				GenderBox.Items.Add(g.Gender1);
				if(GenderBox.Items.Count != 1)
				{
					GenderBox.SelectedIndex = user.IdGender;
				}	
			}
			foreach (Positon r in DbUtils.Db.Positon)
			{
				PositionBox.Items.Add(r.Position);
				if(PositionBox.Items.Count != 1)
				{
					PositionBox.SelectedIndex = user.IdPosition;
				}
				else
				{
					PositionBox.SelectedIndex = 0;
				}
			}

		}
		private void SaveClick(object sender, RoutedEventArgs e)
		{
			if (user.Id == 0)
			{
				try
				{
					var gender = DbUtils.Db.Gender.Single(x => x.Gender1 == GenderBox.SelectedItem.ToString());
					user.IdGender = gender.Id;
					var role = DbUtils.Db.Positon.Single(x => x.Position == PositionBox.SelectedItem.ToString());
					user.IdPosition = role.Id;
					if(user.Id == 0)
                    {
						user.Id = DbUtils.Db.Employee.Count() + 1;
                    }
					DbUtils.Db.Employee.Add(user);
					MessageBox.Show("Успешно");
				}
				catch (Exception)
				{
					MessageBox.Show("Неудалось добавить пользователя, проверьте корректность введенных данных");
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
