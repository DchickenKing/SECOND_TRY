# 🌤️ 城市氣象監測系統 (Weather Monitoring API)

本專案為後端工程師技術實作演練，旨在建立一個高效、安全且具備擴充性的 RESTful API 系統，用於處理全台灣各城市的氣象感測數據。

## 🛠️ 技術開發棧 (Tech Stack)
- **開發框架**: .NET 8.0 Web API (C#)
- **資料庫**: Microsoft SQL Server
- **資料存取**: Dapper ORM (輕量級、高效能)
- **結構設計**: 完全採用 **Stored Procedures (預存程序)** 進行資料操作，確保 SQL 注入防護與執行效能。
- **文件規範**: Swagger / OpenAPI

---

## 🏗️ 環境建置與啟動 (Setup)

### 1. 資料庫部署
請至專案內 `Script/` 資料夾，依序於 SQL Server 執行下列腳本：
- `01_CreateTable.sql`: 建立 `WeatherLogs` 核心資料表。
- `02_CreateSPs.sql`: 建立 CRUD 與進階篩選之預存程序。

### 2. 設定連線字串
請確認 `appsettings.json` 內的 `DefaultConnection` 符合您的 SQL Server 環境設定：
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=您的伺服器名稱;Database=WeatherExamDB;Trusted_Connection=True;TrustServerCertificate=True;"
}