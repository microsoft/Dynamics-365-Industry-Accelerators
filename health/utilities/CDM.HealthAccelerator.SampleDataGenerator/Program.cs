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
using System.Configuration;

using CDM.HealthAccelerator.DataModel;

namespace CDM.HealthAccelerator.GenerateSampleData
{
    class Program
    {
        static void Main(string[] args)
        {
            int patientcount = int.Parse(ConfigurationManager.AppSettings["createpatientcount"]);
            int practitionercount = int.Parse(ConfigurationManager.AppSettings["createpractitionercount"]);
            int relatedpersoncount = int.Parse(ConfigurationManager.AppSettings["createrelatedpersoncount"]);
            int contactcount = int.Parse(ConfigurationManager.AppSettings["createcontactcount"]);

            int practitionerrolecount = int.Parse(ConfigurationManager.AppSettings["practitionerrolecount"]);
            int practitionerqualificationcount = int.Parse(ConfigurationManager.AppSettings["practitionerqualificationcount"]);

            int patientallergycount = int.Parse(ConfigurationManager.AppSettings["patientallergycount"]);
            int patientnutritionordercount = int.Parse(ConfigurationManager.AppSettings["patientnutritionordercount"]);
            int patientconditioncount = int.Parse(ConfigurationManager.AppSettings["patientconditioncount"]);
            int patientdevicecount = int.Parse(ConfigurationManager.AppSettings["patientdevicecount"]);
            int patientprocedurecount = int.Parse(ConfigurationManager.AppSettings["patientprocedurecount"]);
            int patientreferralcount = int.Parse(ConfigurationManager.AppSettings["patientreferralcount"]);
            int patientmedicationrequestcount = int.Parse(ConfigurationManager.AppSettings["patientmedicationrequestcount"]);

            bool createpractitionersrole = bool.Parse(ConfigurationManager.AppSettings["createpractitionersrole"]);

            string filepath = ConfigurationManager.AppSettings["filepath"];

            Console.WriteLine("Start Time: " + DateTime.Now.ToString());

            List<Profile> localcontacts = null;
            List<Profile> localpatients = null;
            List<Profile> localpractitioners = null;
            List<Profile> localrelatedpersons = null;

            string practitonerFile = string.Empty;
            string relatedpersonsFile = string.Empty;
            string patientsFile = string.Empty;

            #region Create Standard Contancts 

            if (contactcount > 0)
            {
                CreateCDMHealthData createContacts = new CreateCDMHealthData();
                createContacts.ContactType = Profile.ContactType.Standard;
                createContacts.FileName = filepath + "relatedpersons_" + relatedpersoncount.ToString() + "_" + Guid.NewGuid().ToString() + ".tab";

                localcontacts = Contact.GenerateProfilesByCount(contactcount, "NA");

                foreach (Profile contact in localcontacts)
                {
                    createContacts.IncomingContacts.Enqueue(contact);
                }

                createContacts.CreateCount = createContacts.IncomingContacts.Count;
                createContacts.EmailDomain = ConfigurationManager.AppSettings["emaildomain"];
                createContacts.Clients = int.Parse(ConfigurationManager.AppSettings["clients"]); //;

                Console.WriteLine("\r\nCreating [" + createContacts.CreateCount.ToString() + "]  Contacts\r\n");

                practitonerFile = createContacts.CreateContacts();
            }

            #endregion

            #region Create Practitioners 

            if (practitionercount > 0)
            {
                PractitionerConfiguration configuration = new PractitionerConfiguration();
                configuration.Qualifications = practitionerqualificationcount;
                configuration.Roles = practitionerrolecount;

                CreateCDMHealthData createPractitioners = new CreateCDMHealthData();
                createPractitioners.ContactType = Profile.ContactType.Practitioner;
                createPractitioners.FileName = filepath + "practitioners_" + practitionercount.ToString() + "_" + Guid.NewGuid().ToString() + ".json";

                localpractitioners = Practitioner.GenerateProfilesByCount(practitionercount, configuration);

                foreach (Profile practitioner in localpractitioners)
                {
                    createPractitioners.IncomingContacts.Enqueue(practitioner);
                }

                createPractitioners.CreateCount = createPractitioners.IncomingContacts.Count;
                createPractitioners.EmailDomain = ConfigurationManager.AppSettings["emaildomain"];
                createPractitioners.Clients = int.Parse(ConfigurationManager.AppSettings["clients"]); //;

                Console.WriteLine("\r\nCreating [" + createPractitioners.CreateCount.ToString() + "]  Practitioners\r\n");

                practitonerFile = createPractitioners.CreateContacts();
            }

            #endregion

            #region Create Related Persons
            if (relatedpersoncount > 0)
            {
                CreateCDMHealthData createRelatedPersons = new CreateCDMHealthData();
                createRelatedPersons.FileName = filepath + "relatedpersons_" + relatedpersoncount.ToString() + "_" + Guid.NewGuid().ToString() + ".json";
                createRelatedPersons.ContactType = Profile.ContactType.RelatedPerson;

                localrelatedpersons = RelatedPerson.GenerateProfilesByCount(relatedpersoncount, "NA");

                foreach (Profile relatedperson in localrelatedpersons)
                {
                    createRelatedPersons.IncomingContacts.Enqueue(relatedperson);
                }

                createRelatedPersons.CreateCount = createRelatedPersons.IncomingContacts.Count;
                createRelatedPersons.EmailDomain = ConfigurationManager.AppSettings["emaildomain"];
                createRelatedPersons.Clients = int.Parse(ConfigurationManager.AppSettings["clients"]); //;

                Console.WriteLine("\r\nCreating [" + createRelatedPersons.CreateCount.ToString() + "]  Related Persons\r\n");

                relatedpersonsFile = createRelatedPersons.CreateContacts();
            }

            #endregion

            #region Create Patients

            if (patientcount > 0)
            {
                PatientConfiguration configuration = new PatientConfiguration();
                configuration.PractionerFileName = practitonerFile;
                configuration.RelatedPersonsFileName = relatedpersonsFile;
                configuration.AllergyIntoleranceCount = patientallergycount;
                configuration.NutritionOrderCount = patientnutritionordercount;
                configuration.ConditionCount = patientconditioncount;
                configuration.DeviceCount = patientdevicecount;
                configuration.ProcedureCount = patientprocedurecount;
                configuration.MedicationCount = patientmedicationrequestcount;

                CreateCDMHealthData createPatients = new CreateCDMHealthData();
                createPatients.FileName = filepath + "patients_" + patientcount.ToString() + "_" + Guid.NewGuid().ToString() + ".json";
                createPatients.ContactType = Profile.ContactType.Patient;

                localpatients = Patient.GenerateProfilesByCount(patientcount, configuration);

                foreach (Profile patient in localpatients)
                {
                    createPatients.IncomingContacts.Enqueue(patient);
                }

                createPatients.CreateCount = createPatients.IncomingContacts.Count;
                createPatients.EmailDomain = ConfigurationManager.AppSettings["emaildomain"];
                createPatients.Clients = int.Parse(ConfigurationManager.AppSettings["clients"]); //;

                Console.WriteLine("\r\nCreating [" + createPatients.CreateCount.ToString() + "]  Patients\r\n");

                patientsFile = createPatients.CreateContacts();
            }

            #endregion

            Console.WriteLine("End Time: " + DateTime.Now.ToString());

            return;
        }
    }
}
