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
    public class RiskAssessment
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
        /// Create a risk assessment. 
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

                    Entity riskAssessment = new Entity("msemr_riskassessment");
                    
                    riskAssessment["msemr_name"] = "Operational Risk";

                    //Setting context type as encounter
                    riskAssessment["msemr_contexttype"] = new OptionSetValue(935000000); //Encounter
                    Guid encounterId = Encounter.GetEncounterId(_serviceProxy, "E23556");
                    if (encounterId != Guid.Empty)
                    {
                        riskAssessment["msemr_contextencounter"] = new EntityReference("msemr_encounter", encounterId);
                    }

                    //Setting performer type as practitioner
                    riskAssessment["msemr_performertype"] = new OptionSetValue(935000000); //Practitioner
                    Guid performerpractitionerContactId = SDKFunctions.GetContactId(_serviceProxy, "James Kirk");
                    if (performerpractitionerContactId != Guid.Empty)
                    {
                        riskAssessment["msemr_performerpractitioner"] = new EntityReference("contact", performerpractitionerContactId);
                    }

                    //Setting reason type as codeable concept
                    riskAssessment["msemr_reasontype"] = new OptionSetValue(935000000); //Codeable concept
                    Guid reasonconceptCodeableConceptId = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Risk Assessment Reason Code", 935000126);
                    if (reasonconceptCodeableConceptId != Guid.Empty)
                    {
                        riskAssessment["msemr_reasonconcept"] = new EntityReference("msemr_codeableconcept", reasonconceptCodeableConceptId);
                    }

                    //Setting subject type as codeable patient
                    riskAssessment["msemr_subjecttype"] = new OptionSetValue(935000000); //Patient
                    Guid subjectpatientContactId = SDKFunctions.GetContactId(_serviceProxy, "James Kirk");
                    if (subjectpatientContactId != Guid.Empty)
                    {
                        riskAssessment["msemr_subjectpatient"] = new EntityReference("contact", subjectpatientContactId);
                    }
                    riskAssessment["msemr_occurrencetype"] = new OptionSetValue(935000000); //Time
                    riskAssessment["msemr_occurrencestartdate"] = DateTime.Now;
                    riskAssessment["msemr_occurrenceenddate"] = DateTime.Now;
                    riskAssessment["msemr_occurrencedatetime"] = DateTime.Now;

                    Guid conditionId = SDKFunctions.GetConditionId(_serviceProxy, "Tooth loss");
                    if (conditionId != Guid.Empty)
                    {
                        riskAssessment["msemr_condition"] = new EntityReference("msemr_condition", conditionId);
                    }
                    
                    Guid methodCodeableConceptId = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Method", 935000124);
                    if (methodCodeableConceptId != Guid.Empty)
                    {
                        riskAssessment["msemr_method"] = new EntityReference("msemr_codeableconcept", methodCodeableConceptId);
                    }
                    
                    Guid codeCodeableConceptId = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Code", 935000123);
                    if (codeCodeableConceptId != Guid.Empty)
                    {
                        riskAssessment["msemr_code"] = new EntityReference("msemr_codeableconcept", codeCodeableConceptId);
                    }
                    
                    riskAssessment["msemr_basedon"] = "";

                    riskAssessment["msemr_parent"] = "";
                    
                    riskAssessment["msemr_basis"] = "";

                    riskAssessment["msemr_status"] = new OptionSetValue(935000000); //Registered

                    riskAssessment["msemr_comment"] = "";

                    riskAssessment["msemr_mitigation"] = "";

                    riskAssessment["msemr_riskassessmentnumber"] = "RAN865";

                    Guid riskAssessmentId = _serviceProxy.Create(riskAssessment);

                    // Verify that the record has been created.
                    if (riskAssessmentId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", riskAssessmentId);
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

                RiskAssessment app = new RiskAssessment();
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
