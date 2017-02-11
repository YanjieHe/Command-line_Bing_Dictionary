using System;
using System.Web;
using System.IO;
using System.Net;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace BingDict
{
    public class BingDictionary
    {
        int samplesLimit;

        public BingDictionary(int samplesLimit = 5)
        {
            this.samplesLimit = samplesLimit;
        }


        public QueryResult SearchWord(string word)
        {
            string value = RequestWord(word);
            QueryResult result = ParseJson(value);
            PrintResult(result);
            return result;
        }

        public void PrintResult(QueryResult result)
        {
			PrintWord(result);
			PrintPronunciation(result);
			PrintDefinitions(result);
			PrintSamples(result);
        }

		void PrintWord(QueryResult result)
		{
            Console.WriteLine(result.word);
		}

		void PrintPronunciation(QueryResult result)
		{
            Console.WriteLine();
            Console.WriteLine("Pronunciation:");
            if (result.pronunciation != null)
            {
                foreach (var item in result.pronunciation)
                {
					if (item.Key == "AmE") {
						Console.WriteLine("US [{0}]", item.Value);
					} else if (item.Key == "BrE") {
						Console.WriteLine("UK [{0}]", item.Value);
					}
                }
            }
		}
		void PrintDefinitions(QueryResult result)
		{
            Console.WriteLine();
            Console.WriteLine("Definitions: ");
            if (result.defs != null)
            {
                for (int i = 0; i < Math.Min(samplesLimit, result.defs.Length); i++)
                {
                    foreach (var definition in result.defs[i])
                    {
                        Console.WriteLine(definition.Value);
                    }
                }
            }
		}

		void PrintSamples(QueryResult result)
		{
            Console.WriteLine();
            Console.WriteLine("Samples: ");
            if (result.sams != null)
            {
                for (int i = 0; i < Math.Min(samplesLimit, result.sams.Length); i++)
                {
                    Console.Write("{0}. ", i + 1);
                    foreach (var sample in result.sams[i])
                    {
                        if (sample.Key != "mp3Url" && sample.Key != "mp4Url")
                        {
                            Console.WriteLine(sample.Value);
                        }
                    }
                }
            }
            Console.WriteLine();
		}

        string RequestWord(string word)
        {
            const string prefix = "http://xtk.azurewebsites.net/BingDictService.aspx?Word=";
            string url = prefix + HttpUtility.UrlEncode(word);
            WebRequest request = WebRequest.Create(url);
            WebResponse response = request.GetResponse();
            using (var reader = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                return reader.ReadToEnd();
            }
        }

        QueryResult ParseJson(string value)
        {
            QueryResult result = JsonConvert.DeserializeObject<QueryResult>(value);
            return result;
        }
    }

    public class QueryResult
    {
        public string word;
        public Dictionary<string,string> pronunciation;
        public Dictionary<string,string>[] defs;
        public Dictionary<string,string>[] sams;
    }
}

