using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.Net.Http;
using System.Diagnostics;
using ConsumeAPIExercise;
using System.Runtime.InteropServices.ComTypes;
using System.Threading.Tasks;
using System.IO;
using System.ComponentModel;

namespace ConsumeAPIExercise
{
    class Program
    {
        static async Task Main(string[] args)
        {

            IAPI theOfficeAPI = new TheOfficeAPI();
            //theOfficeAPI.baseURL = "http://www.officeapi.dev/api"
            theOfficeAPI.baseURL = urls.theOffice;
            theOfficeAPI.path = "/quotes/random";

            await theOfficeAPI.Request();
        }
    }

    interface Printable { 
        void print(String value);
    }

    class SomePrinter : Printable
    {
        public void print(string value)
        {
            Console.WriteLine(value);
        }
    }
}

public interface IAPI {
    string baseURL { get; set; }
    string path { get; set; }

    Dictionary<string, string> parameters { get; set; }

    public Task Request();
}

public class TheOfficeAPI : IAPI
{
    public string baseURL { get; set; }
    public string path { get; set; }

    public string url {
        get {
            return baseURL + path;
        }
    }
    public Dictionary<string, string> parameters { get; set; }

    Printable PrintDelegate { 
        get { 
            return new SomePrinter();    
        } 
    }

    public async Task Request()
    {
        using (var client = new HttpClient()) {
            using (var response = await client.GetAsync(url)) {
                var content = response.Content;
                string contentData = await content.ReadAsStringAsync();
                if (contentData != null) {
                    PrintDelegate.print(contentData);
                }
            }
        }
       
    }
}

public enum urls
{
    [field:Description("http://www.officeapi.dev/api")]
    theOffice
}

