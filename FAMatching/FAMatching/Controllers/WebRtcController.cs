using Microsoft.AspNetCore.Mvc; 
using System.Collections.Generic;
namespace FAMatching.Controllers
{
    [ApiController]
    [Route("webrtc")]
    public class WebRtcController : ControllerBase
    {
        private static Dictionary<string, string> _sessionStore = new Dictionary<string, string>();

        [HttpPost("offer")]
        public IActionResult ReceiveOffer([FromBody] object offer)
        {
            _sessionStore["offer"] = offer.ToString();
            return Ok();
        }

        [HttpGet("offer")]
        public IActionResult GetOffer()
        {
            return Ok(_sessionStore.ContainsKey("offer") ? _sessionStore["offer"] : null);
        }

        [HttpPost("answer")]
        public IActionResult ReceiveAnswer([FromBody] object answer)
        {
            _sessionStore["answer"] = answer.ToString();
            return Ok();
        }

        [HttpGet("answer")]
        public IActionResult GetAnswer()
        {
            return Ok(_sessionStore.ContainsKey("answer") ? _sessionStore["answer"] : null);
        }
    }
}
