using ClientManager.Models;
using ClientManager.Services;
using ClientManager.Commands;
using ClientManager.Commands.BankOperationCommands;
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
    class BankWorkerViewModel : WorkerViewModelBase
    {
        private readonly ObservableCollection<string> _cmbx_bankAccounts;
        public IEnumerable<string> Cmbx_bankAccounts => _cmbx_bankAccounts;

        #region Account Manager
        private string _selectedBankAccount;
        public string SelectedBankAccount
        {
            get { return _selectedBankAccount; }
            set
            {
                _selectedBankAccount = value;
                OnPropertyChanged(nameof(SelectedBankAccount));
            }
        }

        public ICommand OpenAccountCommand;
        public ICommand CloseAccountCommand;
        #endregion

        #region Top up account
        private string _topUpSum;
        public string TopUpSum
        {
            get
            {
                return _topUpSum;
            }
            set
            {
                _topUpSum = value;
                OnPropertyChanged(nameof(ClientParameter));
            }
        }

        public ICommand TopUpToDepositAccountCommand;
        public ICommand TopUpToNonDepositAccountCommand;
        #endregion

        #region Money Transfer
        private string _transferSum;
        public string TransferSum
        {
            get
            {
                return _transferSum;
            }
            set
            {
                _transferSum = value;
                OnPropertyChanged(nameof(ClientParameter));
            }
        }

        private Client _selectedClientToReceiveTransfer;
        public Client SelectedClientToReceiveTransfer
        {
            get 
            {
                return _selectedClientToReceiveTransfer;
            }
            set
            {
                _selectedClientToReceiveTransfer = value;
            }
        }

        private string _selectedBankAccountToReceiveTransfer;
        public string SelectedBankAccountToReceiveTransfer
        {
            get
            {
                return _selectedBankAccountToReceiveTransfer;
            }
            set
            {
                _selectedBankAccountToReceiveTransfer = value;
                OnPropertyChanged(nameof(SelectedBankAccountToReceiveTransfer));
            }
        }

        public ICommand CommitTransferCommand;
        #endregion

        public BankWorkerViewModel(Repository repository, NavigationService renewBankWorkerViewModelService) : base(repository)
        {
            _cmbx_bankAccounts = BankAccountType.allTypes;
            OpenAccountCommand = new OpenAccountCommand();
            CloseAccountCommand = new CloseAccountCommand();
        }
    }
}
