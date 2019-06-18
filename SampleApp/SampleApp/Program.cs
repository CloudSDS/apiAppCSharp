using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SampleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            String strURL = "https://api.cloudsds.com/msdsdevapi/api/";
            String response = "";
            String inputdata = "";

            //Credentials received from Cloudsds
            String UserId = ""; 
            String Password = ""; 

            communicate com = new communicate();
            /* Steps */

            /* 1: Login to get an Authorization Token */
            inputdata = prepareLoginData(UserId, Password);
            response = com.postRequest(strURL + "user/login", "", inputdata);
            Console.WriteLine("Login output : " + response);
            
            /* 2: Get Authorization Token */
            
            var resp = JsonConvert.DeserializeObject(response);
            JObject j = (JObject) resp;
            String accesstoken = j.GetValue("token").ToString();

            /* 3: Call an API to get SDS information */
            inputdata = prepareSearchData("", "Country Delight Gel", "","","","",2,30);
            
            response = com.postRequest(strURL + "v1/msds/searchsds", accesstoken, inputdata);

            Console.WriteLine("Search output : " + response);

            Console.ReadKey();

        }


        static String prepareLoginData(String struser, String strpass)
        {
            string JSON = @"{""loginId"": ""#struser"",""loginPassword"":""#strpass""}";
            JSON = JSON.Replace("#struser", struser);
            JSON = JSON.Replace("#strpass", strpass);

            return JSON;
        }

        static String prepareSearchData(String msds_id, String product_name,
            String product_code,String mfg_name,String issue_date,
            String cas_no, int offset_val, int limit_val)
        {
            var str = @"{ ""msds_id"": ""#msds_id"", ""product_name"": ""#product_name"", ""product_code"": ""#product_code""
                            , ""mfg_name"" : ""#mfg_name"", ""cas_no"" : ""#cas_no"", ""issue_date"" : ""#issue_date""
                            , ""offset_val"": #offset_val, ""limit_val"": #limit_val}";

            str = str.Replace("#msds_id", msds_id);
            str = str.Replace("#product_name", product_name);
            str = str.Replace("#product_code", product_code);
            str = str.Replace("#mfg_name", mfg_name);
            str = str.Replace("#cas_no", cas_no);
            str = str.Replace("#issue_date", issue_date);
            str = str.Replace("#offset_val", offset_val.ToString());
            str = str.Replace("#limit_val", limit_val.ToString());
            return str;
        }
    }
}
