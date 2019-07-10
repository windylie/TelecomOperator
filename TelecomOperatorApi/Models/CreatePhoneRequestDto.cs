using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TelecomOperatorApi.Models
{
    public class CreatePhoneRequestDto
    {
        public string PhoneNo { get; set; }
    }
}
