using ENTITIES;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using static MAXIUI.Repositories.MethodRepository;


namespace MAXIUI.Repositories
{
    public class EmployeeRepository
    {
        private readonly String _ApiBaseAdress;
        private readonly String _Url;

        public EmployeeRepository()
        {
            _ApiBaseAdress = ConfigurationManager.AppSettings["ApiUrl"];
            _Url = "api/Employee";
        }

        public async Task<List<Employee>?> GetEmployeesAsync()
        {
            var apiService = new AgentService<List<Employee>, Response>();
            ApiParametersRepository<List<Employee>> aparameters = new ApiParametersRepository<List<Employee>>(_ApiBaseAdress, _Url, Method.Get, MediaTypes.applicationJson);
            await apiService.GetAsync(aparameters);
            var resp = apiService.resp;
            return resp.Result != null ? JsonConvert.DeserializeObject<List<Employee>>(resp.Result.ToString()) : new List<Employee>();
            
        }

        public async Task<Response> DeleteEmployeeAsync(int Id)
        {
            var apiService = new AgentService<string, Response>();
            string url = _Url + "/" + Id.ToString();
            ApiParametersRepository<string> aparameters = new ApiParametersRepository<string>(_ApiBaseAdress, url, Method.Get, MediaTypes.applicationJson);
            await apiService.DeleteAsync(aparameters);
            return apiService.resp;

        }

        public async Task<Response> UpdateEmployeeAsync(Employee emp)
        {
            var apiService = new AgentService<Employee, Response>();
            ApiParametersRepository<Employee> aparameters = new ApiParametersRepository<Employee>(_ApiBaseAdress, _Url, Method.Get, MediaTypes.applicationJson, emp);
            await apiService.PutAsync(aparameters);
            return apiService.resp;

        }

        public async Task<Response> AddEmployeeAsync(Employee emp)
        {
            var apiService = new AgentService<Employee, Response>();
            ApiParametersRepository<Employee> aparameters = new ApiParametersRepository<Employee>(_ApiBaseAdress, _Url, Method.Get, MediaTypes.applicationJson, emp);
            await apiService.PostAsync(aparameters);
            return apiService.resp;

        }

    }
}
