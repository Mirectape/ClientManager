using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientManager.Exceptions;

namespace ClientManager.Models
{
    public class Repository
    {
        static Random randomizer; // to generate passport and phone numbers 

        static Repository()
        {
            randomizer = new Random();
        }

        /// <summary>
        /// Random collection of first names from the internal data base
        /// </summary>
        private List<string> _firstNames;
        
        /// <summary>
        /// Random collection of second names from the internal data base
        /// </summary>
        private List<string> _secondNames;

        /// <summary>
        /// Random collection of paternal names from the internal data base
        /// </summary>
        private List<string> _paternalNames;

        private List<Client> _clients;
        public List<Client> Clients
        {
            get => _clients;
        }

        private readonly List<Department> _departments; 

        private string _pathToClients;
        private string _pathToDepartments;
        private string _pathToGenericFirstNames;
        private string _pathToGenericSecondNames;
        private string _pathToGenericPathernalNames;

        /// <summary>
        /// Creates repository. Clients are generated randomly if no file with clients is found 
        /// </summary>
        /// <param name="count">Number of clients to random-generate</param>
        public Repository(int count)
        {
            _pathToClients = "C:/Users/user/source/repos/ClientManager/clientsData.json";
            _pathToDepartments = "C:/Users/user/source/repos/ClientManager/departmentsData.json";
            _pathToGenericFirstNames = "C:/Users/user/source/repos/ClientManager/GenericFirstNames.json";
            _pathToGenericSecondNames = "C:/Users/user/source/repos/ClientManager/GenericSecondNames.json";
            _pathToGenericPathernalNames = "C:/Users/user/source/repos/ClientManager/GenericPaternalNames.json";

            _clients = new List<Client>();

            if(!File.Exists(_pathToClients))
            {
                GenerateClients(count);
            }
            else
            {
                _clients = this.LoadData<List<Client>>(_pathToClients);
            }

            if (!File.Exists(_pathToDepartments))
            {
                _departments = new List<Department>()
                {
                    new Department(1, "Department_1"),
                    new Department(2, "Department_2")
                };
                this.SaveData(_departments, _pathToDepartments);
            }
            else
            {
                _departments = this.LoadData<List<Department>>(_pathToDepartments);
            }
        }

        public void AddClient(Client anotherClient)
        {
            foreach (var existingClient in _clients)
            {
                if(existingClient.Conflicts(anotherClient))
                {
                    throw new ClientConflictException(existingClient, anotherClient);
                }
            }

            _clients.Add(anotherClient);
        }

        /// <summary>
        /// Replaces selected client with newly assigned replacement client
        /// </summary>
        /// <param name="selectedClient">client to select</param>
        /// <param name="replacementClient">replaces selected client</param>
        public void ReplaceClient(Client selectedClient, Client replacementClient)
        {
            for (int i = 0; i < _clients.Count; i++)
            {
                if(_clients[i] == selectedClient)
                {
                    _clients[i] = replacementClient;
                }
            }
        }

        public void SortClients()
        {
            _clients = _clients.OrderBy(n => n.SecondName).ToList<Client>();
        }

        public IEnumerable<Client> GetAllClients() => _clients;

        public IEnumerable<Department> GetAllDepartments() => _departments;

        private void GenerateClients(int count)
        {
            _firstNames = LoadData<List<string>>(_pathToGenericFirstNames);
            _secondNames = LoadData<List<string>>(_pathToGenericSecondNames);
            _paternalNames = LoadData<List<string>>(_pathToGenericPathernalNames);

            for (int i = 0; i < count; i++)
            {
                _clients.Add(new Client(
                    _firstNames[Repository.randomizer.Next(_firstNames.Count())],
                    _secondNames[Repository.randomizer.Next(_secondNames.Count())],
                    _paternalNames[Repository.randomizer.Next(_paternalNames.Count())],
                    ((long)(Repository.randomizer.Next(1000000000, 1111111111)) * (long)(Repository.randomizer.Next(13, 20))).ToString(),
                    ((long)(Repository.randomizer.Next(1000000000, 1111111111)) * (long)(Repository.randomizer.Next(3, 10))).ToString()
                    ));
            }
            this.SaveData(_clients, _pathToClients);
        }

        private void SaveData<T>(T data, string path)
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }
            FileStream stream = File.Create(path);
            stream.Close();
            File.WriteAllText(path, JsonConvert.SerializeObject(data));
        }

        private T LoadData<T>(string path)
        {
            if (!File.Exists(path))
            {
                throw new FileNotFoundException($"{path} doesn't exist");
            }
            else
            {
                try
                {
                    var data = JsonConvert.DeserializeObject<T>(File.ReadAllText(path));
                    return data;
                }
                catch (Exception e)
                {
                    Console.WriteLine($"Failed load data due to: {e.Message}");
                    throw e;
                }
            }
        }

        public void SaveChanges() => SaveData<List<Client>>(_clients, _pathToClients);
    }
}
