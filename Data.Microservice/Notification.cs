using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Customer.Notify.Microservice.Domain
{
    [Table("AccountNotification")]
    public class Notification
    {
        [Key]
        public int ID { get; set; }
        public int? CUSTOMER_ID { get; set; }

        public string EMAIL { get; set; }

        public string TEXT_MESSAGE { get; set; }

        public string? EMAIL_SUBJECT { get; set; }

        public DateTime? TIME_OF_CREATION { get; set; }
    }
}
