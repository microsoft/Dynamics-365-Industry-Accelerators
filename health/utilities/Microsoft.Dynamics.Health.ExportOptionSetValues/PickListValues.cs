using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dynamics.Health.ExportOptionSetValues
{
    [Serializable]
    public class PickListValues
    {
        private string pickListDisplay;
        private string pickListValue;

        public string PickListDisplay
        {
            get
            {
                return pickListDisplay;
            }

            set
            {
                pickListDisplay = value;
            }
        }

        public string PickListValue
        {
            get
            {
                return pickListValue;
            }

            set
            {
                pickListValue = value;
            }
        }


    }
}
