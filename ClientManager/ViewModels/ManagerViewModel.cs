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

namespace ClientManager.ViewModels
{
    public class ManagerViewModel : ViewModelBase
    {
        private readonly string _typeOfWorker = "Manager";
        private readonly Repository _repository;

        private readonly ObservableCollection<Client> _clients;
        public IEnumerable<Client> Clients => _clients;

        private readonly ObservableCollection<string> _cmbx_departments;
        public IEnumerable<string> Cmbx_departments => _cmbx_departments;

        private readonly ObservableCollection<string> _cmbx_clientData;
        public IEnumerable<string> Cmbx_clientData => _cmbx_clientData;

        private string _selectedClientData;
        public string SelectedClientData
        {
            get { return _selectedClientData; }
            set 
            { 
                _selectedClientData = value;
                OnPropertyChanged(nameof(SelectedClientData));
            }
        }

        private Client _selectedClient;
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
            }
        }

        private string _clientParameter;
        public string ClientParameter
        {
            get
            {
                return _clientParameter;
            }
            set
            {
                _clientParameter = value;
                OnPropertyChanged(nameof(ClientParameter));
            }
        }

        public ICommand ChangeClientDataCommand { get; }
        public ICommand SortClientsCommand { get; }
        public ICommand OpenAddClientViewCommand { get; }

        public ManagerViewModel(Repository repository, NavigationService addNewClientService, NavigationService renewManagerViewModelService)
        {
            _repository = repository;
            _clients = new ObservableCollection<Client>();
            _cmbx_clientData = new ObservableCollection<string>()
            {
                "First Name", "Second Name", "Paternal Name", "Phone Number", "Passport Number"
            };
            _cmbx_departments = new ObservableCollection<string>();

            ChangeClientDataCommand = new ChangeClientDataCommand(repository, this, renewManagerViewModelService, _typeOfWorker);
            SortClientsCommand = new SortClientsCommand(repository, this);
            OpenAddClientViewCommand = new NavigateCommand(addNewClientService);
            _selectedClient = null;
            UpdateClients();
            UpdateDepartments();
        }

        public void UpdateClients()
        {
            _clients.Clear();

            foreach(var client in _repository.GetAllClients())
            {
                _clients.Add(client);
            }
        }

        public void UpdateDepartments()
        {
            foreach (var department in _repository.GetAllDepartments())
            {
                _cmbx_departments.Add(department.ToString());
            }
        }
    }
}
