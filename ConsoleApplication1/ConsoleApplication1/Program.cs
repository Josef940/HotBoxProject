using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            TimeSpan span = TimeSpan.FromMinutes(2985);
            string label = span.ToString(@"dd\.hh\.mm");
            int steps = 3;
            var sb = new StringBuilder();
            int nextIndex = 0;
            int i = 0;
            while (steps > 0)
            {
                switch (steps)
                {
                    case 3:
                        i = label.IndexOf("h");
                        sb.Append(Convert.ToInt32(label.Substring(nextIndex,2)) + " days, ");
                        steps--;
                        nextIndex+=3;
                        break;
                    case 2:
                        i = label.IndexOf(":");
                        sb.Append(Convert.ToInt32(label.Substring(nextIndex, 2)) + " hours, ");
                        steps--;
                        nextIndex += 3;
                        break;
                    case 1:
                        i = label.Length-1;
                        sb.Append(Convert.ToInt32(label.Substring(nextIndex, 2)) + " minutes");
                        steps--;
                        nextIndex += 3;
                        break;
                    default:
                        break;
                }
            }
            sb.ToString();
            Console.WriteLine(sb);

            Console.WriteLine(label);
        }

        public string MinutesToString(int minutes)
        {
            TimeSpan span = TimeSpan.FromMinutes(minutes);
            string label = span.ToString(@"dd\.hh\.mm");
            var sb = new StringBuilder();
            int i = 0;
            int steps = 3;
            while (steps > 0)
            {
                switch (steps)
                {
                    case 3:
                        sb.Append(Convert.ToInt32(label.Substring(i, 2)) + " days, ");
                        steps--;
                        i += 3;
                        break;
                    case 2:
                        sb.Append(Convert.ToInt32(label.Substring(i, 2)) + " hours, ");
                        steps--;
                        i += 3;
                        break;
                    case 1:
                        sb.Append(Convert.ToInt32(label.Substring(i, 2)) + " minutes");
                        steps--;
                        i += 3;
                        break;
                    default:
                        break;
                }
            }

            return sb.ToString();
        }
    }
}
