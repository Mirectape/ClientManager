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
    public class WorkerViewModelBase : ViewModelBase
    {
        public Repository Repository { get; }

        private readonly ObservableCollection<Client> _clients;
        public IEnumerable<Client> Clients => _clients;

        private readonly ObservableCollection<string> _cmbx_departments;
        public IEnumerable<string> Cmbx_departments => _cmbx_departments;

        private Client _selectedClient;
        public Client SelectedClient
        {
            get { return _selectedClient; }
            set
            {
                _selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
            }
        }

        public ObservableCollection<string> ClientData;
        public IEnumerable<string> Cmbx_clientData => ClientData;

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

        private string _clientParameter;
        public string ClientParameter //Data with which we choose chosen clientData we want to change
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

        public WorkerViewModelBase(Repository repository)
        {
            Repository = repository;
            _clients = new ObservableCollection<Client>();
            _cmbx_departments = new ObservableCollection<string>();
            UpdateClients();
            UpdateDepartments();
            _selectedClient = null;
        }

        public void UpdateClients()
        {
            _clients.Clear();

            foreach (var client in Repository.GetAllClients())
            {
                _clients.Add(client);
            }
        }

        public void UpdateDepartments()
        {
            _cmbx_departments.Clear();

            foreach (var department in Repository.GetAllDepartments())
            {
                _cmbx_departments.Add(department.ToString());
            }
        }
    }
}
