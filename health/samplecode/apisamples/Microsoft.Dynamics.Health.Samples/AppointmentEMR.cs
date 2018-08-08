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
    public class AppointmentEMR
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
        /// Create an appointment.
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

                    Entity appointmentemr = new Entity("msemr_appointmentemr");
                    
                    appointmentemr["subject"] = "Routine";

                    //Setting participant actor type as patient
                    appointmentemr["msemr_participantactortype"] = new OptionSetValue(935000000); //Patient
                    Guid actorpatientContactId = SDKFunctions.GetContactId(_serviceProxy, "Daniel Atlas");
                    if (actorpatientContactId != Guid.Empty)
                    {
                        appointmentemr["msemr_actorpatient"] = new EntityReference("contact", actorpatientContactId);
                    }

                    Guid particpanttypeCodeableConceptId = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Participant Type", 935000092);
                    if (particpanttypeCodeableConceptId != Guid.Empty)
                    {
                        appointmentemr["msemr_particpanttype"] = new EntityReference("msemr_codeableconcept", particpanttypeCodeableConceptId);
                    }

                    appointmentemr["msemr_participantstatus"] = new OptionSetValue(935000000); //Accepted

                    appointmentemr["msemr_appointmentcreationdate"] = DateTime.Now;

                    Guid appointmentTypeId = SDKFunctions.GetAppointmentTypeId(_serviceProxy, "New");
                    if (appointmentTypeId != Guid.Empty)
                    {
                        appointmentemr["msemr_appointmenttype"] = new EntityReference("msdyn_workordertype", appointmentTypeId);
                    }

                    appointmentemr["msemr_comment"] = "";

                    appointmentemr["msemr_description"] = "General";

                    appointmentemr["msemr_starttime"] = DateTime.Now;
                    appointmentemr["msemr_endtime"] = DateTime.Now;
                    appointmentemr["msemr_minutesduration"] = 20;

                    appointmentemr["msemr_patientinstruction"] = "";

                    appointmentemr["msemr_priority"] = 1;

                    appointmentemr["msemr_required"] = new OptionSetValue(935000002); //Information Only

                    Guid servicecategoryCodeableConceptId = SDKFunctions.GetCodeableConceptId(_serviceProxy, "Category", 935000129);
                    if (servicecategoryCodeableConceptId != Guid.Empty)
                    {
                        appointmentemr["msemr_servicecategory"] = new EntityReference("msemr_codeableconcept", servicecategoryCodeableConceptId);
                    }

                    Guid bookingsStatusId = SDKFunctions.GetBookingStatusId(_serviceProxy, "Committed");
                    if (bookingsStatusId != Guid.Empty)
                    {
                        appointmentemr["msemr_status"] = new EntityReference("bookingstatus", bookingsStatusId);
                    }
                    
                    appointmentemr["msemr_supportinginformation"] = "";

                    Guid appointmentemrId = _serviceProxy.Create(appointmentemr);

                    // Verify that the record has been created.
                    if (appointmentemrId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", appointmentemrId);
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

                AppointmentEMR app = new AppointmentEMR();
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
