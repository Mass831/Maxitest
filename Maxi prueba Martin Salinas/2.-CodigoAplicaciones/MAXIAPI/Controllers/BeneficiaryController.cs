

using ENTITIES;
using MAXIAPI.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace MAXIAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BeneficiaryController : ControllerBase
    {
        private IBeneficiary _beneficiary;

        public BeneficiaryController(IBeneficiary beneficiary)
        {

            _beneficiary = beneficiary;
        }

        [HttpGet("{idEmployee:int}")]
        public async Task<ActionResult<Response>> Get(int idEmployee)
        {
            return new Response { IsSuccess = true, Result = await _beneficiary.GetBeneficiariesByEmployeeId(idEmployee) };
        }
        
        [HttpGet("SumPercentage/{idEmployee:int}")]
        public async Task<ActionResult<Response>> GetSumPercentajaBeneficiaries(int idEmployee)
        {
            return new Response { IsSuccess = true, Result = await _beneficiary.GetSumPercentajeByEmployeeId(idEmployee) };
        }

        [HttpPost]
        public async Task<ActionResult<Response>> Post([FromBody] Beneficiary beneficiary)
        {
            var emp = await _beneficiary.Add(beneficiary);
            return emp != null ? new Response { IsSuccess = true, Message = "Beneficiary saved successfully", Result = emp } : new Response { IsSuccess = false, Message = "Error trying to add Beneficiary", Result = emp };
        }

        [HttpPut]
        public async Task<ActionResult<Response>> Put([FromBody] Beneficiary beneficiary)
        {
            var emp = await _beneficiary.Update(beneficiary);
            return emp != null ? new Response { IsSuccess = true, Message = "Beneficiary updated successfully", Result = beneficiary } : new Response { IsSuccess = false, Message = "Error trying to update Beneficiary", Result = beneficiary };
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<Response>> Delete(int id)
        {
            var message = await _beneficiary.Delete(id);
            return string.IsNullOrEmpty(message) ? new Response { IsSuccess = true, Message = "Beneficiary deleted successfully", Result = id } : new Response { IsSuccess = false, Message = "Error trying to delete Beneficiary", Result = id };
        }

    }
}
