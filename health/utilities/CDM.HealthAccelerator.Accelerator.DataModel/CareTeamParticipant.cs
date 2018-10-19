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
    public class CareTeamParticipant : BaseFunctions
    {

        #region Attributes

        private string careTeamParticpantId;
        private string careTeamId;
        private string patientId;
        private string practitionerId;
        private int memberType;
        private string roleId;
        private string relatedPersonId;

        public string CareTeamParticpantId
        {
            get
            {
                return careTeamParticpantId;
            }

            set
            {
                careTeamParticpantId = value;
            }
        }

        public string CareTeamId
        {
            get
            {
                return careTeamId;
            }

            set
            {
                careTeamId = value;
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

        public int MemberType
        {
            get
            {
                return memberType;
            }

            set
            {
                memberType = value;
            }
        }

        public string RoleId
        {
            get
            {
                return roleId;
            }

            set
            {
                roleId = value;
            }
        }

        public string RelatedPersonId
        {
            get
            {
                return relatedPersonId;
            }

            set
            {
                relatedPersonId = value;
            }
        }

        #endregion

        public CareTeamParticipant ()
        {
            InitializeEntity();
        }

        public override void InitializeEntity()
        {
            
        }

        public override Guid WriteToCDS(string cdsUrl, string cdsUserName, string cdsPassword)
        {
            Guid patientRiskAssessmentId = Guid.Empty;

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

                    patientRiskAssessmentId = WriteToCDS(_serviceProxy);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }

            return patientRiskAssessmentId;
        }

        public override Guid WriteToCDS(OrganizationServiceProxy _serviceProxy)
        {
            Guid patientCareTeamParticipantId = Guid.Empty;

            HealthCDM.msemr_careteamparticipant participant = new HealthCDM.msemr_careteamparticipant();

            participant.msemr_CareTeam = new EntityReference(HealthCDM.msemr_careteam.EntityLogicalName, Guid.Parse(CareTeamId));
            participant.msemr_MemberType = new OptionSetValue(MemberType);



            switch (MemberType)
            {
                case (int)HealthCDMEnums.CareTeamParticipant_Membertype.Patient:
                    {
                        participant.msemr_MemberPatient = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse(PatientId));
                    }
                    break;
                case (int)HealthCDMEnums.CareTeamParticipant_Membertype.Practitioner:
                    {
                        participant.msemr_MemberPractioner = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse(PractitionerId));
                    }
                    break;
                case (int)HealthCDMEnums.CareTeamParticipant_Membertype.RelationPerson:
                    {
                        participant.msemr_MemberRelatedPerson = new EntityReference(HealthCDM.Contact.EntityLogicalName, Guid.Parse(RelatedPersonId));
                    }
                    break;
            }

            //participant.msemr_Role = new EntityReference(HealthCDM.msemr_codeableconcept.EntityLogicalName, GetCodeableConceptId(_serviceProxy, RoleId, (int)HealthCDMEnums.CodeableConcept_Type.ParticipantRole));

            try
            {
                patientCareTeamParticipantId = _serviceProxy.Create(participant);

                if (patientCareTeamParticipantId != Guid.Empty)
                {
                    CareTeamParticpantId = patientCareTeamParticipantId.ToString();
                    Console.WriteLine("Created Care Team Participant [" + CareTeamParticpantId + "] for Patient [" + PatientId + "]");
                }
                else
                {
                    throw new Exception("RiskAssessmentId  == null");
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }


            return patientCareTeamParticipantId;
        }

    }
}
