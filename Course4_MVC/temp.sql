 @"  SELECT	bcla.BOOK_CLASS_NAME as book_class_name,
		                            bd.BOOK_NAME as book_name,
		                            FORMAT(bd.BOOK_BOUGHT_DATE, 'd', 'zh-TW') as book_bought_date,
		                            bcod.CODE_NAME as cood_name,
		                            ISNull(mm.USER_ENAME,'') as user_ename

                    FROM BOOK_DATA AS bd
                        INNER JOIN BOOK_CLASS AS bcla
                            ON bd.BOOK_CLASS_ID = bcla.BOOK_CLASS_ID
                        INNER JOIN BOOK_CODE AS bcod
                            ON bd.BOOK_STATUS =  bcod.CODE_ID
                        LEFT JOIN BOOK_LEND_RECORD AS blr
                            ON blr.BOOK_ID = bd.BOOK_ID
                        LEFT JOIN MEMBER_M AS mm
	                        ON blr.KEEPER_ID = mm.[USER_ID]
                    WHERE bcod.CODE_TYPE_DESC = '®ÑÄyª¬ºA' AND (bd.BOOK_NAME = @Book_Name OR @Book_Name='') AND
                          (bcla.BOOK_CLASS_NAME = @Book_Class OR @Book_Class='') AND
                          (mm.USER_ENAME = @Keeper_Name OR @Keeper_Name='') AND
                          (bcod.CODE_NAME = @Book_Status OR @Book_Status='')
                    ORDER BY bcla.BOOK_CLASS_NAME, bd.BOOK_NAME";