using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Forms.DataVisualization.Charting;
using Xceed.Document.NET;
using Xceed.Words.NET;
using Axis = System.Windows.Forms.DataVisualization.Charting.Axis;
using Chart = System.Windows.Forms.DataVisualization.Charting.Chart;
using Series = System.Windows.Forms.DataVisualization.Charting.Series;

namespace OnRoad.Windows
{
    /// <summary>
    /// Логика взаимодействия для CharWindow.xaml
    /// </summary>
    public partial class CharWindow : Window
	{
		Sales sl { get; set; }
		
		public CharWindow()
		{
			InitializeComponent();
		}

		private static Axis SetAxisName(string name)
		{
			var axis = new Axis();
			axis.Title = name;
			return axis;
		}

		public static void SetAxisesName(Chart graphic, string x, string y, int interval)
		{
			graphic.ChartAreas[0].AxisX.Interval = interval;
			graphic.ChartAreas[0].AxisY.IntervalAutoMode = IntervalAutoMode.VariableCount;
			graphic.ChartAreas[0].AxisX = SetAxisName(x);
			graphic.ChartAreas[0].AxisY = SetAxisName(y);
		}

		public static void GetOrdersProfit(Chart graphic, DateTime start, DateTime end)
		{
			int yearCount = end.Year - start.Year > 0 ? end.Year - start.Year : 1;
			yearCount++;

			for (int i = 0; i < yearCount; i++)
			{
				Series current = new Series();
				current.BorderWidth = 8;
				current.Name = (start.Year + i).ToString();
				current.ChartType = SeriesChartType.Column;

				for (int j = 0; j < 12; j++)
				{
					string month = Enum.GetValues(typeof(Month)).GetValue(j).ToString();

					var oders = DbUtils.Db.Sales.ToList();
					double value = 0;
					foreach (var x in oders)
					{
						if (x.Date.Year == start.Year+i && x.Date.Month == j+1)
						{
							value++;
						}
						
					}

					current.Points.AddXY(month, value);
				}
				graphic.Series.Add(current);
			}
		}

		public static void GetMoneyChart(Chart graphic, DateTime start, DateTime end)
		{
			int yearCount = end.Year - start.Year > 0 ? end.Year - start.Year : 1;
			yearCount++;

			for (int i = 0; i < yearCount; i++)
			{
				Series current = new Series();
				current.BorderWidth = 8;
				current.Name = (start.Year + i).ToString();
				current.ChartType = SeriesChartType.Column;

				for (int j = 0; j < 12; j++)
				{
					string month = Enum.GetValues(typeof(Month)).GetValue(j).ToString();

					var oders = DbUtils.Db.Sales.ToList();
					double value = 0;
					foreach (var x in oders)
					{
						if (x.Date.Year == start.Year + i && x.Date.Month == j + 1)
						{
							value+= (double)x.Sum;
						}

					}

					current.Points.AddXY(month, value);
				}
				graphic.Series.Add(current);
			}
		}
		
