using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FixFactorSystem.Models
{
    public partial class UsedPhone
    {
        public int Idu { get; set; }
        public string MobNameu { get; set; }
        public string Ramu { get; set; }
        public string Storageu { get; set; }
        public int Priceu { get; set; }
        public string Imgu { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
