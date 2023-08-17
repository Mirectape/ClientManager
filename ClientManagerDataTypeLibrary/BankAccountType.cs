using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagerDataTypeLibrary
{
    public static class BankAccountType
    {
        public static readonly string depositAccount = "Deposit Account";
        public static readonly string nonDepositAccount = "Non-deposit Account";
        public static readonly ObservableCollection<string> allTypes =
            new ObservableCollection<string>()
            {
                "Deposit Account", "Non-deposit Account"
            };
    }
}
