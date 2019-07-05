using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookMaintain.Models
{
	public class BookSearchArg
	{
	
		[DisplayName("圖書類別")]
		public string Book_Class_Id { get; set; }

		[DisplayName("書名")]
		public string Book_Name { get; set; }

		[DisplayName("借閱狀態")]
		public string Book_Status_Id { get; set; }

		[DisplayName("借閱人")]
		public string Keeper_Id { get; set;}
	}
}