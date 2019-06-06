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

using HealthCDM;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using Microsoft.Xrm.Sdk;

namespace CDM.HealthAccelerator.DataModel
{
    [Serializable]
    public abstract class BaseFunctions
    {
        /// <summary>
        /// how to initialize the interaction in some cases is needed
        /// </summary>
        public abstract void InitializeEntity();

        // Adding a method to be able to self-write to CDS
        public abstract Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword);

        public abstract Guid WriteToCDS(OrganizationServiceProxy _serviceProxy);

        public Guid GetCodeableConceptId(OrganizationServiceProxy _serviceProxy, string text, int type)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("msemr_codeableconcept");

                query.ColumnSet = new ColumnSet("msemr_codeableconceptid");

                query.Criteria.AddCondition("msemr_code", ConditionOperator.Equal, text);
                query.Criteria.AddCondition("msemr_type", ConditionOperator.Equal, type);

                EntityCollection entityCollection = _serviceProxy.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("msemr_codeableconceptid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["msemr_codeableconceptid"]);
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static string GenerateRandomNumber(int places = 8)
        {
            string randomNumber = string.Empty;

            for (int i = 0; i < places; i++)
            {
                randomNumber += BaseRandomGenerator.Next(1, 9).ToString();
            }

            return randomNumber;
        }
        /// <summary>
        /// Use this to help generate fake information for a contact
        /// </summary>
        public static readonly Random BaseRandomGenerator = new Random();

        public static SampleDataCache.GenerateRandomDateTime birthDayRandomGenerator = new SampleDataCache.GenerateRandomDateTime(1955, 1, 1, new DateTime(2000, 1, 1));
    }
}
