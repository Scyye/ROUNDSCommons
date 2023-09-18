using System.Collections.Generic;
using System.Text;

namespace ROUNDSCommons.Utils
{
    public static class StringUtils
    {
        public static string[] FuckingSplitAGodDamnStringWhyTheFuckIsStringDotSplitNotDefinedProperly(this string str, char splitChar) 
        {
            StringBuilder builder = new StringBuilder();
            List<string> result = new List<string>(); 

            foreach (char c in str) 
            { 
                if (c != splitChar) 
                {
                    builder.Append(c);
                } 
                else 
                { 
                    result.Add(builder.ToString()); 
                    builder.Clear(); 
                } 
            } 
            result.Add(builder.ToString()); 
            return result.ToArray(); 
        }
    }
}