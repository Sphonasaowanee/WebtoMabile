using Microsoft.AspNetCore.Mvc;

namespace FAMatching.Controllers
{
    [Route(".well-known")]
    [ApiController]
    public class UniversalLinksController : ControllerBase
    {
        [HttpGet("apple-app-site-association")]
        public async Task<IActionResult> GetAppleAppSiteAssociation()
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", ".well-known", "apple-app-site-association");
                var json = await System.IO.File.ReadAllTextAsync(filePath);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }

        [HttpGet("assetlinks.json")]
        public async Task<IActionResult> GetAssetLinks()
        {
            try
            {
                var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", ".well-known", "assetlinks.json");
                var json = await System.IO.File.ReadAllTextAsync(filePath);
                return Content(json, "application/json");
            }
            catch (Exception ex)
            {
                // Log error
                return StatusCode(500, $"Internal Server Error: {ex.Message}");
            }
        }
    }

}
