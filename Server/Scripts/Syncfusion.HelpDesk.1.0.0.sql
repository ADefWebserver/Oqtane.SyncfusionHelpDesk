/*  
Create SyncfusionHelpdesk table
*/

CREATE TABLE [dbo].[SyncfusionHelpdesk](
	[HelpdeskId] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[Name] [nvarchar](256) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
  CONSTRAINT [PK_SyncfusionHelpdesk] PRIMARY KEY CLUSTERED 
  (
	[HelpdeskId] ASC
  )
)
GO

/*  
Create foreign key relationships
*/
ALTER TABLE [dbo].[SyncfusionHelpdesk] WITH CHECK ADD CONSTRAINT [FK_SyncfusionHelpdesk_Module] FOREIGN KEY([ModuleId])
REFERENCES [dbo].Module ([ModuleId])
ON DELETE CASCADE
GO