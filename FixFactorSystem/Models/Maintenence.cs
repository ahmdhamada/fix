using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FixFactorSystem.Models
{
    public partial class Maintenence
    {
        public int DamageId { get; set; }
        public string DamMobName { get; set; }
        public string DamageDescription { get; set; }
        public string DamImg { get; set; }
        public string PhoneOwner { get; set; }
        public int PhoneNumber { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
