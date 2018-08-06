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

using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Dynamics.Health.Samples
{
    public class EpisodeOfCare
    {
        #region Class Level Members

        /// <summary>
        /// Stores the organization service proxy.
        /// </summary>
        private OrganizationServiceProxy _serviceProxy;

        #endregion Class Level Members

        #region How To Sample Code
        /// <summary>
        /// Create and configure the organization service proxy.
        /// Initiate the method to create any data that this sample requires.
        /// Create an episode of care.
        /// </summary>
        public void Run(ServerConnection.Configuration serverConfig, bool promptforDelete)
        {
            try
            {
                //<snippetMarketingAutomation1>
                // Connect to the Organization service. 
                // The using statement assures that the service proxy will be properly disposed.
                using (_serviceProxy = new OrganizationServiceProxy(serverConfig.OrganizationUri, serverConfig.HomeRealmUri, serverConfig.Credentials, serverConfig.DeviceCredentials))
                {
                    // This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes();

                    Entity episodeofcare = new Entity("msemr_episodeofcare");
                    
                    episodeofcare["msemr_description"] = "Rehabilitation";
                    
                    episodeofcare["msemr_startdatetime"] = DateTime.Now;
                    episodeofcare["msemr_enddatetime"] = DateTime.Now;
                    
                    Guid caremanagerContactId = SDKFunctions.GetContactId(_serviceProxy, "James Kirk");
                    if (caremanagerContactId != Guid.Empty)
                    {
                        episodeofcare["msemr_caremanager"] = new EntityReference("contact", caremanagerContactId);
                    }

                    Guid patientContactId = SDKFunctions.GetContactId(_serviceProxy, "Daniel Atlas");
                    if (patientContactId != Guid.Empty)
                    {
                        episodeofcare["msemr_patient"] = new EntityReference("contact", patientContactId);
                    }

                    Guid organizationAccountId = Organization.GetAccountId(_serviceProxy, "Galaxy Corp");
                    if (organizationAccountId != Guid.Empty)
                    {
                        episodeofcare["msemr_organization"] = new EntityReference("account", organizationAccountId);
                    }
                    
                    episodeofcare["msemr_status"] = new OptionSetValue(935000000); //Planned
                    
                    episodeofcare["msemr_identifier"] = "EPC-153";

                    Guid episodeofcareId = _serviceProxy.Create(episodeofcare);

                    // Verify that the record has been created.
                    if (episodeofcareId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", episodeofcareId);
                    }
                }
            }
            // Catch any service fault exceptions that Microsoft Dynamics CRM throws.
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>)
            {
                // You can handle an exception here or pass it back to the calling method.
                throw;
            }
        }

        public static Guid GetEpisodeOfCareId(IOrganizationService _service, string number)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("msemr_episodeofcare");

                query.ColumnSet = new ColumnSet("msemr_episodeofcareid");

                query.Criteria.AddCondition("msemr_identifier", ConditionOperator.Equal, number);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("msemr_episodeofcareid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["msemr_episodeofcareid"]);
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion How To Sample Code

    }
}
