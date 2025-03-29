using Customer.Identity.Microservice.API.Services;
using Data.Microservice.App;
using Data.Microservice.Domain;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Data.Microservice.API.Controllers
{
    [ApiController]
    [Route("Data.Microservice.API/[controller]")]
    public class DataController : Controller
    {

        private readonly IDataServices _services;
        public DataController(IDataServices s)
        {
            _services = s;
        }

        [HttpGet]
        public ActionResult<List<CData>> AllData()
        {

            try
            {
                var result = _services.GetAllData();

                return Ok(result);

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
        
        [HttpPost]
        [Route("AllDataByID")]
        public ActionResult<List<CData>> AllDataByID(int id)
        {
            try
            {
                var result = _services.GetAllDataByCustomerID(id);

                return result;

            }catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }



        [HttpPost]
        [Route("newAccountData")]
        public async Task<ActionResult<string>> NewAccountData([FromQuery] CData c, [FromQuery] string email, [FromQuery] string subject, [FromQuery] string message, [FromQuery] int customerId)
        {

            try
            {
                var result = await _services.NewCustomerData(c,  email,  subject, message, customerId);

                var producer = new RabbitMQProducer();
                await producer.NotifyAccountCreationStageCompleted();


                return Ok(result);



            }catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }


        [HttpDelete]
        [Route("removeAnAccount")]
        public async Task<ActionResult<string>> RemoveAnAccount([FromQuery] string email, [FromQuery] string subject, [FromQuery] string message, [FromQuery] int customerId)
        {


            try
            {
                var result = await _services.DeleteCustomerData(email, subject, message, customerId);

                return Ok(result);

            }catch(Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut]
        [Route("updateAnAccount")]
        public async Task<ActionResult<string>> UpdateAnAccount(CData c, string email)
        {
            try
            {
                var result = await _services.UpdateCustomerData(c,email);

                return Ok(result);

            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}
