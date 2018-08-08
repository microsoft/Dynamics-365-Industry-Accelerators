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
    public class MedicationAdministration
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
        /// Create a medication administration.
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

                    Entity medicationadministration = new Entity("msemr_medicationadministration");

                    medicationadministration["msemr_medicationadministrationnumber"] = "1234-abc";
                    medicationadministration["msemr_status"] = new OptionSetValue(935000000);
                    medicationadministration["msemr_dosagetext"] = "--";
                    medicationadministration["msemr_notgiven"] = true;
                    medicationadministration["msemr_dosagedose"] = 1;

                    //Setting Effective Type as DateTime
                    medicationadministration["msemr_effectivetype"] = new OptionSetValue(935000000);//DateTime
                    medicationadministration["msemr_effectivedatetime"] = DateTime.Now;

                    //Setting Subject Type as Patient
                    medicationadministration["msemr_subjecttype"] = new OptionSetValue(935000000);
                    Guid SubjectTypePatient = SDKFunctions.GetContactId(_serviceProxy, "James Kirk", 935000000);
                    if (SubjectTypePatient != Guid.Empty)
                    {
                        medicationadministration["msemr_subjecttypepatient"] = new EntityReference("contact", SubjectTypePatient);
                    }

                    //Setting Context Type as Encounter
                    medicationadministration["msemr_contexttype"] = new OptionSetValue(935000000); //Encounter
                    Guid ContextTypeEncounter = Encounter.GetEncounterId(_serviceProxy, "E23556");
                    if (ContextTypeEncounter != Guid.Empty)
                    {
                        medicationadministration["msemr_contexttypeencounter"] = new EntityReference("msemr_encounter", ContextTypeEncounter);
                    }


                    //Setting Medication Type as Reference
                    medicationadministration["msemr_medicationtype"] = new OptionSetValue(935000001);//Refrence
                    Guid MedicationReference = Medication.GetProductId(_serviceProxy, "Panadol");
                    if (MedicationReference != Guid.Empty)
                    {
                        medicationadministration["msemr_medicationreference"] = new EntityReference("product", MedicationReference);
                    }

                    //Setting Dosage Rate Type as  Quantity
                    medicationadministration["msemr_dosageratetype"] = new OptionSetValue(935000001);//Quantity
                    medicationadministration["msemr_dosageratequantity"] = 1;

                    Guid Category = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Pain", 935000080);
                    if (Category != Guid.Empty)
                    {
                        medicationadministration["msemr_category"] = new EntityReference("msemr_codeableconcept", Category);
                    }
                    Guid Prescription = MedicationRequest.getMedicationRequestId(_serviceProxy, "MR-1234");
                    if (Prescription != Guid.Empty)
                    {
                        medicationadministration["msemr_prescription"] = new EntityReference("msemr_medicationrequest", Prescription);
                    }
                    Guid DosageSite = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Route Code", 935000127);
                    if (DosageSite != Guid.Empty)
                    {
                        medicationadministration["msemr_dosagesite"] = new EntityReference("msemr_codeableconcept", DosageSite);
                    }

                    Guid DosageRoute = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Route Code", 935000127);
                    if (DosageRoute != Guid.Empty)
                    {
                        medicationadministration["msemr_dosageroute"] = new EntityReference("msemr_codeableconcept", DosageRoute);
                    }
                    Guid DosageMethod = SDKFunctions.GetCodeableConceptId(_serviceProxy, "678-abc", 935000005);
                    if (DosageMethod != Guid.Empty)
                    {
                        medicationadministration["msemr_dosagemethod"] = new EntityReference("msemr_codeableconcept", DosageMethod);
                    }

                    Guid MedicationAdministrationId = _serviceProxy.Create(medicationadministration);

                    // Verify that the record has been created.
                    if (MedicationAdministrationId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", MedicationAdministrationId);
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

                MedicationAdministration app = new MedicationAdministration();
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
