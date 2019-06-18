using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SampleApp
{
    public class communicate
    {
        //send a POST request
        public string postRequest(string url, string AccessToken, string data)
        {
            //send request to server
            var result = "";
            var request = (HttpWebRequest)WebRequest.Create(url);
            
            request.Method = "POST";
            if (AccessToken.Trim().Length > 0)
            {
                request.Headers.Add("Authorization", AccessToken);
            }
            request.Accept = "application/json";
            Console.WriteLine("Sending POST request...");

            //Posting a json
            request.ContentType = "application/json"; 
            using (var requestStream = request.GetRequestStream())
            {
                using (var writer = new StreamWriter(requestStream))
                {
                    Console.WriteLine("Logging in via POST request...");

                    //write the data to the server
                    writer.Write(data); 
                    Console.WriteLine("Submitted data to server");
                }
            }

            //get response from server
            Console.WriteLine("Reading response from server...");
            using (var responseFeed = request.GetResponse())
            {
                using (var responseStream = responseFeed.GetResponseStream())
                {
                    using (var reader = new StreamReader(responseStream))
                    {
                        result = reader.ReadToEnd();
                        Console.WriteLine("Read response from server");

                        //print the result from the server
                        Console.WriteLine(result); 
                    }
                }
            }

            return result;
        }
    }
}
