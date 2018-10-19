// =====================================================================
//  This file is part of the Microsoft Dynamics Accelerator code samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
// =====================================================================

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.ServiceModel.Description;
using System.Net;
using Newtonsoft.Json;
namespace CDM.HealthAccelerator.DataModel//

{

    /// <summary>
    /// Holder
    /// </summary>
    [Serializable]
    public class Patient : Profile
    {
        #region Patient Specific Attributes

        //private MedicationPriceList priceList = new MedicationPriceList();

        //private MedicineUoM uom = new MedicineUoM();

        private List<Medication> products = new List<Medication>();

        private List<PatientTask> tasks = new List<PatientTask>();

        private List<Specimen> specimens = new List<Specimen>();

        private List<Location> locations = new List<Location>();

        private List<Observation> observations = new List<Observation>();

        private List<CareTeamParticipant> careTeamParticipants = new List<CareTeamParticipant>();

        private List<RiskAssessment> riskAssessments = new List<RiskAssessment>();

        private List<MedicationAdministration> medicationAdminitrations = new List<MedicationAdministration>();

        private List<CareTeam> careteams = new List<CareTeam>();

        private List<CarePlan> careplans = new List<CarePlan>();

        private List<Appointment> appointments = new List<Appointment>();

        private List<Encounter> encounters = new List<Encounter>();

        private List<MedicationRequest> medicationRequests = new List<MedicationRequest>();

        private List<ReferralRequest> referralRequests = new List<ReferralRequest>();

        private List<Procedure> procedures = new List<Procedure>();

        private List<Device> devices = new List<Device>();

        private List<Condition> conditions = new List<Condition>();

        private List<NutritionOrder> nutritionOrders = new List<NutritionOrder>();

        private List<AllergyIntolerance> allergyIntolerances = new List<AllergyIntolerance>();

        private List<EpisodeOfCare> episodesOfCare = new List<EpisodeOfCare>();

        private PatientConfiguration patientConfiguration;

        private string primaryPractitionerId;

        private string emergencyContactId;

        private string primaryContactId;

        private string patientMedicalNumber;

        private string emergencyContactRelationshipTypeId;

        private string primaryContactRelationshipTypeId;

        public string PrimaryPractitionerId
        {
            get
            {
                return primaryPractitionerId;
            }

            set
            {
                primaryPractitionerId = value;
            }
        }

        public string EmergencyContactId
        {
            get
            {
                return emergencyContactId;
            }

            set
            {
                emergencyContactId = value;
            }
        }

        public string PrimaryContactId
        {
            get
            {
                return primaryContactId;
            }

            set
            {
                primaryContactId = value;
            }
        }

        public string PatientMedicalNumber
        {
            get
            {
                return patientMedicalNumber;
            }

            set
            {
                patientMedicalNumber = value;
            }
        }

        public string EmergencyContactRelationshipTypeId
        {
            get
            {
                return emergencyContactRelationshipTypeId;
            }

            set
            {
                emergencyContactRelationshipTypeId = value;
            }
        }

        public string PrimaryContactRelationshipTypeId
        {
            get
            {
                return primaryContactRelationshipTypeId;
            }

            set
            {
                primaryContactRelationshipTypeId = value;
            }
        }

        public PatientConfiguration PatientConfiguration
        {
            get
            {
                return patientConfiguration;
            }

            set
            {
                patientConfiguration = value;
            }
        }

        public List<AllergyIntolerance> AllergyIntolerances
        {
            get
            {
                return allergyIntolerances;
            }

            set
            {
                allergyIntolerances = value;
            }
        }

        public List<NutritionOrder> NutritionOrders
        {
            get
            {
                return nutritionOrders;
            }

            set
            {
                nutritionOrders = value;
            }
        }

        public List<Condition> Conditions
        {
            get
            {
                return conditions;
            }

            set
            {
                conditions = value;
            }
        }

        public List<Device> Devices
        {
            get
            {
                return devices;
            }

            set
            {
                devices = value;
            }
        }

        public List<Procedure> Procedures
        {
            get
            {
                return procedures;
            }

            set
            {
                procedures = value;
            }
        }

        public List<ReferralRequest> ReferralRequests
        {
            get
            {
                return referralRequests;
            }

            set
            {
                referralRequests = value;
            }
        }

        public List<MedicationRequest> MedicationRequests
        {
            get
            {
                return medicationRequests;
            }

            set
            {
                medicationRequests = value;
            }
        }

