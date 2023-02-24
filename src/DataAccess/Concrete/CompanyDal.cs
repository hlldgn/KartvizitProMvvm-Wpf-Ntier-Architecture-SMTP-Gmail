using Data.Contexts.Sqlite;
using DataAccess.Abstract;
using DataAccess.Core;
using Entites.Concrete;

namespace DataAccess.Concrete
{
    public class CompanyDal : EfRepositoryBase<Company, KartvizitProContextSqlite>, ICompanyDal
    {
    }
}
