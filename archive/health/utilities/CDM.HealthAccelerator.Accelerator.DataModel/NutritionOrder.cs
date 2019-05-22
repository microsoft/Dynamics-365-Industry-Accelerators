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
using System.Net;
using Microsoft.Xrm.Sdk;
using System.ServiceModel.Description;

namespace CDM.HealthAccelerator.DataModel
{
    [Serializable]
    public class NutritionOrder : BaseFunctions
    {
        public NutritionOrder()
        {
            InitializeEntity();
        }

        public NutritionOrder(string patientId, string practitionerId, string encounterId)
        {
            patient = patientId;
            practitioner = practitionerId;
            EncounterId = encounterId;
            InitializeEntity();
        }

        #region Attributes

        private string nutritionOrderId;
        private string practitioner;
        private string patient;
        private string orderName;
        private DateTime orderDateTime;
        private int orderStatus;
        private string nutritioOrderNumber;
        private int maxVolumeToDeliver;
        private int caloricDensity;
        private string encounterId;


        public string PractitionerId
        {
            get
            {
                return practitioner;
            }

            set
            {
                practitioner = value;
            }
        }

        public string PatientId
        {
            get
            {
                return patient;
            }

            set
            {
                patient = value;
            }
        }

        public DateTime OrderDateTime
        {
            get
            {
                return orderDateTime;
            }

            set
            {
                orderDateTime = value;
            }
        }

        public string OrderName
        {
            get
            {
                return orderName;
            }

            set
            {
                orderName = value;
            }
        }

        public int OrderStatus
        {
            get
            {
                return orderStatus;
            }

            set
            {
                orderStatus = value;
            }
        }

        public string NutritionOrderId
        {
            get
            {
                return nutritionOrderId;
            }

            set
            {
                nutritionOrderId = value;
            }
        }

        public string NutritioOrderNumber
        {
            get
            {
                return nutritioOrderNumber;
            }

            set
            {
                nutritioOrderNumber = value;
            }
        }

        public int MaxVolumeToDeliver
        {
            get
            {
                return maxVolumeToDeliver;
            }

            set
            {
                maxVolumeToDeliver = value;
            }
        }

        public int CaloricDensity
        {
            get
            {
                return caloricDensity;
            }

            set
            {
                caloricDensity = value;
            }
        }

        public string EncounterId
        {
            get
            {
                return encounterId;
            }

            set
            {
                encounterId = value;
            }
        }

        #endregion

        public override void InitializeEntity()
        {
            SampleDataCache.GenerateRandomDateTime rdt = new SampleDataCache.GenerateRandomDateTime(2017, 1, 1, DateTime.Today);

            orderName = SampleDataCache.NutrionOrders[SampleDataCache.SelectRandomItem.Next(0, SampleDataCache.NutrionOrders.Count - 1)];

            DateTime requestedDateTime = rdt.Next();
            orderDateTime = requestedDateTime;

            OrderStatus = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.NutritionOrder_Status>();

            NutritioOrderNumber = GenerateRandomNumber(8);

            MaxVolumeToDeliver = int.Parse(GenerateRandomNumber(1));

            CaloricDensity = int.Parse(GenerateRandomNumber(2));

        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid nutritionId = Guid.Empty;

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

                    nutritionId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return nutritionId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid nutritionId = Guid.Empty;

            HealthCDM.msemr_nutritionorder addNutritionOrder = new HealthCDM.msemr_nutritionorder();

            addNutritionOrder.msemr_Patient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PatientId)));
            addNutritionOrder.msemr_Orderer = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PractitionerId)));
            addNutritionOrder.msemr_name = OrderName;
            addNutritionOrder.msemr_DateTime = OrderDateTime;
            addNutritionOrder.msemr_Status = new OptionSetValue(OrderStatus);
            addNutritionOrder.msemr_CaloricDensity = caloricDensity;
            addNutritionOrder.msemr_MaxVolumetoDeliver = maxVolumeToDeliver;
            addNutritionOrder.msemr_Encounter = new EntityReference(HealthCDM.msemr_encounter.EntityLogicalName, Guid.Parse(EncounterId));

            try
            {
                nutritionId = _serviceProxy.Create(addNutritionOrder);

                if (nutritionId != Guid.Empty)
                {
                    NutritionOrderId = nutritionId.ToString();
                    Console.WriteLine("Created Nutrition [" + NutritionOrderId + "] for Patient [" + PatientId + "]");
                }
                else
                {
                    throw new Exception("NutritionId == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return nutritionId;
        }
    }
}