        public List<EpisodeOfCare> EpisodesOfCare
        {
            get
            {
                return episodesOfCare;
            }

            set
            {
                episodesOfCare = value;
            }
        }

        public List<Encounter> Encounters
        {
            get
            {
                return encounters;
            }

            set
            {
                encounters = value;
            }
        }

        public List<Appointment> Appointments
        {
            get
            {
                return appointments;
            }

            set
            {
                appointments = value;
            }
        }

        public List<CarePlan> Careplans
        {
            get
            {
                return careplans;
            }

            set
            {
                careplans = value;
            }
        }

        public List<CareTeam> Careteams
        {
            get
            {
                return careteams;
            }

            set
            {
                careteams = value;
            }
        }

        public List<RiskAssessment> RiskAssessments
        {
            get
            {
                return riskAssessments;
            }

            set
            {
                riskAssessments = value;
            }
        }

        public List<CareTeamParticipant> CareTeamParticipants
        {
            get
            {
                return careTeamParticipants;
            }

            set
            {
                careTeamParticipants = value;
            }
        }

        public List<Observation> Observations
        {
            get
            {
                return observations;
            }

            set
            {
                observations = value;
            }
        }

        public List<Location> Locations
        {
            get
            {
                return locations;
            }

            set
            {
                locations = value;
            }
        }

        public List<Specimen> Specimens
        {
            get
            {
                return specimens;
            }

            set
            {
                specimens = value;
            }
        }

        public List<PatientTask> Tasks
        {
            get
            {
                return tasks;
            }

            set
            {
                tasks = value;
            }
        }

        //public MedicineUoM Uom
        //{
        //    get
        //    {
        //        return uom;
        //    }

        //    set
        //    {
        //        uom = value;
        //    }
        //}

        public List<Medication> Products
        {
            get
            {
                return products;
            }

            set
            {
                products = value;
            }
        }

        //public MedicationPriceList PriceList
        //{
        //    get
        //    {
        //        return priceList;
        //    }

        //    set
        //    {
        //        priceList = value;
        //    }
        //}

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Contact"/> class. 
        /// d04787ba-613b-48f5-9d50-652b04073718
        /// </summary>
        public Patient()
        {
            this.InitializeEntity();
        }

        public static void ExportToJson(string filename, List<Profile> profiles)
        {
            try
            {
                string profileData = JsonConvert.SerializeObject(profiles);

                File.WriteAllText(filename, profileData);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// import already exported to file profiles
        /// </summary>
        /// <param name="inputfile">the file that has the profiles</param>
        /// <param name="type">for format tab / csv</param>
        /// <returns>the list of interactions</returns>
        public static List<Patient> ImportProfiles(string inputfile)
        {
            try
            {
                string profilesFile = File.ReadAllText(inputfile);

                List<Patient> profiles = JsonConvert.DeserializeObject<List<Patient>>(profilesFile); ;

                return profiles;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error " + ex.ToString());
                return null;
            }
        }

        /// <summary>
        /// Generation interactions based on a #
        /// </summary>
        /// <param name="profiles">the number of profiles to create</param>
        /// <returns>Give back a list of the speicfic type of interactions generated</returns>
        public static new List<Profile> GenerateProfilesByCount(int profiles, object configuration)
        {
            try
            {
                SampleDataCache.InitializeDataCache();

                List<Profile> listPatients = new List<Profile>();
                List<RelatedPerson> listRelatedPersons;
                List<Practitioner> listPractitioners;
                List<Organization> listOrganizations = null;
               
                //SampleDataCache.RandomDateTime birthDayrdt = new SampleDataCache.RandomDateTime(1955, 1, 1, new DateTime(2000, 1, 1));

                for (int i = 0; i < profiles; i++)
                {
                    ////generate our fake data
                    Patient a = new Patient();

                    a.PatientConfiguration = ((PatientConfiguration)(configuration));

                    int maleorfemale = SampleDataCache.SelectRandomItem.Next(1, 100);

                    a.FirstName = maleorfemale < 50 ? SampleDataCache.Malenames[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.Malenames.Count - 1)] : SampleDataCache.Femalenames[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.Femalenames.Count - 1)];
                    a.LastName = SampleDataCache.Lastnames[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.Lastnames.Count - 1)];

                    SampleDataCache.AddressInfo addressInfo = SampleDataCache.AddressInfos[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.AddressInfos.Count - 1)];

                    // Now set the Emergency and Primary Contact etc
                    string practitionersfile = a.PatientConfiguration.PractionerFileName;
                    string relatedpersonsfile = a.PatientConfiguration.RelatedPersonsFileName;
                    string locationsfile = a.patientConfiguration.LocationsFileName;
                    string organizationsfile = a.patientConfiguration.AccountsFileName;

                    if (!string.IsNullOrEmpty(locationsfile))
                    {
                        // this will get used in multiple places lower on
                        a.Locations = Location.ImportProfiles(locationsfile);
                    }

                    if (!string.IsNullOrEmpty(organizationsfile))
                    {
                        // this will get used in multiple places lower on
                        listOrganizations = Organization.ImportProfiles(organizationsfile);
                    }

                    if (!string.IsNullOrEmpty(relatedpersonsfile))
                    {
                        listRelatedPersons = RelatedPerson.ImportProfiles(relatedpersonsfile);

                        if (listRelatedPersons != null)
                        {
                            Profile emergencyContact = listRelatedPersons[SampleDataCache.SelectRandomItem.Next(0, listRelatedPersons.Count - 1)];
                            Profile primaryContact = listRelatedPersons[SampleDataCache.SelectRandomItem.Next(0, listRelatedPersons.Count - 1)];

                            a.EmergencyContactId = emergencyContact.ContactId;
                            a.PrimaryContactId = primaryContact.ContactId;

                            a.EmergencyContactRelationshipTypeId = SampleDataCache.RelatedPersonTypes[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.RelatedPersonTypes.Count - 1)];
                            a.PrimaryContactRelationshipTypeId = SampleDataCache.RelatedPersonTypes[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.RelatedPersonTypes.Count - 1)];
                        }
                    }

