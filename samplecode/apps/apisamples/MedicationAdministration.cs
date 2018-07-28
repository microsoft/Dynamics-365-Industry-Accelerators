using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Dynamics.Health.Samples
{
    public class MedicationAdministration
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
        /// Create a medication administration.
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

                    Entity medicationadministration = new Entity("msemr_medicationadministration");

                    medicationadministration["msemr_medicationadministrationnumber"] = "1234-abc";
                    medicationadministration["msemr_status"] = new OptionSetValue(935000000);
                    medicationadministration["msemr_dosagetext"] = "--";
                    medicationadministration["msemr_notgiven"] = true;
                    medicationadministration["msemr_dosagedose"] = 1;

                    //Setting Effective Type as DateTime
                    medicationadministration["msemr_effectivetype"] = new OptionSetValue(935000000);//DateTime
                    medicationadministration["msemr_effectivedatetime"] = DateTime.Now;

                    //Setting Subject Type as Patient
                    medicationadministration["msemr_subjecttype"] = new OptionSetValue(935000000);
                    Guid SubjectTypePatient = Contact.GetContactId(_serviceProxy, "James Kirk", 935000000);
                    if (SubjectTypePatient != Guid.Empty)
                    {
                        medicationadministration["msemr_subjecttypepatient"] = new EntityReference("contact", SubjectTypePatient);
                    }

                    //Setting Context Type as Encounter
                    medicationadministration["msemr_contexttype"] = new OptionSetValue(935000000); //Encounter
                    Guid ContextTypeEncounter = Encounter.GetEncounterId(_serviceProxy, "E23556");
                    if (ContextTypeEncounter != Guid.Empty)
                    {
                        medicationadministration["msemr_contexttypeencounter"] = new EntityReference("msemr_encounter", ContextTypeEncounter);
                    }


                    //Setting Medication Type as Reference
                    medicationadministration["msemr_medicationtype"] = new OptionSetValue(935000001);//Refrence
                    Guid MedicationReference = Medication.GetProductId(_serviceProxy, "Panadol");
                    if (MedicationReference != Guid.Empty)
                    {
                        medicationadministration["msemr_medicationreference"] = new EntityReference("product", MedicationReference);
                    }

                    //Setting Dosage Rate Type as  Quantity
                    medicationadministration["msemr_dosageratetype"] = new OptionSetValue(935000001);//Quantity
                    medicationadministration["msemr_dosageratequantity"] = 1;

                    Guid Category = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Pain", 935000080);
                    if (Category != Guid.Empty)
                    {
                        medicationadministration["msemr_category"] = new EntityReference("msemr_codeableconcept", Category);
                    }
                    Guid Prescription = MedicationRequest.getMedicationRequestId(_serviceProxy, "MR-1234");
                    if (Prescription != Guid.Empty)
                    {
                        medicationadministration["msemr_prescription"] = new EntityReference("msemr_medicationrequest", Prescription);
                    }
                    Guid DosageSite = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Route Code", 935000127);
                    if (DosageSite != Guid.Empty)
                    {
                        medicationadministration["msemr_dosagesite"] = new EntityReference("msemr_codeableconcept", DosageSite);
                    }

                    Guid DosageRoute = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Route Code", 935000127);
                    if (DosageRoute != Guid.Empty)
                    {
                        medicationadministration["msemr_dosageroute"] = new EntityReference("msemr_codeableconcept", DosageRoute);
                    }
                    Guid DosageMethod = CodeableConcept.GetCodeableConceptId(_serviceProxy, "678-abc", 935000005);
                    if (DosageMethod != Guid.Empty)
                    {
                        medicationadministration["msemr_dosagemethod"] = new EntityReference("msemr_codeableconcept", DosageMethod);
                    }

                    Guid MedicationAdministrationId = _serviceProxy.Create(medicationadministration);

                    // Verify that the record has been created.
                    if (MedicationAdministrationId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", MedicationAdministrationId);
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
