using System;
using System.Collections.Generic;

namespace FixFactorSystem.Models
{
    public partial class Covers
    {
        public int CoverId { get; set; }
        public string CoverName { get; set; }
        public string CoverColor { get; set; }
        public int? CoverPrice { get; set; }
        public string CoverImg { get; set; }
    }
}
