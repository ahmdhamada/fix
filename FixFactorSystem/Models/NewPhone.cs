using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace FixFactorSystem.Models
{
    public partial class NewPhone
    {
        public int Id { get; set; }
        public string MobName { get; set; }
        public string Ram { get; set; }
        public string Storage { get; set; }
        public int? Price { get; set; }
        public string Image { get; set; }
        [NotMapped]
        public IFormFile File { get; set; }
    }
}
