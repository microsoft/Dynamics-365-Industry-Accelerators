using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Dynamics.Health.Samples
{
    public class ActivityTask
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
        /// Create a task. 
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

                    Entity task = new Entity("task");
                    
                    task["subject"] = "Surgery";

                    Guid businessstatusCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "General Partnership", 935000149);
                    if (businessstatusCodeableConceptId != Guid.Empty)
                    {
                        task["msemr_businessstatus"] = new EntityReference("msemr_codeableconcept", businessstatusCodeableConceptId);
                    }

                    Guid codeCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Code", 935000150);
                    if (codeCodeableConceptId != Guid.Empty)
                    {
                        task["msemr_code"] = new EntityReference("msemr_codeableconcept", codeCodeableConceptId);
                    }

                    //Setting Context Type as Encounter
                    task["msemr_contexttype"] = new OptionSetValue(935000000); //Encounter
                    Guid contextencounterId = Encounter.GetEncounterId(_serviceProxy, "E23556");
                    if (contextencounterId != Guid.Empty)
                    {
                        task["msemr_contextencounter"] = new EntityReference("msemr_encounter", contextencounterId);
                    }

                    task["msemr_definitionuri"] = "";

                    task["msemr_descriptionfocus"] = "";

                    task["msemr_descriptionfor"] = "";

                    task["msemr_groupidentifier"] = "";

                    task["msemr_intent"] = new OptionSetValue(935000000); //Proposal

                    //Setting performer owner type as practitioner
                    task["msemr_performerownertype"] = new OptionSetValue(935000000); //Practitioner
                    Guid performerownerpractitionerContacttId = Contact.GetContactId(_serviceProxy, "James Kirk");
                    if (performerownerpractitionerContacttId != Guid.Empty)
                    {
                        task["msemr_performerownerpractitioner"] = new EntityReference("contact", performerownerpractitionerContacttId);
                    }

                    Guid reasonCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Reason", 935000154);
                    if (reasonCodeableConceptId != Guid.Empty)
                    {
                        task["msemr_reason"] = new EntityReference("msemr_codeableconcept", reasonCodeableConceptId);
                    }

                    Guid referenceId = ActivityDefinition.GetActivityDefinitionId(_serviceProxy, "Activity");
                    if (referenceId != Guid.Empty)
                    {
                        task["msemr_reference"] = new EntityReference("msemr_identifiesspecifictimeswhentheeventoccu", referenceId);
                    }

                    //Setting requestor agent as patient
                    task["msemr_requesteragent"] = new OptionSetValue(935000002); //Patient
                    Guid requesteragentpatientContactId = Contact.GetContactId(_serviceProxy, "James Kirk");
                    if (requesteragentpatientContactId != Guid.Empty)
                    {
                        task["msemr_requesteragentpatient"] = new EntityReference("contact", requesteragentpatientContactId);
                    }

                    Guid requesteronbehalfofAccountId = Organization.GetAccountId(_serviceProxy, "Galaxy Corp");
                    if (requesteronbehalfofAccountId != Guid.Empty)
                    {
                        task["msemr_requesteronbehalfof"] = new EntityReference("msemr_codeableconcept", requesteronbehalfofAccountId);
                    }
                    
                    task["msemr_restrictionperiodstartdate"] = DateTime.Now;
                    task["msemr_restrictionperiodenddate"] = DateTime.Now;
                    task["msemr_restrictionrepetitions"] = 152;

                    task["msemr_status"] = new OptionSetValue(935000000); //Draft

                    Guid statusreasonCodeableConceptId = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Status Reason", 935000155);
                    if (statusreasonCodeableConceptId != Guid.Empty)
                    {
                        task["msemr_statusreason"] = new EntityReference("msemr_codeableconcept", statusreasonCodeableConceptId);
                    }

                    task["msemr_taskpriority"] = new OptionSetValue(935000002); //ASAP
                                    
                    task["msemr_tasknumber"] = "T85746";

                    Guid taskId = _serviceProxy.Create(task);

                    // Verify that the record has been created.
                    if (taskId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", taskId);
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
