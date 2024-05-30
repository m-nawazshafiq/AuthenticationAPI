using AppDataAccess.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AppDataAccess.Utility
{
    public class JWTController
    {
        static readonly char[] padding = { '=' };
        public static JWTController Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new JWTController();
                }
                return instance;
            }
        }

        public static JWTController instance;

        public string GetToken(User user)
        {
            try
            {
                string Header = JsonConvert.SerializeObject(new
                {
                    alg = "HS256",
                    typ = "JWT"
                });
                var plainTextBytes = System.Text.Encoding.ASCII.GetBytes(Header.Trim());
                string EncodedHeader = Convert.ToBase64String(plainTextBytes).TrimEnd(padding).Replace('+', '-').Replace('/', '_');

                long issueDate = Convert.ToInt64(DateTime.Now.ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Seconds);
                long ExpiryDate = Convert.ToInt64(DateTime.Now.AddDays(30).ToUniversalTime().Subtract(new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc)).Seconds);

                string Payload = JsonConvert.SerializeObject(new
                {
                    iat = issueDate,
                    exp = ExpiryDate,
                    role = string.Join(" ", user.Roles.Select(u => u.Name)),
                    scope = string.Join(" ", user.Regions.Select(u => u.Name))
                });
                var PayloadplainTextBytes = System.Text.Encoding.ASCII.GetBytes(Payload.Trim());
                string EncodedPayload = Convert.ToBase64String(PayloadplainTextBytes).TrimEnd(padding).Replace('+', '-').Replace('/', '_');
                string Signature = EncodedHeader + "." + EncodedPayload;
                var base64Bytes =  System.Convert.FromBase64String("MHcCAQEEIPO2MafjHqlH2u5qUQysY/G29F/pG//Ix451RpOYFKmqoAoGCCqGSM49AwEHoUQDQgAE/izpS6XrOgAVw1jerVIUhLlllse+eiNofZVuR9+EqGH2pZUQrWXVrplPq9AnfWZC+BW40ijKOenQA+GtjCJDkQ==");
                var hash = new HMACSHA256(base64Bytes);
                string SignedSignature = Convert.ToBase64String(hash.ComputeHash(Encoding.ASCII.GetBytes(Signature))).TrimEnd(padding).Replace('+', '-').Replace('/', '_');

                string Token = EncodedHeader + "." + EncodedPayload + "." + SignedSignature;
                return Token;
            }
            catch (Exception ex)
            {
                return "-1";
            }
        }
        public bool VerifyToken(string token)
        {
            string base64Key = "MHcCAQEEIPO2MafjHqlH2u5qUQysY/G29F/pG//Ix451RpOYFKmqoAoGCCqGSM49AwEHoUQDQgAE/izpS6XrOgAVw1jerVIUhLlllse+eiNofZVuR9+EqGH2pZUQrWXVrplPq9AnfWZC+BW40ijKOenQA+GtjCJDkQ==";
            string Signature = token.Split('.')[2];
            string data = token.Split('.')[0] + "." + token.Split('.')[1];
            var base64Bytes = Convert.FromBase64String(base64Key);
            var hash = new HMACSHA256(base64Bytes);
            if (Convert.ToBase64String(hash.ComputeHash(Encoding.ASCII.GetBytes(data))).TrimEnd(padding).Replace('+', '-').Replace('/', '_') == Signature)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
