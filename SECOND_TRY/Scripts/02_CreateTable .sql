USE WeatherExamDB;
GO

-- 1. 批次新增氣象資料 (使用 OPENJSON 解析)
CREATE OR ALTER PROCEDURE SP_AddWeatherBatch
    @JsonData NVARCHAR(MAX)
AS
BEGIN
    SET NOCOUNT ON;
    
    INSERT INTO WeatherLogs (CityName, Temperature, Humidity)
    SELECT CityName, Temperature, Humidity
    FROM OPENJSON(@JsonData)
    WITH (
        CityName NVARCHAR(50) '$.CityName',
        Temperature DECIMAL(18,2) '$.Temperature',
        Humidity DECIMAL(18,2) '$.Humidity'
    );
END
GO

-- 2. 查詢特定城市的氣象紀錄
CREATE OR ALTER PROCEDURE SP_GetWeatherByCity
    @CityName NVARCHAR(50)
AS
BEGIN
    SET NOCOUNT ON;
    
    SELECT Id, CityName, Temperature, Humidity, RecordTime 
    FROM WeatherLogs 
    WHERE CityName = @CityName
    ORDER BY RecordTime DESC;
END
GO

-- 3. 刪除單筆紀錄
CREATE OR ALTER PROCEDURE SP_DeleteWeather
    @Id INT
AS
BEGIN
    SET NOCOUNT ON;
    
    DELETE FROM WeatherLogs WHERE Id = @Id;
    SELECT @@ROWCOUNT; -- 回傳受影響的筆數給 C# 判斷
END
GO