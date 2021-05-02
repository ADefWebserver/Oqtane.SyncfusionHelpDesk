/*  
Create SyncfusionHelpDesk tables
*/

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SyncfusionHelpDeskTicketDetails](
	[HelpDeskTicketDetailId] [int] IDENTITY(1,1) NOT NULL,
	[HelpDeskTicketId] [int] NOT NULL,
	[TicketDetailDate] [datetime] NOT NULL,
	[TicketDescription] [nvarchar](max) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_SyncfusionHelpDeskTicketDetails] PRIMARY KEY CLUSTERED 
(
	[HelpDeskTicketDetailId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SyncfusionHelpDeskTickets](
	[HelpDeskTicketId] [int] IDENTITY(1,1) NOT NULL,
	[ModuleId] [int] NOT NULL,
	[TicketStatus] [nvarchar](50) NOT NULL,
	[TicketDate] [datetime] NOT NULL,
	[TicketDescription] [nvarchar](max) NOT NULL,
	[CreatedBy] [nvarchar](256) NOT NULL,
	[CreatedOn] [datetime] NOT NULL,
	[ModifiedBy] [nvarchar](256) NOT NULL,
	[ModifiedOn] [datetime] NOT NULL,
 CONSTRAINT [PK_HelpDeskTickets] PRIMARY KEY CLUSTERED 
(
	[HelpDeskTicketId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
ALTER TABLE [dbo].[SyncfusionHelpDeskTicketDetails]  WITH CHECK ADD  CONSTRAINT [FK_SyncfusionHelpDeskTicketDetails_SyncfusionHelpDeskTickets] FOREIGN KEY([HelpDeskTicketId])
REFERENCES [dbo].[SyncfusionHelpDeskTickets] ([HelpDeskTicketId])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[SyncfusionHelpDeskTicketDetails] CHECK CONSTRAINT [FK_SyncfusionHelpDeskTicketDetails_SyncfusionHelpDeskTickets]
GO

/*  
Create foreign key relationships
*/

ALTER TABLE [dbo].[SyncfusionHelpDeskTickets]  WITH CHECK ADD  CONSTRAINT [FK_SyncfusionHelpDeskTickets_Module] FOREIGN KEY([ModuleId])
REFERENCES [dbo].[Module] ([ModuleId])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[SyncfusionHelpDeskTickets] CHECK CONSTRAINT [FK_SyncfusionHelpDeskTickets_Module]
GO