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

        private int productCount = 5;

        private int taskCount = 1;

        private int specimenCount = 1;

        private int riskAssessmentCount = 1;

        private int referralCount = 1;

        private int careTeamCount = 1;

        private int procedureCount = 1;

        private int deviceCount = 1;

        private int conditionCount = 1;

        private int nutritionOrderCount = 1;

        private string practionerFileName;

        private string relatedPersonsFileName;

        private string locationsFileName;

        private string accountsFileName;

        private string medicationsFileName;

        private string encountersFileName;

        private int medicationCount = 1;

        private int encountersCount = 1;

        private int allergyIntoleranceCount = 1;

        private int episodesOfCareCount = 1;

        private int appointmentCount = 1;

        private int carePlanCount = 1;

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

        public string LocationsFileName
        {
            get
            {
                return locationsFileName;
            }

            set
            {
                locationsFileName = value;
            }
        }

        public string AccountsFileName
        {
            get
            {
                return accountsFileName;
            }

            set
            {
                accountsFileName = value;
            }
        }

        public int EpisodesOfCareCount
        {
            get
            {
                return episodesOfCareCount;
            }

            set
            {
                episodesOfCareCount = value;
            }
        }

        public int AppointmentCount
        {
            get
            {
                return appointmentCount;
            }

            set
            {
                appointmentCount = value;
            }
        }

        public int CarePlanCount
        {
            get
            {
                return carePlanCount;
            }

            set
            {
                carePlanCount = value;
            }
        }

        public int CareTeamCount
        {
            get
            {
                return careTeamCount;
            }

            set
            {
                careTeamCount = value;
            }
        }

        public int RiskAssessmentCount
        {
            get
            {
                return riskAssessmentCount;
            }

            set
            {
                riskAssessmentCount = value;
            }
        }

        public int SpecimenCount
        {
            get
            {
                return specimenCount;
            }

            set
            {
                specimenCount = value;
            }
        }

        public int TaskCount
        {
            get
            {
                return taskCount;
            }

            set
            {
                taskCount = value;
            }
        }

        public int ProductCount
        {
            get
            {
                return productCount;
            }

            set
            {
                productCount = value;
            }
        }
    }
}
