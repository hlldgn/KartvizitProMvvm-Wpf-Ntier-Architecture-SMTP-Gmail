using KartvizitPro.Model.Enums;
using System.IO;
using System.Text;
using System.Windows;
using System.Xml;

namespace KartvizitPro.View
{
    /// <summary>
    /// Interaction logic for Settings.xaml
    /// </summary>
    public partial class Settings : Window
    {
        public Settings()
        {
            InitializeComponent();
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
                            case "MailAddress":
                                txtGmailAddress.Text = read.ReadString();
                                break;
                            case "ccbcc":
                                if (read.ReadString() == "0")
                                {
                                    checkBCC.IsChecked = true;
                                    checkCC.IsChecked = false;
                                }
                                else
                                {
                                    checkBCC.IsChecked = false;
                                    checkCC.IsChecked = true;
                                }
                                break;
                            case "Title":
                                txtTitle.Text = read.ReadString();
                                break;
                            case "Body":
                                txtBody.Text = read.ReadString();
                                break;
                        }
                    }
                }
                read.Close();
            }
        }

        private void btnKaydet_Click(object sender, RoutedEventArgs e)
        {
            if (txtGmailAddress.Text.Length > 0 && txtPassword.Password.Length > 0)
            {
                XmlTextWriter write = new XmlTextWriter("Mail.xml", Encoding.UTF8);
                write.Formatting = Formatting.Indented;
                write.WriteStartDocument();
                write.WriteStartElement("Mail");
                write.WriteAttributeString("Id", "0");
                write.WriteElementString("MailAddress", txtGmailAddress.Text);
                write.WriteElementString("Password", txtPassword.Password.ToString());
                write.WriteElementString("Server", "smtp.gmail.com");
                write.WriteElementString("Port", "587");
                if (checkBCC.IsChecked == true)
                    write.WriteElementString("ccbcc", "0");
                else
                    write.WriteElementString("ccbcc", "1");

                write.WriteElementString("Title", txtTitle.Text);
                write.WriteElementString("Body", txtBody.Text);
                write.WriteEndElement();
                write.Close();
                CMessageBox.Show("Bilgiler başarı ile eklendi.", CMessageTitle.Bilgi,CMessageButton.Tamam, CMessageButton.İptal);
                this.Close();
            }
            else
            {
                CMessageBox.Show("Gmail adresinizi ve şifrenizi girmeniz gerekmektedir.", CMessageTitle.Hata, CMessageButton.Tamam, CMessageButton.İptal);
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
