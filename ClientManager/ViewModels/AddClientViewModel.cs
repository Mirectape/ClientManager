using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ClientManager.Models;
using ClientManager.Services;
using ClientManager.Commands;

namespace ClientManager.ViewModels
{
    public class AddClientViewModel : ViewModelBase
    {
        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string _secondName;
        public string SecondName
        {
            get { return _secondName; }
            set
            {
                _secondName = value;
                OnPropertyChanged(nameof(SecondName));
            }
        }

        private string _parternalName;
        public string PaternalName
        {
            get { return _parternalName; }
            set
            {
                _parternalName = value;
                OnPropertyChanged(nameof(PaternalName));
            }
        }

        private string _phoneNumber;
        public string PhoneNumber
        {
            get { return _phoneNumber; }
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        private string _passportNumber;
        public string PassportNumber
        {
            get { return _passportNumber; }
            set
            {
                _passportNumber = value;
                OnPropertyChanged(nameof(PassportNumber));
            }
        }

        public ICommand SubmitCommand { get; }
        public ICommand CancelCommand { get; }

        public AddClientViewModel(Repository repository, NavigationService navigationService)
        {
            SubmitCommand = new AddClientCommand(repository, this, navigationService);
            CancelCommand = new NavigateCommand(navigationService);
        }
    }
}
