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
    public class ReferralRequest
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
        /// Create a referral request. 
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

                    Practitioner practioner = new Practitioner();

                    Entity referralRequest = new Entity("msemr_referralrequest");

                    //Primary Field
                    referralRequest["msemr_name"] = "Andrea Leonardo";

                    //Setting context type as encounter
                    referralRequest["msemr_contexttype"] = new OptionSetValue(935000000); //Encounter
                    Guid subjectcontextencounterId = Encounter.GetEncounterId(_serviceProxy, "E23556");
                    if (subjectcontextencounterId != Guid.Empty)
                    {
                        referralRequest["msemr_subjectcontextencounter"] = new EntityReference("msemr_encounter", subjectcontextencounterId);
                    }
                    Guid encounterId = Encounter.GetEncounterId(_serviceProxy, "E23556");
                    if (encounterId != Guid.Empty)
                    {
                        referralRequest["msemr_initiatingencounter"] = new EntityReference("msemr_encounter", encounterId);
                    }

                    //Setting requester agent as Practitioner
                    referralRequest["msemr_requesteragent"] = new OptionSetValue(935000000); //Practitioner
                    Guid requesteragentpractitionerContactId = SDKFunctions.GetContactId(_serviceProxy, "Daniel Atlas");
                    if (requesteragentpractitionerContactId != Guid.Empty)
                    {
                        referralRequest["msemr_requesteragentpractitioner"] = new EntityReference("contact", requesteragentpractitionerContactId);
                    }

                    Guid requesteronbehalfofAccountId = Organization.GetAccountId(_serviceProxy, "Galaxy Corp");
                    if (requesteronbehalfofAccountId != Guid.Empty)
                    {
                        referralRequest["msemr_requesteronbehalfof"] = new EntityReference("account", requesteronbehalfofAccountId);
                    }

                    Guid requestorContactId = SDKFunctions.GetContactId(_serviceProxy, "James Kirk");
                    if (requestorContactId != Guid.Empty)
                    {
                        referralRequest["msemr_requestor"] = new EntityReference("contact", requestorContactId);
                    }

                    referralRequest["msemr_occurenceperiodstartdate"] = DateTime.Now;
                    referralRequest["msemr_occurenceperiodenddate"] = DateTime.Now;
                    referralRequest["msemr_occurrencedate"] = DateTime.Now;
                    referralRequest["msemr_occurrencetype"] = new OptionSetValue(935000000); //Date

                    referralRequest["msemr_authoredon"] = DateTime.Now;

                    Guid typeCodeableConceptId = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Referral Request", 935000094);
                    if (typeCodeableConceptId != Guid.Empty)
                    {
                        referralRequest["msemr_type"] = new EntityReference("msemr_codeableconcept", typeCodeableConceptId);
                    }

                    Guid basedonreferralId = SDKFunctions.GetReferralRequestId(_serviceProxy, "Ref452");
                    if (basedonreferralId != Guid.Empty)
                    {
                        referralRequest["msemr_basedonreferral"] = new EntityReference("msemr_referralrequest", basedonreferralId);
                    }                    

                    Guid practitionerspecialtyCodeableConceptId = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Psychiatric", 935000101);
                    if (practitionerspecialtyCodeableConceptId != Guid.Empty)
                    {
                        referralRequest["msemr_practitionerspecialty"] = new EntityReference("msemr_codeableconcept", practitionerspecialtyCodeableConceptId);
                    }

                    referralRequest["msemr_priority"] = new OptionSetValue(935000000); //Routine

                    referralRequest["msemr_intent"] = new OptionSetValue(935000000); //Proposal

                    referralRequest["msemr_subject"] = new OptionSetValue(935000000); //Patient

                    Guid subjectpatientContactId = SDKFunctions.GetContactId(_serviceProxy, "Emily Williams");
                    if (subjectpatientContactId != Guid.Empty)
                    {
                        referralRequest["msemr_subjectpatient"] = new EntityReference("contact", subjectpatientContactId);
                    }

                    referralRequest["msemr_status"] = new OptionSetValue(935000000); //Draft
                    
                    referralRequest["msemr_description"] = "";

                    referralRequest["msemr_referralrequestnumber"] = "Ref452";

                    referralRequest["msemr_groupidentifier"] = "";

                    Guid referralRequestId = _serviceProxy.Create(referralRequest);

                    // Verify that the record has been created.
                    if (referralRequestId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", referralRequestId);
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

                ReferralRequest app = new ReferralRequest();
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
