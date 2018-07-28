using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Dynamics.Health.Samples
{
    public class Location
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
        /// Create a location. 
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
                    
                    Entity location = new Entity("msemr_location");
                    
                    location["msemr_name"] = "Noble Hospital Center";

                    location["msemr_addresstype"] = new OptionSetValue(935000001); //Physical
                    location["msemr_addressuse"] = new OptionSetValue(935000001); //Work
                    location["msemr_addresscity"] = "Albuquerque";
                    location["msemr_addresscountry"] = "US";                   
                    location["msemr_addressline1"] = "1770";
                    location["msemr_addressline2"] = "Byrd";
                    location["msemr_addressline3"] = "Lane";
                    location["msemr_addresspostalcode"] = "87107";
                    location["msemr_addressstate"] = "New Mexico";
                    location["msemr_addresstext"] = "Primary Location";
                    location["msemr_addressperiodend"] = DateTime.Now;
                    location["msemr_addressperiodstart"] = DateTime.Now.AddDays(20);
                    
                    Guid accountId = Organization.GetAccountId(_serviceProxy, "Galaxy Corp");
                    if (accountId != Guid.Empty)
                    {
                        location["msemr_managingorganization"] = new EntityReference("account", accountId);
                    }

                    Guid partOfLocationId = GetLocationId(_serviceProxy, "FHIR Organizational Unit");
                    if (partOfLocationId != Guid.Empty)
                    {
                        location["msemr_partof"] = new EntityReference("msemr_location", partOfLocationId);
                    }

                    Guid physicaltypeCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "si", 935000072);
                    if (physicaltypeCodeableConceptId != Guid.Empty)
                    {
                        location["msemr_physicaltype"] = new EntityReference("msemr_codeableconcept", physicaltypeCodeableConceptId);
                    }

                    Guid typeCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Dx", 935000131);
                    if (typeCodeableConceptId != Guid.Empty)
                    {
                        location["msemr_type"] = new EntityReference("msemr_codeableconcept", typeCodeableConceptId);
                    }
                    
                    location["msemr_mode"] = new OptionSetValue(935000001); //Kind

                    location["msemr_status"] = new OptionSetValue(935000000); //Active

                    location["msemr_operationalstatus"] = new OptionSetValue(935000004); //Occupied
                    
                    location["msemr_description"] = "Primary Location";

                    location["msemr_locationnumber"] = "L442";
                    location["msemr_locationalias1"] = "General Hospital";
                    location["msemr_locationpositionaltitude"] = (decimal) 18524.5265;
                    location["msemr_locationpositionlatitude"] = (decimal) 24.15;
                    location["msemr_locationpositionlongitude"] = (decimal) 841.12;

                    Guid locationId = _serviceProxy.Create(location);

                    // Verify that the record has been created.
                    if (locationId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", locationId);
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

        public static Guid GetLocationId(IOrganizationService _service, string name)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("msemr_location");

                query.ColumnSet = new ColumnSet("msemr_locationid");

                query.Criteria.AddCondition("msemr_name", ConditionOperator.Equal, name);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("msemr_locationid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["msemr_locationid"]);
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
