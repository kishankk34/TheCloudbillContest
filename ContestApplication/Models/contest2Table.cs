using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContestApplication.Models
{
    public class contest2Table
    {
        [Key]
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(MAX)")]
        public string fileToUpload { get; set; }

        public bool submittedfile { get; set; }
    }
}
