using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace HotelsBookingSystem.GatewayAPI.Controllers
{
    [Route("/api/v1")]
    public class AccountController : ControllerBase
    {
        [Route("authorization")]
        public async Task Login(string returnUrl = "http://gateway.rsoi-ilyasov.ru/api/v1/callback")
        {
            await HttpContext.ChallengeAsync("Auth0", new AuthenticationProperties() { RedirectUri = returnUrl });
        }

        [Authorize]
        [Route("logout")]
        [HttpPost]
        public async Task Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        }
        
        [Route("callback")]
        public async Task<IActionResult> Test()
        {
            string accessToken = await HttpContext.GetTokenAsync("access_token");
            string idToken = await HttpContext.GetTokenAsync("id_token");

            return Ok(new JsonResult(new { test = "test", accessToken = accessToken, idToken = idToken }).Value);
        }
    }
}