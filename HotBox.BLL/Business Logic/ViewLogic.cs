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
            if (input.Length == 0 || input.Equals("0"))
                return "0";
            var stringBuilder = new StringBuilder();
            stringBuilder.Append(input);
            var indexesToRemove = new List<int>();
            bool startZeroesEnded = false;
            for (int i = 0; i<input.Length-1; i++)
            {

            }
            return null;
        }
        public string ValidTextBoxInteger(string input)
        {
            if (input.Length == 0 || input.Equals("0"))
                return "0";

            int MAX_TEXT_LENGTH = 6;

            var stringBuilder = new StringBuilder();
            stringBuilder.Append(input);

            //var chars = input.ToCharArray();

            bool startZeroesEnded = false;

            int index = 0;
            for(int i = 0; i<input.Length; i++)
            {
                if (!startZeroesEnded && stringBuilder[index].Equals("0"))
                {
                    stringBuilder.Remove(index, 1);
                    index--;
                }
                else if (!Char.IsNumber(stringBuilder[index]))
                {
                    stringBuilder.Remove(index, 1);
                    //startZeroesEnded = true;
                    index--;
                }
                index++;
            }
            //for(int i = 0; i<input.Length; i++)
            //{
            //    if (!startZeroesEnded && chars[index].Equals("0"))
            //    {
            //        stringBuilder.Remove(index, 1);
            //        index--;
            //    }
            //    else
            //    {
            //        if (!Char.IsNumber(chars[index]))
            //        {
            //            stringBuilder.Remove(index, 1);
            //            index--;
            //        }else
            //            startZeroesEnded = true;
            //    }
            //    index++;
            //}
       

            if (stringBuilder.Length > MAX_TEXT_LENGTH)
                stringBuilder.Remove(MAX_TEXT_LENGTH - 1, stringBuilder.Length - 1);

            var output = stringBuilder.ToString();
            return output;
        }

        public void removeIndexesFromString(List<int> indexesToRemove, ref StringBuilder stringBuilder)
        {
            stringBuilder.Replace("Q",string.Empty);
        }
    }
}
