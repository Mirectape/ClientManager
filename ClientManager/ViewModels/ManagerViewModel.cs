using ClientManager.Models;
using ClientManager.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.ViewModels
{
    public class ManagerViewModel : ViewModelBase, WorkerViewModelBase
    {
        public Repository Repository { get; set; }
        public IEnumerable<Client> Clients { get; }

        public ManagerViewModel(Repository repository, NavigationService navigationService)
        {
            Repository = repository;
            Clients = Repository.GetAllClients();
        }

        public void ChangeClientData(Client selectedClient, object param, DataType dataType)
        {
            throw new NotImplementedException();
        }
    }
}
