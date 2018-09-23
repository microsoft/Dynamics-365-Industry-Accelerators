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

        public NutritionOrder(string patientId, string practitionerId)
        {
            patient = patientId;
            practitioner = practitionerId;
            InitializeEntity();
        }

        private string nutritionOrderId;
        private string practitioner;
        private string patient;
        private string orderName;
        private DateTime orderDateTime;
        private int orderStatus;

        public string Practitioner
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

        public string Patient
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

        public override void InitializeEntity()
        {
            SampleDataCache.RandomDateTime rdt = new SampleDataCache.RandomDateTime(2017, 1, 1, DateTime.Today);

            orderName = SampleDataCache.NutrionOrders[SampleDataCache.RandomContactGenerator.Next(0, SampleDataCache.NutrionOrders.Count - 1)];

            DateTime requestedDateTime = rdt.Next();
            orderDateTime = requestedDateTime;

            OrderStatus = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.NutritionOrder_Status>();

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

                    HealthCDM.msemr_nutritionorder addNutritionOrder = new HealthCDM.msemr_nutritionorder();

                    addNutritionOrder.msemr_Patient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((Patient)));
                    addNutritionOrder.msemr_Orderer = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((Practitioner)));
                    addNutritionOrder.msemr_name = OrderName;
                    addNutritionOrder.msemr_DateTime = OrderDateTime;
                    addNutritionOrder.msemr_Status = new OptionSetValue(OrderStatus);

                    try
                    {
                        nutritionId = _serviceProxy.Create(addNutritionOrder);

                        if (nutritionId != Guid.Empty)
                        {
                            NutritionOrderId = nutritionOrderId.ToString();
                            Console.WriteLine("Created Nutrition [" + NutritionOrderId + "] for Patient [" + Patient + "]");
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

            addNutritionOrder.msemr_Patient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((Patient)));
            addNutritionOrder.msemr_Orderer = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((Practitioner)));
            addNutritionOrder.msemr_name = OrderName;
            addNutritionOrder.msemr_DateTime = OrderDateTime;
            addNutritionOrder.msemr_Status = new OptionSetValue(OrderStatus);

            try
            {
                nutritionId = _serviceProxy.Create(addNutritionOrder);

                if (nutritionId != Guid.Empty)
                {
                    NutritionOrderId = nutritionId.ToString();
                    Console.WriteLine("Created Nutrition [" + NutritionOrderId + "] for Patient [" + Patient + "]");
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

        public static void ExportToJson(string filename, List<Profile> profiles)
        {
            throw new NotImplementedException();
        }
    }
}
