using ClientManager.Models;
using ClientManager.Services;
using ClientManager.Commands;
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
        BankWorkerViewModel(Repository repository) : base(repository) 
        {

        }
    }
}
