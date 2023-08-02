using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.Models
{
    public class Client
    {
        private static int _id;

        static Client()
        {
            _id = 0;
        }

        public int ClientID { get; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string PaternalName { get; set; }
        public string PhoneNumber { get; set; }
        public string PassportNumber { get; set; }
        public DepositBankAccount<int> DepositBankAccount { get; set; }
        public NonDepositBankAccount<int> NonDepositBankAccount { get; set; }

        public Client(string firstName, string secondName, string paternalName, 
            string phoneNumber, string passportNumber)
        {
            ClientID = NextID();
            FirstName = firstName;
            SecondName = secondName;
            PaternalName = paternalName;
            PhoneNumber = phoneNumber;
            PassportNumber = passportNumber;
            DepositBankAccount = new DepositBankAccount<int>();
            NonDepositBankAccount = new NonDepositBankAccount<int>();
        }

        public override string ToString()
        {
            return $"{this.FirstName} {this.SecondName}";
        }

        public override bool Equals(object obj)
        {
            return obj is Client anotherClient &&
                this.PassportNumber == anotherClient.PassportNumber;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(FirstName, SecondName, PaternalName,
                PhoneNumber, PassportNumber);
        }

        public bool Conflicts(Client anotherClient)
        {
            if(this.PassportNumber != anotherClient.PassportNumber)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public static bool operator ==(Client client_1, Client client_2)
        {
            if(client_1 is null && client_2 is null)
            {
                return true;
            }

            return !(client_1 is null) && client_1.Equals(client_2);
        }

        public static bool operator !=(Client client_1, Client client_2)
        {
            return !(client_1 == client_2);
        }

        private int NextID()
        {
            _id++;
            return _id;
        }

    }
}
