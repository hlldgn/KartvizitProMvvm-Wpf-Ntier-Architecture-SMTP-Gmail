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
using System.Windows;
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
            updateCommand = new RelayCommand(UpdateCompany);
            LoadData();
        }
        #region DeleteOperation
        private RelayCommand deleteCommand;

        public RelayCommand DeleteCommand
        {
            get { return deleteCommand; }
        }
        private void DeleteCompany()
        {
            try
            {
                if (MessageBoxResult.Yes== CustomMessageBoxViewModel.ShowDialog(CompanyDto.Name + " Adlı Firmayı silmek istediğinize emin misiniz?",
                    MessageBoxButton.YesNo, PackIconKind.QuestionMark))
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
        #endregion
        #region AddOperation
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
                    CustomMessageBoxViewModel.ShowDialog(message.Message,MessageBoxButton.OK,PackIconKind.Error);
                }
                LoadData();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region ListOperation
        private string companySearch;

        public string CompanySearch
        {
            get { return companySearch; }
            set
            { 
                companySearch = value; 
                SearchData(companySearch); 
                OnPropertyChanged("CompanySearch"); 
            }
        }

        private void SearchData(string search)
        {
            var companies = _companyService.Search(search);
            var companyDto = mapper._mapper.Map<IEnumerable<Company>,
                ObservableCollection<CompanyDto>>(companies);
            CompanyList = companyDto;
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
        #endregion
        #region UpdateOperation
        private RelayCommand updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }

        private void UpdateCompany()
        {
            UpdateCompanyView form = new UpdateCompanyView(companyDto);
            form.Show();
        }
        #endregion
        #region CurrentModel

        private CompanyDto companyDto;

        public CompanyDto CompanyDto
        {
            get { return companyDto; }
            set { companyDto = value; OnPropertyChanged("CompanyDto"); }
        }
        private CompanyInsertDto companyInsertDto;

        public CompanyInsertDto CompanyInsertDto
        {
            get { return companyInsertDto; }
            set { companyInsertDto = value; }
        }
        #endregion
    }
}
