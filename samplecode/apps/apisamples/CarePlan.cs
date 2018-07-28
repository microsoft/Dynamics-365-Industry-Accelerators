using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Dynamics.Health.Samples
{
    public class CarePlan
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
        /// Create a careplan.
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

                    Entity careplan = new Entity("msemr_careplan");
                    
                    careplan["msemr_careplanidentifier"] = "CP25741";
                    careplan["msemr_title"] = "Dieting Care Plan";
                    careplan["msemr_planstatus"] = new OptionSetValue(935000003);

                    // Setting Context Type
                    careplan["msemr_contexttype"] = new OptionSetValue(935000000); //Encounter
                    Guid EncounterId = Encounter.GetEncounterId(_serviceProxy, "E23556");
                    if (EncounterId != Guid.Empty)
                    {
                        careplan["msemr_encounteridentifier"] = new EntityReference("msemr_encounter", EncounterId);
                    }

                    // Setting Subject Type
                    careplan["msemr_subjecttype"] = new OptionSetValue(935000000); //Patient
                    Guid PatientId = Contact.GetContactId(_serviceProxy, "James Kirk", 935000000);
                    if (PatientId != Guid.Empty)
                    {
                        careplan["msemr_patientidentifier"] = new EntityReference("contact", PatientId);
                    }

                    careplan["msemr_plandescription"] = "this plan is very specific for this type of case";
                    careplan["msemr_planintent"] = new OptionSetValue(935000001);
                    careplan["msemr_planstartdate"] = DateTime.Now;
                    careplan["msemr_planenddate"] = DateTime.Now;

                    Guid CarePlanId = _serviceProxy.Create(careplan);

                    // Verify that the record has been created.
                    if (CarePlanId != Guid.Empty)
                    {

                        Console.WriteLine("Succesfully created {0}.", CarePlanId);
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
