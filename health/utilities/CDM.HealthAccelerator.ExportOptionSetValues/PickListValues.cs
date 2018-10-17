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
