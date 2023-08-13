using ClientManager.Services;
using ClientManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.Commands
{
    public class ChooseDataWorkerCommand : CommandBase
    {
        private readonly MainMenuViewModel _mainMenuViewModel; //selected dataWorker
        private readonly NavigationService _createManagerViewModel;
        private readonly NavigationService _createConsultantViewModel;
        private readonly NavigationService _createBankWorkerViewModel;

        public ChooseDataWorkerCommand(MainMenuViewModel mainMenuViewModel, NavigationService createManagerViewModel, 
            NavigationService createConsultantViewModel)
        {
            _mainMenuViewModel = mainMenuViewModel;
            _createManagerViewModel = createManagerViewModel;
            _createConsultantViewModel = createConsultantViewModel;
        }

        public override void Execute(object parameter)
        {
            switch (_mainMenuViewModel.SelectedDataWorker)
            {
                case "Manager":
                    _createManagerViewModel.Navigate();
                    break;
                case "Consultant":
                    _createConsultantViewModel.Navigate();
                    break;
                case "Bank Worker":
                    break;
                default:
                    break;
            }
        }
    }
}
