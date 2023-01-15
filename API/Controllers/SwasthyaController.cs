using API.Extensions;
using BL.Operations;
using Common.ApiModels;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class SwasthyaController : ControllerBase
    {
        IndividualOperations individualOperations;

        public SwasthyaController(IndividualOperations individualOperations)
        {
            this.individualOperations = individualOperations;
        }

        [HttpPut]
        [Route("RegisterIndividual/email/{email}/password/{password}/name/{name}/phoneNumber/{phoneNumber}/dateOfBirth/{dateOfBirth}")]
        public async Task<ActionResult<IndividualResponseModel>> AddIndividualItem(string email, string password, string name, string phoneNumber, string dateOfBirth)
        {
            dateOfBirth = Uri.UnescapeDataString(dateOfBirth);

            //string format = "dd/MM/yyyy";

            //var isDate = DateTime.TryParseExact(dateOfBirth, "M-d-yyyy h:mm tt zzz", CultureInfo.CurrentCulture, DateTimeStyles.None, out var parsedDate);
            //if (!isDate)
            //{
            //    return BadRequest("Invalid Date of Birth");
            //}

            var individual = await individualOperations.AddIndividualDataAsync(email, password, name, phoneNumber, dateOfBirth);
            return Ok(individual.ToAPIModel());
        }
    }
}