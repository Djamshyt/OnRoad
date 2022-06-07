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
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace OnRoad.Windows
{
    /// <summary>
    /// Логика взаимодействия для PayWindow.xaml
    /// </summary>
    public partial class PayWindow : Window
    {
        public Sales sl { get; set; }
		public Employee emp { get; set; }

		double CarPrice = 0.0;
		double AddServPrice = 0.0;

		string clName = "";
        public PayWindow(Sales sale, Employee em)
        {
            InitializeComponent();
			DataContext = this;
			sl = sale;
			SaleDate.SetValue(DatePicker.SelectedDateProperty, DateTime.Now);
			LoadCombo();
			emp = em;
			UserName.Text = em.Surname + " " + em.Name + " " + em.Patronymic;
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
				if (CarBox.Items.Count != 1 && sl.IdCar != null)
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
					if (sl.IdAddService != 0)
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
			CarBox.SelectedIndex = 0;
			AdditionalBox.SelectedIndex = 0;
			ClientBox.SelectedIndex = 0;
		}
		private void SaveClick(object sender, RoutedEventArgs e)
		{
			if (sl.Id == 0)
			{
				try
				{
					var car = DbUtils.Db.Car.Single(x => x.Model.Model1 == CarBox.SelectedItem.ToString());
					sl.IdCar = car.Id;
					var clnt = DbUtils.Db.Clients.Single(x => x.Id == ClientBox.SelectedIndex + 1);
					sl.IdClient = clnt.Id;
					var addtnl = DbUtils.Db.AddService.Single(x => x.ServiceName == AdditionalBox.SelectedItem.ToString());
					sl.IdAddService = addtnl.Id;
                    if ((bool)Rb1.IsChecked)
                    {
						sl.PayMethod = "Наличные";
					}
					if ((bool)Rb2.IsChecked)
					{
						sl.PayMethod = "Безналичный расчет";
					}
					sl.Sum = Full_Sum(CarBox, AdditionalBox);
					sl.IdEmployee = emp.Id;
					sl.Date = (DateTime)SaleDate.SelectedDate;
					DbUtils.Db.Sales.Add(sl);
					Chek();
					MessageBox.Show("Покупка успешно совершена. Чек сгенерирован");
				}
				catch (Exception ex)
				{
					//MessageBox.Show("Неудалось оформить покупку, проверьте корректность введенных данных");
					MessageBox.Show(ex.ToString());
				}

			}
			DbUtils.Db.SaveChanges();
		}

        private void ClientBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			foreach (var clph in DbUtils.Db.Clients)
			{
				string cl = clph.Surname + " " + clph.Name + " " + clph.Patronymic;
				if (cl == ClientBox.SelectedItem.ToString())
				{
					ClientPhone.Text = clph.PhoneNumber;
					ClientMail.Text = clph.Mail;
					clName = cl;
				}
			}
		}

        private void CarBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			foreach (var car in DbUtils.Db.Car)
			{
				string cl = car.Model.Model1;
				if (cl == CarBox.SelectedItem.ToString())
				{
					DrawingGroup dGroup = new DrawingGroup();
					using (DrawingContext drawingContext = dGroup.Open())
					{
						var bmpImage = new BitmapImage();
						bmpImage.BeginInit();
						bmpImage.CacheOption = BitmapCacheOption.OnLoad;

						bmpImage.StreamSource = new MemoryStream(car.Image);
						bmpImage.EndInit();
						drawingContext.DrawImage(bmpImage, new Rect(0, 0, bmpImage.PixelWidth, bmpImage.PixelHeight));
						drawingContext.Close();
					}
					DrawingImage dImage = new DrawingImage(dGroup);
					if (dImage.CanFreeze)
						dImage.Freeze();
					CarImg.Source = dImage;
					CarModel.Text = car.Model.Model1;
					float sum = (float)car.Sum;
					CarCost.Text = sum.ToString("C");
					CarPrice = sum;
					Full_Sum(CarBox, AdditionalBox);
				}
			}
		}

        private void AdditionalBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
			foreach (var addserv in DbUtils.Db.AddService)
			{
				string cl = addserv.ServiceName;
				if (cl == AdditionalBox.SelectedItem.ToString())
				{
					AddCost.Text = addserv.Cost.ToString("C");
					AddServPrice = addserv.Cost;
					AddDesc.Text = addserv.Description;
					Full_Sum(CarBox, AdditionalBox);
				}
			}
		}

        private double Full_Sum(ComboBox carbox, ComboBox addbox)
        {
			double Full = 0.0;
			if (carbox.SelectedItem != null && addbox.SelectedItem != null)
            {
				if(carbox.SelectedItem.ToString() != "Машина." || addbox.SelectedItem.ToString() != "Доп. услуги.")
                {
					FinalyPrice.Text = (AddServPrice + CarPrice).ToString("C");
					Full = AddServPrice + CarPrice;
				}
				
            }
			return Full;
        }

		public void Chek()
		{

			using (var document = DocX.Create("Чек.docx"))
			{


				var Image = document.AddImage("C:/Users/KlimowiH/source/repos/OnRoad/OnRoad/Resources/LogoWord.png");
				var picture = Image.CreatePicture(150, 250);
				var v = document.InsertParagraph();
				v.AppendPicture(picture);


				// Add a title
				//document.InsertParagraph("Donza Platinum").FontSize(15d).SpacingAfter(50d).Alignment = Alignment.center;
				// Insert a Paragraph into this document.
				var p = document.InsertParagraph();
				p.Append("Чек")
				.Font(new Xceed.Document.NET.Font("Times New Roman"))
				.FontSize(18)
				.Color(System.Drawing.Color.Black)
				.SpacingAfter(40).Alignment = Alignment.center;


				var p3 = document.InsertParagraph("Фамилия клиента: ").FontSize(13);
				p3.Append(clName);
				var p4 = document.InsertParagraph("Модель авто: ").FontSize(13);
				p4.Append(CarModel.Text);


				var p5 = document.InsertParagraph("Способ оплаты: ").FontSize(13);
				var paymethod = "";
				if ((bool)Rb1.IsChecked)
				{
					paymethod = "Наличные";
				}
				if ((bool)Rb2.IsChecked)
				{
					paymethod = "Безналичный расчет";
				}
				p5.Append(paymethod);
				var p6 = document.InsertParagraph("Дата покупки: ").FontSize(13);
				var date = ((DateTime)SaleDate.SelectedDate).ToShortDateString();
				p6.Append(date);


				var p14 = document.InsertParagraph("Доп. услуги: ").FontSize(13);
				p14.Append(AddDesc.Text);

				var p8 = document.InsertParagraph();
				p8.Append("Сумма (руб.): " + FinalyPrice.Text);
				p8.SpacingAfter(10);

				var p9 = document.InsertParagraph();
				p9.Append("Сотрудник " + UserName.Text + "/_____________ФИО/Подпись").Alignment = Alignment.right;


				var p10 = document.InsertParagraph("Покупатель " + clName + "/_____________ФИО/Подпись").Alignment = Alignment.right;

				document.Save();

			}
		}
	}
}
