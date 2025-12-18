using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Net;

namespace CoreFileManager.Controllers
{
    [ApiController]
    [Route("logs")]
    public class LogsController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfiguration _configuration;

        public LogsController(IWebHostEnvironment env, IConfiguration configuration)
        {
            _env = env;
            _configuration = configuration;
        }

        [HttpGet("json")]
        public IActionResult GetLatestJsonLog([FromHeader(Name = "X-LOG-KEY")] string apiKey)
        {
            if (!_env.IsDevelopment())
                return NotFound(new { error = "Bu endpoint yalnızca development ortamında kullanılabilir." });

            var expectedKey = _configuration["LogAccess:ApiKey"];
            if (string.IsNullOrWhiteSpace(apiKey) || apiKey != expectedKey)
                return Unauthorized(new { error = "Geçersiz API key." });

            var allowedIps = _configuration.GetSection("LogAccess:AllowedIPs").Get<string[]>() ?? Array.Empty<string>();

            string ipString = HttpContext.Connection.RemoteIpAddress?.ToString() ?? string.Empty;
            if (HttpContext.Request.Headers.ContainsKey("X-Forwarded-For"))
            {
                var headerIp = HttpContext.Request.Headers["X-Forwarded-For"].FirstOrDefault();
                if (!string.IsNullOrEmpty(headerIp))
                    ipString = headerIp.Split(',').First().Trim();
            }

            if (IPAddress.TryParse(ipString, out var parsedIp) && parsedIp.IsIPv4MappedToIPv6)
                ipString = parsedIp.MapToIPv4().ToString();

            if (_env.IsDevelopment() && (ipString == "127.0.0.1" || ipString == "::1"))
                ipString = "127.0.0.1";

            if (!allowedIps.Contains(ipString))
                return StatusCode(403, new { error = $"IP {ipString} bu endpoint'e erişemez." });

            var logsFolder = Path.Combine(Directory.GetCurrentDirectory(), "Logs");
            var latestLogFile = Directory
                .GetFiles(logsFolder, "log-*.json")
                .OrderByDescending(f => f)
                .FirstOrDefault();

            if (latestLogFile == null)
                return NotFound(new { error = "JSON log dosyası bulunamadı." });

            using var stream = new FileStream(latestLogFile, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
            using var reader = new StreamReader(stream, Encoding.UTF8);
            var content = reader.ReadToEnd();

            return Content(content, "application/json");
        }
    }
}
