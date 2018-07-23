using PRSServer.Models;
using PRSServer.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PRSServer.Controllers
{
	[EnableCors(origins: "*", headers: "*", methods: "*")]

	public class ProductsController : ApiController {

		private PRSServerDbContext db = new PRSServerDbContext();

		//GET-ALL
		//indicates that a get method will be used to get this info vs. post which updates
		[HttpGet]
		[ActionName("List")] //this is the name the client will use to call this method
		public JsonResponse List() {
			return new JsonResponse {
				Data = db.Products.ToList()
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
				Data = db.Products.Find(id)
			};
		}

		//POST
		[HttpPost]
		[ActionName("Create")]
		public JsonResponse Create(Product product) {
			if (product == null) {
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

			db.Products.Add(product);
			db.SaveChanges();
			return new JsonResponse {
				Message = "Create successful.",
				Data = product
			};
		}

		//CHANGE
		[HttpPost]
		[ActionName("Change")]
		public JsonResponse Change(Product product) {
			if (product == null) {
				return new JsonResponse {
					Result = "Failed",
					Message = "Create requires an instance of Major"
				};
			}

			var newProduct = db.Products.Find(product.Id);
			if (newProduct == null) {
				return new JsonResponse {
					Result = "Failed",
					Message = "Cannot find product in the database"
				};
			}
				newProduct.PartNumber = product.PartNumber;
				newProduct.Name = product.Name;
				newProduct.Price = product.Price;
				newProduct.Unit = product.Unit;
				newProduct.PhotoPath = product.PhotoPath;
				newProduct.Active = product.Active;
				newProduct.VendorId = product.VendorId;

			if (!ModelState.IsValid) {
				return new JsonResponse {
					Result = "Failed",
					Message = "Model state is invalid. See data.",
					Error = ModelState
				};
			}
			db.SaveChanges();
			return new JsonResponse {
				Message = "Change successful.",
				Data = newProduct
			};
		}

		//DELETE
		[HttpPost]
		[ActionName("Remove")]
		public JsonResponse Remove(Product product) {
			if (product == null) {
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
			db.Entry(product).State = System.Data.Entity.EntityState.Deleted;
			db.SaveChanges();
			return new JsonResponse {
				Message = "Remove successful.",
				Data = product
			};
		}

		//REMOVE/ID
		[HttpGet]
		[ActionName("RemoveId")]
		public JsonResponse Remove(int? id) {
			if (id == null)
				return new JsonResponse {
					Result = "Failed",
					Message = "RemoveId requires a Product.Id"
				};
			var product = db.Products.Find(id);
			if (product == null)
				return new JsonResponse {
					Result = "Failed",
					Message = $"No product has Id of {id}"
				};
			db.Products.Remove(product);
			db.SaveChanges();
			return new JsonResponse {
				Message = "Remove successful.",
				Data = product
			};
		}
	}
}