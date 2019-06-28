SELECT PivotTable.BOOK_CLASS_ID AS ClassId , PivotTable.BOOK_CLASS_NAME AS ClassName, 
	CASE WHEN PivotTable.[2016] IS NULL THEN '0' ELSE PivotTable.[2016] END AS 'CNT2016',
	CASE WHEN PivotTable.[2017] IS NULL THEN '0' ELSE PivotTable.[2017] END AS 'CNT2017',
	CASE WHEN PivotTable.[2018] IS NULL THEN '0' ELSE PivotTable.[2018] END AS 'CNT2018',
	CASE WHEN PivotTable.[2019] IS NULL THEN '0' ELSE PivotTable.[2019] END AS 'CNT2019'
FROM(
	SELECT bc.BOOK_CLASS_ID, bc.BOOK_CLASS_NAME, YEAR(blr.LEND_DATE) as [year], COUNT(bc.BOOK_CLASS_ID) as Cnt
	FROM BOOK_LEND_RECORD AS blr
	LEFT JOIN BOOK_DATA AS bd
		ON blr.BOOK_ID = bd.BOOK_ID
	LEFT JOIN BOOK_CLASS AS bc
		ON bd.BOOK_CLASS_ID = bc.BOOK_CLASS_ID
	GROUP BY bc.BOOK_CLASS_ID, bc.BOOK_CLASS_NAME, YEAR(blr.LEND_DATE)
	)Result

PIVOT
(
	SUM(Result.Cnt)
	FOR Result.[year]
		IN ([2016], [2017], [2018], [2019])

)AS PivotTable
ORDER BY PivotTable.BOOK_CLASS_ID