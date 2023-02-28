using Data.Contexts.Sqlite;
using DataAccess.Abstract;
using DataAccess.Core;
using Entites.Concrete;
using System.Linq.Expressions;

namespace DataAccess.Concrete
{
    public class CompanyDal : EfRepositoryBase<Company, KartvizitProContextSqlite>, ICompanyDal
    {
    }
}
