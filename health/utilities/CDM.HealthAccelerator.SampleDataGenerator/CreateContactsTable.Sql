
/****** Object:  Table [dbo].[CRMContacts]    Script Date: 4/17/2015 7:29:19 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[CRMContacts](
	[id] [bigint] IDENTITY(1,1) NOT NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NOT NULL,
	[address1_line1] [nvarchar](4000) NOT NULL,
	[address1_city] [nvarchar](4000) NOT NULL,
	[address1_stateorprovince] [nvarchar](4000) NOT NULL,
	[address1_postalcode] [nvarchar](4000) NOT NULL,
	[telephone1] [nvarchar](50) NOT NULL,
	[emailaddress1] [nvarchar](100) NOT NULL
) ON [PRIMARY]

GO


