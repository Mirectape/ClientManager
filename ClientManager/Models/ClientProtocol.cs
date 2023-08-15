using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.Models
{
    public class ClientProtocol<T>
    {
        private DepositBankAccount<T> _depositBankAccount;
        private NonDepositBankAccount<T> _nonDepositBankAccount;

        //Client personal data
        public DateTime TimeDataChanged { get; set; }
        public string WhatChanged { get; set; }
        public string WhoChanged { get; set; }

        //Bank account data 
        public string TopUpInfo { get; set; }
        public string MoneyTransferInfo { get; set; }
        public string MoneyReceptionInfo { get; set; }

        public ClientProtocol(DepositBankAccount<T> depositBankAccount, NonDepositBankAccount<T> nonDepositBankAccount)
        {
            _depositBankAccount = depositBankAccount;
            _nonDepositBankAccount = nonDepositBankAccount;
        }

        public string GetWhoChangedPersonalDataLast()
        {
            if(WhatChanged != null)
            {
                return $"{WhoChanged} changed {WhatChanged} at {TimeDataChanged}";
            }
            else
            {
                return "No information on that account";
            }
        }

        public string GetAccountStatusInfo(BankAccount<T> bankAccount)
        {
            if (bankAccount.isActivated == true)
            {
                return $"Enabled";
            }
            else if (bankAccount.isActivated == false)
            {
                return $"Disabled";
            }
            else
            {
                return null;
            }
        }

        public string GetAccountStatusChanged(BankAccount<T> bankAccount)
        {
            if(bankAccount.isActivated == true)
            {
                return $"This account is successfully activated";
            }
            else if(bankAccount.isActivated == false)
            {
                return $"This account is closed";
            }
            else
            {
                return null;
            }
        }
    }
}
