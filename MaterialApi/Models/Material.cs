using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MaterialApi.Models
{
    public class Material
    {
        public string Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public bool Hidden { get; set; }
        [Required]
        public string Author { get; set; }
        [Required]
        public string Notes { get; set; }
        [JsonConverter(typeof(JsonStringEnumConverter))]
        [Required]
        public KindOfPhase Phase { get; set; }
    }
}
