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

namespace CDM.HealthAccelerator.DataModel
{
    [Serializable]
    public static class HealthCDMEnums
    {
        public static Random RNG = new Random();

        public enum CareTeamParticipant_Membertype
        {
            Practitioner = 935000000,
            RelationPerson = 935000001,
            Patient = 935000002,
            Organization = 935000003,
            CareTeam = 935000004,
        }

        public enum Goal_Goaltargetmeasurevalueduetype
        {
            Date = 935000000,
            Duration = 935000001,
        }

        public enum Goal_Goalstatus
        {
            Proposed = 935000000,
            Accepted = 935000001,
            Planned = 935000002,
            InProgress = 935000003,
            OnTarget = 935000004,
            AheadofTarget = 935000005,
            BehindTarget = 935000006,
            Sustaining = 935000007,
            Achieved = 935000008,
            OnHold = 935000009,
            Cancelled = 935000010,
            EnteredInError = 935000011,
            Rejected = 935000012,
        }

        public enum Goal_Goalexpressedbytype
        {
            Patient = 935000000,
            Practitioner = 935000001,
            RelatedPerson = 935000002,
        }

        public enum Goal_Goaltargetdetailquantitycomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThanOrEqual = 935000002,
            GreaterThan = 935000003,
        }

        public enum Goal_Goaltargetdetailtype
        {
            Quantity = 935000000,
            Range = 935000001,
            CodeableConcept = 935000002,
        }

        public enum Goal_Goalsubjecttype
        {
            Patient = 935000000,
            Group = 935000001,
            Organization = 935000002,
        }

        public enum Goal_Goalstarttype
        {
            Date = 935000000,
            CodeableConcept = 935000001,
        }

        public enum ReferralRequestRecipient_Recipienttype
        {
            Recipient = 935000000,
            Organization = 935000001,
            HealthCareService = 935000002,
        }

        public enum ProcedureBasedOn_Basedontype
        {
            CarePlan = 935000000,
            ProcedureRequest = 935000001,
            ReferralRequest = 935000002,
        }

        public enum CarePlanAuthor_Planauthortype
        {
            Patient = 935000000,
            Practitioner = 935000001,
            RelatedPerson = 935000002,
            Organization = 935000003,
            CareTeam = 935000004,
        }

        public enum CarePlanDefinition_Definitiontype
        {
            PlanDefinition = 935000000,
            Questionnaire = 935000001,
        }

        public enum SpecimenProcessing_Processingtimetype
        {
            DateTime = 935000000,
            Period = 935000001,
        }

        public enum MedicationAdministrationPartOf_Partoftype
        {
            MedicationAdministration = 935000000,
            Procedure = 935000001,
        }

        public enum CommunicationRequestReasonReference_Reasonreferencetype
        {
            Condition = 935000000,
            Observation = 935000001,
        }

        public enum CarePlanActivity_Activityscheduledtype
        {
            Timing = 935000000,
            Period = 935000001,
            String = 935000002,
        }

        public enum CarePlanActivity_Activityproducttype
        {
            CodeableConcept = 935000000,
            Reference = 935000001,
        }

        public enum CarePlanActivity_Activitydefinitiontype
        {
            PlanDefinition = 935000000,
            ActivityDefinition = 935000001,
            Questionnaire = 935000002,
        }

        public enum CarePlanActivity_Activityreferencetype
        {
            Appointment = 935000000,
            CommunicationRequest = 935000001,
            DeviceRequest = 935000002,
            MedicationRequest = 935000003,
            NutritionOrder = 935000004,
            Task = 935000005,
            ProcedureRequest = 935000006,
            ReferralRequest = 935000007,
            VisionPrescription = 935000008,
            RequestGroup = 935000009,
        }

        public enum CarePlanActivity_Activityproductreferencetype
        {
            Medication = 935000000,
            Substance = 935000001,
        }

        public enum CarePlanActivity_Activitystatus
        {
            NotStarted = 935000000,
            Scheduled = 935000001,
            InProgress = 935000002,
            OnHold = 935000003,
            Completed = 935000004,
            Cancelled = 935000005,
            Unknown = 935000006,
        }

        public enum Observation_Valuetype
        {
            Quantity = 935000000,
            CodeableConcept = 935000001,
            String = 935000002,
            Boolean = 935000003,
            Range = 935000004,
            Ratio = 935000005,
            SampleData = 935000006,
            Attachment = 935000007,
            Time = 935000008,
            DateTime = 935000009,
            Period = 935000010,
        }

