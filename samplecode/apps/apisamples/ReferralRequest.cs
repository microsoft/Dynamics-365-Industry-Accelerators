using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Dynamics.Health.Samples
{
    public class ReferralRequest
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
        /// Create a referral request. 
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

                    Entity referralRequest = new Entity("msemr_referralrequest");

                    //Primary Field
                    referralRequest["msemr_name"] = "Andrea Leonardo";

                    //Setting context type as encounter
                    referralRequest["msemr_contexttype"] = new OptionSetValue(935000000); //Encounter
                    Guid subjectcontextencounterId = Encounter.GetEncounterId(_serviceProxy, "E23556");
                    if (subjectcontextencounterId != Guid.Empty)
                    {
                        referralRequest["msemr_subjectcontextencounter"] = new EntityReference("msemr_encounter", subjectcontextencounterId);
                    }
                    Guid encounterId = Encounter.GetEncounterId(_serviceProxy, "E23556");
                    if (encounterId != Guid.Empty)
                    {
                        referralRequest["msemr_initiatingencounter"] = new EntityReference("msemr_encounter", encounterId);
                    }

                    //Setting requester agent as Practitioner
                    referralRequest["msemr_requesteragent"] = new OptionSetValue(935000000); //Practitioner
                    Guid requesteragentpractitionerContactId = Contact.GetContactId(_serviceProxy, "Daniel Atlas");
                    if (requesteragentpractitionerContactId != Guid.Empty)
                    {
                        referralRequest["msemr_requesteragentpractitioner"] = new EntityReference("contact", requesteragentpractitionerContactId);
                    }

                    Guid requesteronbehalfofAccountId = Organization.GetAccountId(_serviceProxy, "Galaxy Corp");
                    if (requesteronbehalfofAccountId != Guid.Empty)
                    {
                        referralRequest["msemr_requesteronbehalfof"] = new EntityReference("account", requesteronbehalfofAccountId);
                    }

                    Guid requestorContactId = Contact.GetContactId(_serviceProxy, "James Kirk");
                    if (requestorContactId != Guid.Empty)
                    {
                        referralRequest["msemr_requestor"] = new EntityReference("contact", requestorContactId);
                    }

                    referralRequest["msemr_occurenceperiodstartdate"] = DateTime.Now;
                    referralRequest["msemr_occurenceperiodenddate"] = DateTime.Now;
                    referralRequest["msemr_occurrencedate"] = DateTime.Now;
                    referralRequest["msemr_occurrencetype"] = new OptionSetValue(935000000); //Date

                    referralRequest["msemr_authoredon"] = DateTime.Now;

                    Guid typeCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Referral Request", 935000094);
                    if (typeCodeableConceptId != Guid.Empty)
                    {
                        referralRequest["msemr_type"] = new EntityReference("msemr_codeableconcept", typeCodeableConceptId);
                    }

                    Guid basedonreferralId = GetReferralRequestId(_serviceProxy, "Ref452");
                    if (basedonreferralId != Guid.Empty)
                    {
                        referralRequest["msemr_basedonreferral"] = new EntityReference("msemr_referralrequest", basedonreferralId);
                    }                    

                    Guid practitionerspecialtyCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Psychiatric", 935000101);
                    if (practitionerspecialtyCodeableConceptId != Guid.Empty)
                    {
                        referralRequest["msemr_practitionerspecialty"] = new EntityReference("msemr_codeableconcept", practitionerspecialtyCodeableConceptId);
                    }

                    referralRequest["msemr_priority"] = new OptionSetValue(935000000); //Routine

                    referralRequest["msemr_intent"] = new OptionSetValue(935000000); //Proposal

                    referralRequest["msemr_subject"] = new OptionSetValue(935000000); //Patient

                    Guid subjectpatientContactId = Contact.GetContactId(_serviceProxy, "Emily Williams");
                    if (subjectpatientContactId != Guid.Empty)
                    {
                        referralRequest["msemr_subjectpatient"] = new EntityReference("contact", subjectpatientContactId);
                    }

                    referralRequest["msemr_status"] = new OptionSetValue(935000000); //Draft
                    
                    referralRequest["msemr_description"] = "";

                    referralRequest["msemr_referralrequestnumber"] = "Ref452";

                    referralRequest["msemr_groupidentifier"] = "";

                    Guid referralRequestId = _serviceProxy.Create(referralRequest);

                    // Verify that the record has been created.
                    if (referralRequestId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", referralRequestId);
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

        public static Guid GetReferralRequestId(IOrganizationService _service, string name)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("msemr_referralrequest");

                query.ColumnSet = new ColumnSet("msemr_referralrequestid");

                query.Criteria.AddCondition("msemr_referralrequestnumber", ConditionOperator.Equal, name);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("msemr_referralrequestid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["msemr_referralrequestid"]);
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
