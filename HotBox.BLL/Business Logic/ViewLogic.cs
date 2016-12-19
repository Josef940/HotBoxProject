using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotBox.BLL.Business_Logic
{
    public class ViewLogic
    {
        public string ValidTextBoxInteger(string input)
        {
            int MAX_TEXT_LENGTH = 6;
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(input);

            var chars = input.ToCharArray();

            for(int i = 0; i<chars.Length; i++)
            {
                if (!Char.IsNumber(chars[i]))
                    stringBuilder.Remove(i,1);
            }

            if (stringBuilder.Length > MAX_TEXT_LENGTH)
                stringBuilder.Length = MAX_TEXT_LENGTH;
            var output = stringBuilder.ToString();
            return output; ;
        }
    }
}
