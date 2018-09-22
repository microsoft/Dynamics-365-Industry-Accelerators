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
using System.Text;
using System.Threading.Tasks;

namespace CDM.HealthAccelerator.DataModel
{
    public static class SampleDataCache
    {
        public class RandomDateTime
        {
            DateTime start;
            Random gen;
            int range;

            public RandomDateTime(int year, int day, int month, DateTime startDate)
            {
                start = new DateTime(year, month, day);
                gen = new Random();
                range = (startDate - start).Days;
            }

            public DateTime Next()
            {
                return start.AddDays(gen.Next(range)).AddHours(gen.Next(0, 24)).AddMinutes(gen.Next(0, 60)).AddSeconds(gen.Next(0, 60));
            }

            public DateTime AddYears(DateTime currentdt, int min, int max)
            {
                return currentdt.AddYears(gen.Next(min, max));
            }
        }

        /// <summary>
        /// Use this to help generate fake information for a contact
        /// </summary>
        public static readonly Random RandomContactGenerator = new Random();
        /// <summary>
        /// this is used to randomize data for these fields
        /// the address line is not included but this data is real from census
        /// data
        /// </summary>
        internal class AddressInfo
        {

            public AddressInfo(string Country)
            {
                this.Country = Country;

                this.Telephone = RandomContactGenerator.Next(1, 9).ToString()
                                 + RandomContactGenerator.Next(1, 9).ToString()
                                 + RandomContactGenerator.Next(1, 9).ToString() + "-"
                                 + RandomContactGenerator.Next(1, 9).ToString()
                                 + RandomContactGenerator.Next(1, 9).ToString()
                                 + RandomContactGenerator.Next(1, 9).ToString()
                                 + RandomContactGenerator.Next(1, 9).ToString();

                this.Address = RandomContactGenerator.Next(1, 9).ToString()
                               + RandomContactGenerator.Next(1, 9).ToString()
                               + RandomContactGenerator.Next(1, 9).ToString()
                               + RandomContactGenerator.Next(1, 9).ToString() + " "
                               + Lastnames[RandomContactGenerator.Next(0, Lastnames.Count - 1)];

                int streettype = RandomContactGenerator.Next(1, 100);

                if (streettype < 25)
                {
                    this.Address += " Lane";
                }
                else if (streettype > 25 && streettype < 50)
                {
                    this.Address += " Drive";
                }
                else if (streettype > 50 && streettype < 75)
                {
                    this.Address += " Street";
                }
                else if (streettype > 75)
                {
                    this.Address += " Court";
                }

            }

            /// <summary>
            /// the zip
            /// </summary>
            private string zipcode;

            /// <summary>
            /// the city
            /// </summary>
            private string city;

            /// <summary>
            /// the state
            /// </summary>
            private string state;

            /// <summary>
            /// the areacode
            /// </summary>
            private string areacode;

            /// <summary>
            /// the lat
            /// </summary>
            private string latitude;

            /// <summary>
            /// the long
            /// </summary>
            private string longitude;

            /// <summary>
            /// the country
            /// </summary>
            private string country;

            /// <summary>
            /// the telephone
            /// </summary>
            private string telephone;

            /// <summary>
            /// the address
            /// </summary>
            private string address;

            public string Zipcode
            {
                get
                {
                    return this.zipcode;
                }

                set
                {
                    this.zipcode = value;
                }
            }

            public string City
            {
                get
                {
                    return this.city;
                }

                set
                {
                    this.city = value;
                }
            }

            public string State
            {
                get
                {
                    return this.state;
                }

                set
                {
                    this.state = value;
                }
            }

            public string Areacode
            {
                get
                {
                    return this.areacode;
                }

                set
                {
                    this.areacode = value;
                }
            }

            public string Latitude
            {
                get
                {
                    return this.latitude;
                }

                set
                {
                    this.latitude = value;
                }
            }

            public string Longitude
            {
                get
                {
                    return this.longitude;
                }

                set
                {
                    this.longitude = value;
                }
            }

            public string Country
            {
                get
                {
                    return this.country;
                }

                set
                {
                    this.country = value;
                }
            }

            public string Telephone
            {
                get
                {
                    return this.telephone;
                }

                set
                {
                    this.telephone = value;
                }
            }

