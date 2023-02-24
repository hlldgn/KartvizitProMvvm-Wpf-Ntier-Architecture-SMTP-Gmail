using AutoMapper;
using Business.Abstract;
using Business.Autofac;
using Entites.Concrete;
using KartvizitPro.Commands;
using KartvizitPro.Mapper;
using KartvizitPro.Model;
using KartvizitPro.Model.Enums;
using KartvizitPro.View;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KartvizitPro.ViewModel
{
    public class CompanyViewModel:ViewModelBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _Mapper;
        private readonly MapperMap mapper;
        public CompanyViewModel()
        {
            _companyService = InstanceFactory.GetInstance<ICompanyService>();
            mapper = new MapperMap(_Mapper);
            companyDto = new CompanyDto();
            companyInsertDto = new CompanyInsertDto();
            addCommand = new RelayCommand(AddCompany);
            deleteCommand = new RelayCommand(DeleteCompany);
            LoadData();
        }
        private CompanyInsertDto companyInsertDto;

        public CompanyInsertDto CompanyInsertDto
        {
            get { return companyInsertDto; }
            set { companyInsertDto = value; }
        }

        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }

        private void DeleteCompany()
        {
            try
            {
                if (DialogResult.Yes == CMessageBox.Show(CompanyDto.Name + " Adlı Firmayı silmek istediğinize emin misiniz?",
                    CMessageTitle.Onay, CMessageButton.Evet, CMessageButton.Hayır))
                {
                    var company = mapper._mapper.Map<CompanyDto, Company>(CompanyDto);
                    var result = _companyService.Delete(company);
                    LoadData();
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        private CompanyDto companyDto;

        public CompanyDto CompanyDto
        {
            get { return companyDto; }
            set { companyDto = value; OnPropertyChanged("CompanyDto"); }
        }
        private void LoadData()
        {
            var companies = _companyService.GetAll();
            var companyDto = mapper._mapper.Map<IEnumerable<Company>,
                ObservableCollection<CompanyDto>>(companies);
            CompanyList = companyDto;
        }
        private ObservableCollection<CompanyDto> companyList;

        public ObservableCollection<CompanyDto> CompanyList
        {
            get { return companyList; }
            set { companyList = value; OnPropertyChanged("CompanyList"); }
        }
        private RelayCommand addCommand;

        public RelayCommand AddCommand
        {
            get { return addCommand; }
        }
        private void AddCompany()
        {
            try
            {
                var company = mapper._mapper.Map<CompanyInsertDto, Company>(CompanyInsertDto);
                var message = _companyService.Add(company);
                if (message.Message != null)
                {
                    CMessageBox.Show(message.Message, CMessageTitle.Hata, CMessageButton.Tamam, CMessageButton.İptal);
                }
                LoadData();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
