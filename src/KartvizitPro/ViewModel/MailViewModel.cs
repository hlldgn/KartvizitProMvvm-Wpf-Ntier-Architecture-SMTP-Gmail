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
using System.Linq;

namespace KartvizitPro.ViewModel
{
    public class MailViewModel : ViewModelBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _Mapper;
        private readonly MapperMap mapper;
        public MailViewModel()
        {
            _companyService = InstanceFactory.GetInstance<ICompanyService>();
            mapper = new MapperMap(_Mapper);
            mailCompanyDto = new MailCompanyDto();
            mailCompanyInsertDto = new MailCompanyInsertDto();
            mail = String.Empty;
            selectSector = String.Empty;
            fastAddMail = new RelayCommand(AddFastMail);
            manuelAddMail = new RelayCommand(AddManuelMail);
            dataGridAddMail = new RelayCommand(AddDataGridMail);
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
            var group = items.Select(x => x.Sector).Distinct().ToList();
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

        private void AddDataGridMail()
        {
            if (!MailExists(mailCompanyDto.Email))
            {
                mailInsert.Add(new MailCompanyInsertDto { Name=mailCompanyDto.Name,
                Email=mailCompanyDto.Email});
            }
            MailInsert = mailInsert;
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
            }
        }
        private string mail;

        public string Mail
        {
            get { return mail; }
            set { mail = value; OnPropertyChanged("Mail"); }
        }
        private void AddManuelMail()
        {
            if (mail.Length > 0 && mail != String.Empty)
            {
                if (!MailExists(mail))
                {
                    mailInsert.Add(new MailCompanyInsertDto { Name = "Bilinmiyor", Email = mail });
                    MailInsert = mailInsert;
                }
            }
            else
                CustomMessageBoxViewModel.ShowDialog("Öncelikle alıcı mail adresini girmeniz gerekmektedir.",
                    "Hata",System.Windows.MessageBoxButton.OK,PackIconKind.Error);
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

    }
}
