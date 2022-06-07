using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Button = System.Windows.Controls.Button;


namespace OnRoad.Windows
{
	/// <summary>
	/// Логика взаимодействия для AdminWindow.xaml
	/// </summary>
	public partial class AdminWindow : Window
	{
		public Employee UserBinding { get; set; }
		public AdminWindow(Employee user)
		{
			InitializeComponent();
			DataContext = this;
			UserBinding = user;
			PosBlock.Text = ": " + user.Positon.Position;
			EmployeeControl.ItemsSource = DbUtils.Db.Employee.ToList();
			ClientControl.ItemsSource = DbUtils.Db.Clients.ToList();
			CarControl.ItemsSource = DbUtils.Db.Car.ToList();
			SupplyControl.ItemsSource = DbUtils.Db.Supply.OrderByDescending(x => x.DateIn).ToList();
			SalesControl.ItemsSource = DbUtils.Db.Sales.OrderByDescending(x => x.Date).ToList();
			ClientOrdersControl.ItemsSource = DbUtils.Db.ClientOrder.OrderByDescending(x => x.Date).ToList();
			ConsultationControl.ItemsSource = DbUtils.Db.Consultations.ToList();
			if (user.IdPosition == 2)
			{
				ClientsBar.Visibility = Visibility.Visible;
				CarBar.Visibility = Visibility.Visible;
				SupplyBar.Visibility = Visibility.Visible;
				SalesBar.Visibility = Visibility.Visible;
				ConsutatinBar.Visibility = Visibility.Visible;
				CientOrdersBar.Visibility = Visibility.Visible;
			}
		}
		private void Exit(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

		private void SwitchUser(object sender, RoutedEventArgs e)
		{
			MainWindow Auth = new MainWindow();
			Auth.Show();
			this.Close();
		}
		
		private void AddEditClick(object sender, RoutedEventArgs e)
		{
			var b = sender as Button;
			Employee emp = b.DataContext as Employee;
			AddEdit add = new AddEdit(emp);
			add.ShowDialog();
		}
		private void Add(object sender, RoutedEventArgs e)
		{
			if (Page.SelectedIndex == 0)
			{
				Employee emp = new Employee();
				AddEdit Ad = new AddEdit(emp);
				Ad.ShowDialog();
			}
			else if (Page.SelectedIndex == 1)
			{
				Clients clnt = new Clients();
				ClientEdit Ad = new ClientEdit(clnt);
				Ad.ShowDialog();
			}
			else if (Page.SelectedIndex == 2)
			{
				Car car = new Car();
				CarEdit cr = new CarEdit(car);
				cr.ShowDialog();
			}
			else if (Page.SelectedIndex == 3)
			{
				Supply supply = new Supply();
				//CarEdit cr = new CarEdit(car);
				//cr.ShowDialog();
			}
			else if (Page.SelectedIndex == 4)
			{
				Sales sales = new Sales();
				PayWindow cr = new PayWindow(sales, UserBinding);
				cr.ShowDialog();
			}
		}
		private void ClientClick(object sender, RoutedEventArgs e)
		{
			var b = sender as Button;
			Clients clnt = b.DataContext as Clients;
			ClientEdit add = new ClientEdit(clnt);
			add.ShowDialog();
		}
		private void CarClick(object sender, RoutedEventArgs e)
		{
			var b = sender as Button;
			Car car = b.DataContext as Car;
			CarEdit add = new CarEdit(car);
			add.ShowDialog();
		}
		private void SaleClick(object sender, RoutedEventArgs e)
		{
			var b = sender as Button;
			Sales sl = b.DataContext as Sales;
			SalesEdit add = new SalesEdit(sl);
			add.ShowDialog();
		}

		private void ChartClick(object sender, RoutedEventArgs e)
		{
            CharWindow add = new CharWindow();
            add.ShowDialog();
        }

		private void FilterBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(Page.SelectedIndex == 2)
            {
				if(FilterBox.SelectedIndex == 0)
                {
					CarControl.ItemsSource = DbUtils.Db.Car.ToList();
				}
				else if(FilterBox.SelectedIndex == 1)
                {
					CarControl.ItemsSource = DbUtils.Db.Car.Where(x => x.CarType.TransportType == "Автомобиль").ToList();
				}
				else if (FilterBox.SelectedIndex == 2)
				{
					CarControl.ItemsSource = DbUtils.Db.Car.Where(x => x.CarType.TransportType == "Мотоцикл").ToList();
				}
			}
		}

