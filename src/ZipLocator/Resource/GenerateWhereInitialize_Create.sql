/* Create ZipCode Table  */
IF Not EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[**tblZipCode**]') AND type in (N'U'))

CREATE TABLE [dbo].[**tblZipCode**](
	[ZIPCode] [varchar](5) NOT NULL,
	[Latitude] [float] NOT NULL,
	[Longitude] [float] NOT NULL
) ON [PRIMARY]

GO


/* Create Mindharbor_DistanceAssistant Function */

IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[Mindharbor_DistanceAssistant]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Mindharbor_DistanceAssistant]

GO

CREATE FUNCTION [dbo].[Mindharbor_DistanceAssistant] (	@dblLat1 REAL,	@dblLong1 REAL,	@dblLat2 REAL,	@dblLong2 REAL)  RETURNS REAL  AS BEGIN	DECLARE @x REAL	SET @x = 0.0	IF @dblLat1 <> @dblLat2 OR @dblLong1 <> @dblLong2 BEGIN		SET @dblLat1 = @dblLat1 * PI() / 180		SET @dblLong1 = @dblLong1 * PI() / 180		SET @dblLat2 = @dblLat2 * PI() / 180		SET @dblLong2 = @dblLong2* PI() / 180		SET @x = Sin(@dblLat1) * Sin(@dblLat2) + Cos(@dblLat1) * Cos(@dblLat2) * Cos(@dblLong2 - @dblLong1)		IF 1 = @x SET @x = 0		ELSE SET @x = 3963 * (-1 * Atan(@x / Sqrt(1 - @x * @x)) + PI() / 2)	END	RETURN @x END

GO

/* Create Mindharbor_RadiusAssistant Function */
 
IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[Mindharbor_RadiusAssistant]') AND type in (N'FN', N'IF', N'TF', N'FS', N'FT'))
DROP FUNCTION [dbo].[Mindharbor_RadiusAssistant]

GO
 
CREATE FUNCTION [dbo].[Mindharbor_RadiusAssistant] (
@ZIPCode NCHAR(5),
@Miles REAL
) RETURNS @MaxPoints TABLE (
 Latitude REAL,Longitude REAL,MaxLat REAL,MinLat REAL,MaxLong REAL,MinLong REAL)
 AS BEGIN DECLARE @Latitude REAL
SET @Latitude = 0 
DECLARE @Longitude REAL
 SET @Longitude = 0
 DECLARE @MaxLat REAL
 SET @MaxLat = 0
DECLARE @MinLat REAL 
SET @MinLat = 0 
DECLARE @MaxLong REAL SET @MaxLong = 0 
DECLARE @MinLong REAL
SET @MinLong = 0
 SELECT @Latitude = Latitude, @Longitude = Longitude FROM **tblZipCode** WHERE ZIPCode = @ZIPCode	IF 0 <> @@ROWCOUNT BEGIN	DECLARE @MilesPerDegree REAL	SET @MilesPerDegree = 69.172	SET @MaxLat = @Latitude + @Miles / @MilesPerDegree	SET @MinLat = @Latitude - (@MaxLat - @Latitude)	SET @MaxLong = @Longitude + @Miles / (Cos(@MinLat * PI() / 180) * @MilesPerDegree)		SET @MinLong = @Longitude - (@MaxLong - @Longitude)	INSERT @MaxPoints SELECT @Latitude,@Longitude,@MaxLat,@MinLat,@MaxLong,@MinLong
END	RETURN END


GO
/****** Object:  StoredProcedure [dbo].[InsertZipCodeDataToTable]    Script Date: 08/24/2007 09:34:28 ******/
IF  EXISTS (SELECT * FROM sysobjects WHERE id = OBJECT_ID(N'[dbo].[**sp_InsertZipCodeDataToTable**]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[**sp_InsertZipCodeDataToTable**]
	
GO


CREATE PROCEDURE [dbo].[**sp_InsertZipCodeDataToTable**]
	@ZIPCode char(5),
	@Latitude float,
	@Longitude float
 AS

insert into **tblZipCode**(ZIPCode,Latitude,Longitude) values(@ZIPCode,@Latitude,@Longitude) 
	
	