﻿IF EXISTS(select * from sys.databases where name='GSATest')
DROP DATABASE GSATest
GO
CREATE DATABASE [GSATest]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'GSATest_data', FILENAME = N'C:\Temp\GSATest_data.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'GSATest_log', FILENAME = N'C:\Temp\GSATest_log.ldf' , SIZE = 73728KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [GSATest] SET COMPATIBILITY_LEVEL = 130
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [GSATest].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [GSATest] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [GSATest] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [GSATest] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [GSATest] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [GSATest] SET ARITHABORT OFF 
GO
ALTER DATABASE [GSATest] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [GSATest] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [GSATest] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [GSATest] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [GSATest] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [GSATest] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [GSATest] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [GSATest] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [GSATest] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [GSATest] SET  ENABLE_BROKER 
GO
ALTER DATABASE [GSATest] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [GSATest] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [GSATest] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [GSATest] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [GSATest] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [GSATest] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [GSATest] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [GSATest] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [GSATest] SET  MULTI_USER 
GO
ALTER DATABASE [GSATest] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [GSATest] SET DB_CHAINING OFF 
GO
ALTER DATABASE [GSATest] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [GSATest] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [GSATest] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [GSATest] SET QUERY_STORE = OFF
GO