using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Dynamics.Health.Samples
{
   public  class Device
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
        /// Create a device.
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

                    Entity device = new Entity("msemr_device");

                    device["msemr_name"] = "MAGNETOM Sola";
                    device["msemr_manufacturer"] = "Siemens Healthineers";
                    device["msemr_manufacturerdate"] = DateTime.Now;
                    device["msemr_model"] = "1.5T";
                    device["msemr_carrieraidc"] = "abc-123-zxy";
                    device["msemr_devicenumber"] = "dvc-00789";
                    device["msemr_devicestatus"] = new OptionSetValue(935000000);
                    device["msemr_expirationdate"] = DateTime.Now.AddYears(3);

                    Guid LocationId = Location.GetLocationId(_serviceProxy, "Noble Hospital Center");
                    if (LocationId != Guid.Empty)
                    {
                        device["msemr_location"] = new EntityReference("msemr_location", LocationId);
                    }
                    device["msemr_lotnumber"] = "123456";
                   

                    Guid PatientId = Contact.GetContactId(_serviceProxy, "James Kirk", 935000000);
                    if (PatientId != Guid.Empty)
                    {
                        device["msemr_patient"] = new EntityReference("contact", PatientId);
                    }

                    Guid CodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "271003", 935000037);
                    if (CodeableConceptId != Guid.Empty)
                    {
                        device["msemr_type"] = new EntityReference("msemr_codeableconcept", CodeableConceptId);
                    }
                    device["msemr_udi"] = "xyz-123";
                    device["msemr_udicarrierhrf"] = "abc-987";
                    device["msemr_udientrytype"] = new OptionSetValue(935000002);
                    device["msemr_udiissuer"] = "GS1";
                    device["msemr_udijurisdiction"] = "http://hl7.org/fhir/NamingSystem/fda-udi";
                    device["msemr_url"] = "http://www.device.com";
                    device["msemr_version"] = "1.0.0.0";
                    
                    Guid DeviceId = _serviceProxy.Create(device);

                    // Verify that the record has been created.
                    if (DeviceId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", DeviceId);
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

        public static Guid GetDeviceId(IOrganizationService _service, string name)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("msemr_device");

                query.ColumnSet = new ColumnSet("msemr_deviceid");

                query.Criteria.AddCondition("msemr_name", ConditionOperator.Equal, name);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("msemr_deviceid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["msemr_deviceid"]);
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

    }
}
