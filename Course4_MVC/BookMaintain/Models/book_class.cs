using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BookMaintain.Models
{
	public class Bookclass
	{



        
        /// <summary>
		/// 類別代號
		/// </summary>
		public int BOOK_CLASS_ID { get; set; }
		/// <summary>
		/// 類別名稱
		/// </summary>
		public string BOOK_CLASS_NAME { get; set; }
		/// <summary>
		/// 建立時間
		/// </summary>
		public string CREATE_DATE { get; set; }
		/// <summary>
		/// 建立使用者
		/// </summary>
		public string CREATE_USER { get; set; }
		/// <summary>
		/// 修改時間
		/// </summary>
		public string MODIFY_DATE { get; set; }
		/// <summary>
		/// 修改使用者
		/// </summary>
		public string MODIFY_USER { get; set; }
        
    }
}