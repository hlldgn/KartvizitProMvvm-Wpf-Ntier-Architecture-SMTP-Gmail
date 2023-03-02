using System.ComponentModel;

namespace KartvizitPro.Model
{
    public class FileAddOpenFileDto:INotifyPropertyChanged
    {
        #region PropertyChanged
        public event PropertyChangedEventHandler? PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
        #endregion

        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; OnPropertyChanged("FileName"); }
        }
        private string safeFileName;

        public string SafeFileName
        {
            get { return safeFileName; }
            set { safeFileName = value; OnPropertyChanged("SafeFileName"); }
        }


    }
}
