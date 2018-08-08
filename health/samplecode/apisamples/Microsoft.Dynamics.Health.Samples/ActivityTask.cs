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
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Dynamics.Health.Samples
{
    public class ActivityTask
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
        /// Create a task. 
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

                    Entity task = new Entity("task");
                    
                    task["subject"] = "Surgery";

                    Guid businessstatusCodeableConceptId = SDKFunctions.GetCodeableConceptId(_serviceProxy, "General Partnership", 935000149);
                    if (businessstatusCodeableConceptId != Guid.Empty)
                    {
                        task["msemr_businessstatus"] = new EntityReference("msemr_codeableconcept", businessstatusCodeableConceptId);
                    }

                    Guid codeCodeableConceptId = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Code", 935000150);
                    if (codeCodeableConceptId != Guid.Empty)
                    {
                        task["msemr_code"] = new EntityReference("msemr_codeableconcept", codeCodeableConceptId);
                    }

                    //Setting Context Type as Encounter
                    task["msemr_contexttype"] = new OptionSetValue(935000000); //Encounter
                    Guid contextencounterId = Encounter.GetEncounterId(_serviceProxy, "E23556");
                    if (contextencounterId != Guid.Empty)
                    {
                        task["msemr_contextencounter"] = new EntityReference("msemr_encounter", contextencounterId);
                    }

                    task["msemr_definitionuri"] = "";

                    task["msemr_descriptionfocus"] = "";

                    task["msemr_descriptionfor"] = "";

                    task["msemr_groupidentifier"] = "";

                    task["msemr_intent"] = new OptionSetValue(935000000); //Proposal

                    //Setting performer owner type as practitioner
                    task["msemr_performerownertype"] = new OptionSetValue(935000000); //Practitioner
                    Guid performerownerpractitionerContacttId = SDKFunctions.GetContactId(_serviceProxy, "James Kirk");
                    if (performerownerpractitionerContacttId != Guid.Empty)
                    {
                        task["msemr_performerownerpractitioner"] = new EntityReference("contact", performerownerpractitionerContacttId);
                    }

                    Guid reasonCodeableConceptId = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Reason", 935000154);
                    if (reasonCodeableConceptId != Guid.Empty)
                    {
                        task["msemr_reason"] = new EntityReference("msemr_codeableconcept", reasonCodeableConceptId);
                    }

                    Guid referenceId = SDKFunctions.GetActivityDefinitionId(_serviceProxy, "Activity");
                    if (referenceId != Guid.Empty)
                    {
                        task["msemr_reference"] = new EntityReference("msemr_identifiesspecifictimeswhentheeventoccu", referenceId);
                    }

                    //Setting requestor agent as patient
                    task["msemr_requesteragent"] = new OptionSetValue(935000002); //Patient
                    Guid requesteragentpatientContactId = SDKFunctions.GetContactId(_serviceProxy, "James Kirk");
                    if (requesteragentpatientContactId != Guid.Empty)
                    {
                        task["msemr_requesteragentpatient"] = new EntityReference("contact", requesteragentpatientContactId);
                    }

                    Guid requesteronbehalfofAccountId = Organization.GetAccountId(_serviceProxy, "Galaxy Corp");
                    if (requesteronbehalfofAccountId != Guid.Empty)
                    {
                        task["msemr_requesteronbehalfof"] = new EntityReference("msemr_codeableconcept", requesteronbehalfofAccountId);
                    }
                    
                    task["msemr_restrictionperiodstartdate"] = DateTime.Now;
                    task["msemr_restrictionperiodenddate"] = DateTime.Now;
                    task["msemr_restrictionrepetitions"] = 152;

                    task["msemr_status"] = new OptionSetValue(935000000); //Draft

                    Guid statusreasonCodeableConceptId = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Status Reason", 935000155);
                    if (statusreasonCodeableConceptId != Guid.Empty)
                    {
                        task["msemr_statusreason"] = new EntityReference("msemr_codeableconcept", statusreasonCodeableConceptId);
                    }

                    task["msemr_taskpriority"] = new OptionSetValue(935000002); //ASAP
                                    
                    task["msemr_tasknumber"] = "T85746";

                    Guid taskId = _serviceProxy.Create(task);

                    // Verify that the record has been created.
                    if (taskId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", taskId);
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

                ActivityTask app = new ActivityTask();
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
