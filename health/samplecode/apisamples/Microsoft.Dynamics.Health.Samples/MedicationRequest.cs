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
    public class MedicationRequest
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
        /// Create a medication request. 
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

                    Entity medicationrequest = new Entity("msemr_medicationrequest");

                    medicationrequest["msemr_identifier"] = "MR-1234";
                    medicationrequest["msemr_groupidentifier"] = "MRG-12345";
                    medicationrequest["msemr_priority"] = new OptionSetValue(935000000);
                    medicationrequest["msemr_intent"] = new OptionSetValue(935000000);
                    medicationrequest["msemr_status"] = new OptionSetValue(935000000);
                    medicationrequest["msemr_dispenserequestvalidityperiodstartdate"] = DateTime.Now;
                    medicationrequest["msemr_dispenserequestvalidityperiodenddate"] = DateTime.Now;
                    medicationrequest["msemr_authoredon"] = DateTime.Now;
                    medicationrequest["msemr_expectedsupplyduration"] = (decimal)3.5;

                    // Setting Request Type as Organisation
                    medicationrequest["msemr_requesteragenttype"] = new OptionSetValue(935000001); //Organisation
                    Guid RequesteragentTypeOrganization = Organization.GetAccountId(_serviceProxy, "Galaxy Corp");
                    if (RequesteragentTypeOrganization != Guid.Empty)
                    {
                        medicationrequest["msemr_requesteragenttypeorganization"] = new EntityReference("account", RequesteragentTypeOrganization);
                    }

                    //Setting Medication Type as Medication Reference
                    medicationrequest["msemr_medicationtype"] = new OptionSetValue(935000001);
                    Guid MedicationTypeReference = Medication.GetProductId(_serviceProxy, "Panadol");
                    if (MedicationTypeReference != Guid.Empty)
                    {
                        medicationrequest["msemr_medicationtypereference"] = new EntityReference("product", MedicationTypeReference);
                    }


                    // Setting Subject Type as Patient
                    medicationrequest["msemr_subjecttype"] = new OptionSetValue(935000000);
                    Guid SubjectTypePatient = SDKFunctions.GetContactId(_serviceProxy, "James Kirk", 935000000);
                    if (SubjectTypePatient != Guid.Empty)
                    {
                        medicationrequest["msemr_subjecttypepatient"] = new EntityReference("contact", SubjectTypePatient);
                    }

                    //Setting Context Type as Episode as Care
                    medicationrequest["msemr_contexttype"] = new OptionSetValue(935000000);
                    Guid ConextTypeEpisodeofCare = EpisodeOfCare.GetEpisodeOfCareId(_serviceProxy, "EPC-153");
                    if (ConextTypeEpisodeofCare != Guid.Empty)
                    {
                        medicationrequest["msemr_contexttypeepisodeofcare"] = new EntityReference("msemr_episodeofcare", ConextTypeEpisodeofCare);
                    }


                    Guid Recorder = SDKFunctions.GetContactId(_serviceProxy, "Emily Williams", 935000001);
                    if (Recorder != Guid.Empty)
                    {
                        medicationrequest["msemr_recorder"] = new EntityReference("contact", Recorder);
                    }
                    Guid Category = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Pain", 935000080);
                    if (Category != Guid.Empty)
                    {
                        medicationrequest["msemr_category"] = new EntityReference("msemr_codeableconcept", Category);
                    }
                    Guid PriorPrescription = getMedicationRequestId(_serviceProxy, "MR-1234");
                    if (PriorPrescription != Guid.Empty)
                    {
                        medicationrequest["msemr_priorprescription"] = new EntityReference("msemr_medicationrequest", PriorPrescription);
                    }
                    Guid RequesterOnBehalfOf = Organization.GetAccountId(_serviceProxy, "Galaxy Corp");
                    if (RequesterOnBehalfOf != Guid.Empty)
                    {
                        medicationrequest["msemr_requesteronbehalfof"] = new EntityReference("account", RequesterOnBehalfOf);
                    }

                    Guid SubstitutionReason = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Substitution Reason", 935000144);
                    if (SubstitutionReason != Guid.Empty)
                    {
                        medicationrequest["msemr_substitutionreason"] = new EntityReference("msemr_codeableconcept", SubstitutionReason);
                    }



                    Guid DispenseRequestPerformer = Organization.GetAccountId(_serviceProxy, "Galaxy Corp");
                    if (DispenseRequestPerformer != Guid.Empty)
                    {
                        medicationrequest["msemr_dispenserequestperformer"] = new EntityReference("account", DispenseRequestPerformer);
                    }

                    medicationrequest["msemr_substitutionallowed"] = true;
                    medicationrequest["msemr_dispenserequestquantity"] = 1;
                    medicationrequest["msemr_dispenserequestnumberofrepeatsallowed"] = 1;
                    medicationrequest["msemr_dispenserequestexpectedsupplyduration"] = 1;


                    Guid MedicationRequestId = _serviceProxy.Create(medicationrequest);

                    // Verify that the record has been created.
                    if (MedicationRequestId != Guid.Empty)
                    {

                        Console.WriteLine("Succesfully created {0}.", MedicationRequestId);
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

        public static Guid getMedicationRequestId(IOrganizationService _service, string name)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("msemr_medicationrequest");

                query.ColumnSet = new ColumnSet("msemr_medicationrequestid");

                query.Criteria.AddCondition("msemr_identifier", ConditionOperator.Equal, name);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("msemr_medicationrequestid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["msemr_medicationrequestid"]);
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

                MedicationRequest app = new MedicationRequest();
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
