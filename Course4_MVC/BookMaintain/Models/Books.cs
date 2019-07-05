using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BookMaintain.Models
{
	public class Books
	{
		[DisplayName("圖書類別ID")]
        
        public string b_CLASS_ID { get; set; }

		[DisplayName("圖書類別")]
        [Required(ErrorMessage = "此欄位必填")]
        public string b_CLASS_NAME { get; set; }

		[DisplayName("書本ID")]
		public int b_BOOK_ID { get; set; }

		[DisplayName("書名")]
        [Required(ErrorMessage = "此欄位必填")]
        public string b_BOOK_NAME { get; set; }

		[DisplayName("借閱狀態ID")]
		public string b_STATUS_ID { get; set; }

		[DisplayName("借閱狀態")]
		public string b_STATUS { get; set; }

		[DisplayName("借閱人ID")]
		public string b_KEEPER_ID { get; set; }

		[DisplayName("借閱人")]
		public string b_KEEPER { get; set; }

		[DisplayName("購買日期")]
        [Required(ErrorMessage = "此欄位必填")]
        public string b_BOUGHT_DATE { get; set; }

		[DisplayName("作者")]
        [Required(ErrorMessage = "此欄位必填")]
        public string b_AUTHOR { get; set; }

		[DisplayName("出版商")]
        [Required(ErrorMessage = "此欄位必填")]
        public string b_PUBLISHER { get; set; }

		[DisplayName("內容簡介")]
        [Required(ErrorMessage = "此欄位必填")]
        public string b_INSTRODUCTION { get; set; }


        

        


	}
}