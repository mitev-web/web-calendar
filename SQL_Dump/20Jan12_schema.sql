USE [master]
GO
/****** Object:  Database [WebCalendar]    Script Date: 01/20/2012 20:36:01 ******/
CREATE DATABASE [WebCalendar] ON  PRIMARY 
( NAME = N'WebCalendar', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\WebCalendar.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'WebCalendar_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.SQLEXPRESS\MSSQL\DATA\WebCalendar_log.ldf' , SIZE = 2560KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [WebCalendar] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [WebCalendar].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [WebCalendar] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [WebCalendar] SET ANSI_NULLS OFF
GO
ALTER DATABASE [WebCalendar] SET ANSI_PADDING OFF
GO
ALTER DATABASE [WebCalendar] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [WebCalendar] SET ARITHABORT OFF
GO
ALTER DATABASE [WebCalendar] SET AUTO_CLOSE ON
GO
ALTER DATABASE [WebCalendar] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [WebCalendar] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [WebCalendar] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [WebCalendar] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [WebCalendar] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [WebCalendar] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [WebCalendar] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [WebCalendar] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [WebCalendar] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [WebCalendar] SET  DISABLE_BROKER
GO
ALTER DATABASE [WebCalendar] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [WebCalendar] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [WebCalendar] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [WebCalendar] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [WebCalendar] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [WebCalendar] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [WebCalendar] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [WebCalendar] SET  READ_WRITE
GO
ALTER DATABASE [WebCalendar] SET RECOVERY SIMPLE
GO
ALTER DATABASE [WebCalendar] SET  MULTI_USER
GO
ALTER DATABASE [WebCalendar] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [WebCalendar] SET DB_CHAINING OFF
GO
USE [WebCalendar]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 01/20/2012 20:36:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[UserName] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](255) NOT NULL,
	[PasswordSalt] [nvarchar](255) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[FirstName] [nvarchar](50) NULL,
	[LastName] [nvarchar](50) NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Users__A9D105342F10007B] UNIQUE NONCLUSTERED 
(
	[Email] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY],
 CONSTRAINT [UQ__Users__C9F2845631EC6D26] UNIQUE NONCLUSTERED 
(
	[UserName] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Contacts]    Script Date: 01/20/2012 20:36:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Contacts](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Email] [nvarchar](50) NULL,
	[FirstName] [nvarchar](50) NOT NULL,
	[LastName] [nvarchar](50) NULL,
	[Phone] [nvarchar](50) NULL,
	[Address] [nvarchar](50) NULL,
	[Note] [text] NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_Contacts] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Categories]    Script Date: 01/20/2012 20:36:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categories](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Description] [text] NULL,
	[Name] [nvarchar](50) NOT NULL,
	[UserID] [int] NOT NULL,
 CONSTRAINT [PK_Categories] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Meetings]    Script Date: 01/20/2012 20:36:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Meetings](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[Date] [smalldatetime] NOT NULL,
	[Description] [text] NOT NULL,
	[Location] [nvarchar](50) NULL,
	[UserID] [int] NOT NULL,
	[CategoryID] [int] NOT NULL,
 CONSTRAINT [PK_Meetings] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[MeetingsContacts]    Script Date: 01/20/2012 20:36:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[MeetingsContacts](
	[MeetingID] [int] NOT NULL,
	[ContactID] [int] NOT NULL,
 CONSTRAINT [PK_MeetingsContacts] PRIMARY KEY CLUSTERED 
(
	[MeetingID] ASC,
	[ContactID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Default [DF_Meetings_CategoryID]    Script Date: 01/20/2012 20:36:02 ******/
ALTER TABLE [dbo].[Meetings] ADD  CONSTRAINT [DF_Meetings_CategoryID]  DEFAULT ((1)) FOR [CategoryID]
GO
/****** Object:  ForeignKey [FK_Contacts_Users]    Script Date: 01/20/2012 20:36:02 ******/
ALTER TABLE [dbo].[Contacts]  WITH CHECK ADD  CONSTRAINT [FK_Contacts_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Contacts] CHECK CONSTRAINT [FK_Contacts_Users]
GO
/****** Object:  ForeignKey [FK_Categories_Users]    Script Date: 01/20/2012 20:36:02 ******/
ALTER TABLE [dbo].[Categories]  WITH CHECK ADD  CONSTRAINT [FK_Categories_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Categories] CHECK CONSTRAINT [FK_Categories_Users]
GO
/****** Object:  ForeignKey [FK_Meetings_Categories]    Script Date: 01/20/2012 20:36:02 ******/
ALTER TABLE [dbo].[Meetings]  WITH CHECK ADD  CONSTRAINT [FK_Meetings_Categories] FOREIGN KEY([CategoryID])
REFERENCES [dbo].[Categories] ([ID])
GO
ALTER TABLE [dbo].[Meetings] CHECK CONSTRAINT [FK_Meetings_Categories]
GO
/****** Object:  ForeignKey [FK_Meetings_Users]    Script Date: 01/20/2012 20:36:02 ******/
ALTER TABLE [dbo].[Meetings]  WITH CHECK ADD  CONSTRAINT [FK_Meetings_Users] FOREIGN KEY([UserID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Meetings] CHECK CONSTRAINT [FK_Meetings_Users]
GO
/****** Object:  ForeignKey [FK_MeetingsContacts_Contacts]    Script Date: 01/20/2012 20:36:02 ******/
ALTER TABLE [dbo].[MeetingsContacts]  WITH CHECK ADD  CONSTRAINT [FK_MeetingsContacts_Contacts] FOREIGN KEY([ContactID])
REFERENCES [dbo].[Contacts] ([ID])
GO
ALTER TABLE [dbo].[MeetingsContacts] CHECK CONSTRAINT [FK_MeetingsContacts_Contacts]
GO
/****** Object:  ForeignKey [FK_MeetingsContacts_Meetings]    Script Date: 01/20/2012 20:36:02 ******/
ALTER TABLE [dbo].[MeetingsContacts]  WITH CHECK ADD  CONSTRAINT [FK_MeetingsContacts_Meetings] FOREIGN KEY([MeetingID])
REFERENCES [dbo].[Meetings] ([ID])
GO
ALTER TABLE [dbo].[MeetingsContacts] CHECK CONSTRAINT [FK_MeetingsContacts_Meetings]
GO
