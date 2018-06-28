using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRSServer {
    public class PurchaseRequestLineitem
    {

        public int Id { get; set; }

        [StringLength(80)]
        [Required]
        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        public int ProductId { get; set; }
        public virtual Product Product { get; set; }

        public int PurchaseRequestId { get; set; }

		[JsonIgnore]
		public virtual PurchaseRequest PurchaseRequest { get; set; }
    }
}
