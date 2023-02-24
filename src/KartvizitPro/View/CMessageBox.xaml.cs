using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Forms;
using KartvizitPro.Model.Enums;
using MaterialDesignThemes.Wpf;

namespace KartvizitPro.View
{
    /// <summary>
    /// Interaction logic for CMessageBox.xaml
    /// </summary>
    public partial class CMessageBox : Window
    {
        public CMessageBox()
        {
            InitializeComponent();
        }
        static CMessageBox cMessageBox;

        static DialogResult result = System.Windows.Forms.DialogResult.No;

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.No;
            cMessageBox.Close();
        }

        private void btnOk_Click(object sender, RoutedEventArgs e)
        {
            result = System.Windows.Forms.DialogResult.Yes;
            cMessageBox.Close();
        }
        public string GetTitle(CMessageTitle title)
        {
            return Enum.GetName(typeof(CMessageTitle), title);
        }
        public string GetButton(CMessageButton button)
        {
            return Enum.GetName(typeof(CMessageButton), button);
            
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="messageBody">Your message body</param>
        /// <param name="title">Your message box form title</param>
        /// <param name="okButton">Your button content this ok button</param>
        /// <param name="noButton">Your button content this no button</param>
        /// <returns></returns>
        public static DialogResult Show(string messageBody,
            CMessageTitle title,
            CMessageButton okButton,
            CMessageButton noButton)
        {
            cMessageBox = new CMessageBox();
            cMessageBox.btnOk.Content = cMessageBox.GetButton(okButton);
            cMessageBox.btnCancel.Content = cMessageBox.GetButton(noButton);
            cMessageBox.txtMessage.Text = messageBody;
            cMessageBox.txtTitle.Text = cMessageBox.GetTitle(title);
            switch (title)
            {
                case CMessageTitle.Hata:
                    cMessageBox.msgLogo.Kind = PackIconKind.Error;
                    cMessageBox.msgLogo.Foreground = Brushes.DarkRed;
                    break;
                case CMessageTitle.Bilgi:
                    cMessageBox.msgLogo.Kind = PackIconKind.InfoCircle;
                    cMessageBox.msgLogo.Foreground = Brushes.Blue;
                    cMessageBox.btnCancel.Visibility = Visibility.Collapsed;
                    cMessageBox.btnOk.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case CMessageTitle.Dikkat:
                    cMessageBox.msgLogo.Kind = PackIconKind.Warning;
                    cMessageBox.msgLogo.Foreground = Brushes.Yellow;
                    cMessageBox.btnCancel.Visibility = Visibility.Collapsed;
                    cMessageBox.btnOk.SetValue(Grid.ColumnSpanProperty, 2);
                    break;
                case CMessageTitle.Onay:
                    cMessageBox.msgLogo.Kind = PackIconKind.QuestionMark;
                    cMessageBox.msgLogo.Foreground = Brushes.Gray;
                    break;
            }
            cMessageBox.Show();
            return result;
        }
    }
}
