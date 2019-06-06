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
using System.IO;
using System.Linq;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using System.ServiceModel.Description;
using System.Net;
using Newtonsoft.Json;


namespace CDM.HealthAccelerator.DataModel
{
    [Serializable]
    public class Appointment : BaseFunctions
    {

        #region Attributes

        private string appointmentId;
        private string locationId;
        private string patientId;
        private string practitionerId;
        private DateTime appointmentStartDateTime;
        private DateTime appointmentEndDateTime;
        private int minutesDuration;
        private int participantActorType;
        private int participantStatus;
        private string instructions;
        private int priority; //0-9
        private int priorityCode;
        private string subject;
        private string regardingObjectId;
        private string description;
        private int appointmentType;

        public string AppointmentId
        {
            get
            {
                return appointmentId;
            }

            set
            {
                appointmentId = value;
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

        public string PractitionerId
        {
            get
            {
                return practitionerId;
            }

            set
            {
                practitionerId = value;
            }
        }

        public DateTime AppointmentStartDateTime
        {
            get
            {
                return appointmentStartDateTime;
            }

            set
            {
                appointmentStartDateTime = value;
            }
        }

        public DateTime AppointmentEndDateTime
        {
            get
            {
                return appointmentEndDateTime;
            }

            set
            {
                appointmentEndDateTime = value;
            }
        }

        public int MinutesDuration
        {
            get
            {
                return minutesDuration;
            }

            set
            {
                minutesDuration = value;
            }
        }

        public int ParticipantActorType
        {
            get
            {
                return participantActorType;
            }

            set
            {
                participantActorType = value;
            }
        }

        public int ParticipantStatus
        {
            get
            {
                return participantStatus;
            }

            set
            {
                participantStatus = value;
            }
        }

        public string Instructions
        {
            get
            {
                return instructions;
            }

            set
            {
                instructions = value;
            }
        }

        public int Priority
        {
            get
            {
                return priority;
            }

            set
            {
                priority = value;
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

        public string RegardingObjectId
        {
            get
            {
                return regardingObjectId;
            }

            set
            {
                regardingObjectId = value;
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

        public string Description
        {
            get
            {
                return description;
            }

            set
            {
                description = value;
            }
        }

        public int AppointmentType
        {
            get
            {
                return appointmentType;
            }

            set
            {
                appointmentType = value;
            }
        }


        #endregion

        public Appointment ()
        {
            InitializeEntity();
        }

        public Appointment(string patientId, string locationId, string practitionerId, string patientName)
        {
            PatientId = patientId;
            LocationId = locationId;
            PractitionerId = practitionerId;
            RegardingObjectId = patientId;

            Subject = patientName + " - Appointment for Subject";

            InitializeEntity();
        }

        public override void InitializeEntity()
        {
            SampleDataCache.GenerateRandomDateTime rdt = new SampleDataCache.GenerateRandomDateTime(2017, 1, 1, DateTime.Today);

            DateTime startDate = rdt.Next();
            DateTime endDate = rdt.AddHours(startDate, 1, 2);

            AppointmentStartDateTime = startDate;
            AppointmentEndDateTime = endDate;

            ParticipantActorType = (int)HealthCDMEnums.AppointmentEMRParticipant_Participantactortype.Patient;
            ParticipantStatus = (int)HealthCDMEnums.AppointmentEMR_Participantstatus.Accepted;

            Priority = int.Parse(GenerateRandomNumber(1));
            PriorityCode = HealthCDMEnums.RandomEnumInt<HealthCDMEnums.AppointmentEMR_Prioritycode>();

            MinutesDuration = int.Parse(GenerateRandomNumber(2));

            Instructions = "Enter through location specified to office";
            regardingObjectId = patientId;
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid patientAppointmentId = Guid.Empty;

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

                    patientAppointmentId = WriteToCDS(_serviceProxy);

                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientAppointmentId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid patientAppointmentId = Guid.Empty;

            HealthCDM.msemr_appointmentemr addAppointment = new HealthCDM.msemr_appointmentemr();

            if (!string.IsNullOrEmpty(PatientId))
            {
                addAppointment.msemr_ActorPatient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PatientId)));
            }

            if (!string.IsNullOrEmpty(PractitionerId))
            {
                addAppointment.msemr_ActorPractitioner = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse((PractitionerId)));
            }

            if (!string.IsNullOrEmpty(LocationId))
            {
                addAppointment.msemr_ActorLocation = new EntityReference(HealthCDM.msemr_location.EntityLogicalName, Guid.Parse((LocationId)));
            }

            addAppointment.msemr_Description = Description;
            addAppointment.ScheduledStart = AppointmentStartDateTime;
            addAppointment.ScheduledEnd = AppointmentEndDateTime;
            addAppointment.msemr_Starttime = AppointmentStartDateTime;
            addAppointment.msemr_Endtime = AppointmentEndDateTime;
            addAppointment.msemr_Priority = priority;
            addAppointment.PriorityCode = new OptionSetValue(PriorityCode);
            addAppointment.RegardingObjectId = new EntityReference(HealthCDM.Contact.EntityLogicalName, new Guid(RegardingObjectId == null ? PatientId : RegardingObjectId));
            addAppointment.msemr_MinutesDuration = minutesDuration;
            addAppointment.msemr_ParticipantStatus = new OptionSetValue(ParticipantStatus);
            addAppointment.msemr_ParticipantActorType = new OptionSetValue(ParticipantActorType);
            addAppointment.msemr_PatientInstruction = instructions;
            addAppointment.Subject = Description;

            try
            {
                patientAppointmentId = _serviceProxy.Create(addAppointment);

                if (patientAppointmentId != Guid.Empty)
                {
                    AppointmentId = patientAppointmentId.ToString();
                    Console.WriteLine("Created Appointment Request [" + patientAppointmentId + "] for Patient [" + PatientId + "]");
                }
                else
                {
                    throw new Exception("Appointment Request Id == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientAppointmentId;
        }
    }
}
