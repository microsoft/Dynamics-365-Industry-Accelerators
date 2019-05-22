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
    public class Encounter
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
        /// Create an encounter. 
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

                    Entity encounter = new Entity("msemr_encounter");
                    
                    encounter["msemr_name"] = "Routine";

                    Guid subjectpatientContactId = SDKFunctions.GetContactId(_serviceProxy, "Daniel Atlas");
                    if (subjectpatientContactId != Guid.Empty)
                    {
                        encounter["msemr_subjectpatient"] = new EntityReference("contact", subjectpatientContactId);
                    }

                    encounter["msemr_class"] = new OptionSetValue(935000008); //short stay

                    encounter["msemr_encounterstartdate"] = DateTime.Now;
                    encounter["msemr_encounterenddate"] = DateTime.Now;
                    encounter["msemr_encounterclass"] = new OptionSetValue(935000001); //outPatient
                    encounter["msemr_encounterlength"] = (decimal)30.5;
                    encounter["msemr_encounterstatus"] = new OptionSetValue(935000000); //Planned
                    encounter["msemr_encounteridentifier"] = "Routine 25352";
                    Guid priorityCodeableConceptId = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Urgent", 935000102);
                    if (priorityCodeableConceptId != Guid.Empty)
                    {
                        encounter["msemr_encounterpriority"] = new EntityReference("msemr_codeableconcept", priorityCodeableConceptId);
                    }
                    Guid groupCodeableConceptId = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Group Identifier", 935000063);
                    if (groupCodeableConceptId != Guid.Empty)
                    {
                        encounter["msemr_encountergroupidentifier"] = new EntityReference("msemr_codeableconcept", groupCodeableConceptId);
                    }
                    Guid parentEncountertId = GetEncounterId(_serviceProxy, "E23556");
                    if (parentEncountertId != Guid.Empty)
                    {
                        encounter["msemr_encounterparentencounteridentifier"] = new EntityReference("msemr_encounter", parentEncountertId);
                    }
                    Guid patientIdentifier = SDKFunctions.GetContactId(_serviceProxy, "James Kirk");
                    if (patientIdentifier != Guid.Empty)
                    {
                        encounter["msemr_encounterpatientidentifier"] = new EntityReference("contact", patientIdentifier);
                    }

                    encounter["msemr_periodstart"] = DateTime.Now;
                    encounter["msemr_periodend"] = DateTime.Now;
                    encounter["msemr_duration"] = 30;
                    encounter["msemr_priority"] = new OptionSetValue(935000000); //ASAP

                    encounter["msemr_hospitalizationpreadmissionnumber"] = "25352";
                    Guid destinationLocationId = SDKFunctions.GetLocationId(_serviceProxy, "Noble Hospital Center");
                    if (destinationLocationId != Guid.Empty)
                    {
                        encounter["msemr_hospitalizationdestination"] = new EntityReference("msemr_location", destinationLocationId);
                    }
                    Guid dischargedispositionCodeableConceptId = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Discharge Disposition", 935000042);
                    if (dischargedispositionCodeableConceptId != Guid.Empty)
                    {
                        encounter["msemr_hospitalizationdischargedisposition"] = new EntityReference("msemr_codeableconcept", dischargedispositionCodeableConceptId);
                    }
                    Guid admitsourceCodeableConceptId = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Admit Source", 935000007);
                    if (admitsourceCodeableConceptId != Guid.Empty)
                    {
                        encounter["msemr_hospitalizationadmitsource"] = new EntityReference("msemr_codeableconcept", admitsourceCodeableConceptId);
                    }
                    Guid readmissionCodeableConceptId = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Re-admission", 935000114);
                    if (readmissionCodeableConceptId != Guid.Empty)
                    {
                        encounter["msemr_hospitalizationreadmission"] = new EntityReference("msemr_codeableconcept", readmissionCodeableConceptId);
                    }

                    Guid originLocationId = SDKFunctions.GetLocationId(_serviceProxy, "Alpine");
                    if (originLocationId != Guid.Empty)
                    {
                        encounter["msemr_hospitalizationorigin"] = new EntityReference("msemr_location", originLocationId);
                    }

                    Guid accountId = Organization.GetAccountId(_serviceProxy, "Galaxy Corp");
                    if (accountId != Guid.Empty)
                    {
                        encounter["msemr_onbehalfof"] = new EntityReference("account", accountId);
                    }

                    Guid encounterId = _serviceProxy.Create(encounter);

                    // Verify that the record has been created.
                    if (encounterId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", encounterId);
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
        
        public static Guid GetEncounterId(IOrganizationService _service, string number)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("msemr_encounter");

                query.ColumnSet = new ColumnSet("msemr_encounterid");

                query.Criteria.AddCondition("msemr_encounteridentifier", ConditionOperator.Equal, number);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("msemr_encounterid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["msemr_encounterid"]);
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

        #region Main Method

        /// <summary>
        /// Standard Main() method used by most SDK samples.
        /// </summary>
        /// <param name="args"></param>
        static public void Main(string[] args)
        {
            try
            {
                // Obtain the target organization's Web address and client logon 
                // credentials from the user.
                ServerConnection serverConnect = new ServerConnection();
                ServerConnection.Configuration config = serverConnect.GetServerConfiguration();

                Encounter app = new Encounter();
                app.Run(config, true);
            }
            catch (FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> ex)
            {
                Console.WriteLine("The application terminated with an error.");
                Console.WriteLine("Timestamp: {0}", ex.Detail.Timestamp);
                Console.WriteLine("Code: {0}", ex.Detail.ErrorCode);
                Console.WriteLine("Message: {0}", ex.Detail.Message);
                Console.WriteLine("Plugin Trace: {0}", ex.Detail.TraceText);
                Console.WriteLine("Inner Fault: {0}",
                    null == ex.Detail.InnerFault ? "No Inner Fault" : "Has Inner Fault");
            }
            catch (System.TimeoutException ex)
            {
                Console.WriteLine("The application terminated with an error.");
                Console.WriteLine("Message: {0}", ex.Message);
                Console.WriteLine("Stack Trace: {0}", ex.StackTrace);
                Console.WriteLine("Inner Fault: {0}",
                    null == ex.InnerException.Message ? "No Inner Fault" : ex.InnerException.Message);
            }
            catch (System.Exception ex)
            {
                Console.WriteLine("The application terminated with an error.");
                Console.WriteLine(ex.Message);

                // Display the details of the inner exception.
                if (ex.InnerException != null)
                {
                    Console.WriteLine(ex.InnerException.Message);

                    FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault> fe = ex.InnerException
                        as FaultException<Microsoft.Xrm.Sdk.OrganizationServiceFault>;
                    if (fe != null)
                    {
                        Console.WriteLine("Timestamp: {0}", fe.Detail.Timestamp);
                        Console.WriteLine("Code: {0}", fe.Detail.ErrorCode);
                        Console.WriteLine("Message: {0}", fe.Detail.Message);
                        Console.WriteLine("Plugin Trace: {0}", fe.Detail.TraceText);
                        Console.WriteLine("Inner Fault: {0}",
                            null == fe.Detail.InnerFault ? "No Inner Fault" : "Has Inner Fault");
                    }
                }
            }
            // Additional exceptions to catch: SecurityTokenValidationException, ExpiredSecurityTokenException,
            // SecurityAccessDeniedException, MessageSecurityException, and SecurityNegotiationException.

            finally
            {
                Console.WriteLine("Press <Enter> to exit.");
                Console.ReadLine();
            }
        }
        #endregion Main method
    }
}
