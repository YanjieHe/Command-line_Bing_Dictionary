using System;
using Mono.Terminal;
using System.Text.RegularExpressions;

namespace BingDict
{
    class MainClass
    {
        public static void Main(string[] args)
        {
			PrintIntroduction();

            LineEditor editor = new LineEditor("Bing Dictionary");
            var dic = new BingDictionary();
            var sounder = new Sounder();
            QueryResult LastQuery = null;

            while (true)
            {
                string line = editor.Edit("> ", string.Empty); 
                if (line == null)
                {
                    break;
                }
                else
                {
                    line = line.Trim();
                    if (line.Length == 0)
                    {
                        continue;
                    }
                    else if (line == "-q")
                    {
                        break;
                    }
                    else if (line == "-a" || line == "-b")
                    {
                        if (LastQuery != null)
                        {
                            sounder.PronounceWord(line, LastQuery);
                        }
                        else
                        {
                            Console.WriteLine("No query history.");
                        }
                    }
                    else
                    {
                        LastQuery = dic.SearchWord(line);
                    }
                }
            }
        }

		static void PrintIntroduction()
		{
            Console.WriteLine("=== Command-line Bing dictionary ===");
            Console.WriteLine("Author: Jason He");
            Console.WriteLine();
            Console.WriteLine("Type Ctrl-D or input -q to quit.");
		}

    }
}
