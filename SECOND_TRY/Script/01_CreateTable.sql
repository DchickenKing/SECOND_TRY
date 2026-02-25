-- 1. 建立資料庫 (若已建立可略過此行)
CREATE DATABASE WeatherExamDB;
GO

USE WeatherExamDB;
GO

-- 2. 建立氣象紀錄表
CREATE TABLE WeatherLogs (
    Id INT PRIMARY KEY IDENTITY(1,1),
    CityName NVARCHAR(50) NOT NULL,
    Temperature DECIMAL(18,2) NOT NULL,
    Humidity DECIMAL(18,2) NOT NULL,
    RecordTime DATETIME DEFAULT GETDATE()
);
GO