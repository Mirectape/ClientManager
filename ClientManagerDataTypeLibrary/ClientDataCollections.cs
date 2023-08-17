using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagerDataTypeLibrary
{
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
}
