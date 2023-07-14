using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.Models
{
    public class Account
    {
        public bool isActivated { get; set; }

        public Account()
        {
            isActivated = false;
        }

        public virtual void OpenAccount()
        {
            isActivated = true;
        }
        public virtual void CloseAccount()
        {
            isActivated = false;
        }
    }

    public class BankAccount<T>: Account
    {
        public string NameOfBank { get; set; }
        public T depositMoney { get; set; }
    }

    public class DepositBankAccount<T>: BankAccount<T>
    {
        
    }

    public class NonDepositBankAccount<T>: BankAccount<T>
    {

    }
}
