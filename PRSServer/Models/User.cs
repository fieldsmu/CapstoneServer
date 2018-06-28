using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRSServer {

	public class User { //public here allows us to access this class outside of namespace--
						//-- defaults to internal meaning it can only be used in this namespace

		public int Id { get; set; }

		[Required]	//modifies Username to not null in SQL
		[StringLength(30)]	//modified Username to only allow 30 characters
		[Index(IsUnique=true)]	//makes each username unique in the table
		public string Username { get; set; }

		[Required]
		[StringLength(30)]
		public string Password { get; set; }

		[Required]
		[StringLength(30)]
		public string Firstname { get; set; }

		[Required]
		[StringLength(30)]
		public string Lastname { get; set; }

		[Required]
		[StringLength(12)]
		public string Phone { get; set; }

		[Required]
		[StringLength(255)]
		public string Email { get; set; }

		public bool IsReviewer { get; set; } //if you want this to allow null put a "?" after "bool"
		public bool IsAdmin { get; set; }
		public bool Active { get; set; }


		public User() { }

		public User(string Username, string Password, string Firstname, string Lastname, string Phone, string Email, bool IsReviewer, bool IsAdmin, bool Active) {
			this.Username = Username;
			this.Password = Password;
			this.Firstname = Firstname;
			this.Lastname = Lastname;
			this.Phone = Phone;
			this.Email = Email;
			this.IsReviewer = IsReviewer;
			this.IsAdmin = IsAdmin;
			this.Active = Active;
		}

	}
}
