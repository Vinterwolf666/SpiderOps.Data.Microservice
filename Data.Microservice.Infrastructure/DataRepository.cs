using Data.Microservice.App;
using Data.Microservice.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Data.Microservice.Infrastructure
{
    public class DataRepository : IDataRepository
    {

        private readonly DataDbContext _dbContext;

        public DataRepository(DataDbContext dbContext)
        {

            _dbContext = dbContext;

        }


        public async Task<string> DeleteCustomerData(int id, string email, string text_message)
        {
            var dataID = await _dbContext.DataDomain.FirstOrDefaultAsync(a => a.CUSTOMERS_ID == id);
            string apiUrl = $"http://34.42.106.100/Customer.Notify.Microservice.API.Notify/SendARemoveAccountNotification?email={email}&text_message={text_message}&id={id}";
            using (HttpClient client = new HttpClient())

                if (dataID != null)
            {

                
                HttpResponseMessage response = await client.PostAsync(apiUrl,null);
                response.EnsureSuccessStatusCode(); 

                _dbContext.DataDomain.Remove(dataID);

                await _dbContext.SaveChangesAsync();

                return "Account removed successfully, and notification sent";
            }
            else
            {
                return "Invalid account, does not exist";
            }

        }

        public List<CData> GetAllData()
        {
            var data = _dbContext.DataDomain.ToList();

            return data;
        }

        public List<CData> GetAllDataByCustomerID(int id)
        {
            var data = _dbContext.DataDomain.Where(a => a.CUSTOMERS_ID == id).ToList();

            return data;
        }

        public async Task<string> NewCustomerData(CData c, string email)
        {
            // Guardar los datos en la base de datos
            await _dbContext.DataDomain.AddAsync(c);
            await _dbContext.SaveChangesAsync();

            // Obtener el ID del cliente
            var id = c.CUSTOMERS_ID;

            // Construir la URL del endpoint con parámetros en query
            string apiUrl = $"http://34.42.106.100/Customer.Notify.Microservice.API.Notify/SendACreationAccountNotification?email={email}&text_message=account_creation&id={id}";

            using (HttpClient client = new HttpClient())
            {
                try
                {
                    // Enviar la solicitud POST sin cuerpo porque el endpoint usa query parameters
                    HttpResponseMessage response = await client.PostAsync(apiUrl, null);
                    response.EnsureSuccessStatusCode(); // Lanza una excepción si hay error en la respuesta

                    return "Account created successfully. Notification sent.";
                }
                catch (HttpRequestException ex)
                {
                    return $"Account created, but failed to send notification: {ex.Message}";
                }
            }
        }


        public async Task<string> UpDateCustomerData(CData c)
        {
            var dataID = _dbContext.DataDomain.FirstOrDefault(a => a.CUSTOMERS_ID == c.CUSTOMERS_ID);

   

            if (dataID != null)
            {
                
                dataID.FIRSTNAME = c.FIRSTNAME;
                dataID.SECONDNAME = c.SECONDNAME;
                dataID.LASTNAME = c.LASTNAME;
                dataID.HOME_ADDRESS = c.HOME_ADDRESS;


               await _dbContext.SaveChangesAsync();

                return "Account updated successfully";
            }
            else
            {
                return "Invalid ID";
            }



        }
    }
}
