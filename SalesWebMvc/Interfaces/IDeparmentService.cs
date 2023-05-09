using SalesWebMvc.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SalesWebMvc.Interfaces
{
    public interface IDepartmentService
    {
        List<Department> GetDepartments();

        int CreateDepartment(Department department);

       
    }
}
