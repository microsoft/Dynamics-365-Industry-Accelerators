using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Dynamics.Health.Samples
{
    public class RiskAssessment
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
        /// Create a risk assessment. 
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

                    Entity riskAssessment = new Entity("msemr_riskassessment");
                    
                    riskAssessment["msemr_name"] = "Operational Risk";

                    //Setting context type as encounter
                    riskAssessment["msemr_contexttype"] = new OptionSetValue(935000000); //Encounter
                    Guid encounterId = Encounter.GetEncounterId(_serviceProxy, "E23556");
                    if (encounterId != Guid.Empty)
                    {
                        riskAssessment["msemr_contextencounter"] = new EntityReference("msemr_encounter", encounterId);
                    }

                    //Setting performer type as practitioner
                    riskAssessment["msemr_performertype"] = new OptionSetValue(935000000); //Practitioner
                    Guid performerpractitionerContactId = Contact.GetContactId(_serviceProxy, "James Kirk");
                    if (performerpractitionerContactId != Guid.Empty)
                    {
                        riskAssessment["msemr_performerpractitioner"] = new EntityReference("contact", performerpractitionerContactId);
                    }

                    //Setting reason type as codeable concept
                    riskAssessment["msemr_reasontype"] = new OptionSetValue(935000000); //Codeable concept
                    Guid reasonconceptCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Risk Assessment Reason Code", 935000126);
                    if (reasonconceptCodeableConceptId != Guid.Empty)
                    {
                        riskAssessment["msemr_reasonconcept"] = new EntityReference("msemr_codeableconcept", reasonconceptCodeableConceptId);
                    }

                    //Setting subject type as codeable patient
                    riskAssessment["msemr_subjecttype"] = new OptionSetValue(935000000); //Patient
                    Guid subjectpatientContactId = Contact.GetContactId(_serviceProxy, "James Kirk");
                    if (subjectpatientContactId != Guid.Empty)
                    {
                        riskAssessment["msemr_subjectpatient"] = new EntityReference("contact", subjectpatientContactId);
                    }
                    riskAssessment["msemr_occurrencetype"] = new OptionSetValue(935000000); //Time
                    riskAssessment["msemr_occurrencestartdate"] = DateTime.Now;
                    riskAssessment["msemr_occurrenceenddate"] = DateTime.Now;
                    riskAssessment["msemr_occurrencedatetime"] = DateTime.Now;

                    Guid conditionId = Condition.GetConditionId(_serviceProxy, "Tooth loss");
                    if (conditionId != Guid.Empty)
                    {
                        riskAssessment["msemr_condition"] = new EntityReference("msemr_condition", conditionId);
                    }
                    
                    Guid methodCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Method", 935000124);
                    if (methodCodeableConceptId != Guid.Empty)
                    {
                        riskAssessment["msemr_method"] = new EntityReference("msemr_codeableconcept", methodCodeableConceptId);
                    }
                    
                    Guid codeCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Code", 935000123);
                    if (codeCodeableConceptId != Guid.Empty)
                    {
                        riskAssessment["msemr_code"] = new EntityReference("msemr_codeableconcept", codeCodeableConceptId);
                    }
                    
                    riskAssessment["msemr_basedon"] = "";

                    riskAssessment["msemr_parent"] = "";
                    
                    riskAssessment["msemr_basis"] = "";

                    riskAssessment["msemr_status"] = new OptionSetValue(935000000); //Registered

                    riskAssessment["msemr_comment"] = "";

                    riskAssessment["msemr_mitigation"] = "";

                    riskAssessment["msemr_riskassessmentnumber"] = "RAN865";

                    Guid riskAssessmentId = _serviceProxy.Create(riskAssessment);

                    // Verify that the record has been created.
                    if (riskAssessmentId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", riskAssessmentId);
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
