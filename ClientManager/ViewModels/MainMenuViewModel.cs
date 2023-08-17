using ClientManager.Services;
using ClientManager.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ClientManagerDataTypeLibrary;

namespace ClientManager.ViewModels
{
    public class MainMenuViewModel: ViewModelBase
    {
        private readonly ObservableCollection<string> _cmbx_dataWorker;
        public IEnumerable<string> Cmbx_dataWorker => _cmbx_dataWorker;

        private string _selectedDataWorker;
        public string SelectedDataWorker
        {
            get { return _selectedDataWorker; }
            set
            {
                _selectedDataWorker = value;
                OnPropertyChanged(nameof(SelectedDataWorker));
            }
        }

        public ICommand ChooseDataWorkerCommand { get; }

        public MainMenuViewModel(NavigationService CreateManagerViewModel, NavigationService CreateConsultantViewModel, NavigationService CreateBankWorkerViewModel)
        {
            _cmbx_dataWorker = DataWorkerType.allTypes;
            ChooseDataWorkerCommand = new ChooseDataWorkerCommand(this, CreateManagerViewModel, CreateConsultantViewModel, CreateBankWorkerViewModel);
        }
    }
}
