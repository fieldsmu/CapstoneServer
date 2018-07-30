using PRSServer.Models;
using PRSServer.Utility;
using PRSServer.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PRSServer.Controllers {

	[EnableCors(origins: "*", headers: "*", methods: "*")]

	public class PurchaseRequestLineitemsController : ApiController {

		private PRSServerDbContext db = new PRSServerDbContext();


		//calculate purchase requests total
		private void RecalcLineItemTotal(int purchaseRequestId) {
			var pr = db.PurchaseRequests.Find(purchaseRequestId);
			if (pr == null) return;
			var lines = db.PurchaseRequestLineitems.Where(li => li.PurchaseRequestId == purchaseRequestId);
			pr.Total = lines.Sum(li => li.Quantity * li.Product.Price);
			db.SaveChanges();
		}

		//GET-ALL
		//indicates that a get method will be used to get this info vs. post which updates
		[HttpGet]
		[ActionName("List")] //this is the name the client will use to call this method
		public JsonResponse List() {
			return new JsonResponse {
				Data = db.PurchaseRequestLineitems.ToList()
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
				Data = db.PurchaseRequestLineitems.Find(id)
			};
		}

		//POST
		[HttpPost]
		[ActionName("Create")]
		public JsonResponse Create(PurchaseRequestLineitem purchaserequestlineitem) {
			if (purchaserequestlineitem == null) {
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

			db.PurchaseRequestLineitems.Add(purchaserequestlineitem);
			db.SaveChanges();
			RecalcLineItemTotal(purchaserequestlineitem.PurchaseRequestId);
			return new JsonResponse {
				Message = "Create successful.",
				Data = purchaserequestlineitem
			};
		}

		//CHANGE
		[HttpPost]
		[ActionName("Change")]
		public JsonResponse Change(PurchaseRequestLineitem purchaserequestlineitem) {
			if (purchaserequestlineitem == null) {
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
			db.Entry(purchaserequestlineitem).State = System.Data.Entity.EntityState.Modified;
			db.SaveChanges();
			RecalcLineItemTotal(purchaserequestlineitem.PurchaseRequestId);
			return new JsonResponse {
				Message = "Change successful.",
				Data = purchaserequestlineitem
			};
		}

		//DELETE
		[HttpPost]
		[ActionName("Remove")]
		public JsonResponse Remove(PurchaseRequestLineitem purchaserequestlineitem) {
			if (purchaserequestlineitem == null) {
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
			db.Entry(purchaserequestlineitem).State = System.Data.Entity.EntityState.Deleted;
			db.SaveChanges();
			RecalcLineItemTotal(purchaserequestlineitem.PurchaseRequestId);
			return new JsonResponse {
				Message = "Remove successful.",
				Data = purchaserequestlineitem
			};
		}

		//REMOVE/ID
		[HttpGet]
		[ActionName("RemoveId")]
		public JsonResponse Remove(int? id) {
			if (id == null)
				return new JsonResponse {
					Result = "Failed",
					Message = "RemoveId requires a PurchaseRequestLineitem.Id"
				};
			var purchaserequestlineitem = db.PurchaseRequestLineitems.Find(id);
			if (purchaserequestlineitem == null)
				return new JsonResponse {
					Result = "Failed",
					Message = $"No purchaserequestlineitem has Id of {id}"
				};
			db.PurchaseRequestLineitems.Remove(purchaserequestlineitem);
			db.SaveChanges();
			return new JsonResponse {
				Message = "Remove successful.",
				Data = purchaserequestlineitem
			};
		}

		//REMOVE/ID
		[HttpGet]
		[ActionName("PurchaseOrder")]
		public JsonResponse PurchaseOrder(int? id) {
			if (id == null)
				return new JsonResponse {
					Result = "Failed",
					Message = "Purchase order requires a Vendor Id"
				};
			List<PurchaseRequestLineitem> allprlis = new List<PurchaseRequestLineitem>();
			allprlis=db.PurchaseRequestLineitems.ToList();
			List<PurchaseRequestLineitem> filteredpurchaserequestlineitems = new List<PurchaseRequestLineitem>();
			foreach (PurchaseRequestLineitem apr in allprlis) {
				if (apr.Product.VendorId == id && apr.PurchaseRequest.Status == "Approved") {
					filteredpurchaserequestlineitems.Add(apr);
				}
			}

			Dictionary<int, PurchaseOrder> quantityCounter = new Dictionary<int, PurchaseOrder>();
			foreach (PurchaseRequestLineitem fprli in filteredpurchaserequestlineitems) {
				if(quantityCounter.ContainsKey(fprli.ProductId)) {
					quantityCounter[fprli.ProductId].Quantity += fprli.Quantity;
				} else {
					PurchaseOrder newPO = new PurchaseOrder();
					newPO.Name = fprli.Product.Name;
					newPO.PId = fprli.ProductId;
					newPO.Quantity = fprli.Quantity;
					newPO.Price = fprli.Product.Price;
					newPO.Product = fprli.Product;
					quantityCounter.Add(fprli.ProductId, newPO);
				}
			}

			return new JsonResponse {
				Message = "Get purchase order successful",
				Data = quantityCounter.Values
			};
		}
	}
}