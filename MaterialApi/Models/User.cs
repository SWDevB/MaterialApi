using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MaterialApi.Models
{
    public class User
    {
        [Required]
        public Credentials Credentials { get; set; }
        public bool IsAdmin { get; set; }
    }
}
