using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace ContestApplication.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the ContestApplicationUser class
    public class ContestApplicationUser : IdentityUser
    {
        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string FirstName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string LastName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string CollegeName { get; set; }

        [PersonalData]
        [Column(TypeName = "nvarchar(100)")]
        public string ApplicantType { get; set; }
    }
}
