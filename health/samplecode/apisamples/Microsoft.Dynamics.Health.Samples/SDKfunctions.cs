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


using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace Microsoft.Dynamics.Health.Samples
{
    public class SDKFunctions
    {
        public static Guid GetLocationId(IOrganizationService _service, string name)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("msemr_location");

                query.ColumnSet = new ColumnSet("msemr_locationid");

                query.Criteria.AddCondition("msemr_name", ConditionOperator.Equal, name);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("msemr_locationid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["msemr_locationid"]);
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Guid GetReferralRequestId(IOrganizationService _service, string name)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("msemr_referralrequest");

                query.ColumnSet = new ColumnSet("msemr_referralrequestid");

                query.Criteria.AddCondition("msemr_referralrequestnumber", ConditionOperator.Equal, name);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("msemr_referralrequestid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["msemr_referralrequestid"]);
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }



        // COMPLETE THESE -- STUBS FOR REAL CODE
        public static Guid GetActivityDefinitionId(IOrganizationService _service, string name)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("msemr_identifiesspecifictimeswhentheeventoccu");

                query.ColumnSet = new ColumnSet("msemr_identifiesspecifictimeswhentheeventoccuid");

                query.Criteria.AddCondition("msemr_name", ConditionOperator.Equal, name);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("msemr_identifiesspecifictimeswhentheeventoccuid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["msemr_identifiesspecifictimeswhentheeventoccuid"]);
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Guid GetBookingStatusId(IOrganizationService _service, string name)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("bookingstatus");

                query.ColumnSet = new ColumnSet("bookingstatusid");

                query.Criteria.AddCondition("name", ConditionOperator.Equal, name);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("bookingstatusid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["bookingstatusid"]);
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Guid GetSpecimenId(IOrganizationService _service, string name)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("msemr_specimen");

                query.ColumnSet = new ColumnSet("msemr_specimenid");

                query.Criteria.AddCondition("msemr_specimennumber", ConditionOperator.Equal, name);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("msemr_specimenid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["msemr_specimenid"]);
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Guid GetAppointmentTypeId(IOrganizationService _service, string name)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("msdyn_workordertype");

                query.ColumnSet = new ColumnSet("msdyn_workordertypeid");

                query.Criteria.AddCondition("msdyn_name", ConditionOperator.Equal, name);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("msdyn_workordertypeid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["msdyn_workordertypeid"]);
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Guid GetDeviceMetricId(IOrganizationService _service, string name)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("msemr_devicemetric");

                query.ColumnSet = new ColumnSet("msemr_devicemetricid");

                query.Criteria.AddCondition("msemr_description", ConditionOperator.Equal, name);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("msemr_devicemetricid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["msemr_devicemetricid"]);
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Guid GetConditionId(IOrganizationService _service, string name)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("msemr_condition");

                query.ColumnSet = new ColumnSet("msemr_conditionid");

                query.Criteria.AddCondition("msemr_name", ConditionOperator.Equal, name);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("msemr_conditionid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["msemr_conditionid"]);
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Guid GetContactId(IOrganizationService _service, string name, int contactType = 0)

        {
            try
            {
                Guid id = Guid.Empty;
                QueryExpression query = new QueryExpression("contact");

                query.ColumnSet = new ColumnSet("contactid");

                if (!string.IsNullOrEmpty(name))
                {
                    query.Criteria.AddCondition("fullname", ConditionOperator.Equal, name);
                }
                if (contactType != 0)
                {
                    query.Criteria.AddCondition("msemr_contacttype", ConditionOperator.Equal, contactType);
                }

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {

                    if (entityCollection[0].Attributes.Contains("contactid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["contactid"]);
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Guid GetCodeableConceptId(IOrganizationService _service, string code, int type)
        {
            try
            {
                Guid id = Guid.Empty;

                QueryExpression query = new QueryExpression("msemr_codeableconcept");

                query.ColumnSet = new ColumnSet("msemr_codeableconceptid");

                query.Criteria.AddCondition("msemr_code", ConditionOperator.Equal, code);
                query.Criteria.AddCondition("msemr_type", ConditionOperator.Equal, type);

                EntityCollection entityCollection = _service.RetrieveMultiple(query);

                if (entityCollection != null && entityCollection.Entities != null && entityCollection.Entities.Count > 0)
                {
                    if (entityCollection[0].Attributes.Contains("msemr_codeableconceptid"))
                    {
                        id = (Guid)(entityCollection[0].Attributes["msemr_codeableconceptid"]);
                    }
                }
                return id;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
