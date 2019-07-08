using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BookManagement.Controllers
{
	public class BookController : Controller
	{
		readonly Models.CodeService codeService = new Models.CodeService();
		readonly Models.BookService bookService = new Models.BookService();

		int bookid_byinsert;
		// GET: BookByAJAX
		public ActionResult Index()
		{
			return View();
		}

		[HttpPost]
		public JsonResult GetBookClassDropDownListData()
		{
			return Json(this.codeService.GetBookClassName());
		}
		[HttpPost]
		public JsonResult GetLendUserDropDownListData()
		{
			return Json(this.codeService.GetUserName());
		}
		[HttpPost]
		public JsonResult GetBookStatusDropDownListData()
		{
			return Json(this.codeService.GetCodeName());
		}
		[HttpPost]
		public JsonResult GetSearchGrid(Models.BookSearchArg bookscondition)
		{
			return Json(bookService.GetBookByCondtioin(bookscondition));
		}
		[HttpPost]
		public JsonResult DeleteBook(int bookid)
		{
			return Json(bookService.DeleteBook(bookid));
		}

		[HttpPost]
		public JsonResult GetBookRecordByBookID(int bookid)
		{
			return Json(bookService.GetBookLendRecord(bookid));
		}

		public ActionResult InsertBook()
		{
			return View();
		}

		public JsonResult AddBook(Models.Books book)
		{
			return Json(bookService.InsertBook(book));
		}

		//public JsonResult ShowBookData(int book)
		[HttpPost]
		public ActionResult EditBook(int bookid)
		{
			ViewData["bookid"] = bookid;
			return View();
		}

		[HttpPost]
		public JsonResult GetOriginBookData(int bookid)
		{
			return Json(this.bookService.GetBookData(bookid));
		}

        [HttpPost]
        public JsonResult UpdateBook(Models.Books book)
        {
            //string note = book.BookNote;
            //book.BookNote = bookService.Replace(note);
            return Json(this.bookService.UpdateBookData(book));
        } 
	}
}