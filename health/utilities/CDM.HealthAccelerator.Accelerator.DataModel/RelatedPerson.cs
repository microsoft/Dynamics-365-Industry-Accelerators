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
    using System.Configuration;
    using System.Diagnostics.CodeAnalysis;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Xrm.Sdk;
    using Microsoft.Xrm.Sdk.Client;
    using System.ServiceModel.Description;
    using System.Net;
    using Newtonsoft.Json;

    /// <summary>
    /// Holder
    /// </summary>
    [Serializable]
    public class RelatedPerson : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RelatedPerson"/> class. 
        /// d04787ba-613b-48f5-9d50-652b04073718
        /// </summary>
        public RelatedPerson()
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
        public static List<RelatedPerson> ImportProfiles(string inputfile)
        {
            try
            {
                string profilesFile = File.ReadAllText(inputfile);

                List<RelatedPerson> profiles = JsonConvert.DeserializeObject<List<RelatedPerson>>(profilesFile);

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
                //SampleDataCache.RandomDateTime birthDayrdt = new SampleDataCache.RandomDateTime(1955, 1, 1, new DateTime(2000, 1, 1));

                SampleDataCache.InitializeDataCache();

                List<Profile> listContacts = new List<Profile>();

                for (int i = 0; i < profiles; i++)
                {
                    ////generate our fake data
                    RelatedPerson a = new RelatedPerson();

                    int maleorfemale = SampleDataCache.SelectRandomItem.Next(1, 100);

                    a.FirstName = maleorfemale < 50 ? SampleDataCache.Malenames[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.Malenames.Count - 1)] : SampleDataCache.Femalenames[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.Femalenames.Count - 1)];

                    a.LastName = SampleDataCache.Lastnames[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.Lastnames.Count - 1)];

                    a.PrimaryLanguageCode = SampleDataCache.CodeableConcepts[HealthCDMEnums.CodeableConcept_Type.Language.ToString()]
                        .Values.ElementAt(SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.CodeableConcepts[HealthCDMEnums.CodeableConcept_Type.Language.ToString()]
                        .Values.Count - 1)).Key;

                    SampleDataCache.AddressInfo addressInfo = SampleDataCache.AddressInfos[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.AddressInfos.Count - 1)];

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
                    a.CDMContactType = ProfileType.RelatedPerson;
                    a.BirthDate = birthDayRandomGenerator.Next();

                    listContacts.Add(a);
                }

                return listContacts;
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

                addContact.msemr_ContactType = new OptionSetValue((int)HealthCDMEnums.Contact_Contacttype.RelatedPerson);

                try
                {
                    profileId = _serviceProxy.Create(addContact);

                    if (profileId != Guid.Empty)
                    {
                        ContactId = profileId.ToString();

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
    }
}