                    if (!string.IsNullOrEmpty(practitionersfile))
                    {
                        listPractitioners = Practitioner.ImportProfiles(practitionersfile);

                        if (listPractitioners != null)
                        {
                            Profile primaryPractitioner = listPractitioners[SampleDataCache.SelectRandomItem.Next(0, listPractitioners.Count - 1)];

                            a.PrimaryPractitionerId = primaryPractitioner.ContactId;
                        }
                    }

                    if (a.PatientConfiguration.AllergyIntoleranceCount > 0)
                    {
                        for (int iAllergies = 0; iAllergies < a.PatientConfiguration.AllergyIntoleranceCount; iAllergies++)
                        {
                            AllergyIntolerance ai = new AllergyIntolerance();
                            a.AllergyIntolerances.Add(ai);
                        }
                    }

                    if (a.PatientConfiguration.NutritionOrderCount > 0)
                    {
                        for (int iNutritionOrders = 0; iNutritionOrders < a.PatientConfiguration.NutritionOrderCount; iNutritionOrders++)
                        {
                            NutritionOrder no = new NutritionOrder();
                            a.NutritionOrders.Add(no);
                        }
                    }


                    if (a.PatientConfiguration.ConditionCount > 0)
                    {
                        for (int iConditions = 0; iConditions < a.PatientConfiguration.ConditionCount; iConditions++)
                        {
                            Condition condition = new Condition();

                            condition.VerificationStatus = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.Condition_Verificationstatus>();
                            a.Conditions.Add(condition);
                        }
                    }

                    if (a.PatientConfiguration.DeviceCount > 0)
                    {
                        for (int iDevice = 0; iDevice < a.PatientConfiguration.DeviceCount; iDevice++)
                        {
                            Device device = new Device();

                            if (a.Locations != null)
                            {
                                Location location = a.Locations[SampleDataCache.SelectRandomItem.Next(0, a.Locations.Count - 1)];

                                device.LocationId = location.LocationId;
                            }

                            a.Devices.Add(device);
                        }
                    }
                
                    if (a.PatientConfiguration.ProcedureCount > 0)
                    {
                        for (int iProcedure = 0; iProcedure < a.PatientConfiguration.ProcedureCount; iProcedure++)
                        {
                            Procedure procedure = new Procedure();

                            if (a.Locations != null)
                            {
                                Location location = a.Locations[SampleDataCache.SelectRandomItem.Next(0, a.Locations.Count - 1)];

                                procedure.LocationId = location.LocationId;
                            }

                            a.Procedures.Add(procedure);
                        }
                    }

                    if (a.PatientConfiguration.ReferralCount > 0)
                    {
                        for (int iReferral = 0; iReferral < a.PatientConfiguration.ReferralCount; iReferral++)
                        {
                            ReferralRequest request = new ReferralRequest();
                            a.ReferralRequests.Add(request);
                        }
                    }

