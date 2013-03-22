using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace CardShop.Models
{
    [MetadataType(typeof(webpages_RolesMetaData))]
    public partial class webpages_Roles
    {
    }

    public class webpages_RolesMetaData
    {
        [Key]
        public int RoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<User> UserProfiles { get; set; }
    }
}