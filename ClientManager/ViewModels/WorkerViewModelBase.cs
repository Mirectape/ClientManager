using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientManager.Models;

namespace ClientManager.ViewModels
{
    public enum DataType
    {
        FirstName,
        SecondName,
        PaternalName,
        PhoneNumber,
        PassportNumber,
        DepositAccount,
        NonDepositAccount
    }

    public interface WorkerViewModelBase
    {
        Repository Repository { get; set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="selectedClient"></param>
        /// <param name="param">parameter to add as change</param>
        /// <param name="dataType">what to change</param>
        void ChangeClientData(Client selectedClient, object param, DataType dataType);
    }
}
