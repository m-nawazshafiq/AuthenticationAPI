using AppDataAccess;
using AppDataAccess.Model;
using AppDataAccess.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AuthenticationAPI.Controllers
{
    public class AuthController : ApiController
    {
        [HttpPost]
        [Route("api/auth/login")]
        public HttpResponseMessage Login(UserCredentials userCredentials)
        {
            HttpResponseMessage response = null;
            if (!string.IsNullOrEmpty(userCredentials.Password) && !string.IsNullOrEmpty(userCredentials.UserName))
            {
                User user = UserRepository.Instance.GetFirstOrDefault(x => { return x.UserName == userCredentials.UserName; });

                if (user != null)
                {
                    if (Md5Hash.ComputeHash(userCredentials.Password, user.Salt) == user.Password)
                    {                        
                        response = Request.CreateResponse(HttpStatusCode.OK , new { Token = JWTController.Instance.GetToken(user) });
                    }
                    else {
                        response = Request.CreateResponse(HttpStatusCode.Forbidden, new { Message = "Invalid credentials." });
                    }
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.NotFound, new { Message = "User not found." });
                }
            }
            else {
                response = Request.CreateResponse(HttpStatusCode.Unauthorized, new { Message = "Invalid info." });
            }

            return response;
        }
    }
}