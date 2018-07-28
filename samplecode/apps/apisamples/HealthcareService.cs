using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Dynamics.Health.Samples
{
    public class HealthcareService
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
        /// Create a healthcare service. 
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

                    Entity healthcareService = new Entity("msemr_healthcareservice");
                    
                    healthcareService["msemr_name"] = "Surgical Treatment";

                    healthcareService["msemr_appointmentrequired"] = true;

                    healthcareService["msemr_availabilityexceptions"] = "Public holiday availability";

                    healthcareService["msemr_comment"] = "More details";

                    healthcareService["msemr_healthcareservice"] = "Healthcare Service";

                    healthcareService["msemr_extradetails"] = "Extra Details";

                    healthcareService["msemr_notavailableduringenddatetime"] = DateTime.Now;
                    healthcareService["msemr_notavailableduringstartdatetime"] = DateTime.Now;
                    healthcareService["msemr_notavailabledescription"] = "Machine failure";

                    healthcareService["msemr_eligibilitynote"] = "Eligibility Note";
                    Guid eligibilityCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Eligibile", 935000132);
                    if (eligibilityCodeableConceptId != Guid.Empty)
                    {
                        healthcareService["msemr_eligibility"] = new EntityReference("msemr_codeableconcept", eligibilityCodeableConceptId);
                    }

                    Guid providedbyAccountId = Organization.GetAccountId(_serviceProxy, "Galaxy Corp");
                    if (providedbyAccountId != Guid.Empty)
                    {
                        healthcareService["msemr_providedby"] = new EntityReference("account", providedbyAccountId);
                    }

                    Guid categoryCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Category", 935000129);
                    if (categoryCodeableConceptId != Guid.Empty)
                    {
                        healthcareService["msemr_category"] = new EntityReference("msemr_codeableconcept", categoryCodeableConceptId);
                    }

                    Guid healthcareServiceId = _serviceProxy.Create(healthcareService);

                    // Verify that the record has been created.
                    if (healthcareServiceId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", healthcareServiceId);
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
