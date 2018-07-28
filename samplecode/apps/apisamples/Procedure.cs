using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Dynamics.Health.Samples
{
   public class Procedure
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
        /// Create a procedure.
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

                    Entity procedure = new Entity("msemr_procedure");

                    procedure["msemr_datetime"] = DateTime.Now;
                    procedure["msemr_performedstartdate"] = DateTime.Now;
                    procedure["msemr_performedenddate"] = DateTime.Now;
                    Guid EpisodeofCare = EpisodeOfCare.GetEpisodeOfCareId(_serviceProxy, "EPC-153");
                    if (EpisodeofCare != Guid.Empty)
                    {
                        procedure["msemr_episodeofcare"] = new EntityReference("msemr_episodeofcare", EpisodeofCare);
                    }
                   
                    Guid Code = Medication.GetProductId(_serviceProxy, "Panadol");
                    if (Code != Guid.Empty)
                    {
                        procedure["msemr_code"] = new EntityReference("product", Code);
                    }

                    Guid LocationId = Location.GetLocationId(_serviceProxy, "Noble Hospital Center");
                    if (LocationId != Guid.Empty)
                    {
                        procedure["msemr_location"] = new EntityReference("msemr_location", LocationId);
                    }
                    Guid NotDoneReason = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Blood pressure was high", 935000109);
                    if (NotDoneReason != Guid.Empty)
                    {
                        procedure["msemr_notdonereason"] = new EntityReference("msemr_codeableconcept", NotDoneReason);
                    }
                    Guid Category = CodeableConcept.GetCodeableConceptId(_serviceProxy, "xyz-123", 935000103);
                    if (Category != Guid.Empty)
                    {
                        procedure["msemr_category"] = new EntityReference("msemr_codeableconcept", NotDoneReason);
                    }
                    Guid EncounterId = Encounter.GetEncounterId(_serviceProxy, "E23556");
                    if (EncounterId != Guid.Empty)
                    {
                        procedure["msemr_encounter"] = new EntityReference("msemr_encounter", EncounterId);
                    }

                    //Setting Subject Type as Patient
                    procedure["msemr_subjecttype"] = new OptionSetValue(935000000);  //Patient
                    Guid Patient = Contact.GetContactId(_serviceProxy, "James Kirk", 935000000);
                    if (Patient != Guid.Empty)
                    {
                        procedure["msemr_patient"] = new EntityReference("contact", Patient);
                    }
                   
                    Guid Outcome = CodeableConcept.GetCodeableConceptId(_serviceProxy, "mno-123", 935000107);
                    if (Outcome != Guid.Empty)
                    {
                        procedure["msemr_outcome"] = new EntityReference("msemr_codeableconcept", Outcome);
                    }
                    procedure["msemr_status"] = new OptionSetValue(935000000);
                    procedure["msemr_procedureidentifier"] = "mdf/xyz";
                    procedure["msemr_notdone"] = true;


                    Guid ProcedureId = _serviceProxy.Create(procedure);

                    // Verify that the record has been created.
                    if (ProcedureId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", ProcedureId);
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
