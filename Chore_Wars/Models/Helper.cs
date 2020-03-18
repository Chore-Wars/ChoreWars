using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Chore_Wars.Models
{
    public class Helper
    {
        private readonly IHttpContextAccessor _contextAccessor;
        public Helper(IHttpContextAccessor contextAccessor)
        {
            _contextAccessor = contextAccessor;
        }
        public Player PopulateFromSession()
        {
            //tries to get the "AllPlayerSession" as a string. If it exists, de JSON-ify that object
            //and re-instantiate(?) it as an object of type List<Player>
            //if the "AllPlayerSession" JSON-ified situation is blank (null), do nothing.
            string playerJson = _contextAccessor.HttpContext.Session.GetString("PlayerSession");
            if (playerJson != null)
            {
                return JsonConvert.DeserializeObject<Player>(playerJson);
            }
            return null;
        }


    }
}