            /// <summary>
            /// the address
            /// </summary>
            public string Address
            {
                get
                {
                    return this.address;
                }

                set
                {
                    this.address = value;
                }
            }
        }

        // These are resources to use for randomly creating CDS resources
        internal static readonly List<string> ReferralRequests = new List<string>();

        internal static readonly List<string> Devices = new List<string>();

        internal static readonly List<string> Malenames = new List<string>();

        internal static readonly List<string> Femalenames = new List<string>();

        internal static readonly List<string> Lastnames = new List<string>();

        internal static readonly List<string> PractitionerRoles = new List<string>();

        internal static readonly List<string> PractitionerQualifications = new List<string>();

        internal static readonly List<string> PractitionerSalutations = new List<string>();

        internal static readonly List<AddressInfo> AddressInfos = new List<AddressInfo>();

        internal static readonly List<string> RelatedPersonTypes = new List<string>();

        internal static readonly List<string> Languages = new List<string>();

        internal static readonly List<string> AllergyItems = new List<string>();

        internal static readonly List<string> NutrionOrders = new List<string>();

        internal static readonly List<string> Conditions = new List<string>();

        internal static readonly List<string> Procedures = new List<string>();

        internal static readonly List<string> Medications = new List<string>();
        
        internal static bool CacheInitted = false;
        internal static object cachelock = new object();

