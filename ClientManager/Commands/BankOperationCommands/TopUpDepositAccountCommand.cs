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
    public class TopUpDepositAccountCommand : CommandBase
    {
        private readonly Repository _repository;
        private readonly BankWorkerViewModel _bankWorkerViewModel;
        private readonly NavigationService _navigationService;

        public TopUpDepositAccountCommand(Repository repository, BankWorkerViewModel bankWorkerViewModel,
            NavigationService navigationService)
        {
            _repository = repository;
            _bankWorkerViewModel = bankWorkerViewModel;
            _bankWorkerViewModel.PropertyChanged += onViewModelPropertyChanged;
            _navigationService = navigationService;
        }

        private void onViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(BankWorkerViewModel.TopUpSum))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return _bankWorkerViewModel.TopUpSum > 0 &&
                base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            if(_bankWorkerViewModel.SelectedClient != null)
            {
                _bankWorkerViewModel.SelectedClient.DepositBankAccount.DepositMoney += _bankWorkerViewModel.TopUpSum;
                _bankWorkerViewModel.SelectedClient.ClientProtocol.TopUpInfo = 
                    $"Deposit was topped up by Bank worker at {DateTime.Now} by {_bankWorkerViewModel.TopUpSum}";
                MessageBox.Show(_bankWorkerViewModel.SelectedClient.ClientProtocol.TopUpInfo, "Information",
                            MessageBoxButton.OK, MessageBoxImage.Exclamation);
                _bankWorkerViewModel.UpdateClients();
                _repository.SaveChanges();
                _navigationService.Navigate();
            }
        }
    }
}
