using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDM.HealthAccelerator.GenerateEnums
{
    [Serializable]
    public class CodeableConceptEnum
    {
        private List<string> values = new List<string>();
    
        private string enumName;

        public List<string> Values
        {
            get
            {
                return values;
            }

            set
            {
                values = value;
            }
        }

        public string EnumName
        {
            get
            {
                return enumName;
            }

            set
            {
                enumName = value;
            }
        }

        public string GenerateEnum()
        {

            string newenum = "public enum " + EnumName.Replace(" ", "");
            newenum += "\r\n{\r\n";

            foreach (string value in Values)
            {
                if (!string.IsNullOrEmpty(value.Trim()))
                {
                    newenum += "CODE_" + value + ",\r\n";
                }
            }

            newenum += "}\r\n\r\n";

            return newenum;
        }
    }
}
