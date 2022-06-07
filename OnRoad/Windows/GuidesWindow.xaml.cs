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
    /// Логика взаимодействия для GuidesWindow.xaml
    /// </summary>
    public partial class GuidesWindow : Window
    {
        public GuidesWindow()
        {
            InitializeComponent();
            AddServGrid.ItemsSource = DbUtils.Db.AddService.ToList();
            ColorGrid.ItemsSource = DbUtils.Db.Color.ToList();
            EngineGrid.ItemsSource = DbUtils.Db.Engine.ToList();
            CountryGrid.ItemsSource = DbUtils.Db.CountryOfAssembly.ToList();
            StatusGrid.ItemsSource = DbUtils.Db.Status.ToList();
        }
    }
}
