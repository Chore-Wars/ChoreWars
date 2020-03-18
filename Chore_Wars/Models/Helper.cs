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
        public Player sessionPlayer = new Player();

        public Helper()
        {
            HttpContext context;
        }
        //public static void PopulateFromSession()
        //{
        //    //tries to get the "AllPlayerSession" as a string. If it exists, de JSON-ify that object
        //    //and re-instantiate(?) it as an object of type List<Player>
        //    //if the "AllPlayerSession" JSON-ified situation is blank (null), do nothing.
        //    string playerJson = context.Session.GetString("PlayerSession");
        //    if (playerJson != null)
        //    {
        //        sessionPlayer = JsonConvert.DeserializeObject<Player>(playerJson);
        //    }
        //}


    }
}
