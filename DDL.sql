USE [master]
GO
/****** Object:  Database [CST451]    Script Date: 12/19/2021 11:36:00 PM ******/
CREATE DATABASE [CST451]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'CST451', FILENAME = N'C:\Users\anoth\CST451.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'CST451_log', FILENAME = N'C:\Users\anoth\CST451_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [CST451] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [CST451].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [CST451] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [CST451] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [CST451] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [CST451] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [CST451] SET ARITHABORT OFF 
GO
ALTER DATABASE [CST451] SET AUTO_CLOSE OFF 
GO
ALTER DATABASE [CST451] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [CST451] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [CST451] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [CST451] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [CST451] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [CST451] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [CST451] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [CST451] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [CST451] SET  DISABLE_BROKER 
GO
ALTER DATABASE [CST451] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [CST451] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [CST451] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [CST451] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [CST451] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [CST451] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [CST451] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [CST451] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [CST451] SET  MULTI_USER 
GO
ALTER DATABASE [CST451] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [CST451] SET DB_CHAINING OFF 
GO
ALTER DATABASE [CST451] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [CST451] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [CST451] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [CST451] SET QUERY_STORE = OFF
GO
USE [CST451]
GO
ALTER DATABASE SCOPED CONFIGURATION SET LEGACY_CARDINALITY_ESTIMATION = OFF;
GO
ALTER DATABASE SCOPED CONFIGURATION SET MAXDOP = 0;
GO
ALTER DATABASE SCOPED CONFIGURATION SET PARAMETER_SNIFFING = ON;
GO
ALTER DATABASE SCOPED CONFIGURATION SET QUERY_OPTIMIZER_HOTFIXES = OFF;
GO
USE [CST451]
GO
/****** Object:  Table [dbo].[Appointments]    Script Date: 12/19/2021 11:36:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Appointments](
	[ID] [int] NOT NULL,
	[FROMUSER] [nchar](10) NOT NULL,
	[TOPRO] [nchar](10) NOT NULL,
	[SETDATE] [nchar](10) NOT NULL,
	[MSGREQUEST] [nchar](10) NULL,
	[APPROVE] [nchar](10) NULL,
	[FINISHED] [nchar](10) NULL,
 CONSTRAINT [PK_Appointments] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Logistics]    Script Date: 12/19/2021 11:36:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Logistics](
	[ID] [int] NOT NULL,
	[USERRATING] [nchar](257) NULL,
	[PRORATING] [nchar](257) NULL,
	[USERREPORT] [nchar](256) NULL,
	[PROREPORT] [nchar](256) NULL,
 CONSTRAINT [PK_Logistics] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Professionals]    Script Date: 12/19/2021 11:36:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Professionals](
	[ID] [int] NOT NULL,
	[PRONAME] [nchar](10) NOT NULL,
	[PROEMAIL] [nchar](10) NULL,
	[PROWEBSITE] [nchar](10) NOT NULL,
	[PRODESCRIPTION] [nchar](10) NOT NULL,
	[PROAVATAR] [nchar](10) NULL,
	[PROHEADER] [nchar](10) NULL,
	[USERID] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Professionals] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Users]    Script Date: 12/19/2021 11:36:00 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Users](
	[ID] [int] IDENTITY(1,1) NOT NULL,
	[FIRSTNAME] [nvarchar](150) NOT NULL,
	[LASTNAME] [nvarchar](150) NOT NULL,
	[USERNAME] [nvarchar](150) NOT NULL,
	[PASSWORD] [nvarchar](150) NOT NULL,
	[EMAIL] [nvarchar](150) NOT NULL,
	[NUMBER] [nvarchar](150) NULL,
	[AVATAR] [nvarchar](150) NULL,
	[SUSPENDED] [int] NULL,
PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Professionals]  WITH CHECK ADD  CONSTRAINT [FK_ProTable_Users] FOREIGN KEY([ID])
REFERENCES [dbo].[Users] ([ID])
GO
ALTER TABLE [dbo].[Professionals] CHECK CONSTRAINT [FK_ProTable_Users]
GO
USE [master]
GO
ALTER DATABASE [CST451] SET  READ_WRITE 
GO
