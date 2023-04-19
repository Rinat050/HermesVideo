using HermesVideo.ADOApp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HermesVideo.PagesApp
{
    /// <summary>
    /// Interaction logic for ClientsListPage.xaml
    /// </summary>
    public partial class ClientsListPage : Page
    {
        private List<Client> _allClients;
        private List<Client> _currentClients;

        private Predicate<Client> _filterQuery = x => true;
        private Func<Client, object> _sortQuery = x => x.Id;

        public ClientsListPage()
        {
            InitializeComponent();

            lvClients.ItemsSource = App.Connection.Client.ToList();
        }
    }
}
