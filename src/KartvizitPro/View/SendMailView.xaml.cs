using KartvizitPro.ViewModel;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net.Mail;
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

namespace KartvizitPro.View
{
    /// <summary>
    /// Interaction logic for SendMailView.xaml
    /// </summary>
    public partial class SendMailView : Window
    {
        private SmtpClient _smtp;
        private MailMessage _message;
        public SendMailView(SmtpClient smtp,MailMessage message)
        {
            InitializeComponent();
            _smtp = smtp;
            _message = message;
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += Worker_DoWork;
            worker.RunWorkerCompleted += Worker_RunWorkerCompleted;
            worker.RunWorkerAsync();
        }

        private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
        {
            this.Close();
        }

        private void Worker_DoWork(object? sender, DoWorkEventArgs e)
        {
            try
            {
                System.Threading.Thread.Sleep(100);
                _smtp.Send(_message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
