USE [toutiao]
GO

/****** Object:  Table [dbo].[News]    Script Date: 09/27/2017 13:19:08 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[News](
	[Id] [int] IDENTITY(10000,1) NOT NULL,
	[Title] [nvarchar](100) NULL,
	[Source] [nvarchar](100) NULL,
	[Logo] [nvarchar](200) NULL,
	[Labels] [nvarchar](100) NULL,
	[Category] [nvarchar](50) NULL,
	[Pubdate] [datetime] NULL,
	[Detail] [nvarchar](max) NULL,
 CONSTRAINT [PK_News] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


