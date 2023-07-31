using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientManager.Models
{
    public class Department
    {
        public int DepartmentID { get; set; }
        public string DepartmentName { get; set; }

        public Department(int id, string name)
        {
            DepartmentID = id;
            DepartmentName = name;
        }

        public override string ToString()
        {
            return $"{DepartmentName}";
        }

        public override bool Equals(object obj)
        {
            return obj is Department anotherDepartment &&
                this.DepartmentID == anotherDepartment.DepartmentID &&
                this.DepartmentName == anotherDepartment.DepartmentName;
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(DepartmentID, DepartmentName);
        }

        public static bool operator ==(Department department_1, Department department_2)
        {
            if(department_1 is null && department_2 is null)
            {
                return true;
            }
            return !(department_1 is null) && department_1.Equals(department_2);
        }

        public static bool operator !=(Department department_1, Department department_2)
        {
            return !(department_1 == department_2);
        }
    }
}
