using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Dynamics.Health.Samples
{
    public class Organization
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
        /// Create an Organization. 
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

                    Practitioner practioner = new Practitioner();

                    Entity organization = new Entity("account");
                    
                    organization["name"] = "Galaxy Corp";
                    
                    organization["msemr_address1periodstartdate"] = DateTime.Now;
                    organization["msemr_address1periodenddate"] = DateTime.Now;
                    
                    organization["msemr_telecom1startdate"] = DateTime.Now;
                    organization["msemr_telecom1enddate"] = DateTime.Now;
                    organization["msemr_telecom1system"] = new OptionSetValue(935000001); //Email
                    organization["msemr_telecom1use"] = new OptionSetValue(935000001); //Work
                    organization["msemr_telecom1rank"] = 18;

                    organization["msemr_accounttype"] = new OptionSetValue(935000001); //Organization

                    organization["msemr_alias"] = "Galaxy";

                    Guid primaryContact = Contact.GetContactId(_serviceProxy, "James Kirk");
                    if (primaryContact != Guid.Empty)
                    {
                        organization["primarycontactid"] = new EntityReference("contact", primaryContact);
                    }
                    Guid contact1puroposeCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "BILL", 935000027);
                    if (contact1puroposeCodeableConceptId != Guid.Empty)
                    {
                        organization["msemr_contact1puropose"] = new EntityReference("msemr_codeableconcept", contact1puroposeCodeableConceptId);
                    }
                    
                    Guid organizationId = _serviceProxy.Create(organization);

                    // Verify that the record has been created.
                    if (organizationId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", organizationId);
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

        public static Guid GetAccountId(IOrganizationService _service, string name)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("account");

                query.ColumnSet = new ColumnSet("accountid");

                query.Criteria.AddCondition("name", ConditionOperator.Equal, name);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("accountid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["accountid"]);
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
