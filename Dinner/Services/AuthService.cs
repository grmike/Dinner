using Newtonsoft.Json;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Hosting;

namespace Dinner.Services
{
    public class AuthService
    {
        ConcurrentDictionary<int, string> _tokens = new ConcurrentDictionary<int, string>();

        public bool IsAuthorized(string token)
        {
            var val = Encoding.ASCII.GetString(Convert.FromBase64String(token));
            if (int.TryParse(val.Substring(32), out int user)) {
                if (_tokens.TryGetValue(user, out string usertoken)) {
                    return token == usertoken;
                };
            }
            return false;
        }

        public int? GetUser(string token)
        {
            var val = Encoding.ASCII.GetString(Convert.FromBase64String(token));
            if (int.TryParse(val.Substring(36), out int user))
            {
                if (_tokens.TryGetValue(user, out string usertoken))
                {
                    return token == usertoken ? user : (int?)null;
                };
            }
            return null;
        }

        public string Authorize(int user)
        {
            var guid = Guid.NewGuid().ToString();
            string token = Convert.ToBase64String(Encoding.ASCII.GetBytes(guid + user.ToString()));
            _tokens.AddOrUpdate(user, token, (key, oldtoken) => token);
            return token;
        }
    }
}