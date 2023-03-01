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
using System.Windows;

namespace KartvizitPro.ViewModel
{
    public class UpdateCompanyViewModel:ViewModelBase
    {
        private readonly ICompanyService _companyService;
        private readonly IMapper _Mapper;
        private readonly MapperMap mapper;
        public UpdateCompanyViewModel(CompanyDto companyDto)
        {
            _companyService=InstanceFactory.GetInstance<ICompanyService>();
            CompanyDto = companyDto;
            mapper = new MapperMap(_Mapper);
            updateCommand = new RelayCommand(UpdateCompany);
        }
        #region UpdateOperation
        private RelayCommand updateCommand;

        public RelayCommand UpdateCommand
        {
            get { return updateCommand; }
        }

        private void UpdateCompany()
        {
            try
            {
                var company = mapper._mapper.Map<CompanyDto, Company>(CompanyDto);
                var result = _companyService.Update(company);

                if (result.Message != null)
                {
                    CustomMessageBoxViewModel.ShowDialog(result.Message,"Hata", MessageBoxButton.OK,
                        PackIconKind.Error);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        #endregion
        #region CurrentModel
        private CompanyDto companyDto;

        public CompanyDto CompanyDto
        {
            get
            {
                //companyDto.Phone=companyDto.Phone.Replace(" ", "");
                //companyDto.Phone2=companyDto.Phone2.Replace(" ", "");
                //companyDto.Fax=companyDto.Fax.Replace(" ", "");
                return companyDto;
            }
            set
            {
                companyDto = value; OnPropertyChanged("CompanyDto");
            }
        }
        #endregion
    }
}
