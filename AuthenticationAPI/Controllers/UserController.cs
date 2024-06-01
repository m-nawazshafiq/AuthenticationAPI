using AppDataAccess.Model;
using AppDataAccess.Utility;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Web.Http;

namespace AuthenticationAPI.Controllers
{
    public class UserController : ApiController
    {
        [HttpGet]
        [Route("api/User/Info")]
        public HttpResponseMessage UserInfo()
        {
            HttpRequestMessage request = Request;
            HttpResponseMessage response = null;

            if (request.Headers.Contains("Authorization") && !string.IsNullOrEmpty(request.Headers.GetValues("Authorization").FirstOrDefault()))
            {
                string TokenInfo = request.Headers.GetValues("Authorization").FirstOrDefault();
                string Token = TokenInfo.Split(' ')[1];
                if (JWTController.Instance.VerifyToken(Token))
                {
                    // Replace URL-safe characters
                    string encodedPayload = Token.Split('.')[1].Replace('-', '+').Replace('_', '/');
                    // Add padding if needed
                    int padding = encodedPayload.Length % 4;
                    if (padding > 0)
                    {
                        encodedPayload = encodedPayload.PadRight(encodedPayload.Length + (4 - padding), '=');
                    }
                    // Decode the Base64 string to a byte array
                    byte[] payloadBytes = Convert.FromBase64String(encodedPayload);
                    string payload = Encoding.ASCII.GetString(payloadBytes);
                    JwtPayload jwtPayload = JsonConvert.DeserializeObject<JwtPayload>(payload);
                    response = Request.CreateResponse(HttpStatusCode.Forbidden, new { Regions = jwtPayload.scope.Replace(" ", ","), Roles = jwtPayload.role.Replace(" ", ",") });
                }
                else 
                {
                    response = Request.CreateResponse(HttpStatusCode.Forbidden, new { Message = "Token not valid." });
                }
            }
            else
            {
                response = Request.CreateResponse(HttpStatusCode.Unauthorized, new { Message = "Token not provided." });
            }

            return response;
        }

    }
}