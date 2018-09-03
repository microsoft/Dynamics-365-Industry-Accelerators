using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Sdk.Client;
using Microsoft.Xrm.Sdk.Messages;
using Microsoft.Xrm.Sdk.Metadata;
using Microsoft.Xrm.Sdk.Query;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Dynamics.Health.ExportOptionSetValues
{
    class Program
    {
        private static OrganizationServiceProxy _serviceProxy;
        private static List<string> picklistmappingdata = new List<string>();
        private static int totalOptions = 0;
        private static List<PickListEnums> enums = new List<PickListEnums>();
        private static string newNumerations = string.Empty;

        static void Main(string[] args)
        {
            string solutionName = "Dynamics365ElectronicMedicalRecords";

            // this is a sample utility
            // you should not store your password and information directly in this file
            // but use this only for an as-is sample
            Uri OrganizationUri = new Uri("https://healthacceleratordevmgernfull.crm.dynamics.com/XRMServices/2011/Organization.svc");
            Uri HomeRealmUri = null;

            ClientCredentials Credentials = null;
            ClientCredentials DeviceCredentials = null;

            // this is a sample utility
            // you should not store your password and information directly in this file
            // but use this only for an as-is sample
            Credentials = new ClientCredentials();
            Credentials.UserName.UserName = "michael@msdynaccelerators.onmicrosoft.com";
            Credentials.UserName.Password = "Kendra#2018";

            // we need this to support communicating with Dynamics Online Instances 
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (_serviceProxy = new OrganizationServiceProxy(OrganizationUri, HomeRealmUri, Credentials, DeviceCredentials))
            {
                // This statement is required to enable early-bound type support.
                _serviceProxy.EnableProxyTypes();

                IEnumerable<EntityMetadata> entities = getSolutionEntities(solutionName, _serviceProxy);

                foreach(EntityMetadata em in entities)
                {
                    foreach (AttributeMetadata am in em.Attributes)
                    {
                        if (am.AttributeType == AttributeTypeCode.Picklist)
                        {
                            if ((em.LogicalName.ToLower().StartsWith("msemr_")) || (am.LogicalName.ToLower().StartsWith("msemr_")))
                            {
                                PickListEnums ple = new PickListEnums();
                                ple = GetOptionPickListValues(em.LogicalName, em.DisplayName.UserLocalizedLabel.Label.ToString(), am.LogicalName, _serviceProxy);

                                enums.Add(ple);

                                newNumerations += ple.GenerateEnum();
                            }
                        }
                    }
                }

                string picklistmappingfilename = @"\healthcdm_picklist_values.csv";

                string picklistmappingfilepath = AppDomain.CurrentDomain.BaseDirectory.EndsWith(@"\") ?
                                        AppDomain.CurrentDomain.BaseDirectory + "data":
                                        AppDomain.CurrentDomain.BaseDirectory + @"\data";

                string enumfilepath = AppDomain.CurrentDomain.BaseDirectory.EndsWith(@"\") ?
                                        AppDomain.CurrentDomain.BaseDirectory + "data" :
                                        AppDomain.CurrentDomain.BaseDirectory + @"\data";

                string picklistmappingfile = picklistmappingfilepath + picklistmappingfilename;
                string enumfile = enumfilepath + @"\enums.cs";

                // if we don't have any don't write anything
                if (picklistmappingdata.Count > 0)
                {
                    File.Delete(picklistmappingfile); // Remove this line if you don't want to erase the current version
                    File.WriteAllLines(picklistmappingfile, picklistmappingdata);
                }
                else
                {
                    Console.WriteLine("Could not find any Codeable Concept Types");
                }

                File.Delete(enumfile); // Remove this line if you don't want to erase the current version
                File.WriteAllText(enumfile, newNumerations);
            }
        }
        public static IEnumerable<EntityMetadata> getSolutionEntities(string SolutionUniqueName, IOrganizationService Service)
        {
            // get solution components for solution unique name
            QueryExpression componentsQuery = new QueryExpression
            {
                EntityName = "solutioncomponent",
                ColumnSet = new ColumnSet("objectid"),
                Criteria = new FilterExpression(),
            };

            LinkEntity solutionLink = new LinkEntity("solutioncomponent", "solution", "solutionid", "solutionid", JoinOperator.Inner);
            solutionLink.LinkCriteria = new FilterExpression();
            solutionLink.LinkCriteria.AddCondition(new ConditionExpression("uniquename", ConditionOperator.Equal, SolutionUniqueName));

            componentsQuery.LinkEntities.Add(solutionLink);
            componentsQuery.Criteria.AddCondition(new ConditionExpression("componenttype", ConditionOperator.Equal, 1));

            EntityCollection ComponentsResult = Service.RetrieveMultiple(componentsQuery);
            
            //Get all entities
            RetrieveAllEntitiesRequest AllEntitiesrequest = new RetrieveAllEntitiesRequest()
            {
                EntityFilters = EntityFilters.Entity | Microsoft.Xrm.Sdk.Metadata.EntityFilters.Attributes,
                RetrieveAsIfPublished = true
            };
            RetrieveAllEntitiesResponse AllEntitiesresponse = (RetrieveAllEntitiesResponse)Service.Execute(AllEntitiesrequest);
            //Join entities Id and solution Components Id 
            return AllEntitiesresponse.EntityMetadata.Join(ComponentsResult.Entities.Select(x => x.Attributes["objectid"]), x => x.MetadataId, y => y, (x, y) => x);
        }

        public static PickListEnums GetOptionPickListValues(string entityLogicalName, string entityDisplayName, string attributeName, IOrganizationService _serviceProxy)
        {
            // set the properties to get back the codeable concept picklist values
            // from the picklist (optionset)
            RetrieveAttributeRequest retrieveEntityRequest = new RetrieveAttributeRequest
            {
                EntityLogicalName = entityLogicalName,
                LogicalName = attributeName,
                RetrieveAsIfPublished = true
            };

            // execute the call and retrieve the actual metadata directly
            RetrieveAttributeResponse retrieveAttributeResponse = (RetrieveAttributeResponse)_serviceProxy.Execute(retrieveEntityRequest);
            var attributeMetadata = (EnumAttributeMetadata)retrieveAttributeResponse.AttributeMetadata;

            // retrieve each option list item 
            var optionList = (from o in attributeMetadata.OptionSet.Options
                              select new { Value = o.Value, Text = o.Label.UserLocalizedLabel.Label }).ToList();

            // iterate through each option and write out the value and text
            // this will enable us to map the "Text" value given to us in the
            // codeable concepts file that we generated OR if you generate some new ones
            // you can run this program again to create the mapping file
            // or at least have the copy that was generated for you

            PickListEnums ple = new PickListEnums();

            ple.AttributeLogicalName = attributeName;
            ple.EntityDisplayName = entityDisplayName;
            ple.EntityLogicalName = entityLogicalName;

            ple.EnumName = GenerateEnumName(entityDisplayName, attributeName);

            foreach (var option in optionList)
            {
                totalOptions++;
                PickListValues plv = new PickListValues();

                // add to our stirng list of options
                picklistmappingdata.Add(entityDisplayName + "," + entityLogicalName + "," + attributeName + "," + option.Text.Trim() + "," + option.Value.ToString().Trim()); //default to first string of the label

                plv.PickListDisplay = option.Text.Trim();
                plv.PickListValue = option.Value.ToString().Trim();

                ple.EnumValues.Add(option.Text.Trim(), plv);

                // write out our option count
                Console.SetCursorPosition(0, Console.CursorTop);
                Console.Write("Total Options Found [" + totalOptions.ToString() + "]");

                System.Threading.Thread.Sleep(1);
            }

            return ple;
        }

        public static string GenerateEnumName(string entityDisplayName, string attributeLogicalName)
        {
            string entityPart = entityDisplayName.Replace(" ", "");
            string attributePart = attributeLogicalName.Replace("msemr_", "");

            attributePart = attributePart.First().ToString().ToUpper() + attributePart.Substring(1);

            return entityPart + "_" + attributePart;
        }
    }
}
