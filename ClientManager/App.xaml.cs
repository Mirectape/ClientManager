using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using ClientManager.Models;
using ClientManager.Store;
using ClientManager.ViewModels;
using ClientManager.Services;
using System.Collections.ObjectModel;

namespace ClientManager
{
    public static class DataWorkerType
    {
        public static readonly string manager = "Manager";
        public static readonly string consultant = "Consultant";
        public static readonly string bankWorker = "Bank worker";
        public static readonly ObservableCollection<string> allTypes =
            new ObservableCollection<string>()
            {
                "Manager", "Consultant", "Bank worker"
            };
    }

    public static class ClientDataCollections
    {
        public static readonly ObservableCollection<string> ClientDataCollectionForManager =
            new ObservableCollection<string>()
            {
                "First Name", "Second Name", "Paternal Name", "Phone Number", "Passport Number"
            };
        public static readonly ObservableCollection<string> ClientDataCollectionForConsultant =
            new ObservableCollection<string>()
            {
                "Phone Number"
            };
    }

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

    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly Repository _repository;
        private readonly NavigationStore _navigationStore;

        public App()
        {
            _repository = new Repository(5);
            _navigationStore = new NavigationStore();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            _navigationStore.CurrentViewModel = new MainMenuViewModel(new NavigationService(_navigationStore, CreateManagerViewModel), 
                new NavigationService(_navigationStore, CreateConsultantViewModel), new NavigationService(_navigationStore, CreateBankWorkerViewModel));

            MainWindow = new MainWindow()
            {
                DataContext = new MainViewModel(_navigationStore)
            };

            MainWindow.Show();
            base.OnStartup(e);
        }

        private ManagerViewModel CreateManagerViewModel()
        {
            return new ManagerViewModel(_repository, new NavigationService(_navigationStore, CreateAddClientViewModel), new NavigationService(_navigationStore, CreateManagerViewModel));
        }

        private ConsultantViewModel CreateConsultantViewModel()
        {
            return new ConsultantViewModel(_repository, new NavigationService(_navigationStore, CreateConsultantViewModel));
        }

        private AddClientViewModel CreateAddClientViewModel()
        {
            return new AddClientViewModel(_repository, new NavigationService(_navigationStore, CreateManagerViewModel));
        }

        private BankWorkerViewModel CreateBankWorkerViewModel()
        {
            return new BankWorkerViewModel(_repository, new NavigationService(_navigationStore, CreateBankWorkerViewModel));
        }
    }
}
