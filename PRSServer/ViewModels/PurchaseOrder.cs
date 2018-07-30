using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRSServer.ViewModels {
	public class PurchaseOrder {

		public string Name { get; set; }
		public int PId { get; set; }
		public int Quantity { get; set; }
		public decimal Price { get; set; }
		public virtual Product Product { get; set; }
	}
}