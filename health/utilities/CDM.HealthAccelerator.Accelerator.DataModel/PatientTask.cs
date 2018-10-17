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
    public class PatientTask : BaseFunctions
    {
        #region Attributes

        private string taskId;
        private string ownerPatientId;
        private string ownerPractitionerId;
        private string requestingPatientId;
        private string requestingPractitionerId;
        private string encounterId;
        private int durationInMinutes;
        private int intent;
        private int requestorType;
        private int taskPriority;
        private int ownerType;
        private string subject;
        private DateTime scheduledTime;
        private int priorityCode;

        public string TaskId
        {
            get
            {
                return taskId;
            }

            set
            {
                taskId = value;
            }
        }

        public string OwnerPatientId
        {
            get
            {
                return ownerPatientId;
            }

            set
            {
                ownerPatientId = value;
            }
        }

        public string OwnerPractitionerId
        {
            get
            {
                return ownerPractitionerId;
            }

            set
            {
                ownerPractitionerId = value;
            }
        }

        public string RequestingPatientId
        {
            get
            {
                return requestingPatientId;
            }

            set
            {
                requestingPatientId = value;
            }
        }

        public string RequestingPractitionerId
        {
            get
            {
                return requestingPractitionerId;
            }

            set
            {
                requestingPractitionerId = value;
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

        public int DurationInMinutes
        {
            get
            {
                return durationInMinutes;
            }

            set
            {
                durationInMinutes = value;
            }
        }

        public int Intent
        {
            get
            {
                return intent;
            }

            set
            {
                intent = value;
            }
        }

        public int RequestorAgent
        {
            get
            {
                return requestorType;
            }

            set
            {
                requestorType = value;
            }
        }

        public int TaskPriority
        {
            get
            {
                return taskPriority;
            }

            set
            {
                taskPriority = value;
            }
        }

        public int OwnerType
        {
            get
            {
                return ownerType;
            }

            set
            {
                ownerType = value;
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

        public DateTime ScheduledTime
        {
            get
            {
                return scheduledTime;
            }

            set
            {
                scheduledTime = value;
            }
        }

        public int PriorityCode
        {
            get
            {
                return priorityCode;
            }

            set
            {
                priorityCode = value;
            }
        }


        #endregion

        public PatientTask()
        {
            InitializeEntity();
        }

        public override void InitializeEntity()
        {
            TaskPriority =  (int)HealthCDMEnums.Task_Taskpriority.Routine;
            RequestorAgent = (int)HealthCDMEnums.Task_Requesteragent.Patient;
            Intent = (int)HealthCDMEnums.Task_Performerownertype.Patient;
            DurationInMinutes = int.Parse(GenerateRandomNumber(2));

            OwnerType = (int)HealthCDMEnums.Task_Performerownertype.Patient;

            SampleDataCache.GenerateRandomDateTime rdt = new SampleDataCache.GenerateRandomDateTime(2018, 1, 1, DateTime.Today);
            ScheduledTime = rdt.Next();


        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid patientTaskId = Guid.Empty;

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

                    patientTaskId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientTaskId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid patientTaskId = Guid.Empty;

            HealthCDM.Task addTask = new HealthCDM.Task();

            addTask.msemr_PerformerOwnerPatient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((OwnerPatientId)));
            addTask.msemr_PerformerOwnerPractitioner = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((OwnerPractitionerId)));

            addTask.msemr_RequesterAgentPatient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((RequestingPatientId)));
            addTask.msemr_RequesterAgentPractitioner = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((RequestingPractitionerId)));

            addTask.RegardingObjectId = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((OwnerPatientId)));
            addTask.msemr_ContextEncounter = new EntityReference(HealthCDM.msemr_encounter.EntityLogicalName, Guid.Parse((EncounterId)));

            addTask.ScheduledEnd = scheduledTime;
            addTask.ActualDurationMinutes = DurationInMinutes;
            addTask.msemr_Intent = new OptionSetValue(Intent);
            addTask.msemr_PerformerOwnerType = new OptionSetValue(OwnerType);
            addTask.msemr_TaskPriority = new OptionSetValue(TaskPriority);
            addTask.msemr_RequesterAgent = new OptionSetValue(RequestorAgent);
            addTask.Subject = Subject;

            try
            {
                patientTaskId = _serviceProxy.Create(addTask);

                if (patientTaskId != Guid.Empty)
                {
                    TaskId = patientTaskId.ToString();
                    Console.WriteLine("Created Task [" + TaskId + "] for Patient [" + OwnerPatientId + "]");
                }
                else
                {
                    throw new Exception("TaskId == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientTaskId;
        }
    }
}
