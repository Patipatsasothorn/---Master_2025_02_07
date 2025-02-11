SELECT * FROM BOL_DocHead
SELECT * FROM BOL_DocDetail
SELECT * FROM BOL_StatusLog

DELETE FROM BOL_UsersPolicy

SELECT * FROM BOL_UsersPolicy
SELECT * FROM UserRight
----DBCC CHECKIDENT ('BOL_UsersPolicy', RESEED, 0);

--SELECT
--	h.DocID,
--	h.DocDate,
--	h.Plant,
--	h.Reason,
--	h.Dep,
--	h.ReqDate,
--	h.Remark,
--	h.Status,
--	d.PartNum,
--	d.Qty,
--	d.Unit,
--	d.WareHouse,
--	d.Bin

--FROM BOL_DocHead h
--JOIN BOL_DocDetail d ON h.DocID = d.DocID

--SELECT * FROM _Holiday

--DELETE FROM _Holiday