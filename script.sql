USE [master]
GO
/****** Object:  Database [ChessResultDB]    Script Date: 09/08/2019 8:31:31 PM ******/
CREATE DATABASE [ChessResultDB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'ChessResultDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLSERVER\MSSQL\DATA\ChessResultDB.mdf' , SIZE = 3264KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'ChessResultDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL12.SQLSERVER\MSSQL\DATA\ChessResultDB_log.ldf' , SIZE = 816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [ChessResultDB] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [ChessResultDB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [ChessResultDB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [ChessResultDB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [ChessResultDB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [ChessResultDB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [ChessResultDB] SET ARITHABORT OFF 
GO
ALTER DATABASE [ChessResultDB] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [ChessResultDB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [ChessResultDB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [ChessResultDB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [ChessResultDB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [ChessResultDB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [ChessResultDB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [ChessResultDB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [ChessResultDB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [ChessResultDB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [ChessResultDB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [ChessResultDB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [ChessResultDB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [ChessResultDB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [ChessResultDB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [ChessResultDB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [ChessResultDB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [ChessResultDB] SET RECOVERY FULL 
GO
ALTER DATABASE [ChessResultDB] SET  MULTI_USER 
GO
ALTER DATABASE [ChessResultDB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [ChessResultDB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [ChessResultDB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [ChessResultDB] SET TARGET_RECOVERY_TIME = 0 SECONDS 
GO
ALTER DATABASE [ChessResultDB] SET DELAYED_DURABILITY = DISABLED 
GO
EXEC sys.sp_db_vardecimal_storage_format N'ChessResultDB', N'ON'
GO
USE [ChessResultDB]
GO
/****** Object:  User [vikute]    Script Date: 09/08/2019 8:31:31 PM ******/
CREATE USER [vikute] FOR LOGIN [vikute] WITH DEFAULT_SCHEMA=[dbo]
GO
ALTER ROLE [db_owner] ADD MEMBER [vikute]
GO
ALTER ROLE [db_accessadmin] ADD MEMBER [vikute]
GO
ALTER ROLE [db_securityadmin] ADD MEMBER [vikute]
GO
ALTER ROLE [db_ddladmin] ADD MEMBER [vikute]
GO
ALTER ROLE [db_backupoperator] ADD MEMBER [vikute]
GO
ALTER ROLE [db_datareader] ADD MEMBER [vikute]
GO
ALTER ROLE [db_datawriter] ADD MEMBER [vikute]
GO
/****** Object:  Table [dbo].[Federation]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Federation](
	[FederationID] [int] NOT NULL,
	[Name] [varchar](100) NULL,
	[Flag] [varchar](255) NULL,
	[Acronym] [varchar](50) NULL,
 CONSTRAINT [PK_National] PRIMARY KEY CLUSTERED 
(
	[FederationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[FederationParticipate]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[FederationParticipate](
	[TourID] [int] NOT NULL,
	[FederationID] [int] NOT NULL,
 CONSTRAINT [PK_FederationParticipate] PRIMARY KEY CLUSTERED 
(
	[TourID] ASC,
	[FederationID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Group]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Group](
	[ID] [int] NOT NULL,
	[Name] [nchar](10) NULL,
	[Desciption] [varchar](max) NULL,
 CONSTRAINT [PK_Group] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Pairing]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pairing](
	[PairingID] [int] NOT NULL,
	[TimeStart] [datetime] NULL,
	[TourID] [int] NOT NULL,
	[RoundID] [int] NOT NULL,
 CONSTRAINT [PK_Pairing_1] PRIMARY KEY CLUSTERED 
(
	[PairingID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Player]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Player](
	[PlayerID] [int] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[FederationID] [int] NULL,
	[Sex] [varchar](50) NULL,
	[Birthdate] [date] NULL,
	[Image] [nvarchar](250) NULL,
	[Rating] [int] NULL,
	[FIDEId] [int] NULL,
 CONSTRAINT [PK_FIDEprofile] PRIMARY KEY CLUSTERED 
(
	[PlayerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[PlayerInPair]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PlayerInPair](
	[PairingID] [int] NOT NULL,
	[PlayerID] [int] NOT NULL,
	[IsWhite] [bit] NULL,
	[Mark] [float] NULL,
 CONSTRAINT [PK_PlayerInPair] PRIMARY KEY CLUSTERED 
(
	[PairingID] ASC,
	[PlayerID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Role]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Role](
	[ID] [int] NOT NULL,
	[Name] [varchar](100) NULL,
	[Description] [varchar](max) NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[Round]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Round](
	[ID] [int] NOT NULL,
	[Name] [nchar](10) NULL,
	[Description] [nchar](10) NULL,
 CONSTRAINT [PK_Round] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[TourGroup]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TourGroup](
	[TournamentID] [int] NOT NULL,
	[GroupID] [int] NOT NULL,
 CONSTRAINT [PK_TourGroup_1] PRIMARY KEY CLUSTERED 
(
	[TournamentID] ASC,
	[GroupID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Tournament]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Tournament](
	[TournamentID] [int] NOT NULL,
	[Name] [nvarchar](250) NULL,
	[FederationID] [int] NULL,
	[Description] [varchar](max) NULL,
	[StartDate] [datetime] NULL,
	[EndDate] [datetime] NULL,
	[Winner] [int] NULL,
	[CreatedDate] [date] NULL,
	[UpdateDate] [date] NULL,
	[Status] [bit] NULL,
	[Organizer] [varchar](250) NULL,
	[Director] [varchar](250) NULL,
	[Location] [varchar](500) NULL,
	[ParentTourID] [int] NULL,
 CONSTRAINT [PK_Tournament] PRIMARY KEY CLUSTERED 
(
	[TournamentID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[TourRound]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[TourRound](
	[TourID] [int] NOT NULL,
	[RoundID] [int] NOT NULL,
 CONSTRAINT [PK_TourRound] PRIMARY KEY CLUSTERED 
(
	[TourID] ASC,
	[RoundID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[User]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[User](
	[ID] [int] NOT NULL,
	[UserName] [varchar](250) NULL,
	[Password] [varchar](250) NULL,
	[RoleID] [int] NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
INSERT [dbo].[Federation] ([FederationID], [Name], [Flag], [Acronym]) VALUES (1, N'Angola', N'Angola.png', N'ANG')
INSERT [dbo].[Federation] ([FederationID], [Name], [Flag], [Acronym]) VALUES (2, N'Argentina', N'Argentina.png', N'ARG')
INSERT [dbo].[Federation] ([FederationID], [Name], [Flag], [Acronym]) VALUES (3, N'Brazil', N'Brazil.png', N'BRA')
INSERT [dbo].[Federation] ([FederationID], [Name], [Flag], [Acronym]) VALUES (4, N'Canada', N'Canada.png', N'CAN')
INSERT [dbo].[Federation] ([FederationID], [Name], [Flag], [Acronym]) VALUES (5, N'India', N'India.png', N'IND')
INSERT [dbo].[Federation] ([FederationID], [Name], [Flag], [Acronym]) VALUES (6, N'Vietnam', N'Vietnam.png', N'VIE')
INSERT [dbo].[FederationParticipate] ([TourID], [FederationID]) VALUES (1, 6)
INSERT [dbo].[Pairing] ([PairingID], [TimeStart], [TourID], [RoundID]) VALUES (1, NULL, 1, 1)
INSERT [dbo].[Pairing] ([PairingID], [TimeStart], [TourID], [RoundID]) VALUES (2, NULL, 1, 1)
INSERT [dbo].[Pairing] ([PairingID], [TimeStart], [TourID], [RoundID]) VALUES (3, NULL, 1, 1)
INSERT [dbo].[Pairing] ([PairingID], [TimeStart], [TourID], [RoundID]) VALUES (4, NULL, 1, 2)
INSERT [dbo].[Pairing] ([PairingID], [TimeStart], [TourID], [RoundID]) VALUES (5, NULL, 1, 2)
INSERT [dbo].[Player] ([PlayerID], [Name], [FederationID], [Sex], [Birthdate], [Image], [Rating], [FIDEId]) VALUES (1, N'Kala', 6, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Player] ([PlayerID], [Name], [FederationID], [Sex], [Birthdate], [Image], [Rating], [FIDEId]) VALUES (2, N'Fun', 6, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Player] ([PlayerID], [Name], [FederationID], [Sex], [Birthdate], [Image], [Rating], [FIDEId]) VALUES (3, N'Tin', 6, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Player] ([PlayerID], [Name], [FederationID], [Sex], [Birthdate], [Image], [Rating], [FIDEId]) VALUES (4, N'Hoa', 6, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Player] ([PlayerID], [Name], [FederationID], [Sex], [Birthdate], [Image], [Rating], [FIDEId]) VALUES (5, N'Thien', 6, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Player] ([PlayerID], [Name], [FederationID], [Sex], [Birthdate], [Image], [Rating], [FIDEId]) VALUES (6, N'Nhan', 6, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Player] ([PlayerID], [Name], [FederationID], [Sex], [Birthdate], [Image], [Rating], [FIDEId]) VALUES (7, N'Tai', 6, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Player] ([PlayerID], [Name], [FederationID], [Sex], [Birthdate], [Image], [Rating], [FIDEId]) VALUES (8, N'Kai', NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[PlayerInPair] ([PairingID], [PlayerID], [IsWhite], [Mark]) VALUES (1, 1, 1, 1)
INSERT [dbo].[PlayerInPair] ([PairingID], [PlayerID], [IsWhite], [Mark]) VALUES (1, 2, 0, 0)
INSERT [dbo].[PlayerInPair] ([PairingID], [PlayerID], [IsWhite], [Mark]) VALUES (2, 3, 1, 0)
INSERT [dbo].[PlayerInPair] ([PairingID], [PlayerID], [IsWhite], [Mark]) VALUES (2, 4, 0, 0)
INSERT [dbo].[PlayerInPair] ([PairingID], [PlayerID], [IsWhite], [Mark]) VALUES (4, 1, 0, 2)
INSERT [dbo].[PlayerInPair] ([PairingID], [PlayerID], [IsWhite], [Mark]) VALUES (4, 5, 1, 2)
INSERT [dbo].[PlayerInPair] ([PairingID], [PlayerID], [IsWhite], [Mark]) VALUES (5, 6, 1, 1)
INSERT [dbo].[PlayerInPair] ([PairingID], [PlayerID], [IsWhite], [Mark]) VALUES (5, 7, 0, 0)
INSERT [dbo].[Round] ([ID], [Name], [Description]) VALUES (1, N'Round1    ', NULL)
INSERT [dbo].[Round] ([ID], [Name], [Description]) VALUES (2, N'Round2    ', NULL)
INSERT [dbo].[Round] ([ID], [Name], [Description]) VALUES (3, N'Round3    ', NULL)
INSERT [dbo].[Round] ([ID], [Name], [Description]) VALUES (4, N'Round4    ', NULL)
INSERT [dbo].[Tournament] ([TournamentID], [Name], [FederationID], [Description], [StartDate], [EndDate], [Winner], [CreatedDate], [UpdateDate], [Status], [Organizer], [Director], [Location], [ParentTourID]) VALUES (1, N'Giải Cờ vua Vietchess Văn Quán mở rộng tháng 9 năm 2019', 6, NULL, CAST(N'2019-09-03 00:00:00.000' AS DateTime), CAST(N'2919-09-20 00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Tournament] ([TournamentID], [Name], [FederationID], [Description], [StartDate], [EndDate], [Winner], [CreatedDate], [UpdateDate], [Status], [Organizer], [Director], [Location], [ParentTourID]) VALUES (2, N'	Sub 18 Serial Chihuahua Fecha 4 TEC II 90 Finish', 2, NULL, CAST(N'2019-09-01 00:00:00.000' AS DateTime), CAST(N'2019-10-01 00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Tournament] ([TournamentID], [Name], [FederationID], [Description], [StartDate], [EndDate], [Winner], [CreatedDate], [UpdateDate], [Status], [Organizer], [Director], [Location], [ParentTourID]) VALUES (3, N'Chess Wiz Season-3 Junior Chess Championship 2019- Under 12 Open', 2, NULL, CAST(N'2019-09-01 00:00:00.000' AS DateTime), CAST(N'2019-10-01 00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[Tournament] ([TournamentID], [Name], [FederationID], [Description], [StartDate], [EndDate], [Winner], [CreatedDate], [UpdateDate], [Status], [Organizer], [Director], [Location], [ParentTourID]) VALUES (4, N'Sub 8 Serial Chihuahua Fecha 4 TEC II 90 Finish', 3, NULL, CAST(N'2019-09-01 00:00:00.000' AS DateTime), CAST(N'2019-09-15 00:00:00.000' AS DateTime), NULL, NULL, NULL, NULL, NULL, NULL, NULL, NULL)
INSERT [dbo].[TourRound] ([TourID], [RoundID]) VALUES (1, 1)
INSERT [dbo].[TourRound] ([TourID], [RoundID]) VALUES (1, 2)
INSERT [dbo].[TourRound] ([TourID], [RoundID]) VALUES (1, 3)
INSERT [dbo].[TourRound] ([TourID], [RoundID]) VALUES (2, 1)
INSERT [dbo].[TourRound] ([TourID], [RoundID]) VALUES (2, 2)
ALTER TABLE [dbo].[FederationParticipate]  WITH CHECK ADD  CONSTRAINT [FK_FederationParticipate_Federation] FOREIGN KEY([FederationID])
REFERENCES [dbo].[Federation] ([FederationID])
GO
ALTER TABLE [dbo].[FederationParticipate] CHECK CONSTRAINT [FK_FederationParticipate_Federation]
GO
ALTER TABLE [dbo].[FederationParticipate]  WITH CHECK ADD  CONSTRAINT [FK_FederationParticipate_Tournament] FOREIGN KEY([TourID])
REFERENCES [dbo].[Tournament] ([TournamentID])
GO
ALTER TABLE [dbo].[FederationParticipate] CHECK CONSTRAINT [FK_FederationParticipate_Tournament]
GO
ALTER TABLE [dbo].[Pairing]  WITH CHECK ADD  CONSTRAINT [FK_Pairing_TourRound] FOREIGN KEY([TourID], [RoundID])
REFERENCES [dbo].[TourRound] ([TourID], [RoundID])
GO
ALTER TABLE [dbo].[Pairing] CHECK CONSTRAINT [FK_Pairing_TourRound]
GO
ALTER TABLE [dbo].[Player]  WITH CHECK ADD  CONSTRAINT [FK_Player_Federation] FOREIGN KEY([FederationID])
REFERENCES [dbo].[Federation] ([FederationID])
GO
ALTER TABLE [dbo].[Player] CHECK CONSTRAINT [FK_Player_Federation]
GO
ALTER TABLE [dbo].[PlayerInPair]  WITH CHECK ADD  CONSTRAINT [FK_PlayerInPair_Pairing] FOREIGN KEY([PairingID])
REFERENCES [dbo].[Pairing] ([PairingID])
GO
ALTER TABLE [dbo].[PlayerInPair] CHECK CONSTRAINT [FK_PlayerInPair_Pairing]
GO
ALTER TABLE [dbo].[PlayerInPair]  WITH CHECK ADD  CONSTRAINT [FK_PlayerInPair_Player] FOREIGN KEY([PlayerID])
REFERENCES [dbo].[Player] ([PlayerID])
GO
ALTER TABLE [dbo].[PlayerInPair] CHECK CONSTRAINT [FK_PlayerInPair_Player]
GO
ALTER TABLE [dbo].[TourGroup]  WITH CHECK ADD  CONSTRAINT [FK_TourGroup_Group] FOREIGN KEY([GroupID])
REFERENCES [dbo].[Group] ([ID])
GO
ALTER TABLE [dbo].[TourGroup] CHECK CONSTRAINT [FK_TourGroup_Group]
GO
ALTER TABLE [dbo].[TourGroup]  WITH CHECK ADD  CONSTRAINT [FK_TourGroup_Tournament] FOREIGN KEY([TournamentID])
REFERENCES [dbo].[Tournament] ([TournamentID])
GO
ALTER TABLE [dbo].[TourGroup] CHECK CONSTRAINT [FK_TourGroup_Tournament]
GO
ALTER TABLE [dbo].[Tournament]  WITH CHECK ADD  CONSTRAINT [FK_Tournament_National] FOREIGN KEY([FederationID])
REFERENCES [dbo].[Federation] ([FederationID])
GO
ALTER TABLE [dbo].[Tournament] CHECK CONSTRAINT [FK_Tournament_National]
GO
ALTER TABLE [dbo].[Tournament]  WITH CHECK ADD  CONSTRAINT [FK_Tournament_Tournament] FOREIGN KEY([ParentTourID])
REFERENCES [dbo].[Tournament] ([TournamentID])
GO
ALTER TABLE [dbo].[Tournament] CHECK CONSTRAINT [FK_Tournament_Tournament]
GO
ALTER TABLE [dbo].[TourRound]  WITH CHECK ADD  CONSTRAINT [FK_TourRound_Round] FOREIGN KEY([RoundID])
REFERENCES [dbo].[Round] ([ID])
GO
ALTER TABLE [dbo].[TourRound] CHECK CONSTRAINT [FK_TourRound_Round]
GO
ALTER TABLE [dbo].[TourRound]  WITH CHECK ADD  CONSTRAINT [FK_TourRound_Tournament] FOREIGN KEY([TourID])
REFERENCES [dbo].[Tournament] ([TournamentID])
GO
ALTER TABLE [dbo].[TourRound] CHECK CONSTRAINT [FK_TourRound_Tournament]
GO
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_User_Role] FOREIGN KEY([RoleID])
REFERENCES [dbo].[Role] ([ID])
GO
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_User_Role]
GO
/****** Object:  StoredProcedure [dbo].[FederationManagement_GetAllFederation]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FederationManagement_GetAllFederation] 
AS
BEGIN
	SELECT	Federation.FederationID, 
			Federation.Name, 
			Federation.Acronym, 
			Federation.Flag		
	FROM	Federation
END

GO
/****** Object:  StoredProcedure [dbo].[FederationManagement_GetFederationByID]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FederationManagement_GetFederationByID] 
	@ID		INT		= 0
AS
BEGIN
	SELECT	Federation.FederationID, 
			Federation.Name, 
			Federation.Acronym, 
			Federation.Flag
	FROM	Federation 
	WHERE	Federation.FederationID = @ID;
END

GO
/****** Object:  StoredProcedure [dbo].[FederationManagement_GetFederationParticipate]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FederationManagement_GetFederationParticipate] 
	@TourId		INT		= 0
AS
BEGIN
	SELECT	Federation.FederationID, 
			Federation.Name, 
			Federation.Acronym, 
			Federation.Flag
	FROM	Federation 
			INNER JOIN FederationParticipate 
				ON Federation.FederationID = FederationParticipate.FederationID 
	WHERE	FederationParticipate.TourID = @TourId;
END

GO
/****** Object:  StoredProcedure [dbo].[FideProfileManagement_CreateFideProfile]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[FideProfileManagement_CreateFideProfile] 
	@Id             INT           = 0,
	@Name           VARCHAR(250)  = NULL,
	@FederationID   INT			  = NULL,
	@Sex            VARCHAR(50)   = NULL,
	@Birthdate      DATE		  = NULL,
	@Image          VARCHAR(250)  = NULL,
	@Rating         int		      = NULL
	
AS
BEGIN
	INSERT INTO Player (ID,
						Name,
						FederationID,
						Sex,
						Birthdate,
						Image,
						Rating )
				VALUES (@Id,
						@Name, 
						@FederationID, 
						@Sex, 
						@Birthdate, 
						@Image, 
						@Rating) 
END

GO
/****** Object:  StoredProcedure [dbo].[RoundManagement_GetListRoundByTournament]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RoundManagement_GetListRoundByTournament]
	@TourID		INT		= 0
AS
BEGIN
	SELECT  Round.ID,
			Round.Name
	FROM	TourRound INNER JOIN Round
				ON TourRound.RoundID = Round.ID
	WHERE	TourRound.TourID = @TourID
END

GO
/****** Object:  StoredProcedure [dbo].[RoundManager_GetListPairing]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[RoundManager_GetListPairing]
	@RoundID		INT			= 0,
	@TourID			INT			= 0
AS
BEGIN
	SELECT      Pairing.TourID,
				Pairing.RoundID,
				Player.Name,
				Player.PlayerID, 
				PlayerInPair.Mark,
				Pairing.PairingID
	FROM        Pairing 
	INNER JOIN
                PlayerInPair 
				ON Pairing.PairingID = PlayerInPair.PairingID 
				INNER JOIN Player 
					ON PlayerInPair.PlayerID = Player.PlayerID
	WHERE		Pairing.RoundID = @RoundID
				AND Pairing.TourID = @TourID
END

GO
/****** Object:  StoredProcedure [dbo].[Tourmanager_GetRank]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Tourmanager_GetRank]
	@TourID		INT			= 0
AS
BEGIN
	SELECT    dbo.Player.PlayerID, dbo.Player.Name, SUM(dbo.PlayerInPair.Mark) AS TotalMark, dbo.Pairing.TourID
	FROM      dbo.Player 
			  INNER JOIN dbo.PlayerInPair 
				ON dbo.Player.PlayerID = dbo.PlayerInPair.PlayerID 
				INNER JOIN dbo.Pairing 
				ON dbo.PlayerInPair.PairingID = dbo.Pairing.PairingID
	WHERE	dbo.Pairing.TourID = @TourID
GROUP BY dbo.Player.PlayerID, dbo.Player.Name, dbo.Pairing.TourID
END

GO
/****** Object:  StoredProcedure [dbo].[TournamentManagement_FindTournament]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TournamentManagement_FindTournament]
	@Name			NVARCHAR(100)		= NULL,
	@StartDate		DATE				= NULL,
	@EndDate		DATE				= NULL,
	@FederationId	INT					= 0
AS
BEGIN
	SELECT	Tournament.TournamentID,
			Tournament.Name,
			Tournament.Organizer,
			Tournament.Director,
			Tournament.Location,
			Tournament.StartDate,
			Tournament.EndDate,
			Tournament.UpdateDate,
			Federation.FederationID, 
			Federation.Name, 
			Federation.Acronym, 
			Federation.Flag
	FROM	Tournament
			LEFT JOIN Federation 
				ON Tournament.FederationID = Federation.FederationID 
	WHERE	Tournament.Name LIKE '%@Name%'
				AND Tournament.StartDate = @StartDate
				AND Tournament.EndDate = @EndDate
END

GO
/****** Object:  StoredProcedure [dbo].[TournamentManagement_GetAllTournaments]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TournamentManagement_GetAllTournaments]

AS
BEGIN
	SELECT	TOP(50)
			Tournament.TournamentID,
			Tournament.Name,
			Tournament.Organizer,
			Tournament.Director,
			Tournament.Location,
			Tournament.StartDate,
			Tournament.EndDate,
			Tournament.UpdateDate,
			Federation.FederationID, 
			Federation.Name, 
			Federation.Acronym, 
			Federation.Flag	
	FROM Tournament
			LEFT JOIN Federation 
				ON Tournament.FederationID = Federation.FederationID;
END

GO
/****** Object:  StoredProcedure [dbo].[TournamentManagement_GetTournamentByFederation]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TournamentManagement_GetTournamentByFederation]
	@FederationID		INT		= 0
AS
BEGIN
	SELECT	Tournament.TournamentID,
			Tournament.Name,
			Tournament.Organizer,
			Tournament.Director,
			Tournament.Location,
			Tournament.StartDate,
			Tournament.EndDate,
			Tournament.UpdateDate,
			Federation.FederationID, 
			Federation.Name, 
			Federation.Acronym, 
			Federation.Flag	
	FROM Tournament
			LEFT JOIN Federation 
				ON Tournament.FederationID = Federation.FederationID
				AND Tournament.FederationID = @FederationID;
END

GO
/****** Object:  StoredProcedure [dbo].[TournamentManagement_GetTournamentByID]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TournamentManagement_GetTournamentByID]
	@Id             INT           = 0
AS
BEGIN
	SELECT	Tournament.TournamentID,
			Tournament.Name,
			Tournament.Organizer,
			Tournament.Director,
			Tournament.Location,
			Tournament.StartDate,
			Tournament.EndDate,
			Tournament.UpdateDate,
			Federation.FederationID, 
			Federation.Name, 
			Federation.Acronym, 
			Federation.Flag
	FROM Tournament
		LEFT JOIN Federation 
			ON Tournament.FederationID = Federation.FederationID 
	WHERE Tournament.TournamentID = @Id
END

GO
/****** Object:  StoredProcedure [dbo].[TournamentManagement_GetTournamentInProgress]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TournamentManagement_GetTournamentInProgress]
	
AS
BEGIN
	SELECT	Tournament.TournamentID,
			Tournament.Name,
			Tournament.Organizer,
			Tournament.Director,
			Tournament.Location,
			Tournament.StartDate,
			Tournament.EndDate,
			Tournament.UpdateDate,
			Federation.FederationID, 
			Federation.Name, 
			Federation.Acronym, 
			Federation.Flag
	FROM Tournament 
		LEFT JOIN Federation 
		ON Tournament.FederationID = Federation.FederationID
	WHERE Tournament.EndDate >= CAST(GETDATE() AS DATE)
		AND Tournament.StartDate <= CAST(GETDATE() AS DATE)
END

GO
/****** Object:  StoredProcedure [dbo].[TournamentManagement_GetTournamentInProgressByFederation]    Script Date: 09/08/2019 8:31:31 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[TournamentManagement_GetTournamentInProgressByFederation]
	@FederationId		INT			=0
AS
BEGIN
	SELECT	Tournament.TournamentID,
			Tournament.Name,
			Tournament.Organizer,
			Tournament.Director,
			Tournament.Location,
			Tournament.StartDate,
			Tournament.EndDate,
			Tournament.UpdateDate,
			Federation.FederationID, 
			Federation.Name, 
			Federation.Acronym, 
			Federation.Flag
	FROM Tournament 
		LEFT JOIN Federation 
			ON Tournament.FederationID = Federation.FederationID
	WHERE Tournament.EndDate >= CAST(GETDATE() AS DATE)
		AND Tournament.StartDate <= CAST(GETDATE() AS DATE)
		AND Tournament.FederationID = @FederationId;
END

GO
USE [master]
GO
ALTER DATABASE [ChessResultDB] SET  READ_WRITE 
GO
