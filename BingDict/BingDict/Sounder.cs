using System;
using System.Diagnostics;

namespace BingDict
{
    public class Sounder
    {
        public Sounder()
        {
            
        }

        void PlayAudio(string link)
        {
            Process process = new Process();
            process.StartInfo.FileName = "mplayer";
            process.StartInfo.Arguments = link;
            process.StartInfo.CreateNoWindow = true;
            process.StartInfo.UseShellExecute = false;
            process.StartInfo.RedirectStandardError = true;
            process.StartInfo.RedirectStandardOutput = true;
            process.Start();
            process.WaitForExit();
            process.Close();
        }

        public void PronounceWord(string command, QueryResult result)
        {
            if (result.pronunciation != null)
            {
                if (command == "-a")
                {
					PlayPronunciation("AmEmp3", result);
                }
                else if (command == "-b")
                {
					PlayPronunciation("BrEmp3", result);
                }
                else
                {
                    Console.WriteLine("Unrecognizable argument.");
                }
            }
            else
            {
                Console.WriteLine("Missing Pronunciation.");
            }
        }

		void PlayPronunciation(string type, QueryResult result)
		{
			if (result.pronunciation.ContainsKey(type))
			{
				PlayAudio(result.pronunciation[type]);
			}
			else
			{
				Console.WriteLine("Missing Pronunciation.");
			}
		}

    }
}

