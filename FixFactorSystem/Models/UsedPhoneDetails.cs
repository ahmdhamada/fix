using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FixFactorSystem.Models
{
    public partial class UsedPhoneDetails
    {
        public int Idu { get; set; }
        public string MobNameu { get; set; }
        public string Ramu { get; set; }
        public string Storageu { get; set; }
        public string Camerau { get; set; }
        public string Imgu { get; set; }
        public string Color { get; set; }
        public string Display { get; set; }
        public string Battary { get; set; }
        public string AddDetails { get; set; }
        public int Price { get; set; }

        [NotMapped]
        public IFormFile File { get; set; }
    }
}
