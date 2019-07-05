using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace BookMaintain.Models
{
	public class BookService
	{
		private string GetDBConnectionString()
		{
			return
				System.Configuration.ConfigurationManager.ConnectionStrings["DBConn"].ConnectionString.ToString();
		}

        /// <summary>
        /// 依據給予條件查詢書籍
        /// </summary>
        /// <param name="arg"></param>
        /// <returns></returns>
		public List<Models.Books> GetBookByCondition(Models.BookSearchArg arg)
		{
			DataTable dt = new DataTable(); //表示記憶體中資料的一個資料表
			string sql = @"SELECT	bcla.BOOK_CLASS_ID AS BOOKCLASSID,
									bcla.BOOK_CLASS_NAME AS BOOKCLASSNAME,
									bd.BOOK_ID AS BOOKID,
									bd.BOOK_NAME AS BOOKNAME,
									bd.BOOK_STATUS AS BOOKSTATUSID,
									bcod.CODE_NAME AS BOOKSTATUS,
									ISNULL(mm.[USER_ID],'') AS KEEPERID,
									ISNULL(mm.USER_ENAME,'') AS KEEPER,
									FORMAT(bd.BOOK_BOUGHT_DATE,'yyyy/MM/dd') as BOOKBYUDATE

							FROM BOOK_DATA AS bd
								INNER JOIN BOOK_CLASS AS bcla
									ON bd.BOOK_CLASS_ID = bcla.BOOK_CLASS_ID
								INNER JOIN BOOK_CODE AS bcod
									ON bd.BOOK_STATUS = bcod.CODE_ID AND bcod.CODE_TYPE = 'BOOK_STATUS'
								LEFT JOIN MEMBER_M AS mm
									ON bd.BOOK_KEEPER = mm.[USER_ID]

							WHERE	bd.BOOK_NAME LIKE '%'+@Book_Name+'%' AND
									(bcla.BOOK_CLASS_ID = @Book_Class_Id OR @Book_Class_Id ='') AND
									(ISNULL(mm.[USER_ID],'') =@Keeper_Id OR @Keeper_Id = '') AND
									(bd.BOOK_STATUS = @Book_Status_Id OR @Book_Status_Id = '')

							ORDER BY bd.BOOK_BOUGHT_DATE DESC";

			//先依據給予的條件塞入值，在做sql判斷
			using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(sql, conn);
				cmd.Parameters.Add(new SqlParameter("@Book_Class_Id", arg.Book_Class_Id == null ? string.Empty : arg.Book_Class_Id));
				cmd.Parameters.Add(new SqlParameter("@Book_Name", arg.Book_Name == null ? string.Empty : arg.Book_Name));
				cmd.Parameters.Add(new SqlParameter("@Book_Status_Id", arg.Book_Status_Id == null ? string.Empty : arg.Book_Status_Id));
				cmd.Parameters.Add(new SqlParameter("@Keeper_Id", arg.Keeper_Id == null ? string.Empty : arg.Keeper_Id));
				SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
				sqlAdapter.Fill(dt);
				conn.Close();
			}
			return this.MapBookDataToList(dt);
		}
        /// <summary>
        /// 新增書籍
        /// </summary>
        /// <param name="book"></param>
        /// <returns></returns>
        public int InsertBook(Models.Books book)
        {
            string sql = @"INSERT INTO BOOK_DATA
                        (	BOOK_NAME, 
	                        BOOK_AUTHOR, 
	                        BOOK_PUBLISHER, 
	                        BOOK_NOTE, 
	                        BOOK_BOUGHT_DATE,
                            BOOK_CLASS_ID,
                            BOOK_STATUS
                        )
                        VALUES
                        (
	                        @b_BOOK_NAME,
	                        @b_AUTHOR, 
	                        @b_PUBLISHER,　
	                        @b_INSTRODUCTION, 
	                        @b_BOUGHT_DATE,
                            @b_CLASS_ID,
                            'A'
                        );
                        Select SCOPE_IDENTITY()"; //抓取流水編號
            int BookId;
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@b_BOOK_NAME", book.b_BOOK_NAME));
                cmd.Parameters.Add(new SqlParameter("@b_AUTHOR", book.b_AUTHOR));
                cmd.Parameters.Add(new SqlParameter("@b_PUBLISHER", book.b_PUBLISHER));
                cmd.Parameters.Add(new SqlParameter("@b_INSTRODUCTION", book.b_INSTRODUCTION));
                cmd.Parameters.Add(new SqlParameter("@b_BOUGHT_DATE", book.b_BOUGHT_DATE));
                cmd.Parameters.Add(new SqlParameter("@b_CLASS_ID", book.b_CLASS_NAME));
                cmd.Parameters.Add(new SqlParameter("@b_STATUS_ID", 'A'));
                BookId = Convert.ToInt32(cmd.ExecuteScalar());
                conn.Close();
            }
            return BookId;
        }

        /// <summary>
        /// 透過Book_ID刪除書籍
        /// </summary>
        /// <param name="bookid"></param>
        public void DeleteBookById(int bookid)
        {
            try
            {
				string sql = @"DELETE FROM BOOK_DATA
                               WHERE BOOK_DATA.BOOK_ID = @b_BOOK_ID";
                using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand(sql, conn);
                    cmd.Parameters.Add(new SqlParameter("@b_BOOK_ID", bookid));
					cmd.ExecuteNonQuery();
                    conn.Close();
                }

            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

		public List<Models.BookRecord> GetBookRecordByCondition(int bookid)
		{
			DataTable dt = new DataTable();
			string sql = @"SELECT	
												format(blr.LEND_DATE,'yyyy/MM/dd') AS LENDDATE, 
												mm.[USER_ID] AS USERID, 
												mm.USER_CNAME AS USERCNAME, 
												mm.USER_ENAME AS USERENAME,
												blr.BOOK_ID
								FROM BOOK_LEND_RECORD AS blr
									INNER JOIN MEMBER_M AS mm
										ON blr.KEEPER_ID = mm.[USER_ID]
								WHERE blr.BOOK_ID = @b_BOOK_ID";

			using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
			{
				conn.Open();
				SqlCommand cmd = new SqlCommand(sql, conn);
				cmd.Parameters.Add(new SqlParameter("@b_BOOK_ID", bookid));
				SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
				sqlAdapter.Fill(dt);
				conn.Close();
			}
			return this.MapBookRecordToList(dt);
		}

		/// <summary>
		/// Table Map成List
		/// </summary>
		/// <param name="booksData"></param>
		/// <returns></returns>
		private List<Models.Books> MapBookDataToList(DataTable booksData)
		{
			List<Models.Books> result = new List<Books>();
			foreach (DataRow row in booksData.Rows)
			{
                result.Add(new Books()
                {
					b_CLASS_ID = row["BOOKCLASSID"].ToString(),
                    b_CLASS_NAME = row["BOOKCLASSNAME"].ToString(),
                    b_BOOK_ID = (int)row["BOOKID"],
					b_BOOK_NAME = row["BOOKNAME"].ToString(),
                    b_STATUS_ID = row["BOOKSTATUSID"].ToString(),
                    b_STATUS = row["BOOKSTATUS"].ToString(),
					b_KEEPER_ID = row["KEEPERID"].ToString(),
                    b_KEEPER = row["KEEPER"].ToString(),
					b_BOUGHT_DATE = row["BOOKBYUDATE"].ToString(),

				});
			}
			return result;
		}

		private List<Models.BookRecord> MapBookRecordToList(DataTable bookrecord)
		{
			List<Models.BookRecord> result = new List<BookRecord>();
			foreach (DataRow row in bookrecord.Rows)
			{
				result.Add(new BookRecord()
				{
					Lend_Date = row["LENDDATE"].ToString(),
					Lend_User_Id = row["USERID"].ToString(),
					Lend_User_CName = row["USERCNAME"].ToString(),
					Lend_User_EName = row["USERENAME"].ToString()
				});
			}
			return result;
		}
	}
}