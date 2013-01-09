/****** Object:  Table [dbo].[dbip_city_stage]    Script Date: 1/9/2013 9:54:31 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

SET ANSI_PADDING ON
GO

CREATE TABLE [dbo].[dbip_city_stage](
	[ip_start] [varchar](50) NOT NULL,
	[ip_end] [varchar](50) NOT NULL,
	[city] [nvarchar](255) NULL,
	[region] [nvarchar](255) NULL,
	[country] [nvarchar](255) NULL,
	[Type] [int] NOT NULL,
 CONSTRAINT [PK_dbip_city_stage_1] PRIMARY KEY CLUSTERED 
(
	[ip_start] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

SET ANSI_PADDING OFF
GO

ALTER TABLE [dbo].[dbip_city_stage] ADD  CONSTRAINT [DF_dbip_city_stage_Type]  DEFAULT ((4)) FOR [Type]
GO