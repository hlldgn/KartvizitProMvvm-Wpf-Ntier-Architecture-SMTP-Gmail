using AutoMapper;
using Business.Abstract;
using Business.Autofac;
using Entites.Concrete;
using KartvizitPro.Commands;
using KartvizitPro.Mapper;
using KartvizitPro.Model;
using KartvizitPro.View;
using MaterialDesignThemes.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.NetworkInformation;
using System.Windows;
using System.Windows.Forms;
using System.Xml;

namespace KartvizitPro.ViewModel
{
    public class MailViewModel : ViewModelBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _Mapper;
        private readonly MapperMap mapper;
        private string BccCC = string.Empty;
        private string mailAddress = string.Empty;
        private string password = string.Empty;
        public MailViewModel()
        {
            _companyService = InstanceFactory.GetInstance<ICompanyService>();
            mapper = new MapperMap(_Mapper);
            mailCompanyDto = new MailCompanyDto();
            mailCompanyInsertDto = new MailCompanyInsertDto();
            fileInsert = new ObservableCollection<FileAddOpenFileDto>();
            fileAddOpenFileDto = new FileAddOpenFileDto();
            AddMailDataGridVisible = Visibility.Collapsed;
            FileVisibilty = Visibility.Collapsed;
            dataGridCount = String.Empty;
            mail = String.Empty;
            selectSector = String.Empty;
            title = String.Empty;
            body = String.Empty;
            fastAddMail = new RelayCommand(AddFastMail);
            manuelAddMail = new RelayCommand(AddManuelMail);
            dataGridAddMail = new RelayCommand(AddDataGridMail);
            removeItem = new RelayCommand(RemoveDataGridMail);
            fileAddCommand = new RelayCommand(FileAdd);
            fileRemoveItem = new RelayCommand(FileRemoveDataGrid);
            sendMail = new RelayCommand(SendToMail);
            mailInsert = new ObservableCollection<MailCompanyInsertDto>();
            LoadData();
            GroupBySector();
        }
        #region CompanyList
        private MailCompanyDto mailCompanyDto;

        public MailCompanyDto MailCompanyDto
        {
            get { return mailCompanyDto; }
            set { mailCompanyDto = value; OnPropertyChanged("MailCompanyDto"); }
        }
        private ObservableCollection<MailCompanyDto> companyList;

        public ObservableCollection<MailCompanyDto> CompanyList
        {
            get { return companyList; }
            set { companyList = value; OnPropertyChanged("CompanyList"); }
        }
        private void LoadData()
        {
            var companies = _companyService.EmailNotNull();
            var mailCompanyDto = mapper._mapper.Map<IEnumerable<Company>,
                ObservableCollection<MailCompanyDto>>(companies);
            CompanyList = mailCompanyDto;
        }
        #endregion
        #region CompanySearch
        private string companySearch;

        public string CompanySearch
        {
            get { return companySearch; }
            set
            {
                companySearch = value; SearchData(companySearch);
                OnPropertyChanged("CompanySearch");
            }
        }
        private void SearchData(string search)
        {
            var companies = _companyService.EmailNotNullSearch(search);
            var mailCompanyDto = mapper._mapper.Map<IEnumerable<Company>,
                ObservableCollection<MailCompanyDto>>(companies);
            CompanyList = mailCompanyDto;
        }
        #endregion
        #region GroupBySector
        private string selectSector;

        public string SelectSector
        {
            get { return selectSector; }
            set { selectSector = value; OnPropertyChanged("SelectSector"); }
        }

        private List<string> sector;

        public List<string> Sector
        {
            get { return sector; }
            set { sector = value; OnPropertyChanged("Sector"); }
        }
        private void GroupBySector()
        {
            var items = _companyService.GetAll();
            var group = items.OrderBy(x=>x.Sector).Select(x => x.Sector).Distinct().ToList();
            Sector = group;
        }
        #endregion
        #region MailInsert
        private ObservableCollection<MailCompanyInsertDto> mailInsert;

        public ObservableCollection<MailCompanyInsertDto> MailInsert
        {
            get { return mailInsert; }
            set { mailInsert = value; OnPropertyChanged("MailInsert"); }
        }
        private RelayCommand fastAddMail;

        public RelayCommand FastAddMail
        {
            get { return fastAddMail; }
        }
        private RelayCommand dataGridAddMail;

        public RelayCommand DataGridAddMail
        {
            get { return dataGridAddMail; }
        }

        private MailCompanyInsertDto mailCompanyInsertDto;

        public MailCompanyInsertDto MailCompanyInsertDto
        {
            get { return mailCompanyInsertDto; }
            set { mailCompanyInsertDto = value; OnPropertyChanged("MailCompanyInsertDto"); }
        }
        private Visibility addMailDataGridVisible;

        public Visibility AddMailDataGridVisible
        {
            get { return addMailDataGridVisible; }
            set { addMailDataGridVisible = value; OnPropertyChanged("AddMailDataGridVisible"); }
        }
        private string dataGridCount;

