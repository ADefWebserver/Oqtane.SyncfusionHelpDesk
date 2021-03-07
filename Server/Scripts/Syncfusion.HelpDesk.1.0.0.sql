/*  
Create SyncfusionHelpDesk table
*/

CREATE TABLE [dbo].[SyncfusionHelpDesk](
	[HelpDeskId] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
  CONSTRAINT [PK_SyncfusionHelpDesk] PRIMARY KEY CLUSTERED 
  (
	[HelpDeskId] ASC
  )
)
GO

/*  
Create foreign key relationships
*/
ALTER TABLE [dbo].[SyncfusionHelpDesk] WITH CHECK ADD CONSTRAINT [FK_SyncfusionHelpDesk_Module] FOREIGN KEY([ModuleId])
REFERENCES [dbo].Module ([ModuleId])
ON DELETE CASCADE
GO