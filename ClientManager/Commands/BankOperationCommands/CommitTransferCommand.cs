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
           // if()
        }

        public override void Execute(object parameter)
        {
            throw new NotImplementedException();
        }
    }
}