                    // COMPLETE THIS ONE THE ENCOUNTERS ARE DONE
                    if (a.PatientConfiguration.MedicationCount > 0)
                    {
                        for (int iMedication = 0; iMedication < a.PatientConfiguration.MedicationCount; iMedication++)
                        {
                            MedicationRequest medication = new MedicationRequest();
                            a.MedicationRequests.Add(medication);
                        }
                    }

                    if (a.PatientConfiguration.EpisodesOfCareCount > 0)
                    {
                        for (int iEpisode = 0; iEpisode < a.PatientConfiguration.EpisodesOfCareCount; iEpisode++)
                        {
                            EpisodeOfCare episode = new EpisodeOfCare();

                            episode.PractitionerId = a.PrimaryPractitionerId;
                            episode.Description = a.FullName + " - Episode of Care";
                            episode.AccountId = listOrganizations[SampleDataCache.SelectRandomItem.Next(0, listOrganizations.Count -1)].OrganizationId;

                            a.EpisodesOfCare.Add(episode);
                        }
                    }

                    if (a.PatientConfiguration.EncountersCount > 0)
                    {
                        for (int iEncounter = 0; iEncounter < a.PatientConfiguration.EncountersCount; iEncounter++)
                        {
                            Encounter encounter = new Encounter();

                            if (a.Locations != null)
                            {
                                Location location = a.Locations[SampleDataCache.SelectRandomItem.Next(0, a.Locations.Count - 1)];

                                encounter.HospitalizationOriginId = location.LocationId;
                                encounter.HospitalizationDestinationId = location.LocationId;
                            }

                            a.Encounters.Add(encounter);
                        }
                    }

                    if (a.PatientConfiguration.AppointmentCount > 0)
                    {
                        for (int iAppointment = 0; iAppointment < a.PatientConfiguration.AppointmentCount; iAppointment++)
                        {
                            Appointment appointment = new Appointment();


                            if (a.Locations != null)
                            {
                                Location location = a.Locations[SampleDataCache.SelectRandomItem.Next(0, a.Locations.Count - 1)];

                                appointment.LocationId = location.LocationId;
                            }

                            appointment.PractitionerId = a.PrimaryPractitionerId;

                            a.Appointments.Add(appointment);
                        }
                    }

                    if (a.PatientConfiguration.CarePlanCount > 0)
                    {
                        CarePlan careplan = new CarePlan();

                        a.Careplans.Add(careplan);
                    }


                    if (a.PatientConfiguration.CareTeamCount > 0)
                    {
                        CareTeam team = new CareTeam();

                        a.Careteams.Add(team);
                    }

                    if (a.PatientConfiguration.RiskAssessmentCount > 0)
                    {
                        for (int iAssessment = 0; iAssessment < a.PatientConfiguration.RiskAssessmentCount; iAssessment++)
                        {
                            RiskAssessment assessment = new RiskAssessment();

                            a.RiskAssessments.Add(assessment);
                        }
                    }

                    if (a.PatientConfiguration.SpecimenCount > 0)
                    {
                        for (int iSpecimen = 0; iSpecimen < a.PatientConfiguration.SpecimenCount; iSpecimen++)
                        {
                            Specimen specimen = new DataModel.Specimen();
                            a.Specimens.Add(specimen);
                        }
                    }

                    if (a.PatientConfiguration.TaskCount > 0)
                    {
                        for (int iTask = 0; iTask < a.PatientConfiguration.TaskCount; iTask++)
                        {
                            PatientTask task = new PatientTask();
                            a.Tasks.Add(task);
                        }
                    }

                    if (a.PatientConfiguration.ProductCount > 0)
                    {
                        for (int iProduct = 0; iProduct < a.PatientConfiguration.ProductCount; iProduct++)
                        {
                            Medication product = new Medication();
                            a.Products.Add(product);
                        }
                    }

