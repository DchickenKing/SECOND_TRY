using Dapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using MercuryTech_Test.Models; // 請確認這裡的 namespace 和你的專案一致
using System.Data;
using System.Text.Json;

namespace MercuryTech_Test.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WeatherController : ControllerBase
    {
        private readonly string _connectionString;

        public WeatherController(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection") ?? "";
        }

        // 1. 批次寫入 (POST)
        [HttpPost("upload")]
        public IActionResult UploadWeatherData([FromBody] List<WeatherModel> inputData)
        {
            if (inputData == null || inputData.Count == 0) return BadRequest("無效的資料格式");

            try
            {
                string jsonString = JsonSerializer.Serialize(inputData);
                using (var conn = new SqlConnection(_connectionString))
                {
                    conn.Execute("SP_AddWeatherBatch", new { JsonData = jsonString }, commandType: CommandType.StoredProcedure);
                }
                return Ok(new { message = $"成功批次寫入 {inputData.Count} 筆氣象資料" });
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        // 2. 查詢城市 (GET)
        [HttpGet("search")]
        public IActionResult GetWeatherByCity([FromQuery] string city)
        {
            if (string.IsNullOrWhiteSpace(city)) return BadRequest("請提供城市名稱 (city)");

            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    var result = conn.Query<WeatherModel>(
                        "SP_GetWeatherByCity",
                        new { CityName = city },
                        commandType: CommandType.StoredProcedure
                    ).ToList();

                    if (result.Count == 0) return NotFound($"找不到 {city} 的相關紀錄");
                    return Ok(result);
                }
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }

        // 3. 刪除資料 (DELETE)
        [HttpDelete("{id}")]
        public IActionResult DeleteWeather(int id)
        {
            try
            {
                using (var conn = new SqlConnection(_connectionString))
                {
                    int rows = conn.ExecuteScalar<int>("SP_DeleteWeather", new { Id = id }, commandType: CommandType.StoredProcedure);
                    return rows > 0 ? Ok(new { message = "刪除成功" }) : NotFound("找不到該筆資料，無法刪除");
                }
            }
            catch (Exception ex) { return StatusCode(500, ex.Message); }
        }
    }
}