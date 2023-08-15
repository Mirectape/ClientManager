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
    public class BankWorkerViewModel : WorkerViewModelBase
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

        public ICommand OpenAccountCommand { get; }
        public ICommand CloseAccountCommand { get; }
        #endregion

        #region Top up account
        private int _topUpSum;
        public int TopUpSum
        {
            get
            {
                return _topUpSum;
            }
            set
            {
                _topUpSum = value;
                OnPropertyChanged(nameof(TopUpSum));
            }
        }

        public ICommand TopUpDepositAccountCommand { get; }
        public ICommand TopUpNonDepositAccountCommand { get; }
        #endregion

        #region Money Transfer
        private int _transferSum;
        public int TransferSum
        {
            get
            {
                return _transferSum;
            }
            set
            {
                _transferSum = value;
                OnPropertyChanged(nameof(TransferSum));
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
                OnPropertyChanged(nameof(SelectedClientToReceiveTransfer));
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

        public ICommand CommitTransferCommand { get; }
        #endregion

        public BankWorkerViewModel(Repository repository, NavigationService renewBankWorkerViewModelService) : base(repository)
        {
            _cmbx_bankAccounts = BankAccountType.allTypes;
            OpenAccountCommand = new OpenAccountCommand(repository, this, renewBankWorkerViewModelService);
            CloseAccountCommand = new CloseAccountCommand(repository, this, renewBankWorkerViewModelService);
            TopUpDepositAccountCommand = new TopUpDepositAccountCommand(repository, this, renewBankWorkerViewModelService);
            TopUpNonDepositAccountCommand = new TopUpNonDepositAccountCommand(repository, this, renewBankWorkerViewModelService);
            CommitTransferCommand = new CommitTransferCommand(repository, this, renewBankWorkerViewModelService);
        }
    }
}
