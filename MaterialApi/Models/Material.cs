using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace MaterialApi.Models
{
    public class Material
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool Hidden { get; set; }
        public string Author { get; set; }
        public string Notes { get; set; }
        public KindOfPhase Phase { get; set; }
    }
}
