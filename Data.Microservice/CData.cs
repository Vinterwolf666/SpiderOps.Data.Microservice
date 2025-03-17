using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Microservice.Domain
{
    [Table("CustomersData")]
    public class CData
    {
        [Key]
        public int CUSTOMERS_ID { get; set; }

        public string FIRSTNAME { get; set; }

        public string SECONDNAME { get; set; }

        public string LASTNAME { get; set; }

        public string HOME_ADDRESS { get; set; }
    }
}
