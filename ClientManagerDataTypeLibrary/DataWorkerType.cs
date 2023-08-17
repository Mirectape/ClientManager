using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManagerDataTypeLibrary
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
}
