using ClientManager.Models;
using ClientManager.ViewModels;
using ClientManager.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientManager.Commands
{
    public class ChangeClientDataCommand : CommandBase
    {
        private readonly Repository _repository;
        private readonly WorkerViewModelBase _workerViewModel;
        private readonly NavigationService _navigationService;
        private bool _passportRepetition;

        //For client protocol
        private string _whoChanged;

        public ChangeClientDataCommand(Repository repository, WorkerViewModelBase workerViewModel,
            NavigationService navigationService, string whoChanged)
        {
            _repository = repository;
            _workerViewModel = workerViewModel;
            _workerViewModel.PropertyChanged += onViewModelPropertyChanged;
            _navigationService = navigationService;
            _whoChanged = whoChanged;
            _passportRepetition = false;
        }

        private void onViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(ManagerViewModel.SelectedClientData) ||
                e.PropertyName == nameof(ManagerViewModel.ClientParameter))
            {
                foreach (var client in _repository.Clients)
                {
                    if(_workerViewModel.ClientParameter == client.PassportNumber && _workerViewModel.SelectedClientData == "Passport Number")
                    {
                        _passportRepetition = true;
                    }
                }
                OnCanExecuteChanged();
            }
            _passportRepetition = false;
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_workerViewModel.ClientParameter) &&
                !_passportRepetition &&
               base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            try
            {
                switch (_workerViewModel.SelectedClientData)
                {
                    case "First Name":
                        foreach (var client in _repository.Clients)
                        {
                            if(client.PassportNumber == _workerViewModel.SelectedClient.PassportNumber)
                            {
                                client.FirstName = _workerViewModel.ClientParameter;
                                client.ClientProtocol.WhatChanged = "First Name";
                            }
                        }
                        break;
                    case "Second Name":
                        foreach (var client in _repository.Clients)
                        {
                            if (client.PassportNumber == _workerViewModel.SelectedClient.PassportNumber)
                            {
                                client.SecondName = _workerViewModel.ClientParameter;
                                client.ClientProtocol.WhatChanged = "Second Name";
                            }
                        }
                        break;
                    case "Paternal Name":
                        foreach (var client in _repository.Clients)
                        {
                            if (client.PassportNumber == _workerViewModel.SelectedClient.PassportNumber)
                            {
                                client.PaternalName = _workerViewModel.ClientParameter;
                                client.ClientProtocol.WhatChanged = "Paternal Name";
                            }
                        }
                        break;
                    case "Phone Number":
                        foreach (var client in _repository.Clients)
                        {
                            if (client.PassportNumber == _workerViewModel.SelectedClient.PassportNumber)
                            {
                                client.PhoneNumber = _workerViewModel.ClientParameter;
                                client.ClientProtocol.WhatChanged = "Phone Number";
                            }
                        }
                        break;
                    case "Passport Number":
                        foreach (var client in _repository.Clients)
                        {
                            if (client.PassportNumber == _workerViewModel.SelectedClient.PassportNumber)
                            {
                                client.PassportNumber = _workerViewModel.ClientParameter;
                                client.ClientProtocol.WhatChanged = "Passport Number";
                            }
                        }
                        break;
                    default:
                        break;
                }

                foreach (var client in _repository.Clients)
                {
                    if (client.PassportNumber == _workerViewModel.SelectedClient.PassportNumber)
                    {
                        client.ClientProtocol.TimeDataChanged = DateTime.Now;
                        client.ClientProtocol.WhoChanged = _whoChanged;
                    }
                }
                MessageBox.Show(_workerViewModel.SelectedClient.ClientProtocol.GetWhoChangedPersonalDataLast(), "Information",
                    MessageBoxButton.OK, MessageBoxImage.Exclamation);
                _workerViewModel.UpdateClients();
                _repository.SaveChanges();
                _navigationService.Navigate();

            }
            catch (NullReferenceException)
            {
                MessageBox.Show("Choose the client", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
