using DataAccess.Core;
using Entites.Concrete;
using System.Linq.Expressions;

namespace DataAccess.Abstract
{
    public interface ICompanyDal:IRepository<Company>
    {
    }
}
