using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Dynamics.Health.Samples
{
    public class Observation
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
        /// Create an observation. 
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

                    Entity observation = new Entity("msemr_observation");

                    observation["msemr_identifier"] = "OBS-123";
                    observation["msemr_comment"] = "following Schedule";
                    observation["msemr_description"] = "Recovery is good";
                    observation["msemr_status"] = new OptionSetValue(935000000);

                    //Setting Context Type as Encounter
                    observation["msemr_contexttype"] = new OptionSetValue(935000001); //Encounter
                    Guid EncounterId = Encounter.GetEncounterId(_serviceProxy, "E23556");
                    if (EncounterId != Guid.Empty)
                    {
                        observation["msemr_conexttypeencounter"] = new EntityReference("msemr_encounter", EncounterId);
                    }

                    //Setting Device Type as Device Metric
                    observation["msemr_devicetype"] = new OptionSetValue(935000001);
                    Guid DeviceMetricId = DeviceMetric.GetDeviceMetricId(_serviceProxy, "Device Metric");
                    if (DeviceMetricId != Guid.Empty)
                    {
                        observation["msemr_devicetypedevicemetric"] = new EntityReference("msemr_device", DeviceMetricId);
                    }

                    //Setting Effictive as DateTime
                    observation["msemr_effectivetype"] = new OptionSetValue(935000000); //DateTime
                    observation["msemr_effectivetypedatetime"] = DateTime.Now;

                    //Setting Subject Type as Device
                    observation["msemr_subjecttype"] = new OptionSetValue(935000002);//Device
                    Guid SubjectTypeDevice = Device.GetDeviceId(_serviceProxy, "Pacemaker");
                    if (SubjectTypeDevice != Guid.Empty)
                    {
                        observation["msemr_subjecttypedevice"] = new EntityReference("msemr_device", SubjectTypeDevice);
                    }


                    //Setting Value Type as Range
                    observation["msemr_valuetype"] = new OptionSetValue(935000004); //Range
                    observation["msemr_valuerangehighlimit"] = 10;
                    observation["msemr_valuerangelowlimit"] = 1;


                    Guid BodySite = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Body Site", 100000000);
                    if (BodySite != Guid.Empty)
                    {
                        observation["msemr_bodysite"] = new EntityReference("msemr_codeableconcept", BodySite);
                    }
                    Guid Code = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Observation Name", 935000073);
                    if (Code != Guid.Empty)
                    {
                        observation["msemr_code"] = new EntityReference("msemr_codeableconcept", Code);
                    }

                    Guid EpisodeOfCareId = EpisodeOfCare.GetEpisodeOfCareId(_serviceProxy, "EPC-153");
                    if (EpisodeOfCareId != Guid.Empty)
                    {
                        observation["msemr_episodeofcare"] = new EntityReference("msemr_episodeofcare", EpisodeOfCareId);
                    }
                    Guid DataAbsentReason = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Absent Reason", 935000090);
                    if (DataAbsentReason != Guid.Empty)
                    {
                        observation["msemr_dataabsentreason"] = new EntityReference("msemr_codeableconcept", DataAbsentReason);
                    }
                    Guid DeviceId = Device.GetDeviceId(_serviceProxy, "MAGNETOM Sola");
                    if (DeviceId != Guid.Empty)
                    {
                        observation["msemr_device"] = new EntityReference("msemr_device", DeviceId);
                    }
                    Guid Interpretation = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Interpretation Code", 935000085);
                    if (Interpretation != Guid.Empty)
                    {
                        observation["msemr_interpretation"] = new EntityReference("msemr_codeableconcept", Interpretation);
                    }
                    observation["msemr_issueddate"] = DateTime.Now;
                    Guid Method = CodeableConcept.GetCodeableConceptId(_serviceProxy, "Interpretation Code", 935000086);
                    if (Method != Guid.Empty)
                    {
                        observation["msemr_method"] = new EntityReference("msemr_codeableconcept", Method);
                    }
                    observation["msemr_observationnumber"] = "OBS-123";
                    Guid SpecimenId = Specimen.GetSpecimenId(_serviceProxy, "SP-1234");
                    if (SpecimenId != Guid.Empty)
                    {
                        observation["msemr_specimen"] = new EntityReference("msemr_specimen", SpecimenId);
                    }


                    Guid ObservationId = _serviceProxy.Create(observation);

                    // Verify that the record has been created.
                    if (ObservationId != Guid.Empty)
                    {
                        Console.WriteLine("Succesfully created {0}.", ObservationId);
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
