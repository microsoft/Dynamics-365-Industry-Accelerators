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
using System.Net;
using Microsoft.Xrm.Sdk;

namespace CDM.HealthAccelerator.DataModel
{
    [Serializable]
    public class Device : BaseFunctions
    {
        public Device()
        {
            InitializeEntity();
        }

        public Device(string patientid)
        {
            PatientId = patientid;
            InitializeEntity();
        }

        private string name;
        private string model;
        private string deviceNumber;
        private string version;
        private DateTime expirationDate;
        private string patientId;
        private string deviceId;
        private string manufacturer;
        private string carrierAIDC;
        private string locationId;

        private DateTime manufacturedDate;

        private int deviceStatus;

        public string DeviceId
        {
            get
            {
                return deviceId;
            }

            set
            {
                deviceId = value;
            }
        }

        public string PatientId
        {
            get
            {
                return patientId;
            }

            set
            {
                patientId = value;
            }
        }

        public string Model
        {
            get
            {
                return model;
            }

            set
            {
                model = value;
            }
        }

        public string DeviceNumber
        {
            get
            {
                return deviceNumber;
            }

            set
            {
                deviceNumber = value;
            }
        }

        public string Version
        {
            get
            {
                return version;
            }

            set
            {
                version = value;
            }
        }

        public DateTime ExpirationDate
        {
            get
            {
                return expirationDate;
            }

            set
            {
                expirationDate = value;
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

        public int DeviceStatus
        {
            get
            {
                return deviceStatus;
            }

            set
            {
                deviceStatus = value;
            }
        }

        public string Manufacturer
        {
            get
            {
                return manufacturer;
            }

            set
            {
                manufacturer = value;
            }
        }

        public DateTime ManufacturedDate
        {
            get
            {
                return manufacturedDate;
            }

            set
            {
                manufacturedDate = value;
            }
        }

        public string CarrierAIDC
        {
            get
            {
                return carrierAIDC;
            }

            set
            {
                carrierAIDC = value;
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

        public override void InitializeEntity()
        {
            DeviceNumber = GenerateDeviceNumber();
            Version = GenerateDeviceVersion();
            Model = SampleDataCache.DeviceModels[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.DeviceModels.Count - 1)]; ;
            Manufacturer = SampleDataCache.Manufacturers[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.Manufacturers.Count - 1)]; ;

            SampleDataCache.GenerateRandomDateTime rdt = new SampleDataCache.GenerateRandomDateTime(2017, 1, 1, new DateTime(2022,1,1));

            ExpirationDate = rdt.Next();
            ManufacturedDate = rdt.Next();

            Name = SampleDataCache.Devices[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.Devices.Count - 1)];
            DeviceStatus = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.Device_Devicestatus>();

            CarrierAIDC = GenerateCarrierAIDC();
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid patientDeviceId = Guid.Empty;

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

                    patientDeviceId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientDeviceId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid patientDeviceId = Guid.Empty;

            try
            {

                HealthCDM.msemr_device addPatientDevice = new HealthCDM.msemr_device();

                addPatientDevice.msemr_Patient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse(PatientId));
                addPatientDevice.msemr_DeviceNumber = DeviceNumber;
                addPatientDevice.msemr_Version = Version;
                addPatientDevice.msemr_Model = Model;
                addPatientDevice.msemr_ExpirationDate = ExpirationDate;
                addPatientDevice.msemr_name = Name;
                addPatientDevice.msemr_DeviceStatus = new OptionSetValue(DeviceStatus);
                addPatientDevice.msemr_Manufacturer = Manufacturer;
                addPatientDevice.msemr_ManufacturerDate = ManufacturedDate;
                addPatientDevice.msemr_CarrierAIDC = CarrierAIDC;
                addPatientDevice.msemr_Location = new EntityReference(HealthCDM.msemr_location.EntityLogicalName, new Guid(LocationId));

                patientDeviceId = _serviceProxy.Create(addPatientDevice);

                if (patientDeviceId != Guid.Empty)
                {
                    DeviceId = patientDeviceId.ToString();
                    Console.WriteLine("Created Device [" + DeviceId + "] for Patient [" + PatientId + "]");
                }
                else
                {
                    throw new Exception("DeviceId == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
            return patientDeviceId;
        }

        private string GenerateDeviceVersion()
        {
            return BaseRandomGenerator.Next(1, 3).ToString() + "."
                + BaseRandomGenerator.Next(1, 9).ToString()
                + BaseRandomGenerator.Next(1, 6).ToString()
                + BaseRandomGenerator.Next(1, 3).ToString() + "."
                + BaseRandomGenerator.Next(1, 9).ToString()
                + BaseRandomGenerator.Next(1, 6).ToString()
                + BaseRandomGenerator.Next(1, 3).ToString();
        }

        private string GenerateDeviceNumber()
        {
            return GenerateRandomNumber(10);
        }

        public string GenerateCarrierAIDC()
        {
            return GenerateRandomNumber(12);
        }
    }
}


