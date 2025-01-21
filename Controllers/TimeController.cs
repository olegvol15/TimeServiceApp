using Microsoft.AspNetCore.Mvc;

namespace TimeService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TimeController : ControllerBase
    {
        [HttpGet("timestamp")]
        public ActionResult<long> GetCurrentTimestamp()
        {
            return Ok(DateTimeOffset.UtcNow.ToUnixTimeSeconds());
        }

        [HttpGet("sql-format")]
        public ActionResult<string> GetCurrentSqlFormat()
        {
            return Ok(DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        [HttpGet("parse-date")]
        public ActionResult<long> ParseSqlDate([FromQuery] string date)
        {
            if (DateTime.TryParseExact(date, "yyyy-MM-dd HH:mm:ss", 
                System.Globalization.CultureInfo.InvariantCulture,
                System.Globalization.DateTimeStyles.None, 
                out DateTime parsedDate))
            {
                return Ok(new DateTimeOffset(parsedDate).ToUnixTimeSeconds());
            }
            return BadRequest("Invalid date format. Use YYYY-MM-DD HH:MM:SS");
        }
    }
}
