using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Dynamics.Health.Samples
{
    public class Medication
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
        /// Create a medication. 
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

                    Entity product = new Entity("product");
                    
                    product["name"] = "Paracetamol";
                    
                    Guid medicationcodeCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "HCPCS", 935000076);
                    if (medicationcodeCodeableConceptId != Guid.Empty)
                    {
                        product["msemr_medicationcode"] = new EntityReference("msemr_codeableconcept", medicationcodeCodeableConceptId);
                    }

                    Guid formCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "1095-C", 935000058);
                    if (formCodeableConceptId != Guid.Empty)
                    {
                        product["msemr_form"] = new EntityReference("msemr_codeableconcept", formCodeableConceptId);
                    }

                    Guid packagecontainerCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Blister pack", 935000077);
                    if (packagecontainerCodeableConceptId != Guid.Empty)
                    {
                        product["msemr_packagecontainer"] = new EntityReference("msemr_codeableconcept", packagecontainerCodeableConceptId);
                    }

                    Guid defaultUnitId = GetDefaultUnit(_serviceProxy, "Primary Unit");
                    if (defaultUnitId != Guid.Empty)
                    {
                        product["defaultuomid"] = new EntityReference("uom", defaultUnitId);
                    }

                    Guid unitGroupId = GetUnitGroup(_serviceProxy, "Default Unit");
                    if (unitGroupId != Guid.Empty)
                    {
                        product["defaultuomscheduleid"] = new EntityReference("uomschedule", unitGroupId);
                    }

                    product["msemr_isoverthecounter"] = true;

                    product["msemr_isbrand"] = true;

                    Guid productid = _serviceProxy.Create(product);

                    // Verify that the record has been created.
                    if (productid != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", productid);
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

        public static Guid GetUnitGroup(IOrganizationService _service, string name)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("uomschedule");

                query.ColumnSet = new ColumnSet("uomscheduleid");

                query.Criteria.AddCondition("name", ConditionOperator.Equal, name);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("uomscheduleid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["uomscheduleid"]);
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Guid GetDefaultUnit(IOrganizationService _service, string name)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("uom");

                query.ColumnSet = new ColumnSet("uomid");

                query.Criteria.AddCondition("name", ConditionOperator.Equal, name);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("uomid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["uomid"]);
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Guid GetProductId(IOrganizationService _service, string name)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("product");

                query.ColumnSet = new ColumnSet("productid");

                query.Criteria.AddCondition("name", ConditionOperator.Equal, name);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("productid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["productid"]);
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