		public static void ClearSeriea(Chart[] gr)
		{
			foreach (var x in gr)
			{
				x.Series.Clear();
			}
		}
		private void SalesCountGenerate(DateTime start, DateTime end)
        {
            try
            {
				int yearCount = end.Year - start.Year > 0 ? end.Year - start.Year : 1;
				yearCount++;
				int SalesCount = 0;
				double FinalySum = 0;
				double sum = 0;
				double procent = 0;
				var Roman = new Xceed.Document.NET.Font("Times New Roman");
				using (var document = DocX.Create("Отчет.docx"))
				{
					// Add a title
					document.InsertParagraph("Отчет продаж")
						.Font(Roman)
						.FontSize(25d)
						.SpacingAfter(15d)
						.Bold(true)
						.Alignment = Alignment.center;
					// Insert a Paragraph into this document.
					var p = document.InsertParagraph();
					// Append some text and add formatting.
					p.Append("Продажи транспорта и услуг c " + DateTime.Parse(StartDate.SelectedDate.ToString()).ToShortDateString() + " по " + DateTime.Parse(EndDate.SelectedDate.ToString()).ToShortDateString())
					.Font(Roman)
					.FontSize(15d)
					.Color(System.Drawing.Color.Black)
					.SpacingAfter(10);
					p.Alignment = Alignment.center;
					var sales = DbUtils.Db.Sales.ToList();
					List<Sales> SalesName = new List<Sales>();
					Sales Sl = new Sales();
					for (int i = 0; i < yearCount; i++)
					{

						for (int j = 0; j < 12; j++)
						{

							foreach (var count in sales)
							{
								if (count.Date.Year == start.Year + i && count.Date.Month == j + 1)
								{
									SalesCount++;
									SalesName.Add(count);
								}
							}
						}
					}


					if (SalesCount != 0)
					{
						var t = document.AddTable(SalesCount + 1, 6);
						t.Design = TableDesign.ColorfulListAccent1;
						t.Alignment = Alignment.center;
						t.Rows[0].Cells[0].Paragraphs[0].Append("Наименование товара");
						t.Rows[0].Cells[1].Paragraphs[0].Append("Количество (шт.)");
						t.Rows[0].Cells[2].Paragraphs[0].Append("Стоимость одного товара (руб.)");
						t.Rows[0].Cells[3].Paragraphs[0].Append("Стоимость сделок (руб.)");
						t.Rows[0].Cells[4].Paragraphs[0].Append("НДС %");
						t.Rows[0].Cells[5].Paragraphs[0].Append("Стоимость с НДС % (руб.)");
						for (int i = 1; i < SalesCount; i++)
						{
							Sl = SalesName[i];
							for (int j = 0; j < 6; j++)
							{

								if (j == 0)
								{
									t.Rows[i].Cells[j].Paragraphs[0].Append("Honda " + Sl.Car.Model.Model1.ToString());
								}
								else if (j == 1)
								{
									t.Rows[i].Cells[j].Paragraphs[0].Append(DbUtils.Db.Sales.Count(x => x.IdCar == Sl.IdCar).ToString());
								}
								else if (j == 2)
								{
									double sum1 = (double)Sl.Sum;
									t.Rows[i].Cells[j].Paragraphs[0].Append(sum1.ToString("C"));

								}
								else if (j == 3)
								{
									var count = DbUtils.Db.Sales.Count(x => x.IdCar == Sl.IdCar);
									sum = (double)(Sl.Sum * count);
									t.Rows[i].Cells[j].Paragraphs[0].Append(sum.ToString("C"));
								}
								else if (j == 4)
								{
									t.Rows[i].Cells[j].Paragraphs[0].Append("13%");
								}
								else if (j == 5)
								{
									var count = DbUtils.Db.Sales.Count(x => x.IdCar == Sl.IdCar);
									sum = (double)(Sl.Sum * count);
									procent = ((sum * 13) / 100);
									var full = sum + procent;
									t.Rows[i].Cells[j].Paragraphs[0].Append(full.ToString("C"));
								}

								FinalySum = FinalySum + (sum + procent);
							}
						}
						// Add a row at the end of the table and sets its values.

						// Insert a new Paragraph into the document.
						var p2 = document.InsertParagraph("Таблица продаж:")
							.Font(Roman)
							.FontSize(20d);
						p2.SpacingAfter(10d);
						// Insert the Table after the Paragraph.
						p2.InsertTableAfterSelf(t);
						var p2_1 = document.InsertParagraph();
						p2_1.Append("Итоговая стоимость без НДС: " + sum.ToString("C"))
						.SpacingAfter(40)
						.Font(Roman)
						.FontSize(15d);
						var p3 = document.InsertParagraph();
						p3.Append("Итоговая стоимость с НДС: " + FinalySum.ToString("C"))
						.SpacingAfter(40)
						.Font(Roman)
						.FontSize(15d);
						var p3_5 = document.InsertParagraph();
						p3_5.Append("___________________")
							.Alignment = Alignment.right;
						var p4 = document.InsertParagraph();
						p4.Append("ФИО работника, подпись")
							.Font(Roman)
							.FontSize(10d)
							.Alignment = Alignment.right;
						var p5 = document.InsertParagraph();
						p5.Append(DateTime.Now.Date.ToString("D")).SpacingAfter(15d).Font(Roman).Alignment = Alignment.left;
						var p6 = document.InsertParagraph();
						p6.Append("М.П.").Font(Roman);



					}
					else
					{
						var EmptyP = document.InsertParagraph();
						// Append some text and add formatting.
						EmptyP.Append("Продажи транспорта и услуг c " + StartDate.SelectedDate + " по " + EndDate.SelectedDate + " не осуществлялись.");

					}

					MessageBox.Show("Отчет сформирован");
					document.Save();

				}
			}
			catch(Exception ex)
            {
				MessageBox.Show(ex.ToString());
            }

		}
		private void CreateChart(object sender, RoutedEventArgs e)
		{
            ClearSeriea(new Chart[] { GraphicMoney, GraphicCount, GraphicModel });
            SetAxisesName(GraphicCount, "Месяц", "Количество продаж за месяц (шт.)", 1);
            GetOrdersProfit(GraphicCount, StartDate.DisplayDate, EndDate.DisplayDate);
            SetAxisesName(GraphicMoney, "Месяц", "Сумма продаж за месяц (руб.)", 1);
            GetMoneyChart(GraphicMoney, StartDate.DisplayDate, EndDate.DisplayDate);
            SetAxisesName(GraphicModel, "Год", "Кол-во продаж (шт.)", 30);
			SalesCountGenerate((System.DateTime)StartDate.SelectedDate, (System.DateTime)EndDate.SelectedDate);

		}

		private void ExitClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}

	}
}
