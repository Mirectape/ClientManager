using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientManager.Models;
using ClientManager.Exceptions;
using ClientManager.Services;
using ClientManager.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace ClientManager.Commands
{
    class AddClientCommand : CommandBase
    {
        private readonly Repository _repository;
        private readonly AddClientViewModel _addClientViewModel;
        private readonly NavigationService _navigationService;

        public AddClientCommand(Repository repository, AddClientViewModel addClientViewModel,
            NavigationService navigationService)
        {
            _repository = repository;
            _addClientViewModel = addClientViewModel;
            _navigationService = navigationService;
            _addClientViewModel.PropertyChanged += onViewModelPropertyChanged;
        }

        private void onViewModelPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if(e.PropertyName == nameof(AddClientViewModel.FirstName) ||
                e.PropertyName == nameof(AddClientViewModel.SecondName) || 
                e.PropertyName == nameof(AddClientViewModel.PassportNumber))
            {
                OnCanExecuteChanged();
            }
        }

        public override bool CanExecute(object parameter)
        {
            return !string.IsNullOrEmpty(_addClientViewModel.FirstName) &&
                !string.IsNullOrEmpty(_addClientViewModel.SecondName) &&
                !string.IsNullOrEmpty(_addClientViewModel.PassportNumber) &&
                base.CanExecute(parameter);
        }

        public override void Execute(object parameter)
        {
            Client newClient = new Client(
                _addClientViewModel.FirstName,
                _addClientViewModel.SecondName,
                _addClientViewModel.PaternalName,
                _addClientViewModel.PhoneNumber,
                _addClientViewModel.PassportNumber
                );

            try
            {
                _repository.AddClient(newClient);

                MessageBox.Show("Successfully added client", "Success",
                    MessageBoxButton.OK, MessageBoxImage.Information);

                _navigationService.Navigate();
            }
            catch (ClientConflictException)
            {

                MessageBox.Show("This passport number already exists", "Error",
                    MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
