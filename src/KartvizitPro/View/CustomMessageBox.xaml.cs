using MaterialDesignThemes.Wpf;
using System.Windows;

namespace KartvizitPro.View
{
    /// <summary>
    /// Interaction logic for CustomMessageBox.xaml
    /// </summary>
    public partial class CustomMessageBox : Window
    {
        public string Message
        {
            get
            {
                return TextBlock_Message.Text;
            }
            set
            {
                TextBlock_Message.Text = value;
            }
        }
        public MessageBoxResult Result { get; set; }
        private void DisplayButtons(MessageBoxButton button)
        {
            switch (button)
            {
                case MessageBoxButton.OKCancel:
                    Button_OK.Visibility = Visibility.Visible;
                    Button_OK.Focus();
                    Button_Cancel.Visibility = Visibility.Visible;

                    Button_Yes.Visibility = Visibility.Collapsed;
                    Button_No.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.YesNo:
                    Button_Yes.Visibility = Visibility.Visible;
                    Button_Yes.Focus();
                    Button_No.Visibility = Visibility.Visible;

                    Button_OK.Visibility = Visibility.Collapsed;
                    Button_Cancel.Visibility = Visibility.Collapsed;
                    break;
                case MessageBoxButton.YesNoCancel:
                    Button_Yes.Visibility = Visibility.Visible;
                    Button_Yes.Focus();
                    Button_No.Visibility = Visibility.Visible;
                    Button_Cancel.Visibility = Visibility.Visible;

                    Button_OK.Visibility = Visibility.Collapsed;
                    break;
                default:
                    Button_OK.Visibility = Visibility.Visible;
                    Button_OK.Focus();

                    Button_Yes.Visibility = Visibility.Collapsed;
                    Button_No.Visibility = Visibility.Collapsed;
                    Button_Cancel.Visibility = Visibility.Collapsed;
                    break;
            }
        }

        private void DisplayImage(PackIconKind? icon)
        {
            if (!icon.HasValue)
            {
                Image_MessageBox.Kind = PackIconKind.Null;
                Image_MessageBox.Visibility = Visibility.Collapsed;
            }
            else
            {
                Image_MessageBox.Kind = icon.Value;
                Image_MessageBox.Visibility = Visibility.Visible;
            }
        }

        public CustomMessageBox(string message,
            MessageBoxButton button, PackIconKind? icon)
        {
            InitializeComponent();

            Message = message;
            DisplayImage(icon);
            DisplayButtons(button);
        }

        private void Button_Cancel_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Cancel;
            Close();
        }

        private void Button_No_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.No;
            Close();
        }

        private void Button_Yes_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.Yes;
            Close();
        }

        private void Button_OK_Click(object sender, RoutedEventArgs e)
        {
            Result = MessageBoxResult.OK;
            Close();
        }
    }
}
