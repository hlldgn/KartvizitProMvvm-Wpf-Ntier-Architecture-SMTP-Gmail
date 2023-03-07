using KartvizitPro.ViewModel;
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
using System.Xml;

namespace KartvizitPro.View
{
    /// <summary>
    /// Interaction logic for MailView.xaml
    /// </summary>
    public partial class MailView : Window
    {
        public string BccCC { get; set; } = "BCC";
        public MailView()
        {
            InitializeComponent();
            this.DataContext = new MailViewModel();
        }

        private void btnExit_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnEkle_Click(object sender, RoutedEventArgs e)
        {
            txtAliciEkle.Clear();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (File.Exists("Mail.xml"))
            {
                XmlTextReader read = new XmlTextReader("Mail.xml");
                while (read.Read())
                {
                    if (read.NodeType == XmlNodeType.Element)
                    {
                        switch (read.Name)
                        {
                            case "ccbcc":
                                if (read.ReadString() == "0")
                                {
                                    BccCC = "BCC";
                                }
                                else
                                {
                                    BccCC = "CC";
                                }
                                break;
                            case "Title":
                                txtTitle.Text = read.ReadString();
                                break;
                            case "Body":
                                txtBody.AppendText(read.ReadString());
                                break;
                        }
                    }
                }
                read.Close();
            }
        }
        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.ChangedButton == System.Windows.Input.MouseButton.Left)
                this.DragMove();
        }
    }
}
