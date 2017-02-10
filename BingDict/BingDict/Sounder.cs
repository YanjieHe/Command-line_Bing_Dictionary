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

        public void PronunceWord(string command, QueryResult result)
        {
            if (result.pronunciation != null)
            {
                if (command == "-a")
                {
                    if (result.pronunciation.ContainsKey("AmEmp3"))
                    {
                        PlayAudio(result.pronunciation["AmEmp3"]);
                    }
                    else
                    {
                        Console.WriteLine("Missing Pronunciation.");
                    }
                }
                else if (command == "-b")
                {
                    if (result.pronunciation.ContainsKey("BrEmp3"))
                    {
                        PlayAudio(result.pronunciation["BrEmp3"]);
                    }
                    else
                    {
                        Console.WriteLine("Missing Pronunciation.");
                    }
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

    }
}

