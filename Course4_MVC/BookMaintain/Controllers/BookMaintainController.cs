using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookMaintain.Controllers
{
    public class BookMaintainController : Controller
    {
        readonly Models.CodeService codeService = new Models.CodeService();
        readonly Models.BookService bookService = new Models.BookService();


        // GET: BookMaintain
        public ActionResult Index()
        {
            ViewBag.Book_Class = this.codeService.GetBookClass("TITLE");
            ViewBag.Keeper_Name = this.codeService.GetKeeperName("TITLE");
            ViewBag.Book_Status = this.codeService.GetBookStatus("TITLE");
            return View("Index");
        }

        [HttpPost()]
        public ActionResult Index(Models.BookSearchArg arg)
        {
            ViewBag.Book_Class = this.codeService.GetBookClass("TITLE"); //選擇圖書類別的DropDownList
            ViewBag.Keeper_Name = this.codeService.GetKeeperName("TITLE"); //選擇借閱人的DropDownList
            ViewBag.Book_Status = this.codeService.GetBookStatus("TITLE"); //選擇書籍狀態的DropDownList
            ViewBag.Book_Search_Result = this.bookService.GetBookByCondition(arg); // 依據arg所查詢的結果(已經map過)
            return View("Index");
        }

		[HttpGet()]
		public ActionResult InsertBook()
		{
			ViewBag.b_CLASS_NAME = this.codeService.GetBookClass("TITLE"); //選擇圖書類別的DropDownList
            return View(new Models.Books());
		}

        [HttpPost()]
        public ActionResult InsertBook(Models.Books book)
        {
            ViewBag.b_CLASS_NAME = this.codeService.GetBookClass("TITLE"); //選擇class name的DropDownList
            if (ModelState.IsValid)
            {
                bookService.InsertBook(book);
				ViewBag.InsertResult = true;
            }

            return View("InsertBook");
        }

		[HttpPost()]
		public JsonResult DeleteBook(int bookid)
        {
            try
            {
                bookService.DeleteBookById(bookid);
                return this.Json(true);
            }
            catch(Exception ex)
            {
                return this.Json(false);
            }
        }
		[HttpPost()]
		public ActionResult BookRecord(int bookid)
		{
			ViewBag.Result = this.bookService.GetBookRecordByCondition(bookid);
			return View("BookRecord");
		}

    }
}