USE [SQQEYEDatabase]
GO
/****** Object:  StoredProcedure [dbo].[sp_CreateExceptionRequest]    Script Date: 8/14/2021 12:49:28 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
ALTER PROCEDURE [dbo].[sp_CreateExceptionRequest]	--sp_CreateExceptionRequest 
	@ExceptionRequestMaster nvarchar(MAX),
	@ExceptionDetails nvarchar(MAX),
	@ExpGenralList nvarchar(MAX),
	@FileUploadJSon nvarchar(MAX),
	@ApproverJson nvarchar(MAX),
	@NoOfApprover int,
	@UserID int
AS
BEGIN
	DECLARE @PrimaryKey int,@maillist nvarchar(MAX);
	INSERT INTO [ExceptionRequestMaster](ExceptionTypeId,[ExceptionTypeName],[BusinessUnitId],[ExceptionReasonID]
           ,[CreateDate],[IsApproved],[UpdateDate],[RivisionNo],[NoApprover],[Reasons],[BuyerId]
           ,[ResponsiblePerson],[UserID],[IsHrInteraction])
	 OUTPUT INSERTED.ExceptionMasterId
     SELECT 
		ExceptionTypeId,[ExceptionTypeName],[BusinessUnitId],ExceptioReasonsId,GETDATE(),5,GETDATE(),0,@NoOfApprover,[Reasons],[BuyerId]
           ,[ResponsiblePerson],@UserID,0
	 FROM OPENJSON(@ExceptionRequestMaster)
	 WITH(
	  [ExceptionTypeId] int '$.ExceptionTypeId'
	 ,[ExceptionTypeName] nvarchar(MAX) '$.ExceptionTypeName'
	 ,[BusinessUnitId]	int '$.BusinessUnitId',
	 ExceptioReasonsId int '$.ExceptioReasonsId'
	 ,[Reasons] nvarchar(MAX) '$.Reasons'
	 ,[BuyerId]	int '$.BuyerId'
	 ,[ResponsiblePerson] nvarchar(MAX) '$.ResponsiblePerson');
	  SET @PrimaryKey=SCOPE_IDENTITY();
	  --exception details table save
	  IF (ISJSON(@ExceptionDetails) > 0) 
	  BEGIN
		  INSERT INTO [ExceptionDetailsTable]
			   ([ExceptionMasterId],[Recoverable],[GrossWeight],[VolumetricWeight],[AirFreightRate],[AirFreightCost]
			   ,[RecoverableFrom],[RecoverableAmmount],[PoInvoiceNo]
			   ,[ExceptionDetails],[LossOrLiabilityCompany],[ValueOfLoss],[CreateDate],[UpdateDate])
			SELECT 
				@PrimaryKey,[Recoverable],[GrossWeight],[VolumetricWeight],[AirFreightRate],[AirFreightCost]
				   ,[RecoverableFrom],[RecoverableAmmount],[PoInvoiceNo]
				   ,[ExceptionDetails],[LossOrLiabilityCompany],[ValueOfLoss],GETDATE(),GETDATE()
			 FROM OPENJSON(@ExceptionDetails)
			 WITH(
			  [Recoverable] int '$.Recoverable',[GrossWeight] float '$.GrossWeight'
			 ,[VolumetricWeight] float '$.VolumetricWeight',[AirFreightRate] float '$.AirFreightRate'
			 ,[AirFreightCost] float '$.AirFreightCost',[RecoverableAmmount] float '$.RecoverableAmmount'
			 ,[RecoverableFrom] nvarchar(MAX) '$.RecoverableFrom',[PoInvoiceNo] nvarchar(MAX) '$.PoInvoiceNo'
			 ,[ExceptionDetails] nvarchar(MAX) '$.ExceptionDetails',[LossOrLiabilityCompany] nvarchar(MAX) '$.LossOrLiabilityCompany'
			 ,[ValueOfLoss] nvarchar(MAX) '$.ValueOfLoss');
	  END

	--exception genral info list save
	IF (ISJSON(@ExpGenralList) > 0) 
	BEGIN
		INSERT INTO [ExceptionGenaralInformation]
           ([ExceptionMasterId],[StyleNo],[Color],[PO],[FOB],[OriginalDD],[RevisedDD],[Discount],[Claim],[MaterialLiability],[GarmentsLiability],[CreateDate],[UpdateDate],[Quantity])
		SELECT 
			@PrimaryKey,[StyleNo],[Color],[PO],[FOB],[OriginalDD],[RevisedDD],[Discount],[Claim],[MaterialLiability],[GarmentsLiability],GETDATE(),GETDATE(),[Quantity]
		 FROM OPENJSON(@ExpGenralList)
		 WITH(
		 [StyleNo] nvarchar(MAX) '$.StyleNo',[Color] nvarchar(MAX) '$.Color'
		 ,[PO] nvarchar(MAX) '$.PO',[FOB] nvarchar(MAX) '$.FOB'
		 ,[OriginalDD] nvarchar(MAX) '$.OriginalDD',[RevisedDD] nvarchar(MAX) '$.RevisedDD'
		 ,[Discount] nvarchar(MAX) '$.Discount',[Claim] nvarchar(MAX) '$.Claim'
		 ,[MaterialLiability] nvarchar(MAX) '$.MaterialLiability',[GarmentsLiability] nvarchar(MAX) '$.GarmentsLiability'
		 ,[Quantity] float '$.Quantity');
	END

	--exception file upload
	IF (ISJSON(@FileUploadJSon) > 0)  
	BEGIN
	INSERT INTO [dbo].[ExceptionFileTable]
           ([ExceptionFileName],[ExceptionMasterId],[ExceptionFilePath],[CreateDate],[UpdateDate])
	SELECT 
			CapexFileName,@PrimaryKey,CapexFilePath,GETDATE(),GETDATE()
		 FROM OPENJSON(@FileUploadJSon)
		 WITH(CapexFileName nvarchar(MAX) '$.CapexFileName'
		 ,CapexFilePath nvarchar(MAX) '$.CapexFilePath');
	END
	--exception approver list
	IF (ISJSON(@ApproverJson) > 0)  
	BEGIN
	INSERT INTO [dbo].[ExceptionTableApprover]
           ([ExceptionMasterId],[UserId],[ApproverNo],[IsApproved],[CreateDate])
	SELECT 
			@PrimaryKey,ApproverUserId,ApproverNo,0,GETDATE()
		 FROM OPENJSON(@ApproverJson)
		 WITH(ApproverUserId int '$.ApproverUserId',ApproverNo int '$.ApproverNO');
	END

	--update the first approver 
	UPDATE ExceptionTableApprover SET IsApproved=3,UpdateDate=GETDATE() WHERE ExceptionMasterId=@PrimaryKey
	AND ApproverNo=1

	INSERT INTO ExceptionLogTable(ExceptionMasterID,ExceptionUpdate,ExceptionUser,CreateDate,ExceptionRemarks)
	VALUES(@PrimaryKey,'A new Exception Request Created',@UserID,GETDATE(),'Exception Created');

	SET @maillist=(SELECT STUFF((SELECT ';' +UserEmail FROM UserInformation un INNER JOIN ExceptionTableApprover er ON er.UserId=un.UserId
		WHERE er.ExceptionMasterId=@PrimaryKey AND er.IsApproved!=0
          FOR XML PATH('')), 1, 1, ''))

	EXEC dbo.sp_ExceptionSentMailFromDatabase @PrimaryKey ,1,@maillist,'shanzan.khan@sqgc.com'
END

