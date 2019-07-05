using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookMaintain.Models
{
	public class BookRecord
	{
		[DisplayName ("借閱日期")]
		public string Lend_Date { get; set; }

		[DisplayName("借閱人員編號")]
		public string Lend_User_Id { get; set; }

		[DisplayName("英文姓名")]
		public string Lend_User_CName { get; set; }

		[DisplayName("中文姓名")]
		public string Lend_User_EName { get; set; }

	}
}