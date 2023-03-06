using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Domain.Entities
{
    public class Customer
    {
        [Key]
        public long ID { get; set; }
        public string FullName { get; set; }
        public string EmailID { get; set; }

    }
}
