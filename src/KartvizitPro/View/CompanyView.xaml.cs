using KartvizitPro.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }

        private void btnMail_Click(object sender, RoutedEventArgs e)
        {
            MailView frm = new MailView();
            frm.Show();
        }
    }
}
