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

namespace ClientManager
{
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
            _navigationStore.CurrentViewModel = new ManagerViewModel(_repository, new NavigationService(_navigationStore, CreateAddClientViewModel), 
                new NavigationService(_navigationStore, CreateManagerViewModel));

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

        private AddClientViewModel CreateAddClientViewModel()
        {
            return new AddClientViewModel(_repository, new NavigationService(_navigationStore, CreateManagerViewModel));
        }
    }
}
