using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FixFactorSystem.Models
{
    public partial class NewPhoneDetails
    {
        public int Id { get; set; }
        public string MobName { get; set; }
        public string Ram { get; set; }
        public string Storage { get; set; }
        public string Camera { get; set; }
        public string Image { get; set; }
        public string Color { get; set; }
        public string Display { get; set; }
        public string Battary { get; set; }
        public string AddDetails { get; set; }
        public int? Price { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
