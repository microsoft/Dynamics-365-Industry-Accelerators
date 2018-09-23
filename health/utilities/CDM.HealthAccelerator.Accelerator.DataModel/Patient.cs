// =====================================================================
//  This file is part of the Microsoft Dynamics Accelerator code samples.
//
//  Copyright (C) Microsoft Corporation.  All rights reserved.
//
//  This source code is intended only as a supplement to Microsoft
//  Development Tools and/or on-line documentation.  See these other
//  materials for detailed information regarding Microsoft code samples.
//
//  THIS CODE AND INFORMATION ARE PROVIDED "AS IS" WITHOUT WARRANTY OF ANY
//  KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE
//  IMPLIED WARRANTIES OF MERCHANTABILITY AND/OR FITNESS FOR A
//  PARTICULAR PURPOSE.
// =====================================================================
namespace CDM.HealthAccelerator.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Client;
    using System.ServiceModel.Description;
    using System.Net;
    using Newtonsoft.Json;


    /// <summary>
    /// Holder
    /// </summary>
    [Serializable]
    public class Patient : Profile
    {
        #region Patient Specific Attributes



        private List<MedicationRequest> medicationRequests = new List<MedicationRequest>();

        private List<ReferralRequest> referralRequests = new List<ReferralRequest>();

        private List<Procedure> procedures = new List<Procedure>();

        private List<Device> devices = new List<Device>();

        private List<Condition> conditions = new List<Condition>();

        private List<NutritionOrder> nutritionOrders = new List<NutritionOrder>();

        private List<AllergyIntolerance> allergyIntolerances = new List<AllergyIntolerance>();

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
                SampleDataCache.InitializeFakeDataHelpers();

                List<Profile> listPatients = new List<Profile>();
                List<RelatedPerson> listRelatedPersons;
                List<Practitioner> listPractitioners;

                //SampleDataCache.RandomDateTime birthDayrdt = new SampleDataCache.RandomDateTime(1955, 1, 1, new DateTime(2000, 1, 1));

                for (int i = 0; i < profiles; i++)
                {
                    ////generate our fake data
                    Patient a = new Patient();

                    a.PatientConfiguration = ((PatientConfiguration)(configuration));

                    int maleorfemale = SampleDataCache.RandomContactGenerator.Next(1, 100);

                    a.FirstName = maleorfemale < 50 ? SampleDataCache.Malenames[SampleDataCache.RandomContactGenerator.Next(0, SampleDataCache.Malenames.Count - 1)] : SampleDataCache.Femalenames[SampleDataCache.RandomContactGenerator.Next(0, SampleDataCache.Femalenames.Count - 1)];
                    a.LastName = SampleDataCache.Lastnames[SampleDataCache.RandomContactGenerator.Next(0, SampleDataCache.Lastnames.Count - 1)];

                    SampleDataCache.AddressInfo addressInfo = SampleDataCache.AddressInfos[SampleDataCache.RandomContactGenerator.Next(0, SampleDataCache.AddressInfos.Count - 1)];

                    // Now set the Emergency and Primary Contact etc
                    string practitionersfile = a.PatientConfiguration.PractionerFileName;
                    string relatedpersonsfile = a.PatientConfiguration.RelatedPersonsFileName;

                    if (!string.IsNullOrEmpty(relatedpersonsfile))
                    {
                        listRelatedPersons = RelatedPerson.ImportProfiles(relatedpersonsfile);

                        if (listRelatedPersons != null)
                        {
                            Profile emergencyContact = listRelatedPersons[SampleDataCache.RandomContactGenerator.Next(0, listRelatedPersons.Count - 1)];
                            Profile primaryContact = listRelatedPersons[SampleDataCache.RandomContactGenerator.Next(0, listRelatedPersons.Count - 1)];

                            a.EmergencyContactId = emergencyContact.ContactId;
                            a.PrimaryContactId = primaryContact.ContactId;

                            a.EmergencyContactRelationshipTypeId = SampleDataCache.RelatedPersonTypes[SampleDataCache.RandomContactGenerator.Next(0, SampleDataCache.RelatedPersonTypes.Count - 1)];
                            a.PrimaryContactRelationshipTypeId = SampleDataCache.RelatedPersonTypes[SampleDataCache.RandomContactGenerator.Next(0, SampleDataCache.RelatedPersonTypes.Count - 1)];
                        }
                    }

                    if (!string.IsNullOrEmpty(practitionersfile))
                    {
                        listPractitioners = Practitioner.ImportProfiles(practitionersfile);

                        if (listPractitioners != null)
                        {
                            Profile primaryPractitioner = listPractitioners[SampleDataCache.RandomContactGenerator.Next(0, listPractitioners.Count - 1)];

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
                            a.Devices.Add(device);
                        }
                    }

                    if (a.PatientConfiguration.ProcedureCount > 0)
                    {
                        for (int iProcedure = 0; iProcedure < a.PatientConfiguration.ProcedureCount; iProcedure++)
                        {
                            Procedure procedure = new Procedure();
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

                    if (a.PatientConfiguration.MedicationCount > 0)
                    {
                        for (int iMedication = 0; iMedication < a.PatientConfiguration.MedicationCount; iMedication++)
                        {
                            MedicationRequest medication = new MedicationRequest();
                            a.MedicationRequests.Add(medication);
                        }
                    }

                    a.PrimaryLanguageCode = SampleDataCache.Languages[SampleDataCache.RandomContactGenerator.Next(0, SampleDataCache.Languages.Count - 1)];


                    a.Address1City = addressInfo.City;
                    a.Address1Country = addressInfo.Country;
                    a.Address1Line1 = addressInfo.Address;
                    a.Telephone1 = addressInfo.Areacode + "-" + addressInfo.Telephone; //(business)
                    a.MobilePhone = addressInfo.Areacode + "-" + addressInfo.Telephone;
                    a.Telephone2 = addressInfo.Areacode + "-" + addressInfo.Telephone; //(home)
                    a.Address1PostalCode = addressInfo.Zipcode;
                    a.Address1StateOrProvince = addressInfo.State;
                    a.Age = SampleDataCache.RandomContactGenerator.Next(18, 100);
                    a.EmailAddress1 = a.FirstName + "_" + a.LastName + "@testlive.com";
                    a.FullName = a.FirstName + " " + a.LastName;
                    a.GenderCode = maleorfemale < 50 ? (int)ContactGenderCode.Male : (int)ContactGenderCode.Female;
                    a.Salutation = maleorfemale < 50 ? "Mr." : "Mrs.";
                    a.CDMContactType = ContactType.Patient;
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

                            // Add alergies to patient
                            if (AllergyIntolerances.Count > 0)
                            {
                                foreach (AllergyIntolerance ai in AllergyIntolerances)
                                {
                                    ai.PatientId = ContactId;
                                    ai.WriteToCDS(_serviceProxy);
                                }
                            }

                            if (NutritionOrders.Count > 0)
                            {
                                foreach(NutritionOrder no in NutritionOrders)
                                {
                                    no.Patient = ContactId;
                                    no.Practitioner = PrimaryPractitionerId;
                                    no.WriteToCDS(_serviceProxy);
                                }
                            }

                            if (Conditions.Count > 0)
                            {
                                foreach (Condition condition in Conditions)
                                {
                                    condition.PatientId = ContactId;
                                    condition.PractitionerId = PrimaryPractitionerId;
                                    condition.WriteToCDS(_serviceProxy);
                                }
                            }

                            if (Devices.Count > 0)
                            {
                                foreach (Device device in Devices)
                                {
                                    device.PatientId = ContactId;
                                    device.WriteToCDS(_serviceProxy);
                                }
                            }

                            if (Procedures.Count > 0)
                            {
                                foreach (Procedure procedure in Procedures)
                                {
                                    procedure.PatientId = ContactId;
                                    procedure.WriteToCDS(_serviceProxy);
                                }
                            }

                            if (ReferralRequests.Count > 0)
                            {
                                foreach (ReferralRequest request in ReferralRequests)
                                {
                                    request.PatientId = ContactId;
                                    request.PractitionerId = primaryPractitionerId;
                                    request.WriteToCDS(_serviceProxy);
                                }
                            }

                            if (MedicationRequests.Count > 0)
                            {
                                foreach (MedicationRequest request in MedicationRequests)
                                {
                                    request.PatientId = ContactId;
                                    request.PractitionerId = primaryPractitionerId;
                                    request.WriteToCDS(_serviceProxy);
                                }
                            }
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

                        // Add alergies to patient
                        if (AllergyIntolerances.Count > 0)
                        {
                            foreach (AllergyIntolerance ai in AllergyIntolerances)
                            {
                                ai.PatientId = ContactId;
                                ai.WriteToCDS(_serviceProxy);
                            }
                        }

                        if (NutritionOrders.Count > 0)
                        {
                            foreach (NutritionOrder no in NutritionOrders)
                            {
                                no.Patient = ContactId;
                                no.Practitioner = PrimaryPractitionerId;
                                no.WriteToCDS(_serviceProxy);
                            }
                        }

                        if (Conditions.Count > 0)
                        {
                            foreach (Condition condition in Conditions)
                            {
                                condition.PatientId = ContactId;
                                condition.PractitionerId = PrimaryPractitionerId;
                                condition.WriteToCDS(_serviceProxy);
                            }
                        }

                        if (Devices.Count > 0)
                        {
                            foreach (Device device in Devices)
                            {
                                device.PatientId = ContactId;
                                device.WriteToCDS(_serviceProxy);
                            }
                        }

                        if (Procedures.Count > 0)
                        {
                            foreach (Procedure procedure in Procedures)
                            {
                                procedure.PatientId = ContactId;
                                procedure.WriteToCDS(_serviceProxy);
                            }
                        }

                        if (ReferralRequests.Count > 0)
                        {
                            foreach (ReferralRequest request in ReferralRequests)
                            {
                                request.PatientId = ContactId;
                                request.PractitionerId = primaryPractitionerId;
                                request.WriteToCDS(_serviceProxy);
                            }
                        }

                        if (MedicationRequests.Count > 0)
                        {
                            foreach (MedicationRequest request in MedicationRequests)
                            {
                                request.PatientId = ContactId;
                                request.PractitionerId = primaryPractitionerId;
                                request.WriteToCDS(_serviceProxy);
                            }
                        }
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

            medicalnumber = "MRN" + (SampleDataCache.RandomContactGenerator.Next(0, 9)).ToString()
                + (SampleDataCache.RandomContactGenerator.Next(0, 9)).ToString()
                + (SampleDataCache.RandomContactGenerator.Next(0, 9)).ToString()
                + (SampleDataCache.RandomContactGenerator.Next(0, 9)).ToString()
                + "-"
                + (SampleDataCache.RandomContactGenerator.Next(0, 9)).ToString()
                + (SampleDataCache.RandomContactGenerator.Next(0, 9)).ToString()
                + (SampleDataCache.RandomContactGenerator.Next(0, 9)).ToString()
                + (SampleDataCache.RandomContactGenerator.Next(0, 9)).ToString();

            return medicalnumber;
        }
    }
}
