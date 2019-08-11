using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DataStruct.Json
{

    public class Json
    {

        public Web Web { get; set; }

        public Json()
        {
            
            
        }

        public static Json DeserializeJson(string jsonFile)
        {


            string data = Utility.Utility.GetFileData(@jsonFile);
            var typeToDeserialize =  JsonConvert.DeserializeObject<Json>(data);

            return typeToDeserialize;

        }

    }


    public class Web
    {

        public string Client_Id { get; set; }
        public string Project_Id { get; set; }
        public string Auth_Uri { get; set; }
        public string Token_Uri { get; set; }
        public string Auth_provider_x509_cert_url { get; set; }
        public string Client_secret { get; set; }
        public IList<string> Redirect_uris { get; set; }
        public IList<string> Javascript_origins { get; set; }


       


    }


}

