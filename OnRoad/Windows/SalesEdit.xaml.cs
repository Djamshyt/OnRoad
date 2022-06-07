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
	/// Логика взаимодействия для SalesEdit.xaml
	/// </summary>
	public partial class SalesEdit : Window
	{
		public Sales sl { get; set; }
		public SalesEdit(Sales sale)
		{
			InitializeComponent();
			DataContext = this;
			sl = sale;
		
			LoadCombo();
		}
		public void LoadCombo()
		{
			ClientBox.Items.Add("Клиент.");
			foreach (Clients g in DbUtils.Db.Clients)
			{
				ClientBox.Items.Add(g.Surname + " " + g.Name + " " + g.Patronymic);
                if (ClientBox.Items.Count != 1 && sl.IdClient != null)
                {
                    ClientBox.SelectedIndex = (int)sl.IdClient;
                }
            }
			CarBox.Items.Add("Машина.");
			foreach (Car g in DbUtils.Db.Car)
			{
				CarBox.Items.Add(g.Model.Model1);
				if (CarBox.Items.Count != 1)
				{
					CarBox.SelectedIndex = (int)sl.IdCar;
				}
			}
			AdditionalBox.Items.Add("Доп. услуги.");
			foreach (AddService g in DbUtils.Db.AddService)
			{
				AdditionalBox.Items.Add(g.ServiceName);
				if (AdditionalBox.Items.Count != 1)
				{
					if(sl.IdAddService != 0)
					{
						sl.IdAddService = 1;
						AdditionalBox.SelectedIndex = (int)sl.IdAddService;
					}
					else
					{
						AdditionalBox.SelectedIndex = 0;
					}
					
				}
			}
	
		}
		private void SaveClick(object sender, RoutedEventArgs e)
		{
			if (sl.Id == 0)
			{
				try
				{
					var car = DbUtils.Db.Car.Single(x => x.Model == CarBox.SelectedItem);
					sl.IdCar = car.Id;
					var clnt = DbUtils.Db.Clients.Single(x => x.Id == ClientBox.SelectedIndex + 1);
					sl.IdClient = clnt.Id;
					var addtnl = DbUtils.Db.AddService.Single(x => x.ServiceName == CarBox.SelectedItem.ToString());
					sl.IdAddService = addtnl.Id;
					DbUtils.Db.Sales.Add(sl);
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
