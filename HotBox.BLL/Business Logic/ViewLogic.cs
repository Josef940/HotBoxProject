using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBox.BLL.Business_Logic
{
    public class ViewLogic
    {
        public string GetValidInteger(string input, int maxLength)
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

            if (stringBuilder.Length > maxLength)
                stringBuilder.Remove(maxLength, stringBuilder.Length - maxLength);
            else if (stringBuilder.Length == 0 || stringBuilder.Equals("0"))
                return "0";

            var output = stringBuilder.ToString();
            return output;
        }
    }
}
