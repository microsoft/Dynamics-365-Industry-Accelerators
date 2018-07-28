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
        /// <param name="organizationUrl">Contains organization service url</param>
        /// <param name="homeRealmUri">Contains home real Uri</param>
        /// <param name="clientCredentials">Contains client credentials</param>
        /// <param name="deviceCredentials">Contains device credentials</param>

        public void Run(string organizationUrl, string homeRealmUri, ClientCredentials clientCredentials, ClientCredentials deviceCredentials)
        {
            try
            {
                // Connect to the Organization service. 
                // The using statement assures that the service proxy will be properly disposed.
                using (_serviceProxy = new OrganizationServiceProxy(new Uri(organizationUrl), new Uri(homeRealmUri), clientCredentials, deviceCredentials))
                {
                    // This statement is required to enable early-bound type support.
                    _serviceProxy.EnableProxyTypes();

                    Entity appointmentemr = new Entity("msemr_appointmentemr");
                    
                    appointmentemr["subject"] = "Routine";

                    //Setting participant actor type as patient
                    appointmentemr["msemr_participantactortype"] = new OptionSetValue(935000000); //Patient
                    Guid actorpatientContactId = Contact.GetContactId(_serviceProxy, "Daniel Atlas");
                    if (actorpatientContactId != Guid.Empty)
                    {
                        appointmentemr["msemr_actorpatient"] = new EntityReference("contact", actorpatientContactId);
                    }

                    Guid particpanttypeCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Participant Type", 935000092);
                    if (particpanttypeCodeableConceptId != Guid.Empty)
                    {
                        appointmentemr["msemr_particpanttype"] = new EntityReference("msemr_codeableconcept", particpanttypeCodeableConceptId);
                    }

                    appointmentemr["msemr_participantstatus"] = new OptionSetValue(935000000); //Accepted

                    appointmentemr["msemr_appointmentcreationdate"] = DateTime.Now;

                    Guid appointmentTypeId = AppointmentType.GetAppointmentTypeId(_serviceProxy, "New");
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

                    Guid servicecategoryCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Category", 935000129);
                    if (servicecategoryCodeableConceptId != Guid.Empty)
                    {
                        appointmentemr["msemr_servicecategory"] = new EntityReference("msemr_codeableconcept", servicecategoryCodeableConceptId);
                    }

                    Guid bookingsStatusId = BookingStatus.GetBookingStatusId(_serviceProxy, "Committed");
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

    }
}
