using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRSServer {
	public class Product {

		public int Id { get; set; }


		[Required]
		[StringLength(50)]
		public string PartNumber { get; set; }

		[Required]
		[StringLength(150)]
		public string Name { get; set; }

		[Required]
		public decimal Price { get; set; }

		[Required]
		[StringLength(25)]
		public string Unit { get; set; }

		[StringLength(255)]
		public string PhotoPath { get; set; }

		public bool Active { get; set; }// = true;

		public int VendorId { get; set; } //EF knows this is a foreign key because it is a concatenation of a class name "Vendor" and "Id" (i.e., VendorId)
		public virtual Vendor Vendor { get; set; }	//creates a vendor variable which holds all of the vendor information for each instantatiation of a product

		public Product() { }

	}
}
