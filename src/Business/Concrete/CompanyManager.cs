using Business.Abstract;
using Core.Results;
using Core.Utilities.Business;
using DataAccess.Abstract;
using Entites.Concrete;

namespace Business.Concrete
{
    public class CompanyManager : ICompanyService
    {
        ICompanyDal _companyDal;
        public CompanyManager(ICompanyDal companyDal)
        {
            _companyDal = companyDal;
        }

        public IResult Add(Company entity)
        {
            IResult result = BusinessRules.Run(CheckifCompanyNameNull(entity.Name),
                CheckifPhoneLength(entity.Phone),
                CheckifPhoneLength(entity.Phone2),
                CheckifPhoneLength(entity.Fax));
            if (result != null)
            {
                return result;
            }
            entity.Email = ConvertChacracter(entity.Email);
            //entity.Phone = ConvertPhone(entity.Phone);
            //entity.Phone2 = ConvertPhone(entity.Phone2);
            //entity.Fax = ConvertPhone(entity.Fax);
            _companyDal.Add(entity);
            return new SuccessResult();
        }

        public IResult Delete(Company entity)
        {
            _companyDal.Delete(entity);
            return new SuccessResult();
        }

        public IEnumerable<Company> GetAll()
        {
            return _companyDal.GetList();
        }

        public Company GetById(int id)
        {
            return _companyDal.Get(x => x.Id == id);
        }

        public IEnumerable<Company> Search(string search)
        {
            return _companyDal.GetList(x => x.Name.ToUpper().Contains(search)
            || x.Person.ToUpper().Contains(search)
            || x.Email.ToUpper().Contains(search)
            || x.Sector.ToUpper().Contains(search)
            || x.Phone.Contains(search)
            || x.Phone2.Contains(search));
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

        //private string ConvertPhone(string convertPhone)
        //{
        //    if (convertPhone != null && convertPhone.Length > 9)
        //    {
        //        string phoneNumber;
        //        if (convertPhone.StartsWith("0"))
        //        {
        //            phoneNumber = string.Format("{0} {1} {2} {3}", convertPhone.Substring(1, 3), convertPhone.Substring(4, 3),
        //                convertPhone.Substring(7, 2), convertPhone.Substring(9, 2));
        //            return phoneNumber;
        //        }
        //        else
        //        {
        //            phoneNumber = string.Format("{0} {1} {2} {3}", convertPhone.Substring(0, 3), convertPhone.Substring(3, 3),
        //                convertPhone.Substring(6, 2), convertPhone.Substring(8, 2));
        //        }
        //        return phoneNumber;
        //    }
        //    return string.Empty;
        //}
        private IResult CheckifCompanyNameNull(string name)
        {
            if (name.Length > 0)
                return new SuccessResult();
            else
                return new ErrorResult("Firma Adı Gerekli.");
        }
        private IResult CheckifPhoneLength(string phone)
        {
            if (phone.Length > 9)
                return new SuccessResult();
            else if (phone.Length == 0)
                return new SuccessResult();
            else
                return new ErrorResult("Telefon numaraları en az 10 haneden oluşmalıdır.");

        }

        public IResult Update(Company entity)
        {
            IResult result = BusinessRules.Run(CheckifPhoneLength(entity.Phone),
                CheckifPhoneLength(entity.Phone2),
                CheckifPhoneLength(entity.Fax));
            if (result != null)
            {
                return result;
            }
            entity.Email = ConvertChacracter(entity.Email);
            //entity.Phone = ConvertPhone(entity.Phone);
            //entity.Phone2 = ConvertPhone(entity.Phone2);
            //entity.Fax = ConvertPhone(entity.Fax);
            _companyDal.Update(entity);
            return new SuccessResult("Güncelleme işlemi başarı ile gerçekleşmiştir.");
        }
    }
}
