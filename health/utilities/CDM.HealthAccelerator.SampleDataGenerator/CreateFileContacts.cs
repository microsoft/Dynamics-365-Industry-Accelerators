using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDM.HealthAccelerator.GenerateSampleData
{
    public class CreateFileContacts
    {
        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }
        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }
        private string address1_Line1;

        public string Address1_Line1
        {
            get { return address1_Line1; }
            set { address1_Line1 = value; }
        }
        private string address1_City;

        public string Address1_City
        {
            get { return address1_City; }
            set { address1_City = value; }
        }
        private string address1_StateOrProvince;

        public string Address1_StateOrProvince
        {
            get { return address1_StateOrProvince; }
            set { address1_StateOrProvince = value; }
        }
        private string telephone1;

        public string Telephone1
        {
            get { return telephone1; }
            set { telephone1 = value; }
        }
        private string emailAddress1;

        public string EmailAddress1
        {
            get { return emailAddress1; }
            set { emailAddress1 = value; }
        }

        private string address1_postalCode;

        public string Address1_postalCode
        {
            get { return address1_postalCode; }
            set { address1_postalCode = value; }
        }

        private int createCount;

        public int CreateCount
        {
            get { return createCount; }
            set { createCount = value; }
        }
        private int seed;

        public int Seed
        {
            get { return seed; }
            set { seed = value; }
        }

        private string fileShare;

        public string FileShare
        {
            get { return fileShare; }
            set { fileShare = value; }
        }

        private string fileName;

        public string FileName
        {
            get { return fileName; }
            set { fileName = value; }
        }

        public void CreateContacts()
        {
            try
            {
                // Check if file already exists. If yes, delete it. 

                if (File.Exists(this.FileShare + "\\" + this.FileName))
                {
                    File.Delete(this.FileShare + "\\" + this.FileName);
                }

                // Create a new file 
                using (StreamWriter ts = File.CreateText(this.FileShare + "\\" + this.FileName))
                {
                    //add header line
                    string header = string.Empty;
                    header += "FirstName,LastName,Address1_Line1,Address1_City,Address1_StateOrProvince,Address1_PostalCode,Telephone1,EmailAddress1";

                    ts.WriteLine(header);

                    for (int i = 1; i <= this.CreateCount; i++)
                    {
                        string line = string.Empty;
                        line += this.FirstName + "_" + i.ToString() + ",";
                        line += this.LastName + ",";
                        line += this.Address1_Line1 + ",";
                        line += this.Address1_City + ",";
                        line += this.address1_StateOrProvince + ",";
                        line += this.Address1_postalCode + ",";
                        line += this.Telephone1 + ",";
                        line += this.FirstName + "_" + i.ToString() + this.EmailAddress1 + ",";

                        ts.WriteLine(line);

                        Console.WriteLine("Created Line [" + i.ToString() + "]");
                    }
                }

                return;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

    }
}