        public string DataGridCount
        {
            get { return dataGridCount; }
            set { dataGridCount = value; OnPropertyChanged("DataGridCount"); }
        }

        private string DataGridRowCountText(ObservableCollection<MailCompanyInsertDto> mails)
        {
            return mails.Count().ToString() + " Adet Firma Eklendi.";
        }
        private void AddDataGridMail()
        {
            if (!MailExists(mailCompanyDto.Email))
            {
                mailInsert.Add(new MailCompanyInsertDto
                {
                    Name = mailCompanyDto.Name,
                    Email = mailCompanyDto.Email
                });
            }
            MailInsert = mailInsert;
            AddMailDataGridVisible = Visibility.Visible;
            DataGridCount = DataGridRowCountText(mailInsert);
        }
        private void AddFastMail()
        {
            if (selectSector != String.Empty && selectSector.Length > 0)
            {
                var currentSector = selectSector;
                var companies = _companyService.EmailNotNullSectorSearch(currentSector);
                var mailCompanyInsertDto = mapper._mapper.Map<IEnumerable<Company>,
                    ObservableCollection<MailCompanyInsertDto>>(companies);
                foreach (var item in mailCompanyInsertDto)
                {
                    if (!MailExists(item.Email))
                        mailInsert.Add(item);
                }
                MailInsert = mailInsert;
                AddMailDataGridVisible = Visibility.Visible;
                DataGridCount = DataGridRowCountText(mailInsert);
            }
        }
        private string mail;

        public string Mail
        {
            get { return mail; }
            set { mail = value; OnPropertyChanged("Mail"); }
        }
        private string ConvertChacracter(string key)
        {
            string mail = key;
            char[] tr = new char[] { 'ö', 'Ö', 'ü', 'Ü', 'ç', 'Ç', 'İ', 'ı', 'Ğ', 'ğ', 'Ş', 'ş' };
            char[] en = new char[] { 'o', 'O', 'u', 'U', 'c', 'C', 'I', 'i', 'G', 'g', 'S', 's' };
            for (int sayac = 0; sayac < tr.Length; sayac++)
            {
                mail = mail.Replace(tr[sayac], en[sayac]);
            }
            return mail;
        }
        private void AddManuelMail()
        {
            if (mail.Length > 0 && mail != String.Empty)
            {
                if (!MailExists(mail))
                {
                    mailInsert.Add(new MailCompanyInsertDto { Name = "Bilinmiyor", Email = ConvertChacracter(mail) });
                    MailInsert = mailInsert;
                    AddMailDataGridVisible = Visibility.Visible;
                    DataGridCount = DataGridRowCountText(mailInsert);
                }
            }
            else
                CustomMessageBoxViewModel.ShowDialog("Öncelikle alıcı mail adresini girmeniz gerekmektedir.",
                    System.Windows.MessageBoxButton.OK, PackIconKind.Error);
        }
        private bool MailExists(string mail)
        {
            if (MailInsert.Count > 0)
            {
                var result = mailInsert.Any(x => x.Email == mail);
                return result;
            }
            return false;
        }
        private RelayCommand manuelAddMail;

        public RelayCommand ManuelAddMail
        {
            get { return manuelAddMail; }
        }
        #endregion
        #region MailRemoveItem
        private RelayCommand removeItem;

        public RelayCommand RemoveItem
        {
            get { return removeItem; }
        }
        private void RemoveDataGridMail()
        {
            MailInsert.Remove(mailCompanyInsertDto);
            if (MailInsert.Count <= 0)
                AddMailDataGridVisible = Visibility.Collapsed;
            else
                DataGridCount = MailInsert.Count().ToString() + " Adet Firma Eklendi.";
        }
        #endregion
        #region FileInsert
        private RelayCommand fileAddCommand;

        public RelayCommand FileAddCommand
        {
            get { return fileAddCommand; }
        }
        private ObservableCollection<FileAddOpenFileDto> fileInsert;

        public ObservableCollection<FileAddOpenFileDto> FileInsert
        {
            get { return fileInsert; }
            set { fileInsert = value; OnPropertyChanged("FileInsert"); }
        }
        private FileAddOpenFileDto fileAddOpenFileDto;

        public FileAddOpenFileDto FileAddOpenFileDto
        {
            get { return fileAddOpenFileDto; }
            set { fileAddOpenFileDto = value; OnPropertyChanged("FileAddOpenFileDto"); }
        }


