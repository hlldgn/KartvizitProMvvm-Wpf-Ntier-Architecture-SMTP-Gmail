using KartvizitPro.Model;
using KartvizitPro.ViewModel;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Input;

namespace KartvizitPro.View
{
    /// <summary>
    /// Interaction logic for UpdateCompanyView.xaml
    /// </summary>
    public partial class UpdateCompanyView : Window
    {
        public UpdateCompanyView(CompanyDto companyDto)
        {
            InitializeComponent();
            this.DataContext = new UpdateCompanyViewModel(companyDto);
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
        }

        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
