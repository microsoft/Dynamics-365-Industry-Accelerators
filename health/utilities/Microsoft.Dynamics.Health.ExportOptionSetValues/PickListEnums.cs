using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dynamics.Health.ExportOptionSetValues
{
    [Serializable]
    public class PickListEnums
    {
        private string entityDisplayName;
        private string entityLogicalName;
        private string attributeLogicalName;
        private string enumName;

        public string EntityDisplayName
        {
            get
            {
                return entityDisplayName;
            }

            set
            {
                entityDisplayName = value;
            }
        }

        public string EntityLogicalName
        {
            get
            {
                return entityLogicalName;
            }

            set
            {
                entityLogicalName = value;
            }
        }

        public string AttributeLogicalName
        {
            get
            {
                return attributeLogicalName;
            }

            set
            {
                attributeLogicalName = value;
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

        private Dictionary<string, PickListValues> enumValues = new Dictionary<string, PickListValues>();

        public Dictionary<string, PickListValues> EnumValues
        {
            get
            {
                return enumValues;
            }

            set
            {
                enumValues = value;
            }
        }

        public string GenerateEnum()
        {

            string newenum = "public enum " + EnumName.Replace("(", "").Replace(")", "");
            newenum += "\r\n{\r\n";

            foreach (KeyValuePair<string, PickListValues> plv in EnumValues)
            {
                if (plv.Value.PickListDisplay == "<=")
                {
                    newenum += "\t" + "LessThanOrEqual";
                }
                else if (plv.Value.PickListDisplay == ">=")
                {
                    newenum += "\t" + "GreaterThanOrEqual";
                }
                else if (plv.Value.PickListDisplay == "<")
                {
                    newenum += "\t" + "LessThan";
                }
                else if (plv.Value.PickListDisplay == ">")
                {
                    newenum += "\t" + "GreaterThan";
                }
                else if (plv.Value.PickListDisplay == "virtual")
                {
                    newenum += "\t" + "virtualencounter";
                }
                else
                {
                    newenum += "\t" + (plv.Value.PickListDisplay
                        .Replace(" ", ""))
                        .Replace("-", "")
                        .Replace("(", "")
                        .Replace(")", "")
                        .Replace("'", "")
                        .Replace("/", "")
                        .Replace("+", "");
                }

                newenum += " = " + plv.Value.PickListValue + ",\r\n";
            }

            newenum += "}\r\n\r\n";

            return newenum;
        }
    }
}
