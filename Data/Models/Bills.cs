using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
  public class Bills
  {
    public Guid Id { get; set; }

    public DateTime CreateDate { get; set; }
    //note of bill not null
    [Required]
    public string Note { get; set; }
    //status 1->3
    [Range(1, 3, ErrorMessage = "Please enter valid integer Number")]
    public int Status { get; set; }

    public Guid IdUser { get; set; }
    public Guid IdCoupon { get; set; }

    public Guid IdPaymentMethod { get; set; }
    public Guid IdShipAdressMethod { get; set; }
    public Guid IdLocation { get; set; }

    public List<BillDetails> BillDetails { get; set; }
    public virtual Coupons Coupons { get; set; }
    public virtual Users Users { get; set; }

    public virtual PaymentMethods PaymentMethods { get; set; }

    public virtual ShipAdressMethod ShipAdressMethod { get; set; }
    public virtual Location Location { get; set; }
  }
}