        private void FileAdd()
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = true;
            openFile.Title = "Aç";
            openFile.Filter = "Excel (*.xlsx)|*.xlsx" +
                "|Excel (*.xls)|*.xls" +
                "|Bitmap (*.bmp)|*.bmp" +
                "|Jpeg (*.jpg)|*.jpg" +
                "|Pdf (*.pdf)|*.pdf" +
                "|Word (*.doc)|*.doc" +
                "|Word (*.docx)|*.docx";
            openFile.RestoreDirectory = true;
            openFile.FilterIndex = 3;
            if (DialogResult.OK == openFile.ShowDialog())
            {
                if (openFile.FileNames.Length > 0)
                {
                    FileVisibilty = Visibility.Visible;
                    string[] fileName = openFile.FileNames;
                    string[] safeFileName = openFile.SafeFileNames;
                    long totalSizeList = FileSize();
                    long totalSizeOpenFile = 0;
                    for (int i = 0; i < fileName.Count(); i++)
                    {
                        FileInfo info = new FileInfo(fileName[i]);
                        totalSizeOpenFile+=info.Length;
                        long totalFilesSizeTypeMb = totalSizeOpenFile + totalSizeList;

                        if(totalFilesSizeTypeMb<25*1024*1024)
                        {
                            FileInsert.Add(new FileAddOpenFileDto
                            {
                                FileName = fileName[i],
                                SafeFileName = safeFileName[i]
                            });
                        }
                        else
                        {
                            CustomMessageBoxViewModel.ShowDialog("Eklenen Dosya boyutu 25 Mb dan fazla olamaz.",
                                MessageBoxButton.OK, PackIconKind.Error);
                            break;
                        }
                    }
                }
            }
        }
        private long FileSize()
        {
            long totalSize = 0;
            foreach (var item in FileInsert)
            {
                FileInfo info = new FileInfo(item.FileName);
                totalSize += info.Length;
            }
            return totalSize;
        }
        private Visibility fileVisibilty;

        public Visibility FileVisibilty
        {
            get { return fileVisibilty; }
            set { fileVisibilty = value; OnPropertyChanged("FileVisibilty"); }
        }

        #endregion
        #region FileRemoveItem
        private RelayCommand fileRemoveItem;

        public RelayCommand FileRemoveItem
        {
            get { return fileRemoveItem; }
        }
        private void FileRemoveDataGrid()
        {
            FileInsert.Remove(FileAddOpenFileDto);
            if (FileInsert.Count <= 0)
                FileVisibilty = Visibility.Collapsed;
        }

        #endregion
        #region SendMail
        private RelayCommand sendMail;

        public RelayCommand SendMail
        {
            get { return sendMail; }
        }
        private string title;

        public string Title
        {
            get { return title; }
            set { title = value; OnPropertyChanged("Title"); }
        }

        private string body;

        public string Body
        {
            get { return body; }
            set { body = value; OnPropertyChanged("Body"); }
        }

        private bool ReadMailSettings()
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
                            case "MailAddress":
                                mailAddress = read.ReadString();
                                break;
                            case "Password":
                                password = read.ReadString();
                                break;

                        }
                    }
                }
                read.Close();
                return true;
            }
            return false;
        }
        private void SendToMail()
        {
            if (NetworkInterface.GetIsNetworkAvailable())
            {
                if (ReadMailSettings())
                {
                    if (MailInsert.Count > 0)
                    {
                        SmtpClient smtp = new SmtpClient();
                        smtp.Port = 587;
                        smtp.Host = "smtp.gmail.com";
                        smtp.Timeout = int.MaxValue;
                        smtp.UseDefaultCredentials = false;
                        smtp.Credentials = (ICredentialsByHost)new NetworkCredential(
                            mailAddress, password);
                        smtp.EnableSsl = true;
                        smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
                        MailMessage message = new MailMessage();
                        message.Attachments.Clear();
                        message.From = new MailAddress(mailAddress);
                        message.IsBodyHtml = true;
                        if (BccCC == "BCC")
                        {
                            foreach (var item in MailInsert)
                            {
                                message.Bcc.Add(item.Email.ToString());
                            }
                        }
                        else
                        {
                            foreach (var item in MailInsert)
                            {
                                message.CC.Add(item.Email.ToString());
                            }
                        }
                        message.Subject = Title;
                        message.Body = Body;
                        if (FileInsert.Count > 0)
                        {
                            foreach (var item in FileInsert)
                            {
                                message.Attachments.Add(new Attachment(item.FileName));
                            }
                        }
                        SendMailView view = new SendMailView(smtp,message);
                        view.ShowDialog();
                        //smtp.Send(message); //
                    }
                    else
                    {
                        CustomMessageBoxViewModel.ShowDialog("En az bir alıcı girmeniz gerekmektedir.",
                            MessageBoxButton.OK, PackIconKind.Error);
                    }
                }
                else
                {
                    if (MessageBoxResult.Yes == CustomMessageBoxViewModel.ShowDialog("Mail ayarları eksik şimdi oluşturmak ister misiniz?",
                        MessageBoxButton.YesNo, PackIconKind.QuestionMark))
                    {
                        Settings settings = new Settings();
                        settings.ShowDialog();
                    }
                }
            }
            else
            {
                CustomMessageBoxViewModel.ShowDialog("Lütfen internet bağlantınızı kontrol ediniz.",
                    MessageBoxButton.OK, PackIconKind.Error);
            }
        }
        #endregion
    }
}
