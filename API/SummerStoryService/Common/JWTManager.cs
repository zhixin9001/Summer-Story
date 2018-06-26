using JWT;
using JWT.Algorithms;
using JWT.Serializers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common
{
    public class JWTManager
    {
        public static string GenerateToken(string openID)
        {
            var secret = ConfigHelper.config.JWTSecret;
            var unixEpoch = JwtValidator.UnixEpoch; // 1970-01-01 00:00:00 UTC
            var secondsSinceEpoch = Math.Round((DateTime.Now.AddHours(4) - unixEpoch).TotalSeconds);
            var payload = new Dictionary<string, object>
            {
                {Consts.OPENID_KEY,openID },
                {"exp",secondsSinceEpoch }
            };
            IJwtAlgorithm algorithm = new HMACSHA256Algorithm();
            IJsonSerializer serializer = new JsonNetSerializer();
            IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
            IJwtEncoder encoder = new JwtEncoder(algorithm, serializer, urlEncoder);
            return encoder.Encode(payload, secret);
        }

        public static string DecodeToken(string token)
        {
            try
            {
                IJsonSerializer serializer = new JsonNetSerializer();
                IDateTimeProvider provider = new UtcDateTimeProvider();
                IJwtValidator validator = new JwtValidator(serializer, provider);
                IBase64UrlEncoder urlEncoder = new JwtBase64UrlEncoder();
                IJwtDecoder decoder = new JwtDecoder(serializer, validator, urlEncoder);
                var payload = decoder.DecodeToObject<IDictionary<string, string>>(token, ConfigHelper.config.JWTSecret, verify: true);
                return payload[Consts.OPENID_KEY];
            }
            catch (TokenExpiredException e)
            {
                LogHelper.WriteLog(e);
                throw e;
            }
            catch (SignatureVerificationException e)
            {
                LogHelper.WriteLog(e);
                throw e;
            }
        }
    }
}