                    a.PrimaryLanguageCode = SampleDataCache.CodeableConcepts[HealthCDMEnums.CodeableConcept_Type.Language.ToString()]
                        .Values.ElementAt(SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.CodeableConcepts[HealthCDMEnums.CodeableConcept_Type.Language.ToString()]
                        .Values.Count - 1)).Key;


                    a.Address1City = addressInfo.City;
                    a.Address1Country = addressInfo.Country;
                    a.Address1Line1 = addressInfo.Address;
                    a.Telephone1 = addressInfo.Areacode + "-" + addressInfo.Telephone; //(business)
                    a.MobilePhone = addressInfo.Areacode + "-" + addressInfo.Telephone;
                    a.Telephone2 = addressInfo.Areacode + "-" + addressInfo.Telephone; //(home)
                    a.Address1PostalCode = addressInfo.Zipcode;
                    a.Address1StateOrProvince = addressInfo.State;
                    a.Age = SampleDataCache.SelectRandomItem.Next(18, 100);
                    a.EmailAddress1 = a.FirstName + "_" + a.LastName + "@testlive.com";
                    a.FullName = a.FirstName + " " + a.LastName;
                    a.GenderCode = maleorfemale < 50 ? (int)ContactGenderCode.Male : (int)ContactGenderCode.Female;
                    a.Salutation = maleorfemale < 50 ? "Mr." : "Mrs.";
                    a.CDMContactType = ProfileType.Patient;
                    a.PatientMedicalNumber = GenerateMedicalNumber();
                    a.BirthDate = birthDayRandomGenerator.Next();

                    listPatients.Add(a);
                }

                return listPatients;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid profileId = Guid.Empty;

            try
            {

                #region Get our Login Information

                // setup the variables
                OrganizationServiceProxy _serviceProxy;

                // homeRealmUri will stay null for now
                Uri homeRealmUri = null;

                // setup credentials from whatever is in the app.config
                ClientCredentials credentials;

                // same for organizationuri comes from app.config
                Uri organizationUri;

                // set the organization uri from what was in the app.config
                organizationUri = new Uri(cdsUrl);

                credentials = new ClientCredentials();
                credentials.UserName.UserName = cdsUserName;
                credentials.UserName.Password = cdsPassword;

                #endregion

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

                using (_serviceProxy = new OrganizationServiceProxy(organizationUri, homeRealmUri, credentials, null))
                {
                    // To impersonate set the GUID of CRM user here (which I merely took from CRM itself
                    // would need not to use this caller id in the future (as it will change per instance of CRM)
                    //_serviceProxy.CallerId = new Guid("14D40CB7-81D5-E311-93F5-00155D00330C");
                    _serviceProxy.ServiceConfiguration.CurrentServiceEndpoint.Behaviors.Add(new ProxyTypesBehavior());

                    //enable using proxy types
                    _serviceProxy.EnableProxyTypes();

                    profileId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return profileId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid profileId = Guid.Empty;

            try
            {

                HealthCDM.Contact addContact = new HealthCDM.Contact();

                //Set standard atttributes (this could be done via reflection)
                // but for now this is all we are setting
                addContact.GenderCode = new OptionSetValue(GenderCode);
                addContact.FirstName = FirstName;
                addContact.LastName = LastName;
                addContact.Address1_Line1 = Address1Line1;
                addContact.Address1_City = Address1City;
                addContact.Address1_StateOrProvince = Address1StateOrProvince;
                addContact.Address1_PostalCode = Address1PostalCode;
                addContact.Telephone1 = Telephone1;
                addContact.MobilePhone = MobilePhone;
                addContact.Telephone2 = Telephone2;
                addContact.Address1_Country = Address1Country;
                addContact.EMailAddress1 = FirstName + "." + LastName + "@" + EmailAddressDomain;
                addContact.Address1_Country = Address1Country;
                addContact.Salutation = Salutation;
                addContact.BirthDate = BirthDate;
                // set the primary language
                addContact.msemr_Communication1Language = new EntityReference(HealthCDM.msemr_codeableconcept.EntityLogicalName, GetCodeableConceptId(_serviceProxy, PrimaryLanguageCode, (int)HealthCDMEnums.CodeableConcept_Type.Language));

                addContact.msemr_ContactType = new OptionSetValue((int)HealthCDMEnums.Contact_Contacttype.Patient);

                // Set the Primary Practitioner, Emergency Contact & Primary Contacts
                if (!string.IsNullOrEmpty(PrimaryPractitionerId))
                {
                    addContact.msemr_GeneralPractioner = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse(PrimaryPractitionerId));
                }

                if (!string.IsNullOrEmpty(EmergencyContactId))
                {
                    addContact.msemr_Contact1 = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse(EmergencyContactId));
                    addContact.msemr_Contact1Relationship = new EntityReference(HealthCDM.msemr_codeableconcept.EntityLogicalName, GetCodeableConceptId(_serviceProxy, EmergencyContactRelationshipTypeId, (int)HealthCDMEnums.CodeableConcept_Type.PatientRelationshipType));
                }

                if (!string.IsNullOrEmpty(PrimaryContactId))
                {
                    addContact.msemr_Contact2 = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse(PrimaryContactId));
                    addContact.msemr_Contact2Relationship = new EntityReference(HealthCDM.msemr_codeableconcept.EntityLogicalName, GetCodeableConceptId(_serviceProxy, PrimaryContactRelationshipTypeId, (int)HealthCDMEnums.CodeableConcept_Type.PatientRelationshipType));
                }

                addContact.msemr_MedicalRecordNumber = PatientMedicalNumber;

                try
                {
                    profileId = _serviceProxy.Create(addContact);

                    if (profileId != Guid.Empty)
                    {
                        ContactId = profileId.ToString();

                        #region remap section due to dependencies

                        // Add alergies to patient
                        if (AllergyIntolerances.Count > 0)
                        {
                            foreach (AllergyIntolerance ai in AllergyIntolerances)
                            {
                                ai.PatientId = ContactId;
                                ai.WriteToCDS(_serviceProxy);
                            }
                        }

                        if (Devices.Count > 0)
                        {
                            foreach (Device device in Devices)
                            {
                                Location location = Locations[SampleDataCache.SelectRandomItem.Next(0, Locations.Count - 1)];

                                device.PatientId = ContactId;
                                device.LocationId = location.LocationId;
                                device.WriteToCDS(_serviceProxy);
                            }
                        }


                        if (Appointments.Count > 0)
                        {
                            foreach (Appointment appointment in Appointments)
                            {
                                Location location = Locations[SampleDataCache.SelectRandomItem.Next(0, Locations.Count - 1)];

                                appointment.Description = FullName + " - Appointment";
                                appointment.PatientId = ContactId;
                                appointment.LocationId = location.LocationId;
                                appointment.AppointmentId = appointment.WriteToCDS(_serviceProxy).ToString();
                            }
                        }

                        if (Encounters.Count > 0)
                        {
                            foreach (Encounter encounter in Encounters)
                            {
                                Appointment appointment = Appointments[SampleDataCache.SelectRandomItem.Next(0, Appointments.Count - 1)];

                                encounter.Name = FullName +" - Patient Encounter";
                                encounter.SubjectPatientId = ContactId;
                                encounter.AppointmentEmrId = appointment.AppointmentId;
                                encounter.EncounterId = encounter.WriteToCDS(_serviceProxy).ToString();
                            }
                        }

                        if (ReferralRequests.Count > 0)
                        {
                            foreach (ReferralRequest request in ReferralRequests)
                            {
                                Encounter encounter = Encounters[SampleDataCache.SelectRandomItem.Next(0, Encounters.Count - 1)];
                                Appointment appointment = Appointments[SampleDataCache.SelectRandomItem.Next(0, Appointments.Count - 1)];

                                request.PatientId = ContactId;
                                request.PractitionerId = primaryPractitionerId;
                                request.EncounterId = encounter.EncounterId;
                                request.AppointmentId = appointment.AppointmentId;
                                request.WriteToCDS(_serviceProxy);
                            }
                        }

                        if (NutritionOrders.Count > 0)
                        {
                            foreach (NutritionOrder no in NutritionOrders)
                            {
                                Encounter encounter = Encounters[SampleDataCache.SelectRandomItem.Next(0, Encounters.Count - 1)];

                                no.PatientId = ContactId;
                                no.PractitionerId = PrimaryPractitionerId;
                                no.EncounterId = encounter.EncounterId;
                                no.WriteToCDS(_serviceProxy);
                            }
                        }

                        //if (Products.Count > 0)
                        //{
                        //    // we need the Uomid/groupid first
                        //    Uom.WriteToCDS(_serviceProxy);

                        //    foreach (Medication product in Products)
                        //    {
                        //        product.UomId = uom.UomId;
                        //        product.UnitGroupId = uom.GroupId;
                        //        product.Subject = "Sample Product " + GenerateRandomNumber();
                        //        product.WriteToCDS(_serviceProxy);
                        //    }

                        //    PriceList.Products = products;
                        //    PriceList.GroupId = uom.GroupId;
                        //    PriceList.UomId = uom.UomId;

                        //    PriceList.WriteToCDS(_serviceProxy);
                        //}

                        if (MedicationRequests.Count > 0)
                        {
                            foreach (MedicationRequest request in MedicationRequests)
                            {
                                Encounter encounter = Encounters[SampleDataCache.SelectRandomItem.Next(0, Encounters.Count - 1)];
                                Medication product = Products[SampleDataCache.SelectRandomItem.Next(0, Products.Count - 1)];

                                request.PatientId = ContactId;
                                request.PractitionerId = primaryPractitionerId;
                                request.MedicationTypePreference = string.Empty; // This is the PRODUCT reference which we don't have yet
                                request.EncounterId = encounter.EncounterId;
                                request.MedicationTypePreference = product.MedicationId;
                                request.WriteToCDS(_serviceProxy);

                                MedicationAdministration admin = new DataModel.MedicationAdministration();

                                admin.PatientId = ContactId;
                                admin.EncounterId = encounter.EncounterId;
                                admin.Name = FullName + " - medication  administration";
                                admin.MedicationAdministrationId = admin.WriteToCDS(_serviceProxy).ToString();

                                medicationAdminitrations.Add(admin);
                            }
                        }

                        if (EpisodesOfCare.Count > 0)
                        {
                            foreach (EpisodeOfCare request in EpisodesOfCare)
                            {
                                Encounter encounter = Encounters[SampleDataCache.SelectRandomItem.Next(0, Encounters.Count - 1)];

                                request.PatientId = ContactId;
                                request.PractitionerId = primaryPractitionerId;
                                request.Description = FullName + " - Episode of Care";
                                request.EncounterId = encounter.EncounterId;
                                request.EpisodeOfCareId = request.WriteToCDS(_serviceProxy).ToString();

                                // now we will add in the Observations
                                Device device = Devices[SampleDataCache.SelectRandomItem.Next(0, Devices.Count - 1)];
                                Location location = Locations[SampleDataCache.SelectRandomItem.Next(0, Locations.Count - 1)];

                                Observation observation = new Observation();

                                observation.PatientId = ContactId;
                                observation.Description = FullName + " - Observation";
                                observation.EpisodeOfCareId = request.EpisodeOfCareId;
                                observation.LocationId = location.LocationId;
                                observation.DeviceId = device.DeviceId;

                                observation.WriteToCDS(_serviceProxy);

                                Observations.Add(observation);

                            }
                        }

                        if (Careplans.Count > 0)
                        {
                            foreach (CarePlan plan in Careplans)
                            {
                                Encounter encounter = Encounters[SampleDataCache.SelectRandomItem.Next(0, Encounters.Count - 1)];

                                plan.PatientId = ContactId;
                                plan.EncounterId = encounter.EncounterId;
                                plan.Description = FullName + " - Description";
                                plan.Title = FullName + " - Care Plan;";
                                plan.CarePlanId = plan.WriteToCDS(_serviceProxy).ToString();
                            }
                        }

                        if (Conditions.Count > 0)
                        {
                            foreach (Condition condition in Conditions)
                            {
                                Appointment appointment = Appointments[SampleDataCache.SelectRandomItem.Next(0, Appointments.Count - 1)];
                                CarePlan careplan = Careplans[SampleDataCache.SelectRandomItem.Next(0, Careplans.Count - 1)];

                                condition.PatientId = ContactId;
                                condition.PractitionerId = PrimaryPractitionerId;
                                condition.AsserterId = ContactId;
                                condition.AppointmentId = appointment.AppointmentId;
                                condition.CarePlanId = careplan.CarePlanId;
                                condition.WriteToCDS(_serviceProxy);
                            }
                        }

                        if (RiskAssessments.Count > 0)
                        {
                            foreach (RiskAssessment assessment in RiskAssessments)
                            {
                                Encounter encounter = Encounters[SampleDataCache.SelectRandomItem.Next(0, Encounters.Count - 1)];
                                Condition condition = Conditions[SampleDataCache.SelectRandomItem.Next(0, Conditions.Count - 1)];

                                assessment.PatientId = ContactId;
                                assessment.PractitionerId = PrimaryPractitionerId;
                                assessment.ConditionId = condition.ConditionId;
                                assessment.EncounterId = encounter.EncounterId;
                                assessment.Name = FullName + " - Risk Assessment";
                                assessment.WriteToCDS(_serviceProxy);
                            }
                        }

                        if (Procedures.Count > 0)
                        {
                            foreach (Procedure procedure in Procedures)
                            {
                                Encounter encounter = Encounters[SampleDataCache.SelectRandomItem.Next(0, Encounters.Count - 1)];

                                procedure.EncounterId = encounter.EncounterId;
                                procedure.PatientId = ContactId;
                                procedure.WriteToCDS(_serviceProxy);
                            }
                        }

                        if (Careteams.Count > 0)
                        {
                            foreach (CareTeam team in Careteams)
                            {
                                team.PatientId = ContactId;
                                team.Name = addContact.FullName + " - Care Team;";
                                team.CareTeamId = team.WriteToCDS(_serviceProxy).ToString();

                                // for each care team we will add the participants
                                CareTeamParticipant patient = new CareTeamParticipant();
                                CareTeamParticipant relatedperson = new CareTeamParticipant();
                                CareTeamParticipant practitioner = new CareTeamParticipant();

                                patient.CareTeamId = team.CareTeamId;
                                relatedperson.CareTeamId = team.CareTeamId;
                                practitioner.CareTeamId = team.CareTeamId;

                                patient.PatientId = ContactId;

                                relatedperson.RelatedPersonId = PrimaryContactId;
                                relatedperson.PatientId = ContactId;

                                practitioner.PractitionerId = PrimaryPractitionerId;
                                practitioner.PatientId = ContactId;

                                patient.MemberType = (int)HealthCDMEnums.CareTeamParticipant_Membertype.Patient;
                                relatedperson.MemberType = (int)HealthCDMEnums.CareTeamParticipant_Membertype.RelationPerson;
                                practitioner.MemberType = (int)HealthCDMEnums.CareTeamParticipant_Membertype.Practitioner;

                                patient.WriteToCDS(_serviceProxy);
                                relatedperson.WriteToCDS(_serviceProxy);
                                practitioner.WriteToCDS(_serviceProxy);

                                CareTeamParticipants.Add(patient);
                                CareTeamParticipants.Add(relatedperson);
                                CareTeamParticipants.Add(practitioner);

                                //patient.RoleId = SampleDataCache.CodeableConcepts[HealthCDMEnums.CodeableConcept_Type.ParticipantRole.ToString()]
                                //    .Values.ElementAt(SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.CodeableConcepts[HealthCDMEnums.CodeableConcept_Type.ParticipantRole.ToString()]
                                //    .Values.Count - 1)).Key;

                            }
                        }

                        if (Specimens.Count > 0)
                        {
                            foreach(Specimen specimen in Specimens)
                            {
                                Device device = Devices[SampleDataCache.SelectRandomItem.Next(0, Devices.Count - 1)];

                                specimen.PatientId = ContactId;
                                specimen.PractitionerId = PrimaryPractitionerId;
                                specimen.DeviceId = device.DeviceId;
                                specimen.Name = FullName + " - Specimen";
                                specimen.WriteToCDS(_serviceProxy);
                            }
                        }

                        if (Tasks.Count > 0)
                        {
                            foreach (PatientTask task in Tasks)
                            {
                                Encounter encounter = Encounters[SampleDataCache.SelectRandomItem.Next(0, Encounters.Count - 1)];

                                task.OwnerPatientId = ContactId;
                                task.RequestingPatientId = ContactId;
                                task.Subject = FullName + " - Task";
                                task.OwnerPractitionerId = PrimaryPractitionerId;
                                task.RequestingPractitionerId = PrimaryPractitionerId;
                                task.EncounterId = encounter.EncounterId;
                                task.WriteToCDS(_serviceProxy);
                            }
                        }

                        #endregion
                    }
                    else
                    {
                    throw new Exception("Contact Id == null");
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.ToString());
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return profileId;
        }

        private static string GenerateMedicalNumber()
        {
            string medicalnumber = string.Empty;

            medicalnumber = "MRN" + (SampleDataCache.SelectRandomItem.Next(0, 9)).ToString()
                + (SampleDataCache.SelectRandomItem.Next(0, 9)).ToString()
                + (SampleDataCache.SelectRandomItem.Next(0, 9)).ToString()
                + (SampleDataCache.SelectRandomItem.Next(0, 9)).ToString()
                + "-"
                + (SampleDataCache.SelectRandomItem.Next(0, 9)).ToString()
                + (SampleDataCache.SelectRandomItem.Next(0, 9)).ToString()
                + (SampleDataCache.SelectRandomItem.Next(0, 9)).ToString()
                + (SampleDataCache.SelectRandomItem.Next(0, 9)).ToString();

            return medicalnumber;
        }
    }
}
