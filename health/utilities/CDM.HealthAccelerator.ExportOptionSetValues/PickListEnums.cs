// =====================================================================
//  This file is part of the Microsoft Dynamics Accelerator code samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
// =====================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDM.HealthAccelerator.GenerateEnums
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
