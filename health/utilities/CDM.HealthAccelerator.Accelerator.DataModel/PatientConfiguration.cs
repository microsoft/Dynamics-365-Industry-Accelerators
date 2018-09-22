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

namespace CDM.HealthAccelerator.DataModel
{
    [Serializable]
    public class PatientConfiguration
    {
        public PatientConfiguration()
        {
            MedicationCount = 1;
            EncountersCount = 1;
            NutritionOrderCount = 1;
            ConditionCount = 1;
            DeviceCount = 1;
            ProcedureCount = 1;
            ReferralCount = 1;
        }

        private int referralCount;

        private int procedureCount;

        private int deviceCount;

        private int conditionCount;

        private int nutritionOrderCount;

        private string practionerFileName;

        private string relatedPersonsFileName;

        private string medicationsFileName;

        private string encountersFileName;

        private int medicationCount;

        private int encountersCount;

        private int allergyIntoleranceCount;

        public string PractionerFileName
        {
            get
            {
                return practionerFileName;
            }

            set
            {
                practionerFileName = value;
            }
        }

        public string MedicationsFileName
        {
            get
            {
                return medicationsFileName;
            }

            set
            {
                medicationsFileName = value;
            }
        }

        public string EncountersFileName
        {
            get
            {
                return encountersFileName;
            }

            set
            {
                encountersFileName = value;
            }
        }

        public int MedicationCount
        {
            get
            {
                return medicationCount;
            }

            set
            {
                medicationCount = value;
            }
        }

        public int EncountersCount
        {
            get
            {
                return encountersCount;
            }

            set
            {
                encountersCount = value;
            }
        }

        public string RelatedPersonsFileName
        {
            get
            {
                return relatedPersonsFileName;
            }

            set
            {
                relatedPersonsFileName = value;
            }
        }

        public int AllergyIntoleranceCount
        {
            get
            {
                return allergyIntoleranceCount;
            }

            set
            {
                allergyIntoleranceCount = value;
            }
        }

        public int NutritionOrderCount
        {
            get
            {
                return nutritionOrderCount;
            }

            set
            {
                nutritionOrderCount = value;
            }
        }

        public int ConditionCount
        {
            get
            {
                return conditionCount;
            }

            set
            {
                conditionCount = value;
            }
        }

        public int DeviceCount
        {
            get
            {
                return deviceCount;
            }

            set
            {
                deviceCount = value;
            }
        }

        public int ProcedureCount
        {
            get
            {
                return procedureCount;
            }

            set
            {
                procedureCount = value;
            }
        }

        public int ReferralCount
        {
            get
            {
                return referralCount;
            }

            set
            {
                referralCount = value;
            }
        }
    }
}
