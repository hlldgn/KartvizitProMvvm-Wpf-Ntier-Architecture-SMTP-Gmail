using Entites.Abstract;

namespace Entites.Concrete
{
    public class BaseEntity:IEntity
    {
        public BaseEntity(int ıd)
        {
            Id = ıd;
        }
        public BaseEntity()
        {

        }
        public int Id { get; set; }
    }
}
