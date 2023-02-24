using KartvizitPro.ViewModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace KartvizitPro.View
{
    /// <summary>
    /// Interaction logic for CompanyView.xaml
    /// </summary>
    public partial class CompanyView : UserControl
    {
        public CompanyView()
        {
            InitializeComponent();
            this.DataContext = new CompanyViewModel();
        }

        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {
            foreach (TextBox item in gridTxt.Children)
            {
                item.Clear();
            }
        }
    }
}
