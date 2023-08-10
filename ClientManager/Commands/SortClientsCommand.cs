using ClientManager.Models;
using ClientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.Commands
{
    class SortClientsCommand : CommandBase
    {
        private readonly Repository _repository;
        private readonly ManagerViewModel _workerViewModel;

        public SortClientsCommand(Repository repository, ManagerViewModel workerViewModel)
        {
            _repository = repository;
            _workerViewModel = workerViewModel;
        }

        public override void Execute(object parameter)
        {
            _repository.SortClients();
            _workerViewModel.UpdateClients();
            _repository.SaveChanges();
        }
    }
}
