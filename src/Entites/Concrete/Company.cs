
namespace Entites.Concrete
{
    public class Company:BaseEntity
    {
        public string Name { get; set; }
        public string Sector { get; set; }
        public string Phone { get; set; }
        public string Phone2 { get; set; }
        public string Fax { get; set; }
        public string Email { get; set; }
        public string Person { get; set; }
        public string No { get; set; }
        public string Address { get; set; }

        public Company()
        {

        }

        public Company(string name, string sector, string phone, 
           string phone2, string fax, string email, string person,
           string no, string address,int id):base(id)
        {
            Name = name;
            Sector = sector;
            Phone = phone;
            Phone2 = phone2;
            Fax = fax;
            Email = email;
            Person = person;
            No = no;
            Address = address;
        }
    }
}
