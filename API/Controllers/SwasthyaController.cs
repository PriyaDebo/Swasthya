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
        private IConfiguration configuration;

        public SwasthyaController(IConfiguration configuration, IndividualOperations individualOperations)
        {
            this.individualOperations = individualOperations;
            this.configuration = configuration;
        }

        [HttpPut]
        [Route("RegisterIndividual/email/{email}/name/{name}/phoneNumber/{phoneNumber}/dateOfBirth/{dateOfBirth}")]
        public async Task<ActionResult<IndividualResponseModel>> AddIndividualItem(string email, string name, string phoneNumber, string dateOfBirth)
        {
            dateOfBirth = Uri.UnescapeDataString(dateOfBirth);

            //string format = "dd/MM/yyyy";

            //var isDate = DateTime.TryParseExact(dateOfBirth, "M-d-yyyy h:mm tt zzz", CultureInfo.CurrentCulture, DateTimeStyles.None, out var parsedDate);
            //if (!isDate)
            //{
            //    return BadRequest("Invalid Date of Birth");
            //}

            var individual = await individualOperations.AddIndividualDataAsync(email, name, phoneNumber, dateOfBirth);
            return Ok(individual.ToAPIModel());
        }
    }
}