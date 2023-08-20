using ENTITIES;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAXIAPI.Interfaces
{
    public interface IEmployee
    {
        Task<Employee> Add(Employee employee);
        Task<Employee> Update(Employee employee);
        Task<string> Delete(int employeeId);
        Task<Employee> Get(int employeeId);
        Task<List<Employee>> GetEmployees();
    }
}
