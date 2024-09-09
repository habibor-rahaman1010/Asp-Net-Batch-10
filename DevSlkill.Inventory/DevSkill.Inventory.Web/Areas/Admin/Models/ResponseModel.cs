using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class ResponseModel
    {
        public enum ResponseTypes
        {
            Success,
            Danger
        }

        public string? Message { get; set; }
        public ResponseTypes Type { get; set; }
    }
}
