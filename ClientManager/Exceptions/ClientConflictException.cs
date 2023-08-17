using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ClientManager.Models;

namespace ClientManager.Exceptions
{
    public class ClientConflictException : Exception
    {
        public Client ExistingClient { get; }
        public Client IncomingClient { get; }

        public ClientConflictException(Client existingClient, Client incomingClient)
        {
            ExistingClient = existingClient;
            IncomingClient = incomingClient;
        }
    }
}
