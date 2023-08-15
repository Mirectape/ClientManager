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
    public class CommitTransferCommand : CommandBase
    {
        private readonly Repository _repository;
        private readonly BankWorkerViewModel _bankWorkerViewModel;
        private readonly NavigationService _navigationService;

        public CommitTransferCommand(Repository repository, BankWorkerViewModel bankWorkerViewModel,
            NavigationService navigationService)
        {
            _repository = repository;
            _bankWorkerViewModel = bankWorkerViewModel;
            _bankWorkerViewModel.PropertyChanged += onViewModelPropertyChanged;
            _navigationService = navigationService;
        }

        private void onViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(BankWorkerViewModel.SelectedClientToReceiveTransfer) ||
                e.PropertyName == nameof(BankWorkerViewModel.SelectedBankAccountToReceiveTransfer) ||
                e.PropertyName == nameof(BankWorkerViewModel.TransferSum))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return !(_bankWorkerViewModel.SelectedClientToReceiveTransfer == null) &&
                !(_bankWorkerViewModel.SelectedBankAccountToReceiveTransfer == null) &&
                !(_bankWorkerViewModel.SelectedClient == null) &&
                _bankWorkerViewModel.TransferSum > 0 &&
                base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            switch (_bankWorkerViewModel.SelectedBankAccountToReceiveTransfer)
            {
                case "Deposit Account":
                    if(_bankWorkerViewModel.SelectedClient.DepositBankAccount.DepositMoney >= _bankWorkerViewModel.TransferSum)
                    {
                        _bankWorkerViewModel.SelectedClient.DepositBankAccount.DepositMoney -= _bankWorkerViewModel.TransferSum;
                        _bankWorkerViewModel.SelectedClientToReceiveTransfer.DepositBankAccount.DepositMoney += _bankWorkerViewModel.TransferSum;
                        MessageBox.Show("Success!", "Information",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        _bankWorkerViewModel.SelectedClient.ClientProtocol.MoneyTransferInfo =
                            $"Money transfer from this Dep account in amount of { _bankWorkerViewModel.TransferSum} at {DateTime.Now}";
                        _bankWorkerViewModel.SelectedClientToReceiveTransfer.ClientProtocol.MoneyReceptionInfo =
                            $"Money reception to this Dep account in amount of { _bankWorkerViewModel.TransferSum} at {DateTime.Now}";
                    }
                    else
                    {
                        MessageBox.Show("Not enough money!", "Information",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    break;
                case "Non-deposit Account":
                    if (_bankWorkerViewModel.SelectedClient.NonDepositBankAccount.DepositMoney >= _bankWorkerViewModel.TransferSum)
                    {
                        _bankWorkerViewModel.SelectedClient.NonDepositBankAccount.DepositMoney -= _bankWorkerViewModel.TransferSum;
                        _bankWorkerViewModel.SelectedClientToReceiveTransfer.NonDepositBankAccount.DepositMoney += _bankWorkerViewModel.TransferSum;
                        MessageBox.Show("Success!", "Information",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                        _bankWorkerViewModel.SelectedClient.ClientProtocol.MoneyTransferInfo =
                            $"Money transfer from this NonDep account in amount of { _bankWorkerViewModel.TransferSum} at {DateTime.Now}";
                        _bankWorkerViewModel.SelectedClientToReceiveTransfer.ClientProtocol.MoneyReceptionInfo =
                            $"Money reception to this NonDep account in amount of { _bankWorkerViewModel.TransferSum} at {DateTime.Now}";
                    }
                    else
                    {
                        MessageBox.Show("Not enough money!", "Information",
                        MessageBoxButton.OK, MessageBoxImage.Exclamation);
                    }
                    break;
                default:
                    break;
            }
            _bankWorkerViewModel.UpdateClients();
            _repository.SaveChanges();
            _navigationService.Navigate();
        }
    }
}
