CREATE TABLE [dbo].[Sale](
    [Id] [int] IDENTITY(1,1) NOT NULL,
    [Region] [varchar](25) NOT NULL,
    [Person] [varchar](25) NOT NULL,
    [Item] [varchar](25) NOT NULL,
    [Units] [int] NOT NULL,
    [UnitCost] [money] NOT NULL,
    [Total] [money] NOT NULL,
    [AddedOn] [date] NOT NULL,
 CONSTRAINT [PK_Sale] PRIMARY KEY CLUSTERED 
(
    [Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]