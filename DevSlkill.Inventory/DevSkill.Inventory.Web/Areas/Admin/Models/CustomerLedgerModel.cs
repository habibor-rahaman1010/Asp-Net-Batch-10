using DevSkill.Inventory.Domain.Dtos;
using DevSkill.Inventory.Domain.Entities.SalesEntities;
using DevSkill.Inventory.Infrastructure.RazorUtility;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DevSkill.Inventory.Web.Areas.Admin.Models
{
    public class CustomerLedgerModel
    {
        [Display(Name = "Customer")]
        public Guid? CustomerId { get; set; }
        public IList<SelectListItem> Customers { get; set; }

        [Display(Name = "From")]
        public DateTime FromDate { get; set; }

        [Display(Name = "To")]
        public DateTime ToDate { get; set; }

        /// <summary>Null until a customer has actually been chosen.</summary>
        public CustomerLedgerDto? Ledger { get; set; }

        public CustomerLedgerModel()
        {
            Customers = new List<SelectListItem>();

            // A year back, because a statement is usually read over a long stretch.
            FromDate = DateTime.Today.AddYears(-1);
            ToDate = DateTime.Today;
        }

        public void SetCustomerValues(IList<Customer> customers)
        {
            Customers = Utility.ConvertCustomers(customers);
        }
    }
}
