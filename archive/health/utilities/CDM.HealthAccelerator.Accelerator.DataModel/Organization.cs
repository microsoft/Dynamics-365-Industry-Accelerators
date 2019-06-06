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
using Microsoft.Xrm.Sdk;
using System.ServiceModel.Description;
using System.Net;

namespace CDM.HealthAccelerator.DataModel
{
    [Serializable]
    public class Organization : Profile
    {
        #region Account Specific Attributes
        private string name;

        private string organizationId;

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

        #endregion

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
        public static List<Organization> ImportProfiles(string inputfile)
        {
            try
            {
                string profilesFile = File.ReadAllText(inputfile);

                List<Organization> profiles = JsonConvert.DeserializeObject<List<Organization>>(profilesFile); ;

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

                List<Profile> listAccounts = new List<Profile>();

                for (int i = 0; i < profiles; i++)
                {
                    Organization a = new Organization();

                    SampleDataCache.AddressInfo addressInfo = SampleDataCache.AddressInfos[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.AddressInfos.Count - 1)];

                    a.Name = SampleDataCache.Accounts[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.Accounts.Count - 1)];

                    a.Address1City = addressInfo.City;
                    a.Address1Country = addressInfo.Country;
                    a.Address1Line1 = addressInfo.Address;
                    a.Telephone1 = addressInfo.Areacode + "-" + addressInfo.Telephone; //(business)
                    a.Address1PostalCode = addressInfo.Zipcode;
                    a.Address1StateOrProvince = addressInfo.State;

                    listAccounts.Add(a);
                }

                return listAccounts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid accountId = Guid.Empty;

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

                    accountId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return accountId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid accountId = Guid.Empty;

            HealthCDM.Account account = new HealthCDM.Account();

            account.Name = Name;
            account.Address1_Line1 = Address1Line1;
            account.Address1_City = Address1City;
            account.Address1_StateOrProvince = Address1StateOrProvince;
            account.Address1_PostalCode = Address1PostalCode;
            account.Telephone1 = Telephone1;
            account.Address1_Country = Address1Country;

            try
            {
                accountId = _serviceProxy.Create(account);

                if (accountId != Guid.Empty)
                {
                    OrganizationId = accountId.ToString();
                    Console.WriteLine("Created Account [" + OrganizationId + "]");
                }
                else
                {
                    throw new Exception("OrganizationId (AccountId) == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }


            return accountId;
        }
    }
}
