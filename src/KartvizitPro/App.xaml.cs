using Data.Contexts.Sqlite;
using System.Windows;

namespace KartvizitPro
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            using (KartvizitProContextSqlite context=new KartvizitProContextSqlite())
            {
                context.Database.EnsureCreated();
            }            
        }
    }
}
