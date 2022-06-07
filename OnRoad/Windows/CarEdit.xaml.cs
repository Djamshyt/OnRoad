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
	/// Логика взаимодействия для CarEdit.xaml
	/// </summary>
	public partial class CarEdit : Window
	{
		public Car car { get; set; }
		bool Save = false;
		public CarEdit(Car cr)
		{
			InitializeComponent();
			DataContext = this;
			car = cr;
			LoadCombo();
		}
		public void LoadCombo()
		{
			CarTypeBox.Items.Add("Тип машины");
			foreach (CarType g in DbUtils.Db.CarType)
			{
				CarTypeBox.Items.Add(g.TransportType);
				if (CarTypeBox.Items.Count != 1)
				{
					CarTypeBox.SelectedIndex = car.IdCarType;
				}
			}

		}
		private void SaveClick(object sender, RoutedEventArgs e)
		{
			Save = true;
			if (car.Id == 0)
			{
				try
				{
					var CarType = DbUtils.Db.CarType.Single(x => x.TransportType == CarTypeBox.SelectedItem);
					car.IdCarType = CarType.Id;
					DbUtils.Db.Car.Add(car);
					MessageBox.Show("Успешно");
				}
				catch (Exception)
				{
					MessageBox.Show("Неудалось добавить транспорт, проверьте корректность введенных данных");
				}

			}
			DbUtils.Db.SaveChanges();
		}
		private void ExitClick(object sender, RoutedEventArgs e)
		{
			if (Save)
			{
				this.Close();
			}
            else
            {
				MessageBox.Show("Выйти без сохранения?");
				this.Close();
            }
		}

        private void UploadImg_Click(object sender, RoutedEventArgs e)
        {
			var dialog = new System.Windows.Forms.OpenFileDialog();
			dialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;";
			dialog.ShowDialog();
			if(dialog.FileName != null && dialog.FileName != "")
            {
				byte[] image = File.ReadAllBytes(dialog.FileName);
				car.Image = image;
			}
		}
    }
}
