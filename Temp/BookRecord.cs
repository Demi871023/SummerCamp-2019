using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookManagement.Models
{
	public class BookRecord
	{
		[DisplayName("借閱日期")]
		public string LendDate { get; set; }

		[DisplayName("借閱人員編號")]
		public string LendUserId { get; set; }

		[DisplayName("英文姓名")]
		public string LendUserCName { get; set; }

		[DisplayName("中文姓名")]
		public string LendUserEName { get; set; }
	}
}