using PRSServer.Models;
using PRSServer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PRSServer.Controllers
{
    public class VendorsController : ApiController {

		private PRSServerDbContext db = new PRSServerDbContext();

		//GET-ALL
		//indicates that a get method will be used to get this info vs. post which updates
		[HttpGet]
		[ActionName("List")] //this is the name the client will use to call this method
		public JsonResponse List() {
		return new JsonResponse {
			Data = db.Vendors.ToList()
		};
	}

	//GET-ONE
	[HttpGet]
	[ActionName("Get")]
	public JsonResponse Get(int? id) {
		if (id == null) {
			return new JsonResponse {
				Result = "Failed",
				Message = "Id does not exist"
			};
		}
		return new JsonResponse {
			Data = db.Vendors.Find(id)
		};
	}

	//POST
	[HttpPost]
	[ActionName("Create")]
	public JsonResponse Create(Vendor vendor) {
		if (vendor == null) {
			return new JsonResponse {
				Result = "Failed",
				Message = "Create requires an instance of Major"
			};
		}
		if (!ModelState.IsValid) {
			return new JsonResponse {
				Result = "Failed",
				Message = "Model state is invalid. See data.",
				Error = ModelState
			};
		}

		db.Vendors.Add(vendor);
		db.SaveChanges();
		return new JsonResponse {
			Message = "Create successful.",
			Data = vendor
		};
	}

	//CHANGE
	[HttpPost]
	[ActionName("Change")]
	public JsonResponse Change(Vendor vendor) {
		if (vendor == null) {
			return new JsonResponse {
				Result = "Failed",
				Message = "Create requires an instance of Major"
			};
		}
		if (!ModelState.IsValid) {
			return new JsonResponse {
				Result = "Failed",
				Message = "Model state is invalid. See data.",
				Error = ModelState
			};
		}
		db.Entry(vendor).State = System.Data.Entity.EntityState.Modified;
		db.SaveChanges();
		return new JsonResponse {
			Message = "Change successful.",
			Data = vendor
		};
	}

	//DELETE
	[HttpPost]
	[ActionName("Remove")]
	public JsonResponse Remove(Vendor vendor) {
		if (vendor == null) {
			return new JsonResponse {
				Result = "Failed",
				Message = "Create requires an instance of Major"
			};
		}
		if (!ModelState.IsValid) {
			return new JsonResponse {
				Result = "Failed",
				Message = "Model state is invalid. See data.",
				Error = ModelState
			};
		}
		db.Entry(vendor).State = System.Data.Entity.EntityState.Deleted;
		db.SaveChanges();
		return new JsonResponse {
			Message = "Remove successful.",
			Data = vendor
		};
	}

	//REMOVE/ID
	[HttpGet]
	[ActionName("RemoveId")]
	public JsonResponse Remove(int? id) {
		if (id == null)
			return new JsonResponse {
				Result = "Failed",
				Message = "RemoveId requires a Vendor.Id"
			};
		var vendor = db.Vendors.Find(id);
		if (vendor == null)
			return new JsonResponse {
				Result = "Failed",
				Message = $"No vendor has Id of {id}"
			};
		db.Vendors.Remove(vendor);
		db.SaveChanges();
		return new JsonResponse {
			Message = "Remove successful.",
			Data = vendor
		};
	}
}
}
