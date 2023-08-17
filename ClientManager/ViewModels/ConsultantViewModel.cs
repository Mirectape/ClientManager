using ClientManager.Models;
using ClientManager.Services;
using ClientManager.Commands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Collections.ObjectModel;
using System.Windows.Data;
using ClientManagerDataTypeLibrary;

namespace ClientManager.ViewModels
{
    public class ConsultantViewModel : WorkerViewModelBase
    {
        private readonly string _typeOfWorker;

        public ICommand ChangeClientDataCommand { get; }
        public ICommand SortClientsCommand { get; }

        public ConsultantViewModel(Repository repository, NavigationService renewManagerViewModelService) : base(repository)
        {
            _typeOfWorker = DataWorkerType.consultant;
            ClientData = ClientDataCollections.ClientDataCollectionForConsultant;
            ChangeClientDataCommand = new ChangeClientDataCommand(repository, this, renewManagerViewModelService, _typeOfWorker);
            SortClientsCommand = new SortClientsCommand(repository, this);
        }
    }
}
