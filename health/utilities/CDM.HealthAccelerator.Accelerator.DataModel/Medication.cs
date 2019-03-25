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
using System.ServiceModel.Description;
using Microsoft.Xrm.Sdk;
using System.Net;
using HealthCDM;
using Microsoft.Xrm.Sdk.Query;
using Newtonsoft.Json;
using System.IO;

namespace CDM.HealthAccelerator.DataModel
{
    [Serializable]
    public class Medication : Profile
    {
        #region Attributes

        private string subject;
        private DateTime validFromDateTime;
        private DateTime validToDateTime;
        private decimal decimalPlaces = 2;
        private int productStructure = 1;
        private string productNumber = GenerateRandomNumber();
        private string medicationId;
        private string uomId;
        private string unitGroupId;
        private string name;
        private int medicationCount;

        public string MedicationId
        {
            get
            {
                return medicationId;
            }

            set
            {
                medicationId = value;
            }
        }

        public string UomId
        {
            get
            {
                return uomId;
            }

            set
            {
                uomId = value;
            }
        }

        public string UnitGroupId
        {
            get
            {
                return unitGroupId;
            }

            set
            {
                unitGroupId = value;
            }
        }

        public string Subject
        {
            get
            {
                return subject;
            }

            set
            {
                subject = value;
            }
        }

        public DateTime ValidFromDateTime
        {
            get
            {
                return validFromDateTime;
            }

            set
            {
                validFromDateTime = value;
            }
        }

        public DateTime ValidToDateTime
        {
            get
            {
                return validToDateTime;
            }

            set
            {
                validToDateTime = value;
            }
        }

        public decimal DecimalPlaces
        {
            get
            {
                return decimalPlaces;
            }

            set
            {
                decimalPlaces = value;
            }
        }

        public int ProductStructure
        {
            get
            {
                return productStructure;
            }

            set
            {
                productStructure = value;
            }
        }

        public string ProductNumber
        {
            get
            {
                return productNumber;
            }

            set
            {
                productNumber = value;
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

        public int MedicationCount
        {
            get
            {
                return medicationCount;
            }

            set
            {
                medicationCount = value;
            }
        }

        #endregion

        public Medication()
        {
            InitializeEntity();
        }

        /// <summary>
        /// import already exported to file profiles
        /// </summary>
        /// <param name="inputfile">the file that has the profiles</param>
        /// <param name="type">for format tab / csv</param>
        /// <returns>the list of interactions</returns>
        public static List<Medication> ImportProfiles(string inputfile)
        {
            try
            {
                string profilesFile = File.ReadAllText(inputfile);

                List<Medication> profiles = JsonConvert.DeserializeObject<List<Medication>>(profilesFile);

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
        public static new List<Medication> GenerateProfilesByCount(int profiles, object configuration)
        {
            List<Medication> listProducts = new List<Medication>();

            try
            {
                for (int i = 0; i < profiles; i++)
                {
                    Medication a = new Medication();
                    a.MedicationCount = (int)configuration;
                    listProducts.Add(a);
                }

                return listProducts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public new void InitializeEntity()
        {
            SampleDataCache.InitializeDataCache();
            SampleDataCache.GenerateRandomDateTime rdt = new SampleDataCache.GenerateRandomDateTime(2018, 1, 1, DateTime.Today);

            DateTime startDate = rdt.Next();
            DateTime endDate = rdt.AddYears(startDate, 5, 10);

            ValidFromDateTime = startDate;
            ValidToDateTime = endDate;

            Name = SampleDataCache.Medications[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.Medications.Count - 1)];
            Subject = SampleDataCache.Medications[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.Medications.Count - 1)];
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid productMedicationId = Guid.Empty;

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

                    productMedicationId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return productMedicationId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid productMedicationId = Guid.Empty;

            try
            {
                Product sampleProduct = new Product
                {
                    Name = Name,
                    ProductNumber = "PN" + GenerateRandomNumber(),
                    ProductStructure = new OptionSetValue(1),
                    QuantityDecimal = 2,
                    DefaultUoMScheduleId = new EntityReference(UoMSchedule.EntityLogicalName, Guid.Parse(unitGroupId)),
                    DefaultUoMId = new EntityReference(UoM.EntityLogicalName, Guid.Parse(uomId)),
                    ValidFromDate = ValidFromDateTime,
                    ValidToDate = ValidToDateTime
                };

                productMedicationId = _serviceProxy.Create(sampleProduct);

                if (productMedicationId != Guid.Empty)
                {
                    MedicationId = productMedicationId.ToString();
                    Console.WriteLine("Created Product [" + productMedicationId + "]");
                }
                else
                {
                    throw new Exception("MedicationId == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return productMedicationId;
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

 
    }
}