		private void SortBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
		{
			if(Page.SelectedIndex == 0)
            {
				if(SortBox.SelectedIndex == 0)
                {
					EmployeeControl.ItemsSource = DbUtils.Db.Employee.ToList();
				}
				else if(SortBox.SelectedIndex == 1)
                {
					EmployeeControl.ItemsSource = DbUtils.Db.Employee.OrderBy(x => x.Age).ToList();
				}
				else if (SortBox.SelectedIndex == 2)
				{
					EmployeeControl.ItemsSource = DbUtils.Db.Employee.OrderBy(x => x.Surname).ToList();
				}
				else if (SortBox.SelectedIndex == 3)
				{
					EmployeeControl.ItemsSource = DbUtils.Db.Employee.OrderByDescending(x => x.Surname).ToList();
				}
				else if (SortBox.SelectedIndex == 4)
				{
					EmployeeControl.ItemsSource = DbUtils.Db.Employee.OrderBy(x => x.Id).ToList();
				}
			}
			else if(Page.SelectedIndex == 1)
            {
				if (SortBox.SelectedIndex == 0)
				{
					ClientControl.ItemsSource = DbUtils.Db.Clients.ToList();
				}
				else if (SortBox.SelectedIndex == 1)
				{
					ClientControl.ItemsSource = DbUtils.Db.Clients.OrderBy(x => x.Age).ToList();
				}
				else if (SortBox.SelectedIndex == 2)
				{
					ClientControl.ItemsSource = DbUtils.Db.Clients.OrderBy(x => x.Surname).ToList();
				}
				else if (SortBox.SelectedIndex == 3)
				{
					ClientControl.ItemsSource = DbUtils.Db.Clients.OrderByDescending(x => x.Surname).ToList();
				}
			}
			else if(Page.SelectedIndex == 2)
            {
				if (SortBox.SelectedIndex == 0)
				{
					CarControl.ItemsSource = DbUtils.Db.Car.ToList();
				}
				else if (SortBox.SelectedIndex == 1)
				{
					CarControl.ItemsSource = DbUtils.Db.Car.OrderBy(x => x.Model.Model1).ToList();
				}
				else if (SortBox.SelectedIndex == 2)
				{
					CarControl.ItemsSource = DbUtils.Db.Car.OrderByDescending(x => x.Model.Model1).ToList();
				}
				else if (SortBox.SelectedIndex == 3)
				{
					CarControl.ItemsSource = DbUtils.Db.Car.OrderBy(x => x.Sum).ToList();
				}
				else if (SortBox.SelectedIndex == 4)
				{
					CarControl.ItemsSource = DbUtils.Db.Car.OrderByDescending(x => x.Sum).ToList();
				}
			}
		}

        private void Page_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			if (Page.SelectedIndex == 0)
			{
				SortBox.Items.Clear();
				FilterBox.Items.Clear();
				FilterBox.Items.Add("Фильтр.");
				FilterBox.SelectedIndex = 0;

				SortBox.Items.Add("Сортировка");
				SortBox.Items.Add("По возрасту");
				SortBox.Items.Add("От А до Я");
				SortBox.Items.Add("От Я до А");
				SortBox.Items.Add("По номеру сотрудника");
				SortBox.SelectedIndex = 0;


			}
			else if (Page.SelectedIndex == 1)
			{
				SortBox.Items.Clear();
				FilterBox.Items.Clear();
				FilterBox.Items.Add("Фильтр.");
				FilterBox.SelectedIndex = 0;

				SortBox.Items.Add("Сортировка");
				SortBox.Items.Add("По возрасту");
				SortBox.Items.Add("От А до Я");
				SortBox.Items.Add("От Я до А");
				SortBox.SelectedIndex = 0;
			}
			else if (Page.SelectedIndex == 2)
			{
				FilterBox.Items.Clear();
				SortBox.Items.Clear();
				FilterBox.Items.Add("Фильтр.");
				foreach (var prtype in DbUtils.Db.CarType)
				{
					FilterBox.Items.Add(prtype.TransportType);
				}
				FilterBox.SelectedIndex = 0;
				SortBox.Items.Add("Сортировка");
				SortBox.Items.Add("От А до Я");
				SortBox.Items.Add("От Я до А");
				SortBox.Items.Add("По возрастанию цены");
				SortBox.Items.Add("По убыванию цены");
				SortBox.SelectedIndex = 0;
			}
			else if (Page.SelectedIndex == 3)
			{

			}
			else if (Page.SelectedIndex == 4)
			{

			}
		}
		private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
		{
			if (Page.SelectedIndex == 0)
			{
				string a = SerachBox.Text;
				EmployeeControl.ItemsSource = DbUtils.Db.Employee.Where(x => x.Surname.StartsWith(a) || x.Name.StartsWith(a) || x.Patronymic.StartsWith(a) || x.Positon.Position.StartsWith(a)).ToList();
			}
			if (Page.SelectedIndex == 1)
			{
				string a = SerachBox.Text;
				ClientControl.ItemsSource = DbUtils.Db.Clients.Where(x => x.Surname.StartsWith(a) || x.Name.StartsWith(a) || x.Patronymic.StartsWith(a) || x.PhoneNumber.StartsWith(a)).ToList();
			}
			if (Page.SelectedIndex == 2)
			{
				string a = SerachBox.Text;
				CarControl.ItemsSource = DbUtils.Db.Car.Where(x => x.Model.Model1.StartsWith(a) || x.Engine.Engine1.StartsWith(a) || x.CarType.TransportType.StartsWith(a) || x.Color.Color1.StartsWith(a)).ToList();
			}
		}

        private void GuidesButton_Click(object sender, RoutedEventArgs e)
        {
			GuidesWindow gw = new GuidesWindow();
			gw.Show();
        }

        private void RefreshButton_Click(object sender, RoutedEventArgs e)
        {
			EmployeeControl.ItemsSource = DbUtils.Db.Employee.ToList();
			ClientControl.ItemsSource = DbUtils.Db.Clients.ToList();
			CarControl.ItemsSource = DbUtils.Db.Car.ToList();
			SupplyControl.ItemsSource = DbUtils.Db.Supply.OrderByDescending(x => x.DateIn).ToList();
			SalesControl.ItemsSource = DbUtils.Db.Sales.OrderByDescending(x => x.Date).ToList();
			ClientOrdersControl.ItemsSource = DbUtils.Db.ClientOrder.OrderByDescending(x => x.Date).ToList();
			ConsultationControl.ItemsSource = DbUtils.Db.Consultations.ToList();
		}

        private void StatConsBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			DbUtils.Db.SaveChanges();
        }

    }
}
