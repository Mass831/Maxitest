using ENTITIES;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MAXIAPI.Interfaces
{
    public interface IBeneficiary
    {
        Task<Beneficiary> Add(Beneficiary Beneficiary);
        Task<Beneficiary> Update(Beneficiary Beneficiary);
        Task<string> Delete(int BeneficiaryId);
        Task<Beneficiary> Get(int BeneficiaryId);
        Task<List<Beneficiary>> GetSumPercentajeByEmployeeId(int EmployeeId);
        Task<List<Beneficiary>> GetBeneficiariesByEmployeeId(int EmployeeId);
    }
}