        /// <summary>
        /// this is used to initialize our fake data and create it
        /// </summary>
        internal static void InitializeFakeDataHelpers()
        {
            lock (cachelock)
            {
                if (!CacheInitted)
                {

                    string localpath = AppDomain.CurrentDomain.BaseDirectory;

                    if (!localpath.EndsWith(@"\"))
                    {
                        localpath += @"\";
                    }

                    using (TextReader reader = File.OpenText(localpath + @"resourcefile\referralrequests.txt"))
                    {
                        while (true)
                        {
                            string request = reader.ReadLine();

                            if (!string.IsNullOrEmpty(request))
                            {
                                ReferralRequests.Add(request);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    using (TextReader reader = File.OpenText(localpath + @"resourcefile\medications.txt"))
                    {
                        while (true)
                        {
                            string medication = reader.ReadLine();

                            if (!string.IsNullOrEmpty(medication))
                            {
                                 Medications.Add(medication);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    using (TextReader reader = File.OpenText(localpath + @"resourcefile\procedures.txt"))
                    {
                        while (true)
                        {
                            string procedure = reader.ReadLine();

                            if (!string.IsNullOrEmpty(procedure))
                            {
                                Procedures.Add(procedure);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    using (TextReader reader = File.OpenText(localpath + @"resourcefile\conditions.txt"))
                    {
                        while (true)
                        {
                            string condition = reader.ReadLine();

                            if (!string.IsNullOrEmpty(condition))
                            {
                                Conditions.Add(condition);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    using (TextReader reader = File.OpenText(localpath + @"resourcefile\devices.txt"))
                    {
                        while (true)
                        {
                            string device = reader.ReadLine();

                            if (!string.IsNullOrEmpty(device))
                            {
                                Devices.Add(device);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    using (TextReader reader = File.OpenText(localpath + @"resourcefile\nutrition_orders.txt"))
                    {
                        while (true)
                        {
                            string nutritionorder = reader.ReadLine();

                            if (!string.IsNullOrEmpty(nutritionorder))
                            {
                                NutrionOrders.Add(nutritionorder);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    using (TextReader reader = File.OpenText(localpath + @"resourcefile\allergy_item.txt"))
                    {
                        while (true)
                        {
                            string allergytime = reader.ReadLine();

                            if (!string.IsNullOrEmpty(allergytime))
                            {
                                AllergyItems.Add(allergytime);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    //// First read in our female names
                    using (TextReader reader = File.OpenText(localpath + @"resourcefile\practitioner_qualifications.txt"))
                    {
                        while (true)
                        {
                            string qualification = reader.ReadLine();

                            if (!string.IsNullOrEmpty(qualification))
                            {
                                PractitionerQualifications.Add(qualification);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    using (TextReader reader = File.OpenText(localpath + @"resourcefile\practitioner_roles.txt"))
                    {
                        while (true)
                        {
                            string role = reader.ReadLine();

                            if (!string.IsNullOrEmpty(role))
                            {
                                PractitionerRoles.Add(role);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    using (TextReader reader = File.OpenText(localpath + @"resourcefile\practitioner_salutations.txt"))
                    {
                        while (true)
                        {
                            string saluation = reader.ReadLine();

                            if (!string.IsNullOrEmpty(saluation))
                            {
                                PractitionerSalutations.Add(saluation);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }
                    using (TextReader reader = File.OpenText(localpath + @"resourcefile\related_person_types.txt"))
                    {
                        while (true)
                        {
                            string relatedperson = reader.ReadLine();

                            if (!string.IsNullOrEmpty(relatedperson))
                            {
                                RelatedPersonTypes.Add(relatedperson);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    //// First read in our female names
                    using (TextReader reader = File.OpenText(localpath + @"resourcefile\female_names.txt"))
                    {
                        while (true)
                        {
                            string femalename = reader.ReadLine();

                            if (!string.IsNullOrEmpty(femalename))
                            {
                                Femalenames.Add(femalename);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    using (TextReader reader = File.OpenText(localpath + @"resourcefile\languages.txt"))
                    {
                        while (true)
                        {
                            string language = reader.ReadLine();

                            if (!string.IsNullOrEmpty(language))
                            {
                                Languages.Add(language);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    //// the first names
                    using (TextReader reader = File.OpenText(localpath + @"resourcefile\male_names.txt"))
                    {
                        while (true)
                        {
                            string malename = reader.ReadLine();

                            if (!string.IsNullOrEmpty(malename))
                            {
                                Malenames.Add(malename);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    //// no our last names
                    using (TextReader reader = File.OpenText(localpath + @"resourcefile\last_names.txt"))
                    {
                        while (true)
                        {
                            string lastname = reader.ReadLine();

                            if (!string.IsNullOrEmpty(lastname))
                            {
                                Lastnames.Add(lastname);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    //// now our addressInfos, area codes etc
                    using (TextReader reader = File.OpenText(localpath + @"resourcefile\zip_code_database.txt"))
                    {
                        while (true)
                        {
                            string lineinfo = reader.ReadLine();

                            if (!string.IsNullOrEmpty(lineinfo))
                            {
                                string[] items = lineinfo.Split('\t');

                                AddressInfo ai = new AddressInfo("US");

                                ai.Areacode = FixAreaCode(items[3]);

                                ai.Country = "US";
                                ai.City = items[1];
                                ai.Zipcode = FixZipCode(items[0]);
                                ai.Latitude = items[4];
                                ai.Longitude = items[5];
                                ai.State = items[2];

                                AddressInfos.Add(ai);
                            }
                            else
                            {
                                break;
                            }
                        }
                    }

                    CacheInitted = true;
                }
            }
        }

        /// <summary>
        /// Sometimes the zipecode in the list is actually.. only 3/4 so the trailing zeros are gone i need to add them back
        /// </summary>
        /// <param name="zipcode">Test</param>
        /// <returns>dd</returns>
        internal static string FixZipCode(string zipcode)
        {
            try
            {
                if (zipcode.Length < 5)
                {
                    int length = zipcode.Length;

                    int difference = 5 - length;

                    string prefixzeros = string.Empty;

                    for (int i = 0; i < difference; i++)
                    {
                        prefixzeros += "0";
                    }

                    zipcode = prefixzeros + zipcode;
                }

                return zipcode;
            }
            catch (Exception)
            {
                return zipcode;
            }
        }

        /// <summary>
        /// Sometimes the area code has more than 1 listed so I have to parse it
        /// </summary>
        /// <param name="areacode">Asd</param>
        /// <returns>Yes</returns>
        internal static string FixAreaCode(string areacode)
        {
            try
            {
                if (areacode.Contains("\""))
                {
                    //// first get rid of the " which were in it
                    areacode = areacode.Replace("\"", "");

                    string[] items = areacode.Split(',');

                    areacode = items[0];
                }

                return areacode;
            }
            catch (Exception)
            {
                return areacode;
            }
        }
    }
}
