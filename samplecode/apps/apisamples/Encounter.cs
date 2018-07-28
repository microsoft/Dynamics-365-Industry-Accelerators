using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Dynamics.Health.Samples
{
    public class Encounter
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
        /// Create an encounter. 
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

                    Entity encounter = new Entity("msemr_encounter");
                    
                    encounter["msemr_name"] = "Routine";

                    Guid subjectpatientContactId = Contact.GetContactId(_serviceProxy, "Daniel Atlas");
                    if (subjectpatientContactId != Guid.Empty)
                    {
                        encounter["msemr_subjectpatient"] = new EntityReference("contact", subjectpatientContactId);
                    }

                    encounter["msemr_class"] = new OptionSetValue(935000008); //short stay

                    encounter["msemr_encounterstartdate"] = DateTime.Now;
                    encounter["msemr_encounterenddate"] = DateTime.Now;
                    encounter["msemr_encounterclass"] = new OptionSetValue(935000001); //outPatient
                    encounter["msemr_encounterlength"] = (decimal)30.5;
                    encounter["msemr_encounterstatus"] = new OptionSetValue(935000000); //Planned
                    encounter["msemr_encounteridentifier"] = "Routine 25352";
                    Guid priorityCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Urgent", 935000102);
                    if (priorityCodeableConceptId != Guid.Empty)
                    {
                        encounter["msemr_encounterpriority"] = new EntityReference("msemr_codeableconcept", priorityCodeableConceptId);
                    }
                    Guid groupCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Group Identifier", 935000063);
                    if (groupCodeableConceptId != Guid.Empty)
                    {
                        encounter["msemr_encountergroupidentifier"] = new EntityReference("msemr_codeableconcept", groupCodeableConceptId);
                    }
                    Guid parentEncountertId = GetEncounterId(_serviceProxy, "E23556");
                    if (parentEncountertId != Guid.Empty)
                    {
                        encounter["msemr_encounterparentencounteridentifier"] = new EntityReference("msemr_encounter", parentEncountertId);
                    }
                    Guid patientIdentifier = Contact.GetContactId(_serviceProxy, "James Kirk");
                    if (patientIdentifier != Guid.Empty)
                    {
                        encounter["msemr_encounterpatientidentifier"] = new EntityReference("contact", patientIdentifier);
                    }

                    encounter["msemr_periodstart"] = DateTime.Now;
                    encounter["msemr_periodend"] = DateTime.Now;
                    encounter["msemr_duration"] = 30;
                    encounter["msemr_priority"] = new OptionSetValue(935000000); //ASAP

                    encounter["msemr_hospitalizationpreadmissionnumber"] = "25352";
                    Guid destinationLocationId = Location.GetLocationId(_serviceProxy, "Noble Hospital Center");
                    if (destinationLocationId != Guid.Empty)
                    {
                        encounter["msemr_hospitalizationdestination"] = new EntityReference("msemr_location", destinationLocationId);
                    }
                    Guid dischargedispositionCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Discharge Disposition", 935000042);
                    if (dischargedispositionCodeableConceptId != Guid.Empty)
                    {
                        encounter["msemr_hospitalizationdischargedisposition"] = new EntityReference("msemr_codeableconcept", dischargedispositionCodeableConceptId);
                    }
                    Guid admitsourceCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Admit Source", 935000007);
                    if (admitsourceCodeableConceptId != Guid.Empty)
                    {
                        encounter["msemr_hospitalizationadmitsource"] = new EntityReference("msemr_codeableconcept", admitsourceCodeableConceptId);
                    }
                    Guid readmissionCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Re-admission", 935000114);
                    if (readmissionCodeableConceptId != Guid.Empty)
                    {
                        encounter["msemr_hospitalizationreadmission"] = new EntityReference("msemr_codeableconcept", readmissionCodeableConceptId);
                    }

                    Guid originLocationId = Location.GetLocationId(_serviceProxy, "Alpine");
                    if (originLocationId != Guid.Empty)
                    {
                        encounter["msemr_hospitalizationorigin"] = new EntityReference("msemr_location", originLocationId);
                    }

                    Guid accountId = Organization.GetAccountId(_serviceProxy, "Galaxy Corp");
                    if (accountId != Guid.Empty)
                    {
                        encounter["msemr_onbehalfof"] = new EntityReference("account", accountId);
                    }

                    Guid encounterId = _serviceProxy.Create(encounter);

                    // Verify that the record has been created.
                    if (encounterId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", encounterId);
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
        
        public static Guid GetEncounterId(IOrganizationService _service, string number)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("msemr_encounter");

                query.ColumnSet = new ColumnSet("msemr_encounterid");

                query.Criteria.AddCondition("msemr_encounteridentifier", ConditionOperator.Equal, number);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("msemr_encounterid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["msemr_encounterid"]);
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
