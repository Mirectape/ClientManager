using ClientManager.Models;
using ClientManager.Services;
using ClientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ClientManager.Commands.BankOperationCommands
{
    class CloseAccountCommand : CommandBase
    {
        private readonly Repository _repository;
        private readonly BankWorkerViewModel _bankWorkerViewModel;
        private readonly NavigationService _navigationService;

        public CloseAccountCommand(Repository repository, BankWorkerViewModel bankWorkerViewModel,
            NavigationService navigationService)
        {
            _repository = repository;
            _bankWorkerViewModel = bankWorkerViewModel;
            _bankWorkerViewModel.PropertyChanged += onViewModelPropertyChanged;
            _navigationService = navigationService;
        }

        private void onViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName == nameof(BankWorkerViewModel.SelectedBankAccount))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            if (_bankWorkerViewModel.SelectedClient != null)
            {
                switch (_bankWorkerViewModel.SelectedBankAccount)
                {
                    case "Deposit Account":
                        return _bankWorkerViewModel.SelectedClient.DepositBankAccount.isActivated &&
                            base.CanExecute(parameter);
                    case "Non-deposit Account":
                        return _bankWorkerViewModel.SelectedClient.NonDepositBankAccount.isActivated &&
                            base.CanExecute(parameter);
                    default:
                        return false;
                }
            }
            return false;
        }

        public override void Execute(object parameter)
        {
            try
            {
                switch (_bankWorkerViewModel.SelectedBankAccount)
                {
                    case "Deposit Account":
                        foreach (var client in _repository.Clients)
                        {
                            if (client == _bankWorkerViewModel.SelectedClient)
                            {
                                client.DepositBankAccount.CloseAccount();
                            }
                        }
                        MessageBox.Show(_bankWorkerViewModel.SelectedClient.ClientProtocol.GetAccountStatusChanged(
                            _bankWorkerViewModel.SelectedClient.DepositBankAccount), "Information",
                            MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        break;
                    case "Non-deposit Account":
                        foreach (var client in _repository.Clients)
                        {
                            if (client == _bankWorkerViewModel.SelectedClient)
                            {
                                client.NonDepositBankAccount.CloseAccount();
                            }
                        }
                        MessageBox.Show(_bankWorkerViewModel.SelectedClient.ClientProtocol.GetAccountStatusChanged(
                            _bankWorkerViewModel.SelectedClient.NonDepositBankAccount), "Information",
                            MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        break;
                    default:
                        break;
                }
                _bankWorkerViewModel.UpdateClients();
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
