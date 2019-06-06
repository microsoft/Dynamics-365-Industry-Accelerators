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
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xrm.Sdk.Client;
using Newtonsoft.Json;
using System.IO;
using System.Net;
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk;

namespace CDM.HealthAccelerator.DataModel
{
    [Serializable]
    public class Location : Profile
    {

        #region Primary Attributes

        private string name;
        private int operationalStatus;
        private int mode;
        private string locationNumber;
        private int addressType;
        private int addressUse;
        private DateTime periodStart;
        private DateTime periodEnd;
        private string organizationId;
        private string locationType;
        private string locationId;

        public int OperationalStatus
        {
            get
            {
                return operationalStatus;
            }

            set
            {
                operationalStatus = value;
            }
        }

        public string LocationNumber
        {
            get
            {
                return locationNumber;
            }

            set
            {
                locationNumber = value;
            }
        }

        public int AddressType
        {
            get
            {
                return addressType;
            }

            set
            {
                addressType = value;
            }
        }

        public int AddressUse
        {
            get
            {
                return addressUse;
            }

            set
            {
                addressUse = value;
            }
        }

        public DateTime PeriodStart
        {
            get
            {
                return periodStart;
            }

            set
            {
                periodStart = value;
            }
        }

        public DateTime PeriodEnd
        {
            get
            {
                return periodEnd;
            }

            set
            {
                periodEnd = value;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }

            set
            {
                name = value;
            }
        }

        public int Mode
        {
            get
            {
                return mode;
            }

            set
            {
                mode = value;
            }
        }

        public string OrganizationId
        {
            get
            {
                return organizationId;
            }

            set
            {
                organizationId = value;
            }
        }

        public string LocationType
        {
            get
            {
                return locationType;
            }

            set
            {
                locationType = value;
            }
        }

        public string LocationId
        {
            get
            {
                return locationId;
            }

            set
            {
                locationId = value;
            }
        }


        #endregion

        public static new List<Profile> GenerateProfilesByCount(int profiles, object configuration)
        {
            try
            {
                string accountsFile = (string)configuration;

                SampleDataCache.InitializeDataCache();
                SampleDataCache.GenerateRandomDateTime rdt = new SampleDataCache.GenerateRandomDateTime(2010, 1, 1, DateTime.Today);

                List<Profile> listAccounts = new List<Profile>();
                List<Organization> listOrganizations;

                for (int i = 0; i < profiles; i++)
                {
                    Location a = new Location();

                    SampleDataCache.AddressInfo addressInfo = SampleDataCache.AddressInfos[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.AddressInfos.Count - 1)];

                    a.Name = SampleDataCache.Accounts[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.Accounts.Count - 1)];

                    a.Address1City = addressInfo.City;
                    a.Address1Country = addressInfo.Country;
                    a.Address1Line1 = addressInfo.Address;
                    a.Address1PostalCode = addressInfo.Zipcode;
                    a.Address1StateOrProvince = addressInfo.State;

                    a.PeriodStart = rdt.Next(); 
                    a.PeriodEnd = rdt.AddYears(a.PeriodStart, 5, 10); 

                    a.AddressType = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.Location_Addresstype>();
                    a.AddressUse = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.Location_Addressuse>();

                    a.LocationNumber = GenerateRandomNumber(10);

                    a.Mode = (int)HealthCDMEnums.Location_Mode.instance;
                    //a.OrganizationId = (string)configuration;

                    a.OperationalStatus = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.Location_Operationalstatus>();

                    //a.LocationType = SampleDataCache.Accounts[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.Accounts.Count - 1)];
                    a.LocationType = SampleDataCache.CodeableConcepts[HealthCDMEnums.CodeableConcept_Type.LocationType.ToString()]
                    .Values.ElementAt(SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.CodeableConcepts[HealthCDMEnums.CodeableConcept_Type.LocationType.ToString()]
                    .Values.Count - 1)).Key;

                    if (!string.IsNullOrEmpty(accountsFile))
                    {
                        listOrganizations = Organization.ImportProfiles(accountsFile);

                        if (listOrganizations != null)
                        {
                            Organization account = listOrganizations[SampleDataCache.SelectRandomItem.Next(0, listOrganizations.Count - 1)];

                            a.OrganizationId = account.OrganizationId;

                        }
                    }

                    listAccounts.Add(a);

                }

                return listAccounts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
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

        public static List<Location> ImportProfiles(string inputfile)
        {
            try
            {
                string profilesFile = File.ReadAllText(inputfile);

                List<Location> profiles = JsonConvert.DeserializeObject<List<Location>>(profilesFile); ;

                return profiles;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error " + ex.ToString());
                return null;
            }
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid locationId = Guid.Empty;

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

                    locationId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return locationId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid locationId = Guid.Empty;

            HealthCDM.msemr_location location = new HealthCDM.msemr_location();

            location.msemr_name = Name;
            location.msemr_Mode = new OptionSetValue(Mode);
            location.msemr_OperationalStatus = new OptionSetValue(OperationalStatus);
            location.msemr_LocationNumber = LocationNumber;
            location.msemr_AddressCity = Address1City;
            location.msemr_AddressUse = new OptionSetValue(AddressUse);
            location.msemr_AddressType = new OptionSetValue(addressType);
            location.msemr_AddressState = Address1StateOrProvince;
            location.msemr_AddressPostalCode = Address1PostalCode;
            location.msemr_Type = new EntityReference(HealthCDM.msemr_codeableconcept.EntityLogicalName, GetCodeableConceptId(_serviceProxy, LocationType, (int)HealthCDMEnums.CodeableConcept_Type.LocationType));
            location.msemr_ManagingOrganization = new EntityReference(HealthCDM.Account.EntityLogicalName, new Guid(OrganizationId));

            try
            {
                locationId = _serviceProxy.Create(location);

                if (locationId != Guid.Empty)
                {
                    LocationId = locationId.ToString();
                    Console.WriteLine("Created Location [" + locationId + "]");
                }
                else
                {
                    throw new Exception("LocationId == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }


            return locationId;
        }
    }
}
