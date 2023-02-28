using Entites.Concrete;

namespace Business.Abstract
{
    public interface ICompanyService:IBaseService<Company>
    {
        IEnumerable<Company> EmailNotNull();
        IEnumerable<Company> EmailNotNullSearch(string search);
        IEnumerable<Company> EmailNotNullSectorSearch(string search);

    }
}
