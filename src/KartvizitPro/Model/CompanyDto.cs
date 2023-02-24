using System.ComponentModel;

namespace KartvizitPro.Model
{
    public class CompanyDto : INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion
        private int id;

        public int Id
        {
            get { return id; }
            set { id = value; OnPropertyChanged("Id"); }
        }

        private string name;

        public string Name
        {
            get { return name; }
            set { name = value; OnPropertyChanged("Name"); }
        }
        private string sector;

        public string Sector
        {
            get { return sector; }
            set { sector = value; OnPropertyChanged("Sector"); }
        }
        private string phone;

        public string Phone
        {
            get { return phone; }
            set { phone = value; OnPropertyChanged("Phone"); }
        }
        private string phone2;

        public string Phone2
        {
            get { return phone2; }
            set { phone2 = value; OnPropertyChanged("Phone2"); }
        }
        private string fax;

        public string Fax
        {
            get { return fax; }
            set { fax = value; OnPropertyChanged("Fax"); }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; OnPropertyChanged("Email"); }
        }
        private string person;

        public string Person
        {
            get { return person; }
            set { person = value; OnPropertyChanged("Person"); }
        }
        private string no;

        public string No
        {
            get { return no; }
            set { no = value; OnPropertyChanged("No"); }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; OnPropertyChanged("Address"); }
        }
    }
}
