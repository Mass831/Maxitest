using ENTITIES;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Threading.Tasks;
using static MAXIUI.Repositories.MethodRepository;

namespace MAXIUI.Repositories
{
    public class BeneficiaryRepository
    {
        private readonly String _ApiBaseAdress;
        private readonly String _Url;

        public BeneficiaryRepository()
        {
            _ApiBaseAdress = ConfigurationManager.AppSettings["ApiUrl"];
            _Url = "api/Beneficiary";
        }

        public async Task<List<Beneficiary>?> GetBeneficiariesAsync(int Id)
        {
            var apiService = new AgentService<List<Beneficiary>, Response>();
            string url = _Url + "/" + Id.ToString();
            ApiParametersRepository<List<Beneficiary>> aparameters = new ApiParametersRepository<List<Beneficiary>>(_ApiBaseAdress, url, Method.Get, MediaTypes.applicationJson);
            await apiService.GetAsync(aparameters);
            var resp = apiService.resp;
            return resp.Result != null ? JsonConvert.DeserializeObject<List<Beneficiary>>(resp.Result.ToString()) : new List<Beneficiary>();
        }
        public async Task<List<Beneficiary>?> GetSumPercentageBeneficiariesAsync(int Id)
        {
            var apiService = new AgentService<List<Beneficiary>, Response>();
            string url = _Url + "/SumPercentage/" + Id.ToString();
            ApiParametersRepository<List<Beneficiary>> aparameters = new ApiParametersRepository<List<Beneficiary>>(_ApiBaseAdress, url, Method.Get, MediaTypes.applicationJson);
            await apiService.GetAsync(aparameters);
            var resp = apiService.resp;
            return resp.Result != null ? JsonConvert.DeserializeObject<List<Beneficiary>>(resp.Result.ToString()) : new List<Beneficiary>();
        }


        public async Task<Response> DeleteBeneficiaryAsync(int Id)
        {
            var apiService = new AgentService<string, Response>();
            string url = _Url + "/" + Id.ToString();
            ApiParametersRepository<string> aparameters = new ApiParametersRepository<string>(_ApiBaseAdress, url, Method.Get, MediaTypes.applicationJson);
            await apiService.DeleteAsync(aparameters);
            return apiService.resp;
        }

        public async Task<Response> UpdateBeneficiaryAsync(Beneficiary ben)
        {
            var apiService = new AgentService<Beneficiary, Response>();
            ApiParametersRepository<Beneficiary> aparameters = new ApiParametersRepository<Beneficiary>(_ApiBaseAdress, _Url, Method.Get, MediaTypes.applicationJson, ben);
            await apiService.PutAsync(aparameters);
            return apiService.resp;
        }

        public async Task<Response> AddBeneficiaryAsync(Beneficiary ben)
        {
            var apiService = new AgentService<Beneficiary, Response>();
            ApiParametersRepository<Beneficiary> aparameters = new ApiParametersRepository<Beneficiary>(_ApiBaseAdress, _Url, Method.Get, MediaTypes.applicationJson, ben);
            await apiService.PostAsync(aparameters);
            return apiService.resp;
        }
    }
}
