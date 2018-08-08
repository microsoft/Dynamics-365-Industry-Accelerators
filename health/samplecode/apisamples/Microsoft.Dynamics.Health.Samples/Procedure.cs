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
   public class Procedure
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
        /// Create a procedure.
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

                    Entity procedure = new Entity("msemr_procedure");

                    procedure["msemr_datetime"] = DateTime.Now;
                    procedure["msemr_performedstartdate"] = DateTime.Now;
                    procedure["msemr_performedenddate"] = DateTime.Now;
                    Guid EpisodeofCare = EpisodeOfCare.GetEpisodeOfCareId(_serviceProxy, "EPC-153");
                    if (EpisodeofCare != Guid.Empty)
                    {
                        procedure["msemr_episodeofcare"] = new EntityReference("msemr_episodeofcare", EpisodeofCare);
                    }
                   
                    Guid Code = Medication.GetProductId(_serviceProxy, "Panadol");
                    if (Code != Guid.Empty)
                    {
                        procedure["msemr_code"] = new EntityReference("product", Code);
                    }

                    Guid LocationId = SDKFunctions.GetLocationId(_serviceProxy, "Noble Hospital Center");
                    if (LocationId != Guid.Empty)
                    {
                        procedure["msemr_location"] = new EntityReference("msemr_location", LocationId);
                    }
                    Guid NotDoneReason = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Blood pressure was high", 935000109);
                    if (NotDoneReason != Guid.Empty)
                    {
                        procedure["msemr_notdonereason"] = new EntityReference("msemr_codeableconcept", NotDoneReason);
                    }
                    Guid Category = SDKFunctions.GetCodeableConceptId(_serviceProxy, "xyz-123", 935000103);
                    if (Category != Guid.Empty)
                    {
                        procedure["msemr_category"] = new EntityReference("msemr_codeableconcept", NotDoneReason);
                    }
                    Guid EncounterId = Encounter.GetEncounterId(_serviceProxy, "E23556");
                    if (EncounterId != Guid.Empty)
                    {
                        procedure["msemr_encounter"] = new EntityReference("msemr_encounter", EncounterId);
                    }

                    //Setting Subject Type as Patient
                    procedure["msemr_subjecttype"] = new OptionSetValue(935000000);  //Patient
                    Guid Patient = SDKFunctions.GetContactId(_serviceProxy, "James Kirk", 935000000);
                    if (Patient != Guid.Empty)
                    {
                        procedure["msemr_patient"] = new EntityReference("contact", Patient);
                    }
                   
                    Guid Outcome = SDKFunctions.GetCodeableConceptId(_serviceProxy, "mno-123", 935000107);
                    if (Outcome != Guid.Empty)
                    {
                        procedure["msemr_outcome"] = new EntityReference("msemr_codeableconcept", Outcome);
                    }
                    procedure["msemr_status"] = new OptionSetValue(935000000);
                    procedure["msemr_procedureidentifier"] = "mdf/xyz";
                    procedure["msemr_notdone"] = true;


                    Guid ProcedureId = _serviceProxy.Create(procedure);

                    // Verify that the record has been created.
                    if (ProcedureId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", ProcedureId);
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

                Procedure app = new Procedure();
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
