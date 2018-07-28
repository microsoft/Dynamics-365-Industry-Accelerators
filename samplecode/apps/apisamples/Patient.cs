using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Dynamics.Health.Samples
{
    public class Patient
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
        /// Create a patient 
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

                    Entity patient = new Entity("contact");

                    //Setting  contact type as Patient
                    patient["msemr_contacttype"] = new OptionSetValue(935000000);
                    patient["firstname"] = "John";
                    patient["lastname"] = "Smith";

                    Guid GeneralPractioner = Contact.GetContactId(_serviceProxy, "Emily Williams", 935000001);
                    if (GeneralPractioner !=  Guid.Empty)
                    {
                        patient["msemr_generalpractioner"] = new EntityReference("contact", GeneralPractioner);
                    }
                    patient["msemr_medicalrecordnumber"] = "PT-123";
                    patient["emailaddress1"] = "john.smith@hotmail.com";
                    patient["telephone2"] = "1-888-751-4083";
                    patient["mobilephone"] = "555-555-1234";
                    patient["telephone1"] = "653-123-1234";
                    patient["preferredcontactmethodcode"] = new OptionSetValue(3); //Phone
                    patient["gendercode"] = new OptionSetValue(1); //Male
                    patient["familystatuscode"] = new OptionSetValue(2); //Married
                    patient["anniversary"] = DateTime.Now.AddYears(-20);
                    patient["spousesname"] = "Crista Smith";
                    patient["address1_line1"] = "3386";
                    patient["address1_line2"] = "Gateway Avenue";
                    patient["address1_city"] = "Lancaster";
                    patient["address1_stateorprovince"] = "CA";
                    patient["address1_postalcode"] = "93534";
                    patient["address1_country"] = "US";
                 
                    patient["birthdate"] = DateTime.Now.AddYears(-50);

                    Guid PatientId  =_serviceProxy.Create(patient);

                    // Verify that the record has been created.
                    if (PatientId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", PatientId);
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
