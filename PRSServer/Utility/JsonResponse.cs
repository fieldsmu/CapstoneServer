using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PRSServer.Utility {
	public class JsonResponse {

		public static JsonResponse Ok = new JsonResponse();

		public int Code { get; set; } = 0;
		public string Result { get; set; } = "Ok";
		public string Message { get; set; } = "Success";
		public object Data { get; set; }
		public object Error { get; set; }

		public JsonResponse() { }
	}
}