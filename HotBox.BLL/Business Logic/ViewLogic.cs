using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBox.BLL.Business_Logic
{
    public class ViewLogic
    {
        public string GetValidIntegerAsString(string input, int maxLength)
        {
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(input);
            bool startZeroesEnded = false;
            int indexToRemove = 0;
            for (int i = 0; i<input.Length; i++)
            {
                if (!startZeroesEnded && stringBuilder[indexToRemove].ToString().Equals("0"))
                {
                    stringBuilder.Remove(indexToRemove, 1);
                    indexToRemove--;
                }
                else if (!Char.IsNumber(stringBuilder[indexToRemove]))
                {
                    stringBuilder.Remove(indexToRemove, 1);
                    indexToRemove--;
                }
                else
                    startZeroesEnded = true;
                indexToRemove++;
            }
            
            var output = StringBuilderMaxLengthToString(stringBuilder,maxLength);
            return output;
        }

        public string StringBuilderMaxLengthToString(StringBuilder stringBuilder, int maxLength)
        {
            if (stringBuilder.Length > maxLength)
                stringBuilder.Remove(maxLength, stringBuilder.Length - maxLength);
            else if (stringBuilder.Length == 0 || stringBuilder.Equals("0"))
                return "0";
            return stringBuilder.ToString();
        }

        public string MinutesToTimeText(int minutes)
        {
            TimeSpan span = TimeSpan.FromMinutes(minutes);
            string label = span.ToString(@"dd\.hh\.mm");
            var sb = new StringBuilder();
            int i = 0;
            int steps = 3;
            int number = 0;
            while (steps > 0)
            {
                switch (steps)
                {
                    case 3:
                        number = Convert.ToInt32(label.Substring(i, 2));
                        sb.Append(SingleTimeFormat("day", number, false));
                        steps--;
                        i += 3;
                        break;
                    case 2:
                        number = Convert.ToInt32(label.Substring(i, 2));
                        sb.Append(SingleTimeFormat("hour", number, false));
                        steps--;
                        i += 3;
                        break;
                    case 1:
                        number = Convert.ToInt32(label.Substring(i, 2));
                        sb.Append(SingleTimeFormat("minute", number, true));
                        steps--;
                        i += 3;
                        break;
                    default:
                        break;
                }
            }

            return sb.ToString();
        }

        private string SingleTimeFormat(string timeUnit, int num, bool lastUnit)
        {
            var pluralS = num == 1 ? string.Empty : "s";
            var comma = lastUnit ? string.Empty : ", ";
            var s = string.Format("{0} {1}{2}{3}",num,timeUnit,pluralS,comma);

            return s;
        }
    }
}
