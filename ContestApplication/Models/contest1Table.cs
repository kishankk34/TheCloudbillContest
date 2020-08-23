using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace ContestApplication.Areas.Identity.Data
{
    public class contest1Table
    {
        [Key]
        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstContestKey { get; set; }

        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string PossibleSolution { get; set; }

        [Required]
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Code { get; set; }

        public bool submitted { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string Email { get; set; }

    }
}