        public enum Observation_Valuetypequantitycomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThanOrEqual = 935000002,
            GreaterThan = 935000003,
        }

        public enum Observation_Devicetype
        {
            Device = 935000000,
            DeviceMetric = 935000001,
        }

        public enum Observation_Valueratiodenominatorcomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThan = 935000002,
            GreaterThanOrEqual = 935000003,
        }

        public enum Observation_Subjecttype
        {
            Device = 935000002,
            Group = 935000001,
            Location = 935000003,
            Patient = 935000000,
        }

        public enum Observation_Valuerationumeratorcomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThan = 935000002,
            GreaterThanOrEqual = 935000003,
        }

        public enum Observation_Contexttype
        {
            Encounter = 935000000,
            EpisodeOfCare = 935000001,
        }

        public enum Observation_Status
        {
            Registered = 935000000,
            Preliminary = 935000001,
            Final = 935000002,
            Amended = 935000003,
            Corrected = 935000004,
            Cancelled = 935000005,
            EnteredinError = 935000006,
            Unknown = 935000007,
        }

        public enum Observation_Effectivetype
        {
            Datetime = 935000000,
            Period = 935000001,
        }

        public enum MedicationRequestDefinition_Medicationrequestdefinitiontype
        {
            ActivityDefinition = 935000000,
            PlanDefinition = 935000001,
        }

        public enum CodeableConcept_Type
        {
            ActionParticipantRole = 935000000,
            ActionReason = 935000001,
            ActivityReason = 935000002,
            AdditionalDosageInstruction = 935000003,
            AdditiveCode = 935000004,
            AdministrationMethodCode = 935000005,
            AdministrationSiteCode = 935000006,
            AdmitSource = 935000007,
            AllergyIntolerance = 935000008,
            AnimalBreed = 935000009,
            AnimalGender = 935000010,
            AnimalSpecies = 935000011,
            AsNeededReasonCode = 935000012,
            BodyStructure = 100000000,
            CarePlanActivity = 935000013,
            CarePlanActivityCategory = 935000014,
            CarePlanActivityOutcome = 935000015,
            CarePlanCategory = 935000016,
            CareTeamCategory = 935000017,
            ClinicalFinding = 935000018,
            ClinicalImpressionCode = 935000019,
            ClinicalImpressionPrognosis = 935000020,
            CommunicationCategory = 935000021,
            ConditionCategoryCode = 935000022,
            ConditionCode = 935000023,
            ConditionOutcomeCode = 935000024,
            ConditionSeverity = 935000025,
            ConditionStage = 935000026,
            ContactEntityType = 935000027,
            ContactRole = 935000028,
            DataRequirementCodeFilterValueCode = 935000029,
            DefinitionTopic = 935000030,
            DetectedIssue = 935000031,
            DeviceComponentOperationalStatus = 935000032,
            DeviceComponentParameterGroup = 935000033,
            DeviceMetricType = 935000034,
            DeviceSafety = 935000035,
            DeviceSpecificationSpecType = 935000036,
            DeviceType = 935000037,
            DiagnosisRole = 935000038,
            DiagnosticReport = 935000039,
            Diet = 935000040,
            DietCode = 935000041,
            DischargeDisposition = 935000042,
            EncounterReasonCode = 935000043,
            EncounterType = 935000044,
            EndpointPayloadType = 935000045,
            EnteralFormulaAdditiveTypeCode = 935000046,
            EnteralFormulaTypeCode = 935000047,
            EnteralRouteCode = 935000048,
            EpisodeOfCareType = 935000049,
            EventHistory = 935000050,
            FamilyHistoryNotDoneReason = 935000051,
            FamilyMember = 935000053,
            FHIRType = 935000054,
            FinancialAccount = 935000055,
            FluidConsistencyTypeCode = 935000056,
            FoodTypeCode = 935000057,
            FormCode = 935000058,
            GoalCategory = 935000059,
            GoalPriority = 935000060,
            GoalStartEvent = 935000061,
            GoalTargetDetail = 935000062,
            Group = 935000063,
            ImagingStudy = 935000064,
            ImmunizationRequest = 935000065,
            InvestigationType = 935000066,
            Jurisdiction = 935000067,
            Language = 935000068,
            Library = 935000070,
            LinkType = 935000071,
            LocationType = 935000072,
            LOINCCode = 935000073,
            MedicationAdministrationCategory = 935000074,
            MedicationAsNeededReasonCode = 935000075,
            MedicationCode = 935000076,
            MedicationContainer = 935000077,
            MedicationIngredient = 935000078,
            MedicationPackageContent = 935000079,
            MedicationRequestCategory = 935000080,
            MedicationStatement = 935000081,
            NutrientModifierCode = 935000082,
            ObservationCategoryCode = 935000083,
            ObservationComponentValue = 935000084,
            ObservationInterpretationCode = 935000085,
            ObservationMethod = 935000086,
            ObservationReferenceRangeAppliesToCode = 935000087,
            ObservationReferenceRangeMeaningCode = 935000088,
            ObservationValueAbsentReason = 935000089,
            ObservationValueCode = 935000090,
            ParticipantRole = 935000091,
            ParticipantType = 935000092,
            ReferralType = 935000069,
            ParticipationMode = 935000093,
            PatientReferralType = 935000094,
            PatientRelationshipType = 935000095,
            PlanDefinitionActionCode = 935000096,
            PlanDefinitionActionReason = 935000097,
            PlanDefinitionGoalTarget = 935000098,
            PracticeSettingCode = 935000099,
            PractitionerRole = 935000100,
            PractitionerSpecialty = 935000101,
            Priority = 935000102,
            ProcedureCategoryCode = 935000103,
            ProcedureCode = 935000104,
            ProcedureDeviceActionCode = 935000105,
            ProcedureFollowupCode = 935000106,
            ProcedureOutcomeCode = 935000107,
            ProcedurePerformerRoleCode = 935000108,
            ProcedureReasonCode = 935000109,
            Provenance = 935000110,
            QualificationCode = 935000052,
            QuantityUnitCode = 935000111,
            Questionnaire = 935000112,
            QuestionnaireResponse = 935000113,
            ReAdmissionIndicator = 935000114,
            ReasonMedicationGivenCode = 935000116,
            ReasonMedicationNotGivenCode = 935000117,
            ReferralMethod = 935000118,
            RequestGroupActionCode = 935000119,
            RequestGroupReasonCode = 935000120,
            RequestIntent = 935000121,
            ResourceType = 935000122,
            RiskAssessmentCode = 935000123,
            RiskAssessmentMethod = 935000124,
            RiskAssessmentOutcome = 935000125,
            RiskAssessmentReasonCode = 935000126,
            RouteCode = 935000127,
            Sequence = 935000128,
            ServiceCategory = 935000129,
            ServiceCharacteristic = 935000130,
            ServiceDeliveryLocationRoleType = 935000131,
            ServiceEligibility = 935000132,
            ServiceProvisionCondition = 935000133,
            ServiceType = 935000134,
            SnomedCTMedicationCodes = 935000135,
            SpecialArrangement = 935000136,
            SpecialCourtesy = 935000137,
            Specialty = 935000138,
            SpecimenCollectionMethod = 935000139,
            SpecimenContainer = 935000140,
            SpecimenProcessingProcedure = 935000141,
            SpecimenType = 935000142,
            StructureMap = 935000143,
            SubstanceAdminSubstitutionReason = 935000144,
            SubstanceCategoryCode = 935000145,
            SubstanceCode = 935000146,
            SupplementTypeCode = 935000147,
            SymptomCode = 935000148,
            TaskBusinessStatus = 935000149,
            TaskCode = 935000150,
            TaskInputParameterType = 935000151,
            TaskOutputParameterType = 935000152,
            TaskPerformerType = 935000153,
            TaskReason = 935000154,
            TaskStatusReason = 935000155,
            TextureModifiedFoodTypeCode = 935000156,
            TextureModifierCode = 935000157,
            Topic = 935000158,
            UseContext = 935000159,
            ValueSet = 935000160,
            VisionPrescriptionProductCode = 935000161,
            VisionPrescriptionReasonCode = 935000162,
        }

        public enum Slot_Status
        {
            Busy = 935000000,
            Free = 935000001,
            BusyUnavailable = 935000002,
            BusyTentative = 935000003,
            EnteredInError = 935000004,
        }

        public enum ProcedureReasonReference_Reasonreferencetype
        {
            Condition = 935000000,
            Observation = 935000001,
        }

        public enum MedicationAdministrationDefinition_Definitiontype
        {
            PlanDefinition = 935000000,
            ActivityDefinition = 935000001,
        }

        public enum EndpointPayloadMimeType_Payloadmimetype
        {
            applicationfhirxml = 935000000,
            applicationfhirjson = 935000001,
        }

        public enum PlanDefinitionAction_Definitiontype
        {
            ActivityDefinition = 935000000,
            PlanDefinition = 935000001,
        }

        public enum PlanDefinitionAction_Timingtype
        {
            DateTime = 935000000,
            Period = 935000001,
            Duration = 935000002,
            Timing = 935000003,
        }

        public enum PlanDefinitionAction_Cardinalitybehavior
        {
            Single = 935000000,
            Multiple = 935000001,
        }

        public enum PlanDefinitionAction_Type
        {
            Create = 935000000,
            Update = 935000001,
            Remove = 935000002,
            FireEvent = 935000003,
        }

        public enum PlanDefinitionAction_Requiredbehavior
        {
            Must = 935000000,
            Could = 935000001,
            MustUnlessDocumented = 935000002,
        }

        public enum PlanDefinitionAction_Selectionbehavior
        {
            Any = 935000000,
            All = 935000001,
            AllOrNone = 935000002,
            ExactlyOne = 935000003,
            AtMostOne = 935000004,
            OneOrMore = 935000005,
        }

        public enum PlanDefinitionAction_Groupingbehavior
        {
            VisualGroup = 935000000,
            LogicalGroup = 935000001,
            SentenceGroup = 935000002,
        }

        public enum DeviceCalibration_Type
        {
            unspecified = 935000000,
            offset = 935000001,
            gain = 935000002,
            twopoint = 935000003,
        }

        public enum DeviceCalibration_Calibrationstate
        {
            notcalibrated = 935000000,
            calibrationrequired = 935000001,
            calibrated = 935000002,
            unspecified = 935000003,
        }

        public enum ActivityDefinitionRelatedArtifact_Type
        {
            Documentation = 935000000,
            Justification = 935000001,
            Citation = 935000002,
            Predecessor = 935000003,
            Successor = 935000004,
            DerivedFrom = 935000005,
            DependsOn = 935000006,
            ComposedOf = 935000007,
        }

        public enum MedicationRequest_Status
        {
            Active = 935000000,
            OnHold = 935000001,
            Cancelled = 935000002,
            Completed = 935000003,
            EnteredInError = 935000004,
            Stopped = 935000005,
            Draft = 935000006,
            Unknown = 935000007,
        }

        public enum MedicationRequest_Priority
        {
            Routine = 935000000,
            Urgent = 935000001,
            Stat = 935000002,
            ASAP = 935000003,
        }

        public enum MedicationRequest_Contexttype
        {
            Encounter = 935000000,
            EpisodeofCare = 935000001,
        }

        public enum MedicationRequest_Medicationtype
        {
            MedicationCode = 935000000,
            MedicationReference = 935000001,
        }

        public enum MedicationRequest_Requesteragenttype
        {
            Practitioner = 935000000,
            Organization = 935000001,
            Patient = 935000002,
            RelatedPerson = 935000003,
            Device = 935000004,
        }

        public enum MedicationRequest_Intent
        {
            Proposal = 935000000,
            Plan = 935000001,
            Order = 935000002,
            InstanceOrder = 935000003,
        }

        public enum MedicationRequest_Subjecttype
        {
            Device = 935000002,
            Group = 935000001,
            Location = 935000003,
            Patient = 935000000,
        }

        public enum Specimen_Status
        {
            Available = 935000000,
            Unavailable = 935000001,
            Unsatisfactory = 935000002,
            Enteredinerror = 935000003,
        }

        public enum Specimen_Subjecttype
        {
            Patient = 935000000,
            Group = 935000001,
            Device = 935000002,
            Substance = 935000003,
        }

        public enum Specimen_Collectioncollectedtype
        {
            Datetime = 935000000,
            Period = 935000001,
        }

        public enum CarePlan_Subjecttype
        {
            Patient = 935000000,
            Group = 935000001,
        }

        public enum CarePlan_Contexttype
        {
            Encounter = 935000000,
            EpisodeOfCare = 935000001,
        }

        public enum CarePlan_Planstatus
        {
            Pending = 935000000,
            Active = 935000001,
            Suspended = 935000002,
            Completed = 935000003,
            EnteredInError = 935000004,
            Cancelled = 935000005,
            Unknown = 935000006,
        }

        public enum CarePlan_Planintent
        {
            Proposal = 935000000,
            Plan = 935000001,
            Order = 935000002,
            Option = 935000003,
        }

        public enum Location_Mode
        {
            instance = 935000000,
            Kind = 935000001,
        }

        public enum Location_Addresstype
        {
            Postal = 935000000,
            Physical = 935000001,
            Both = 935000002,
        }

        public enum Location_Addressuse
        {
            Home = 935000000,
            Work = 935000001,
            Temp = 935000002,
            Old = 935000003,
        }

        public enum Location_Status
        {
            Active = 935000000,
            Suspended = 935000001,
            Inactive = 935000002,
        }

        public enum Location_Operationalstatus
        {
            Closed = 935000000,
            Housekeeping = 935000001,
            Isolated = 935000002,
            Contaminated = 935000003,
            Occupied = 935000004,
            Unoccupied = 935000005,
        }

        public enum ActivityDefinition_Producttype
        {
            Medication = 935000000,
            Substance = 935000001,
            CodeableConcept = 935000002,
        }

        public enum ActivityDefinition_Timingtype
        {
            Timing = 935000000,
            DateTime = 935000001,
            Period = 935000002,
            Range = 935000003,
        }

        public enum ActivityDefinition_Status
        {
            Draft = 935000000,
            Active = 935000001,
            Retired = 935000002,
            Unknown = 935000003,
        }

        public enum ActivityDefinitionParticipant_Type
        {
            Patient = 935000000,
            Practitioner = 935000001,
            RelatedPerson = 935000002,
        }

        public enum PlanDefinitionActionTriggerDefinition_Eventtimingtype
        {
            Timing = 935000000,
            Reference = 935000001,
            Date = 935000002,
            DateTime = 935000003,
        }

        public enum PlanDefinitionActionTriggerDefinition_Triggertype
        {
            namedevent = 935000000,
            periodic = 935000001,
            dataadded = 935000002,
            datamodified = 935000003,
            dataremoved = 935000004,
            dataaccessed = 935000005,
            dataaccessended = 935000006,
        }

        public enum TaskRestrictionRecipient_Restrictionrecipienttype
        {
            Patient = 935000000,
            Practitioner = 935000001,
            RelatedPerson = 935000002,
            Group = 935000003,
            Organization = 935000004,
        }

        public enum DeviceRequestDefinition_Definitiontype
        {
            ActivityDefinition = 935000000,
            PlanDefinition = 935000001,
        }

        public enum ActivityDefinitionUseContext_Codetype
        {
            Gender = 935000000,
            Age = 935000001,
            Focus = 935000002,
            User = 935000003,
            Workflow = 935000004,
            Task = 935000005,
            Venue = 935000006,
            Species = 935000007,
        }

        public enum ActivityDefinitionUseContext_Valuetype
        {
            CodeableConcept = 935000000,
            Quantity = 935000001,
            Range = 935000002,
        }

        public enum ActivityDefinitionUseContext_Quantitycomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThanOrEqual = 935000002,
            GreaterThan = 935000003,
        }

        public enum EncounterStatusHistory_Encounterstatus
        {
            Planned = 935000000,
            Arrived = 935000001,
            Triaged = 935000002,
            InProgress = 935000003,
            OnLeave = 935000004,
            Finished = 935000005,
            Cancelled = 935000006,
            EnteredinError = 935000007,
            Unknown = 935000008,
        }

        public enum RiskAssessmentPrediction_Probabilitytype
        {
            Decimal = 935000000,
            Range = 935000001,
        }

        public enum RiskAssessmentPrediction_Qualitativerisk
        {
            Negligiblelikelihood = 935000000,
            Lowlikelihood = 935000001,
            Moderatelikelihood = 935000002,
            Highlikelihood = 935000003,
            Certain = 935000004,
        }

        public enum RiskAssessmentPrediction_Periodtype
        {
            Period = 935000000,
            Range = 935000001,
        }

        public enum ProcedureRequestDefinition_Definitiontype
        {
            ActivityDefinition = 935000000,
            PlanDefinition = 935000001,
        }

        public enum DataRequirementDateFilter_Datefiltervaluetype
        {
            DateTime = 935000000,
            Period = 935000001,
            Duration = 935000002,
        }

        public enum EpisodeofCareHistory_Status
        {
            Planned = 935000000,
            Waitlist = 935000001,
            Active = 935000002,
            OnHold = 935000003,
            Finished = 935000004,
            Cancelled = 935000005,
            EnteredinError = 935000006,
        }

        public enum ClinicalImpressionProblem_Problemtype
        {
            Condition = 935000000,
            AllergyIntolerance = 935000001,
        }

        public enum PlanDefinitionActionArtifact_Type
        {
            Documentation = 935000000,
            Justification = 935000001,
            Citation = 935000002,
            Predecessor = 935000003,
            Successor = 935000004,
            DerivedFrom = 935000005,
            DependsOn = 935000006,
            ComposedOf = 935000007,
        }

        public enum ClinicalImpressionInvestigation_Itemtype
        {
            Observation = 935000000,
            QuestionnaireResponse = 935000001,
            FamilyMemberHistory = 935000002,
            DiagnosticReport = 935000003,
            RiskAssessment = 935000004,
            ImagingStudy = 935000005,
        }

        public enum DeviceMetric_Category
        {
            Measurement = 935000000,
            Setting = 935000001,
            Calculation = 935000002,
            Unspecified = 935000003,
        }

        public enum DeviceMetric_Color
        {
            Black = 935000000,
            Red = 935000001,
            Green = 935000002,
            Yellow = 935000003,
            Blue = 935000004,
            Magneta = 935000005,
            Cyan = 935000006,
            White = 935000007,
        }

        public enum DeviceMetric_Metricoperationalstatus
        {
            On = 935000000,
            Off = 935000001,
            Standby = 935000002,
            Enteredinerror = 935000003,
        }

        public enum Device_Devicestatus
        {
            Active = 935000000,
            InActive = 935000001,
            EnteredInError = 935000002,
            Unknown = 935000003,
        }

        public enum Device_Udientrytype
        {
            Barcode = 935000000,
            RFID = 935000001,
            Manual = 935000002,
            Card = 935000003,
            SelfReported = 935000004,
            Known = 935000005,
        }

        public enum RiskAssessment_Status
        {
            Registered = 935000000,
            Preliminary = 935000001,
            Final = 935000002,
            Amended = 935000003,
            Corrected = 935000004,
            Cancelled = 935000005,
            EnteredinError = 935000006,
            Unknown = 935000007,
        }

        public enum RiskAssessment_Performertype
        {
            Pratitioner = 935000000,
            Device = 935000001,
        }

        public enum RiskAssessment_Occurrencetype
        {
            Time = 935000000,
            Period = 935000001,
        }

        public enum RiskAssessment_Contexttype
        {
            Encounter = 935000000,
            EpisodeOfCare = 935000001,
        }

        public enum RiskAssessment_Reasontype
        {
            CodeableConcept = 935000000,
            Reference = 935000001,
        }

        public enum RiskAssessment_Subjecttype
        {
            Patient = 935000000,
            Group = 935000001,
        }

        public enum PlanDefinitionGoalArtifact_Type
        {
            Documentation = 935000000,
            Justification = 935000001,
            Citation = 935000002,
            Predecessor = 935000003,
            Successor = 935000004,
            DerivedFrom = 935000005,
            DependsOn = 935000006,
            ComposedOf = 935000007,
        }

        public enum ClinicalImpressionAction_Actiontype
        {
            ReferralRequest = 935000000,
            ProcedureRequest = 935000001,
            Procedure = 935000002,
            MedicationRequest = 935000003,
            Appointment = 935000004,
        }

        public enum ProcedureDefinition_Definitiontype
        {
            PlanDefinition = 935000000,
            ActivityDefinition = 935000001,
            ServiceDefinition = 935000002,
        }

        public enum PractitionerRoleAvailableTime_Daysofweek
        {
            Mon = 935000000,
            Tue = 935000001,
            Wed = 935000002,
            Thu = 935000003,
            Fri = 935000004,
            Sat = 935000005,
            Sun = 935000006,
        }

        public enum RequestGroupActionParticipant_Actionparticipanttype
        {
            Patient = 935000000,
            Person = 935000001,
            Practitioner = 935000002,
            RelatedPerson = 935000003,
        }

        public enum AllergyIntolerance_Type
        {
            Allergy = 935000000,
            Intolerance = 935000001,
        }

        public enum AllergyIntolerance_Verificationstatus
        {
            Unconfirmed = 935000000,
            Confirmed = 935000001,
            Refuted = 935000002,
            EnteredInError = 935000003,
        }

        public enum AllergyIntolerance_Criticality
        {
            LowRisk = 935000000,
            HighRisk = 935000001,
            UnabletoAssessRisk = 935000002,
        }

        public enum FamilyMemberHistoryCondition_Conditiononsettype
        {
            Age = 935000000,
            Range = 935000001,
            Period = 935000002,
            String = 935000003,
        }

        public enum PlanDefinitionActionCondition_Kind
        {
            Applicability = 935000000,
            Start = 935000001,
            Stop = 935000002,
        }

        public enum AppointmentEMRParticipant_Participationstatus
        {
            Accepted = 935000000,
            Declined = 935000001,
            Tentative = 935000002,
            NeedsAction = 935000003,
        }

        public enum AppointmentEMRParticipant_Participantactortype
        {
            Patient = 935000000,
            Practitioner = 935000001,
            RelatedPerson = 935000002,
            Device = 935000003,
            HealthcareService = 935000004,
            Location = 935000005,
        }

        public enum AppointmentEMRParticipant_Required
        {
            Required = 935000000,
            Optional = 935000001,
            InformationOnly = 935000002,
        }

        public enum EncounterParticipant_Individualtype
        {
            Practitioner = 935000000,
            RelatedPerson = 935000001,
        }

        public enum Procedure_Status
        {
            Preparation = 935000000,
            InProgress = 935000001,
            Suspended = 935000002,
            Aborted = 935000003,
            Completed = 935000004,
            EnteredinError = 935000005,
            Unknown = 935000006,
        }

        public enum Procedure_Subjecttype
        {
            Patient = 935000000,
            PatientGroup = 935000001,
        }

        public enum MedicationPackageContent_Packageitemtype
        {
            ItemCode = 935000000,
            Reference = 935000001,
        }

        public enum Account_Telecom3system
        {
            Phone = 935000000,
            Email = 935000001,
            Fax = 935000002,
            Pager = 935000003,
            SMS = 935000004,
            Skype = 935000005,
        }

        public enum Account_Telecom1system
        {
            Phone = 935000000,
            Email = 935000001,
            Fax = 935000002,
            Pager = 935000003,
            SMS = 935000004,
            Skype = 935000005,
        }

        public enum Account_Accounttype
        {
            Practice = 935000000,
            Organization = 935000001,
        }

        public enum Account_Telecom2use
        {
            Home = 935000000,
            Work = 935000001,
            Temp = 935000002,
            Mobile = 935000003,
            Old = 935000004,
        }

        public enum Account_Telecom3use
        {
            Home = 935000000,
            Work = 935000001,
            Temp = 935000002,
            Mobile = 935000003,
            Old = 935000004,
        }

        public enum Account_Telecom2system
        {
            Phone = 935000000,
            Email = 935000001,
            Fax = 935000002,
            Pager = 935000003,
            SMS = 935000004,
            Skype = 935000005,
        }

        public enum Account_Telecom1use
        {
            Home = 935000000,
            Work = 935000001,
            Temp = 935000002,
            Mobile = 935000003,
            Old = 935000004,
        }

        public enum ClinicalImpressionInvestigationItem_Itemtype
        {
            Observation = 935000000,
            QuestionnaireResponse = 935000001,
            FamilyMemberHistory = 935000002,
            DiagnosticReport = 935000003,
            RiskAssessment = 935000004,
            Item = 935000005,
            Imagingstudy = 935000006,
        }

        public enum AppointmentEMR_Instancetypecode
        {
            NotRecurring = 0,
            RecurringMaster = 1,
            RecurringInstance = 2,
            RecurringException = 3,
            RecurringFutureException = 4,
        }

        public enum AppointmentEMR_Prioritycode
        {
            Low = 0,
            Normal = 1,
            High = 2,
        }

        public enum AppointmentEMR_Deliveryprioritycode
        {
            Low = 0,
            Normal = 1,
            High = 2,
        }

        public enum AppointmentEMR_Community
        {
            Facebook = 1,
            Twitter = 2,
            Other = 0,
        }

        public enum AppointmentEMR_Participantactortype
        {
            Patient = 935000000,
            Practitioner = 935000001,
            RelatedPerson = 935000002,
            Device = 935000003,
            HealthcareService = 935000004,
            Location = 935000005,
        }

        public enum AppointmentEMR_Required
        {
            Required = 935000000,
            Optional = 935000001,
            InformationOnly = 935000002,
        }

        public enum AppointmentEMR_Participantstatus
        {
            Accepted = 935000000,
            Declined = 935000001,
            Tentative = 935000002,
            NeedsAction = 935000003,
        }

        public enum DeviceRequest_Priority
        {
            Routine = 935000000,
            Urgent = 935000001,
            ASAP = 935000002,
            STAT = 935000003,
        }

        public enum DeviceRequest_Perfomertype
        {
            Practitioner = 935000000,
            Organization = 935000001,
            Patient = 935000002,
            Device = 935000003,
            RelatedPerson = 935000004,
            HealthcareService = 935000005,
        }

        public enum DeviceRequest_Contexttype
        {
            Encounter = 935000000,
            EpisodeOfCare = 935000001,
        }

        public enum DeviceRequest_Requesteragenttype
        {
            Device = 935000000,
            Practitioner = 935000001,
            Organization = 935000002,
        }

        public enum DeviceRequest_Codetype
        {
            Reference = 935000000,
            CodeableConcept = 935000001,
        }

        public enum DeviceRequest_Occurrencetype
        {
            DateTime = 935000000,
            Period = 935000001,
            Timing = 935000002,
        }

        public enum DeviceRequest_Status
        {
            Draft = 935000000,
            Active = 935000001,
            Suspended = 935000002,
            Cancelled = 935000003,
            Completed = 935000004,
            EnteredinError = 935000005,
            Unknown = 935000006,
        }

        public enum DeviceRequest_Subjecttype
        {
            Patient = 935000000,
            Group = 935000001,
            Location = 935000002,
            Device = 935000003,
        }

        public enum ObservationPerformer_Obsperformertype
        {
            Practitioner = 935000000,
            Organization = 935000001,
            Patient = 935000002,
            RelatedPerson = 935000003,
        }

        public enum NutritionOrderEnteralFormulaAdministration_Rateratioquantitynumeratorcomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThanOrEqual = 935000002,
            GreaterThan = 935000003,
        }

        public enum NutritionOrderEnteralFormulaAdministration_Rateratioquantitydenominatorcomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThanOrEqual = 935000002,
            GreaterThan = 935000003,
        }

        public enum NutritionOrderEnteralFormulaAdministration_Administrationratetype
        {
            Quantity = 935000000,
            Ratio = 935000001,
        }

        public enum FamilyMemberHistory_Gender
        {
            Male = 935000000,
            Female = 935000001,
            Other = 935000002,
            Unknown = 935000003,
        }

        public enum FamilyMemberHistory_Borntype
        {
            Period = 935000000,
            Date = 935000001,
            String = 935000002,
        }

        public enum FamilyMemberHistory_Agetype
        {
            Age = 935000000,
            Range = 935000001,
            String = 935000002,
        }

        public enum FamilyMemberHistory_Deceasedtype
        {
            Boolean = 935000000,
            Age = 935000001,
            Range = 935000002,
            Date = 935000003,
            String = 935000004,
        }

        public enum FamilyMemberHistory_Estimatedage
        {
        }

        public enum FamilyMemberHistory_Status
        {
            Partial = 935000000,
            Completed = 935000001,
            EnteredInError = 935000002,
            HealthUnknown = 935000003,
        }

        public enum DeviceContactPoint_Contactpointuse
        {
            Home = 935000000,
            Work = 935000001,
            Temp = 935000002,
            Mobile = 935000003,
            Old = 935000004,
        }

        public enum DeviceContactPoint_Contactpointsystem
        {
            Phone = 935000000,
            Fax = 935000001,
            Email = 935000002,
            Pager = 935000003,
            URL = 935000004,
            SMS = 935000005,
            Other = 935000006,
        }

        public enum DataRequirementCodeFilter_Codefiltervaluesettype
        {
            String = 935000000,
            Reference = 935000001,
        }

        public enum ClinicalImpression_Status
        {
            Inprogress = 935000000,
            Completed = 935000001,
            EnteredinError = 935000002,
        }

        public enum ClinicalImpression_Assessmenttimetype
        {
            Datetime = 935000000,
            Period = 935000001,
        }

        public enum ClinicalImpression_Subjecttype
        {
            Patient = 935000000,
            Group = 935000001,
        }

        public enum ClinicalImpression_Contexttype
        {
            Encounter = 935000000,
            EpisodeOfCare = 935000001,
        }

        public enum ReferralRequestBasedOn_Basedontype
        {
            ReferenceReferralRequest = 935000000,
            CarePlan = 935000001,
        }

        public enum PlanDefinitionArtifact_Type
        {
            Documentation = 935000000,
            Justification = 935000001,
            Citation = 935000002,
            Predecessor = 935000003,
            Successor = 935000004,
            DerivedFrom = 935000005,
            DependsOn = 935000006,
            ComposedOf = 935000007,
        }

        public enum PlanDefinitionActionRelatedAction_Relationship
        {
            after = 935000007,
            afterend = 935000008,
            afterstart = 935000006,
            before = 935000001,
            beforeend = 935000002,
            beforestart = 935000000,
            concurrent = 935000004,
            concurrentwithend = 935000005,
            concurrentwithstart = 935000003,
        }

        public enum PlanDefinitionActionRelatedAction_Durationtype
        {
            Duration = 935000000,
            Range = 935000001,
        }

        public enum RequestGroupActionCondition_Actionconditionkind
        {
            Applicability = 935000000,
            Start = 935000001,
            Stop = 935000002,
        }

        public enum ProcedurePerformer_Performertype
        {
            Practitioner = 935000000,
            Organization = 935000001,
            Patient = 935000002,
            RelatedPerson = 935000003,
            Device = 935000004,
        }

        public enum VisionPrescription_Reasontype
        {
            CodeableConcept = 935000000,
            Reference = 935000001,
        }

        public enum VisionPrescription_Status
        {
            Active = 935000000,
            Cancelled = 935000001,
            Draft = 935000002,
            EnteredInError = 935000003,
        }

        public enum FamilyMemberHistoryReasonReference_Reasonreferencetype
        {
            Condition = 935000000,
            Observation = 935000001,
            AllergyIntolerance = 935000002,
            QuestionnaireResponse = 935000003,
        }

        public enum MedicationAdministration_Status
        {
            InProgress = 935000000,
            OnHold = 935000001,
            Completed = 935000002,
            EnteredinError = 935000003,
            Item = 935000004,
            Stopped = 935000005,
            Unknown = 935000006,
        }

        public enum MedicationAdministration_Rateratioquantitynumeratorcomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThanOrEqual = 935000002,
            GreaterThan = 935000003,
        }

        public enum MedicationAdministration_Subjecttype
        {
            Patient = 935000000,
            Group = 935000001,
        }

        public enum MedicationAdministration_Contexttype
        {
            Encounter = 935000000,
            EpisodeOfCare = 935000001,
        }

        public enum MedicationAdministration_Rateratioquantitydenominatorcomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThanOrEqual = 935000002,
            GreaterThan = 935000003,
        }

        public enum MedicationAdministration_Dosageratetype
        {
            Ratio = 935000000,
            Quantity = 935000001,
        }

        public enum MedicationAdministration_Medicationtype
        {
            CodeableConcept = 935000000,
            Reference = 935000001,
        }

        public enum MedicationAdministration_Effectivetype
        {
            Datetime = 935000000,
            Period = 935000001,
        }

        public enum ProcedureRequest_Priority
        {
            Routine = 935000000,
            Urgent = 935000001,
            ASAP = 935000002,
            STAT = 935000003,
        }

        public enum ProcedureRequest_Intent
        {
            Proposal = 935000000,
            Plan = 935000001,
            Order = 935000002,
        }

        public enum ProcedureRequest_Subjecttype
        {
            Device = 935000002,
            Group = 935000001,
            Location = 935000003,
            Patient = 935000000,
        }

        public enum ProcedureRequest_Contexttype
        {
            Encounter = 935000000,
            EpisodeOfCare = 935000001,
        }

        public enum ProcedureRequest_Status
        {
            Draft = 935000000,
            Active = 935000001,
            Suspended = 935000002,
            Cancelled = 935000003,
            Completed = 935000004,
            EnteredinError = 935000005,
            Unknown = 935000006,
        }

        public enum ProcedureRequest_Requesteragent
        {
            Device = 935000000,
            Practitioner = 935000001,
            Organization = 935000002,
        }

        public enum ProcedureRequest_Occurrencetype
        {
            Date = 935000000,
            Period = 935000001,
        }

        public enum ProcedureRequest_Performertype
        {
            Practitioner = 935000000,
            Organization = 935000001,
            Patient = 935000002,
            Device = 935000003,
            Related = 935000004,
            Person = 935000005,
            HealthcareService = 935000006,
        }

        public enum ProcedureUsedReference_Reftype
        {
            Device = 935000000,
            Medication = 935000001,
            Substance = 935000002,
        }

        public enum Endpoint_Status
        {
            Active = 935000000,
            Suspended = 935000001,
            Error = 935000002,
            Off = 935000003,
            Enteredinerror = 935000004,
            Test = 935000005,
        }

        public enum Endpoint_Connectiontype
        {
            IHEXCPD = 935000000,
            IHEXCA = 935000001,
            IHEXDR = 935000002,
            IHEXDS = 935000003,
            IHEIID = 935000004,
            DICOMWADORS = 935000005,
            DICOMQIDORS = 935000006,
            DICOMSTOWRS = 935000007,
            DICOMWADOURI = 935000008,
            HL7FHIR = 935000009,
            HL7FHIRMessaging = 935000010,
            HL7v2MLLP = 935000011,
            Secureemail = 935000012,
            DirectProject = 935000013,
        }

        public enum EncounterLocation_Encounterlocationstatus
        {
            Planned = 935000000,
            Active = 935000001,
            Reserved = 935000002,
            Completed = 935000003,
        }

        public enum NutritionOrder_Status
        {
            Proposed = 935000000,
            Draft = 935000001,
            Planned = 935000002,
            Requested = 935000003,
            Active = 935000004,
            OnHold = 935000005,
            Completed = 935000006,
            Cancelled = 935000007,
            EnteredInError = 935000008,
        }

        public enum Condition_Onsettype
        {
            Age = 935000000,
            Period = 935000001,
            Range = 935000002,
            String = 935000003,
        }

        public enum Condition_Verificationstatus
        {
            Provisional = 935000000,
            Differential = 935000001,
            Confirmed = 935000002,
            Refuted = 935000003,
            EnteredInError = 935000004,
            Unknown = 935000005,
        }

        public enum Condition_Abatementtype
        {
            Age = 935000000,
            Period = 935000001,
            Range = 935000002,
            String = 935000003,
            Boolean = 935000004,
            Datetime = 935000005,
        }

        public enum Condition_Contexttype
        {
            Encounter = 935000000,
            EpisodeOfCare = 935000001,
        }

        public enum Condition_Subjecttype
        {
            Patient = 935000000,
            Group = 935000001,
        }

        public enum Condition_Clinicalstatus
        {
            Active = 935000000,
            Recurrence = 935000001,
            Inactive = 935000002,
            Remission = 935000003,
            Resolved = 935000004,
        }

        public enum ClinicalImpressionFinding_Itemtype
        {
            Condition = 935000000,
            Observation = 935000001,
        }

        public enum ReferralRequestDefinition_Definitiontype
        {
            ActivityDefinition = 935000000,
            PlanDefinition = 935000001,
        }

        public enum PlanDefinitionContributor_Contributortype
        {
            Author = 935000000,
            Editor = 935000001,
            Reviewer = 935000002,
            Endorser = 935000003,
        }

        public enum HealthcareServiceTelecom_Use
        {
            Home = 935000000,
            Work = 935000001,
            Temp = 935000002,
            Mobile = 935000003,
            Old = 935000004,
        }

        public enum HealthcareServiceTelecom_System
        {
            Phone = 935000000,
            Fax = 935000001,
            Email = 935000002,
            Pager = 935000003,
            URL = 935000004,
            SMS = 935000005,
            Other = 935000006,
        }

        public enum SubstanceIngredient_Ingredientquantitydenominatorcomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThanOrEqual = 935000002,
            GreaterThan = 935000003,
        }

        public enum SubstanceIngredient_Ingredientsubstancetype
        {
            SubstanceCodeableConcept = 935000000,
            SubstanceReference = 935000001,
        }

        public enum SubstanceIngredient_Ingredientquantitynumeratorcomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThanOrEqual = 935000002,
            GreaterThan = 935000003,
        }

        public enum PlanDefinitionGoalTarget_Valuetype
        {
            Quantity = 935000000,
            Range = 935000001,
            Concept = 935000002,
        }

        public enum PlanDefinitionGoalTarget_Quantitycomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThanOrEqual = 935000002,
            GreaterThan = 935000003,
        }

        public enum HealthcareServiceAvailableTime_Dayofweek
        {
            Sunday = 192350000,
            Monday = 192350001,
            Tuesday = 192350002,
            Wednesday = 192350003,
            Thursday = 192350004,
            Friday = 192350005,
            Saturday = 192350006,
        }

        public enum MedicalIdentifier_Type
        {
            DriversLicenseNumber = 935000000,
            PassportNumber = 935000001,
            BreedRegistryNumber = 935000002,
            MedicalRecordNumber = 935000003,
            MicrochipNumber = 935000004,
            EmployerNumber = 935000005,
            TaxIDNumber = 935000006,
            NationalInsurancePayerIdentifierPayor = 935000007,
            ProviderNumber = 935000008,
            MedicalLicenseNumber = 935000009,
            DonorRegistrationNumber = 935000010,
            AccessionID = 935000011,
        }

        public enum MedicalIdentifier_Identifiertype
        {
            Patient = 935000000,
            Practitioner = 935000001,
            RelatedPerson = 935000002,
            Organization = 935000003,
        }

        public enum MedicalIdentifier_Use
        {
            Usual = 935000000,
            Official = 935000001,
            Temp = 935000002,
            Secondary = 935000003,
        }

        public enum Contact_Name1use
        {
            Usual = 935000000,
            Official = 935000001,
            Temp = 935000002,
            Nick = 935000003,
            Maiden = 935000004,
            Old = 935000005,
            Anonymus = 935000006,
        }

        public enum Contact_Telecom3use
        {
            Home = 935000000,
            Work = 935000001,
            Temp = 935000002,
            Mobile = 935000003,
            Old = 935000004,
        }

        public enum Contact_Name3use
        {
            Usual = 935000000,
            Official = 935000001,
            Temp = 935000002,
            Nick = 935000003,
            Maiden = 935000004,
            Old = 935000005,
            Anonymus = 935000006,
        }

        public enum Contact_Telecom3system
        {
            Phone = 935000000,
            Email = 935000001,
            Fax = 935000002,
            Pager = 935000003,
            SMS = 935000004,
            Skype = 935000005,
        }

        public enum Contact_Telecom1system
        {
            Phone = 935000000,
            Email = 935000001,
            Fax = 935000002,
            Pager = 935000003,
            SMS = 935000004,
            Skype = 935000005,
        }

        public enum Contact_Telecom2use
        {
            Home = 935000000,
            Work = 935000001,
            Temp = 935000002,
            Mobile = 935000003,
            Old = 935000004,
        }

        public enum Contact_Telecom1use
        {
            Home = 935000000,
            Work = 935000001,
            Temp = 935000002,
            Mobile = 935000003,
            Old = 935000004,
        }

        public enum Contact_Contacttype
        {
            Patient = 935000000,
            Practitioner = 935000001,
            RelatedPerson = 935000002,
        }

        public enum Contact_Name2use
        {
            Usual = 935000000,
            Official = 935000001,
            Temp = 935000002,
            Nick = 935000003,
            Maiden = 935000004,
            Old = 935000005,
            Anonymus = 935000006,
        }

        public enum Contact_Telecom2system
        {
            Phone = 935000000,
            Email = 935000001,
            Fax = 935000002,
            Pager = 935000003,
            SMS = 935000004,
            Skype = 935000005,
        }

        public enum PlanDefinitionGoal_Category
        {
            Dietary = 935000000,
            Safety = 935000001,
            Behavioral = 935000002,
            Nursing = 935000003,
            Physiotherapy = 935000004,
        }

        public enum PlanDefinitionGoal_Priority
        {
            HighPriority = 935000000,
            MediumPriority = 935000001,
            LowPriority = 935000002,
        }

        public enum Encounter_Priority
        {
            ASAP = 935000000,
            callbackresults = 935000001,
            callbackforscheduling = 935000002,
            callbackplacerforscheduling = 935000003,
            contactrecipientforscheduling = 935000004,
            elective = 935000005,
            emergency = 935000006,
            preop = 935000007,
            asneeded = 935000008,
            routine = 935000009,
            rushreporting = 935000010,
            stat = 935000011,
            timingcritical = 935000012,
            useasdirected = 935000013,
            urgent = 935000014,
        }

        public enum Encounter_Encounterstatus
        {
            Planned = 935000000,
            Arrived = 935000001,
            Triaged = 935000002,
            InProgress = 935000003,
            OnLeave = 935000004,
            Finished = 935000005,
            Cancelled = 935000006,
            EnteredinError = 935000007,
            Unknown = 935000008,
        }

        public enum Encounter_Encounterclass
        {
            Inpatient = 935000000,
            Outpatient = 935000001,
            Ambulatory = 935000002,
            Emergency = 935000003,
        }

        public enum Encounter_Class
        {
            ambulatory = 935000000,
            emergency = 935000001,
            field = 935000002,
            homehealth = 935000003,
            inpatientencounter = 935000004,
            inpatientacute = 935000005,
            inpatientnonacute = 935000006,
            preadmission = 935000007,
            shortstay = 935000008,
            virtualencounter = 935000009,
        }

        public enum ProcedureRequestReasonReference_Reasonreferencetype
        {
            Condition = 935000000,
            Observation = 935000001,
        }

        public enum RequestGroup_Authortype
        {
            Device = 935000000,
            Practitioner = 935000001,
        }

        public enum RequestGroup_Priority
        {
            Routine = 935000000,
            Urgent = 935000001,
            ASAP = 935000002,
            STAT = 935000003,
        }

        public enum RequestGroup_Reasontype
        {
            CodeableConcept = 935000000,
            Reference = 935000001,
        }

        public enum RequestGroup_Contexttype
        {
            Encounter = 935000000,
            EpisodeOfCare = 935000001,
        }

        public enum RequestGroup_Status
        {
            Draft = 935000000,
            Active = 935000001,
            Suspended = 935000002,
            Cancelled = 935000003,
            Completed = 935000004,
            EnteredinError = 935000005,
            Unknown = 935000006,
        }

        public enum RequestGroup_Intent
        {
            Proposal = 935000000,
            Plan = 935000001,
            Order = 935000002,
        }

        public enum RequestGroup_Subjecttype
        {
            Patient = 935000000,
            Group = 935000001,
        }

        public enum TimingWhen_Repeatwhen
        {
            HS = 935000000,
            WAKE = 935000001,
            C = 935000002,
            CM = 935000003,
            CD = 935000004,
            CV = 935000005,
            AC = 935000006,
            ACM = 935000007,
            ACD = 935000008,
            ACV = 935000009,
            PC = 935000010,
            PCM = 935000011,
            PCD = 935000012,
            PCV = 935000013,
        }

        public enum FamilyMemberHistoryReason_Reasoncodetype
        {
            Code = 935000000,
            Text = 935000001,
        }

        public enum EncounterClassHistory_Encounterclass
        {
            Inpatient = 935000000,
            Outpatient = 935000001,
            Ambulatory = 935000002,
            Emergency = 935000003,
        }

        public enum CommunicationRequest_Sendertype
        {
            Device = 935000000,
            Organization = 935000001,
            Patient = 935000002,
            Practitioner = 935000003,
            RelatedPerson = 935000004,
        }

        public enum CommunicationRequest_Occurrencetype
        {
            Date = 935000000,
            Period = 935000001,
        }

        public enum CommunicationRequest_Contexttype
        {
            Encounter = 935000000,
            EpisodeOfCare = 935000001,
        }

        public enum CommunicationRequest_Requesteragenttype
        {
            Device = 935000000,
            Patient = 935000001,
            Practitioner = 935000002,
            RelatedPerson = 935000003,
            Organization = 935000004,
        }

        public enum CommunicationRequest_Status
        {
            Draft = 935000000,
            Active = 935000001,
            Suspended = 935000002,
            Cancelled = 935000003,
            Completed = 935000004,
            EnteredinError = 935000005,
            Unknown = 935000006,
        }

        public enum CommunicationRequest_Subjecttype
        {
            Patient = 935000001,
            Group = 935000000,
        }

        public enum CarePlanActivityPerformer_Activityperformertype
        {
            Practitioner = 935000000,
            Organization = 935000001,
            RelatedPerson = 935000002,
            Patient = 935000003,
            CareTeam = 935000004,
        }

        public enum DeviceComponent_Measurementprinciple
        {
            Other = 935000000,
            Chemical = 935000001,
            Electrical = 935000002,
            Impedance = 935000003,
            Nuclear = 935000004,
            Optical = 935000005,
            Thermal = 935000006,
            Biological = 935000007,
            Mechanical = 935000008,
            Acoustical = 935000009,
            Manual = 935000010,
        }

        public enum EndpointContact_Use
        {
            Home = 935000000,
            Work = 935000001,
            Temp = 935000002,
            Old = 935000003,
            Mobile = 935000004,
        }

        public enum EndpointContact_Contactsystem
        {
            Phone = 935000000,
            Fax = 935000001,
            Email = 935000002,
            Pager = 935000003,
            URL = 935000004,
            SMS = 935000005,
            Other = 935000006,
        }

        public enum MedicationRequestBasedOn_Medicationrequestbasedontype
        {
            CarePlan = 935000000,
            MedicationRequest = 935000001,
            ProcedureRequest = 935000002,
            ReferralRequest = 935000003,
        }

        public enum MedicationRequestReasonReference_Reasonreferencetype
        {
            Condition = 935000000,
            Observation = 935000001,
        }

        public enum ProcedurePartOf_Partoftype
        {
            Procedure = 935000000,
            Observation = 935000001,
            MedicationAdministration = 935000002,
        }

        public enum Substance_Status
        {
            Active = 935000000,
            Inactive = 935000001,
            EnteredinError = 935000002,
        }

        public enum PlanDefinitionParticipant_Type
        {
            Patient = 935000000,
            Practitioner = 935000001,
            RelatedPerson = 935000002,
        }

        public enum Timing_Code
        {
            BID = 935000000,
            TID = 935000001,
            QID = 935000002,
            AM = 935000003,
            PM = 935000004,
            QD = 935000005,
            QOD = 935000006,
            Q4H = 935000007,
            Q6H = 935000008,
        }

        public enum Timing_Repeatdurationunit
        {
            s = 935000000,
            min = 935000001,
            h = 935000002,
            d = 935000003,
            wk = 935000004,
            mo = 935000005,
            aunitoftimeUCUM = 935000006,
        }

        public enum Timing_Repeatperiodunit
        {
            s = 935000000,
            min = 935000001,
            h = 935000002,
            d = 935000003,
            wk = 935000004,
            mo = 935000005,
            aunitoftimeUCUM = 935000006,
        }

        public enum Timing_Repeatboundstype
        {
            Duration = 935000000,
            Range = 935000001,
            Period = 935000002,
        }

        public enum ActivityDefinitionContributor_Type
        {
            Author = 935000000,
            Editor = 935000001,
            Reviewer = 935000002,
            Endorser = 935000003,
        }

        public enum CommunicationRequestRecipient_Recipienttype
        {
            Device = 935000000,
            Patient = 935000001,
            Practitioner = 935000002,
            Organization = 935000003,
            RelatedPerson = 935000004,
            Group = 935000005,
            CareTeam = 935000006,
        }

        public enum CareTeam_Subjecttype
        {
            Patient = 935000000,
            Group = 935000001,
        }

        public enum CareTeam_Contexttype
        {
            Encounter = 935000000,
            EpisodeOfCare = 935000001,
        }

        public enum CareTeam_Careteamstatus
        {
            Proposed = 935000000,
            Active = 935000001,
            Suspended = 935000002,
            Inactive = 935000003,
            EnteredInError = 935000004,
        }

        public enum VisionPrescriptionDispense_Dispensebase
        {
            Up = 935000000,
            Down = 935000001,
            In = 935000002,
            Out = 935000003,
        }

        public enum VisionPrescriptionDispense_Dispenseeye
        {
            Right = 935000000,
            Left = 935000001,
        }

        public enum RequestGroupActionDocumentation_Documentationtype
        {
            Documentation = 935000000,
            Justification = 935000001,
            Citation = 935000002,
            Predecessor = 935000003,
            Successor = 935000004,
            DerivedFrom = 935000005,
            DependsOn = 935000006,
            ComposedOf = 935000007,
        }

        public enum FamilyMemberHistoryDefinition_Definitiontype
        {
            PlanDefinition = 935000000,
            Questionnaire = 935000001,
        }

        public enum PractitionerRoleTelecom_Use
        {
            Home = 935000000,
            Work = 935000001,
            Temp = 935000002,
            Old = 935000003,
            Mobile = 935000004,
        }

        public enum PractitionerRoleTelecom_System
        {
            Phone = 935000000,
            Fax = 935000001,
            Email = 935000002,
            Pager = 935000003,
            URL = 935000004,
            SMS = 935000005,
            Other = 935000006,
        }

        public enum ObservationRelatedResource_Obsrelatedresourcetype
        {
            HasMember = 935000000,
            DerivedFrom = 935000001,
            SequelTo = 935000002,
            Replaces = 935000003,
            QualifiedBy = 935000004,
            InterferedBy = 935000005,
        }

        public enum ObservationRelatedResource_Obsrelatedresourcetargettype
        {
            Observation = 935000000,
            QuestionnaireResponse = 935000001,
            Sequence = 935000002,
        }

        public enum MedicationAdministrationReasonReference_Reasonreferencetype
        {
            Condition = 935000000,
            Observation = 935000001,
        }

        public enum ReferralRequestReasonReference_Reasonreferencetype
        {
            Condition = 935000000,
            Observation = 935000001,
        }

        public enum PlanDefinitionUseContext_Codetype
        {
            Gender = 935000000,
            AgeRange = 935000001,
            ClinicalFocus = 935000002,
            UserType = 935000003,
            WorkflowSetting = 935000004,
            WorkflowTask = 935000005,
            ClinicalVenue = 935000006,
            Species = 935000007,
        }

        public enum Task_Requesteragent
        {
            Practitioner = 935000000,
            Organization = 935000001,
            Patient = 935000002,
            RelatedPerson = 935000003,
            Device = 935000004,
        }

        public enum Task_Intent
        {
            Proposal = 935000000,
            Plan = 935000001,
            Order = 935000002,
            Reflexorder = 935000003,
            Fillerorder = 935000004,
            Originalorder = 935000006,
            Instanceorder = 935000005,
            Option = 935000007,
        }

        public enum Task_Contexttype
        {
            Encounter = 935000000,
            EpisodeOfCare = 935000001,
        }

        public enum Task_Performerownertype
        {
            Practitioner = 935000000,
            Organization = 935000001,
            Patient = 935000002,
            Device = 935000003,
            RelatedPerson = 935000004,
            HealthcareService = 935000005,
        }

        public enum Task_Taskpriority
        {
            Routine = 935000000,
            Urgent = 935000001,
            Asap = 935000002,
            Stat = 935000003,
        }

        public enum Task_Status
        {
            Draft = 935000000,
            Requested = 935000001,
            Received = 935000002,
            Accepted = 935000003,
            Rejected = 935000004,
            Ready = 935000005,
            Cancelled = 935000006,
            Inprogress = 935000007,
            Onhold = 935000008,
            Failed = 935000009,
            Completed = 935000010,
            Enteredinerror = 935000011,
        }

        public enum PlanDefinition_Status
        {
            Draft = 935000000,
            Active = 935000001,
            Retired = 935000002,
            Unknown = 935000003,
        }

        public enum PlanDefinition_Type
        {
            OrderSet = 935000000,
            Protocol = 935000001,
            ECARule = 935000002,
        }

        public enum RequestGroupAction_Actiontimingtype
        {
            DateTime = 935000000,
            Period = 935000001,
            Duration = 935000002,
            Range = 935000003,
            Timing = 935000004,
        }

        public enum RequestGroupAction_Actionselectionbehavior
        {
            Any = 935000000,
            All = 935000001,
            AllOrNone = 935000002,
            ExactlyOne = 935000003,
            AtMostOne = 935000004,
            OneOrMore = 935000005,
        }

        public enum RequestGroupAction_Actiongroupingbehavior
        {
            VisualGroup = 935000000,
            LogicalGroup = 935000001,
            SentenceGroup = 935000002,
        }

        public enum RequestGroupAction_Actioncardinalitybehavior
        {
            Single = 935000000,
            Multiple = 935000001,
        }

        public enum RequestGroupAction_Actionrequiredbehavior
        {
            Must = 935000000,
            Could = 935000001,
            MustUnlessDocumented = 935000002,
        }

        public enum RequestGroupAction_Actiontype
        {
            Create = 935000000,
            Update = 935000001,
            Remove = 935000002,
            FireEvent = 935000003,
        }

        public enum SpecimenContainer_Containeradditivetype
        {
            CodeableConcept = 935000000,
            Reference = 935000001,
        }

        public enum ReferralRequest_Priority
        {
            Routine = 935000000,
            Urgent = 935000001,
            Stat = 935000003,
            Asap = 935000002,
        }

        public enum ReferralRequest_Subject
        {
            Patient = 935000000,
            Group = 935000001,
        }

        public enum ReferralRequest_Intent
        {
            Proposal = 935000000,
            Plan = 935000001,
            Order = 935000002,
        }

        public enum ReferralRequest_Contexttype
        {
            Encounter = 935000000,
            EpisodeofCare = 935000001,
        }

        public enum ReferralRequest_Requesteragent
        {
            Practitioner = 935000000,
            Organization = 935000001,
            Patient = 935000002,
            RelatedPerson = 935000003,
            Device = 935000004,
        }

        public enum ReferralRequest_Occurrencetype
        {
            Date = 935000000,
            Period = 935000001,
        }

        public enum ReferralRequest_Status
        {
            Draft = 935000000,
            Active = 935000001,
            Suspended = 935000002,
            Cancelled = 935000003,
            Completed = 935000004,
            EnteredInError = 935000005,
            Unknown = 935000006,
        }

        public enum EpisodeofCare_Status
        {
            Planned = 935000000,
            Waitlist = 935000001,
            Active = 935000002,
            OnHold = 935000003,
            Finished = 935000004,
            Cancelled = 935000005,
            EnteredinError = 935000006,
        }

        public enum RequestGroupActionRelatedAction_Relatedactionoffsettype
        {
            Range = 935000000,
            Duration = 935000001,
        }

        public enum RequestGroupActionRelatedAction_Relatedactionrelationship
        {
            after = 935000007,
            afterend = 935000008,
            afterstart = 935000006,
            before = 935000001,
            beforeend = 935000002,
            beforestart = 935000000,
            concurrent = 935000004,
            concurrentwithend = 935000005,
            concurrentwithstart = 935000003,
        }

        public enum Dosage_Rateratioquantitynumeratorcomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThanOrEqual = 935000002,
            GreaterThan = 935000003,
        }

        public enum Dosage_Ratetype
        {
            Ratio = 935000000,
            Quantity = 935000001,
            Range = 935000002,
        }

        public enum Dosage_Maxdoseperiodratioqtynumeratorcomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThanOrEqual = 935000002,
            GreaterThan = 935000003,
        }

        public enum Dosage_Dosetype
        {
            Range = 935000000,
            Quantity = 935000001,
        }

        public enum Dosage_Rateratioquantitydenominatorcomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThanOrEqual = 935000002,
            GreaterThan = 935000003,
        }

        public enum Dosage_Asneededtype
        {
            Boolean = 935000000,
            CodeableConcept = 935000001,
        }

        public enum Dosage_Maxdoseprdratioqtydenominatorcomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThanOrEqual = 935000002,
            GreaterThan = 935000003,
        }

        public enum TimingDayOfWeek_Repeatdayofweek
        {
            Mon = 935000000,
            Tue = 935000001,
            Wed = 935000002,
            Thu = 935000003,
            Fri = 935000004,
            Sat = 935000005,
            Sun = 935000006,
        }

        public enum ObservationComponent_Valuetype
        {
            Quantity = 935000000,
            CodeableConcept = 935000001,
            String = 935000002,
            Boolean = 935000003,
            Range = 935000004,
            Ratio = 935000005,
            SampleData = 935000006,
            Attachment = 935000007,
            Time = 935000008,
            DateTime = 935000009,
            Period = 935000010,
        }

        public enum ObservationComponent_Valuerationumeratorcomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThan = 935000002,
            GreaterThanOrEqual = 935000003,
        }

        public enum ObservationComponent_Valueratiodenominatorcomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThan = 935000002,
            Item = 935000003,
        }

        public enum ObservationComponent_Valuequantitycomparator
        {
            LessThan = 935000000,
            LessThanOrEqual = 935000001,
            GreaterThanOrEqual = 935000002,
            GreaterThan = 935000003,
        }

        public enum LocationTelecom_Locationtelecomsystem
        {
            Phone = 935000000,
            Fax = 935000001,
            Email = 935000002,
            Pager = 935000003,
            URL = 935000004,
            SMS = 935000005,
            Other = 935000006,
        }

        public enum LocationTelecom_Locationtelecomuse
        {
            Home = 935000000,
            Work = 935000001,
            Temp = 935000002,
            Old = 935000003,
            Mobile = 935000004,
        }

        public enum GoalAddresses_Addressestype
        {
            Condition = 935000000,
            Observation = 935000001,
            MedicationStatement = 935000002,
            NutritionOrder = 935000003,
            ProcedureRequest = 935000004,
            RiskAssessment = 935000005,
        }

        public enum AdditionalName_Nameuse
        {
            Usual = 935000000,
            Official = 935000001,
            Temp = 935000002,
            Nick = 935000003,
            Maiden = 935000004,
            Old = 935000005,
            Anonymus = 935000006,
        }

        public enum ObservationBasedOn_Obsbasedontype
        {
            CarePlan = 935000000,
            DeviceRequest = 935000001,
            ImmunizationRecommendation = 935000002,
            MedicationRequest = 935000003,
            NutritionOrder = 935000004,
            ProcedureRequest = 935000005,
            ReferralRequest = 935000006,
        }

        public enum MedicationAdministrationPerformer_Performeractortype
        {
            Practitioner = 935000000,
            Patient = 935000001,
            RelatedPerson = 935000002,
            Device = 935000003,
        }

        public static int RandomEnumInt<T>()
        {
            Type type = typeof(T);
            Array values = Enum.GetValues(type);
            lock (RNG)
            {
                object value = values.GetValue(RNG.Next(values.Length));
                return (int)Convert.ChangeType(value, type);
            }
        }

        public static T RandomEnumStr<T>()
        {
            Type type = typeof(T);
            Array values = Enum.GetValues(type);
            lock (RNG)
            {
                object value = values.GetValue(RNG.Next(values.Length));
                return (T)Convert.ChangeType(value, type);
            }
        }
    }
}
