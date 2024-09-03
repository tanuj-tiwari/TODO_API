
/****** Object:  Table [dbo].[tbl_TODO]    Script Date: 03-09-2024 12:09:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[tbl_TODO](
	[fld_Id] [int] IDENTITY(1,1) NOT NULL,
	[fld_Title] [varchar](100) NOT NULL,
	[fld_Description] [varchar](max) NOT NULL,
	[fld_Status] [bit] NOT NULL,
	[fld_CreatedBy] [varchar](20) NULL,
	[fld_CreatedDate] [datetime] NULL,
	[fld_ModifiedBy] [varchar](20) NULL,
	[fld_ModifiedDate] [datetime] NULL,
 CONSTRAINT [PK_tbl_TODO] PRIMARY KEY NONCLUSTERED 
(
	[fld_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  StoredProcedure [dbo].[usp_Add_Update_TODO_Data]    Script Date: 03-09-2024 12:09:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TANUJ TIWARI
-- Create date: 02-Sep-2024
-- Description:	INSERT/UPDATE RECORDS IN [dbo].[tbl_TODO]
-- =============================================

CREATE PROCEDURE [dbo].[usp_Add_Update_TODO_Data]
	@Id INT,
	@Title VARCHAR(100),
	@Description VARCHAR(MAX),
	@Status BIT,
	@CreatedBy VARCHAR(20),
	@ResponseOut INT OUT
AS
	
BEGIN
	BEGIN TRY
		IF(@Id = 0)
			BEGIN
				IF NOT EXISTS(SELECT 1 FROM [dbo].[tbl_TODO] WHERE fld_Title = @Title)
					BEGIN
						INSERT INTO [dbo].[tbl_TODO] ([fld_Title], [fld_Description], [fld_Status], [fld_CreatedBy],
						[fld_CreatedDate]) VALUES (@Title, @Description, @Status, @CreatedBy, GETDATE())

						SET @ResponseOut = 1
					END
				ELSE
					BEGIN
						SET @ResponseOut = 3
					END
			END
		ELSE
			BEGIN
				IF NOT EXISTS(SELECT 1 FROM [dbo].[tbl_TODO] WHERE fld_Title = @Title AND fld_Id <> @Id)
					BEGIN
						UPDATE [dbo].[tbl_TODO] SET [fld_Title] = @Title, [fld_Description] = @Description, [fld_Status] = @Status,
						[fld_ModifiedBy] = @CreatedBy, [fld_ModifiedDate] = GETDATE() WHERE [fld_Id] = @Id

						SET @ResponseOut = 2
					END
				ELSE
					BEGIN
						SET @ResponseOut = 3
					END
			END
	END TRY

	BEGIN CATCH
	   DECLARE @ErrorMessage NVARCHAR(1000);
	   DECLARE @ErrorSeverity INT;
	   DECLARE @ErrorState INT;
 
	   SELECT
		  @ErrorMessage = ERROR_MESSAGE(),
		  @ErrorSeverity = ERROR_SEVERITY(),
		  @ErrorState = ERROR_STATE();
 
	   RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
 END CATCH;
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Delete_TODO_Data]    Script Date: 03-09-2024 12:09:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TANUJ TIWARI
-- Create date: 02-SEPTEMBER-2024
-- Description:	DELETE RECORDS FROM [dbo].[tbl_TODO]
-- =============================================

CREATE PROCEDURE [dbo].[usp_Delete_TODO_Data]
	@Id INT
AS

BEGIN
	BEGIN TRY
		DELETE FROM [dbo].[tbl_TODO] WHERE fld_Id = @Id
	END TRY

	BEGIN CATCH
	   DECLARE @ErrorMessage NVARCHAR(1000);
	   DECLARE @ErrorSeverity INT;
	   DECLARE @ErrorState INT;
 
	   SELECT
		  @ErrorMessage = ERROR_MESSAGE(),
		  @ErrorSeverity = ERROR_SEVERITY(),
		  @ErrorState = ERROR_STATE();
 
	   RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
 END CATCH;
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Get_TODO_List]    Script Date: 03-09-2024 12:09:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
-- =============================================
-- Author:		TANUJ TIWARI
-- Create date: 02-SEPTEMBER-2024
-- Description:	GET RECORDS FROM [dbo].[tbl_TODO]
-- =============================================

CREATE PROCEDURE [dbo].[usp_Get_TODO_List]
	@Id INT
AS
	
BEGIN
	BEGIN TRY

		SELECT [fld_Id], [fld_Title], [fld_Description], [fld_Status] FROM [dbo].[tbl_TODO] 
		WHERE [fld_Id] = CASE WHEN @Id <> 0 THEN @Id ELSE [fld_Id] END

	END TRY

	BEGIN CATCH
	   DECLARE @ErrorMessage NVARCHAR(1000);
	   DECLARE @ErrorSeverity INT;
	   DECLARE @ErrorState INT;
 
	   SELECT
		  @ErrorMessage = ERROR_MESSAGE(),
		  @ErrorSeverity = ERROR_SEVERITY(),
		  @ErrorState = ERROR_STATE();
 
	   RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
 END CATCH;
END

GO
/****** Object:  StoredProcedure [dbo].[usp_Get_TODO_Pagination]    Script Date: 03-09-2024 12:09:07 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

-- =============================================
-- Author:		TANUJ TIWARI
-- Create date: 02-SEPTEMBER-2024
-- Description:	GET RECORDS FROM [dbo].[tbl_TODO]
-- =============================================
-- [dbo].[usp_Get_TODO_Pagination] 1,3
--==============================================

CREATE PROCEDURE [dbo].[usp_Get_TODO_Pagination]
	@PageNum INT,
	@PageSize INT
AS
	
BEGIN
	BEGIN TRY

		SELECT [fld_Id], [fld_Title], [fld_Description], [fld_Status] FROM [dbo].[tbl_TODO] 
		ORDER BY fld_Id 
		OFFSET (@PageNum - 1)*@PageSize ROWS
		FETCH NEXT @PAGESIZE ROWS ONLY

	END TRY

	BEGIN CATCH
	   DECLARE @ErrorMessage NVARCHAR(1000);
	   DECLARE @ErrorSeverity INT;
	   DECLARE @ErrorState INT;
 
	   SELECT
		  @ErrorMessage = ERROR_MESSAGE(),
		  @ErrorSeverity = ERROR_SEVERITY(),
		  @ErrorState = ERROR_STATE();
 
	   RAISERROR(@ErrorMessage, @ErrorSeverity, @ErrorState);
 END CATCH;
END

GO

