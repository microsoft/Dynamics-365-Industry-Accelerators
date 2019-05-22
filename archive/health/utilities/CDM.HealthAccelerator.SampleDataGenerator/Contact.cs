// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Contact.cs" company="Microsoft">
//   Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// 
// // <summary>
//   Holder
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace Tools.CRM.CreateContacts
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// Holder
    /// </summary>
    [Serializable]
    public class Contact
    {
        /// <summary>
        /// Age of person
        /// </summary>
        private int age;

        /// <summary>
        /// 84664082-5bcb-41f0-9b6a-b231a12fa172
        /// </summary>
        private string contactId;


        /// <summary>
        /// 5e0a658b-cb3f-4c74-9d3f-35f089dc2168
        /// </summary>
        public string ContactId
        {
            get { return this.contactId; }
            set { this.contactId = value; }
        }


        /// <summary>
        /// 4a795bb6-e70b-468b-86f3-f22f137a7f5c
        /// </summary>
        private int accountRoleCode;


        /// <summary>
        /// 3d56610d-c572-4c0d-b348-7ff168c0e3f8
        /// </summary>
        public int AccountRoleCode
        {
            get { return this.accountRoleCode; }
            set { this.accountRoleCode = value; }
        }


        /// <summary>
        /// c9b18e8a-c351-4aaa-9887-07adc0e3b35f
        /// </summary>
        private int address1AddressTypeCode;


        /// <summary>
        /// 43517f24-818e-4b95-80a8-ca738ceac756
        /// </summary>
        public int Address1AddressTypeCode
        {
            get { return this.address1AddressTypeCode; }
            set { this.address1AddressTypeCode = value; }
        }


        /// <summary>
        /// b0f18993-cd04-4f5e-acb8-da8ebb2cf110
        /// </summary>
        private string address1City;


        /// <summary>
        /// 71715878-bb23-4a75-9ebe-34b0457ba74e
        /// </summary>
        public string Address1City
        {
            get { return this.address1City; }
            set { this.address1City = value; }
        }


        /// <summary>
        /// 5045f4d3-14bc-4b2b-a87a-cc0fd0c76bfd
        /// </summary>
        private string address1Country;


        /// <summary>
        /// 1f0f7663-1cd2-4d2d-bc39-66b9a7b6e3ce
        /// </summary>
        public string Address1Country
        {
            get { return this.address1Country; }
            set { this.address1Country = value; }
        }


        /// <summary>
        /// 071d5cce-829c-4103-b91e-eac384117f34
        /// </summary>
        private string address1County;


        /// <summary>
        /// 1cfbab11-9d6f-4f25-8761-8f7fb5e95424
        /// </summary>
        public string Address1County
        {
            get { return this.address1County; }
            set { this.address1County = value; }
        }


        /// <summary>
        /// 476437bb-04fc-4474-95f7-533847e61851
        /// </summary>
        private string address1Fax;


        /// <summary>
        /// e1432423-388a-48fa-9b3f-e98da4a73892
        /// </summary>
        public string Address1Fax
        {
            get { return this.address1Fax; }
            set { this.address1Fax = value; }
        }


        /// <summary>
        /// 73828e73-4b03-42b9-ae43-dd01fbc48067
        /// </summary>
        private int address1FreightTermsCode;


        /// <summary>
        /// 7e01c6f1-6fa8-4fd5-b47d-88607f0911da
        /// </summary>
        public int Address1FreightTermsCode
        {
            get { return this.address1FreightTermsCode; }
            set { this.address1FreightTermsCode = value; }
        }


        /// <summary>
        /// 9d6c6512-49fd-4623-9127-725ef26be9b6
        /// </summary>
        private double address1Latitude;


        /// <summary>
        /// cb53b876-5f5c-40b7-a2fc-8373495c4984
        /// </summary>
        public double Address1Latitude
        {
            get { return this.address1Latitude; }
            set { this.address1Latitude = value; }
        }


        /// <summary>
        /// cd7caa9e-899b-4c28-92d0-21c230bb801b
        /// </summary>
        private string address1Line1;


        /// <summary>
        /// 4852fe97-c81b-4834-b161-c432ecd23a4a
        /// </summary>
        public string Address1Line1
        {
            get { return this.address1Line1; }
            set { this.address1Line1 = value; }
        }


        /// <summary>
        /// b3e3ead3-a329-432e-a514-514c82824eb4
        /// </summary>
        private string address1Line2;


        /// <summary>
        /// 19bd3f17-34ad-48bd-b91e-65f888cca902
        /// </summary>
        public string Address1Line2
        {
            get { return this.address1Line2; }
            set { this.address1Line2 = value; }
        }


        /// <summary>
        /// c5c7a8bb-a7b6-41f7-84c6-4f7ae0e4fb1c
        /// </summary>
        private string address1Line3;


        /// <summary>
        /// af574907-2982-446b-989b-51bd3869ad89
        /// </summary>
        public string Address1Line3
        {
            get { return this.address1Line3; }
            set { this.address1Line3 = value; }
        }


        /// <summary>
        /// 4b3c0d49-91b3-46f9-90ca-1eabaa282c24
        /// </summary>
        private double address1Longitude;


        /// <summary>
        /// 0f6609df-5979-4b78-8282-aac492cc0d52
        /// </summary>
        public double Address1Longitude
        {
            get { return this.address1Longitude; }
            set { this.address1Longitude = value; }
        }


        /// <summary>
        /// fd18333e-81ea-4e05-9e22-df6e829c4adf
        /// </summary>
        private string address1Name;


        /// <summary>
        /// 019a902c-7385-43b8-ad4e-82edfc3b9c54
        /// </summary>
        public string Address1Name
        {
            get { return this.address1Name; }
            set { this.address1Name = value; }
        }


        /// <summary>
        /// 35a397b3-9b9d-4916-b64d-37b79e00dea8
        /// </summary>
        private string address1PostalCode;


        /// <summary>
        /// 60cf7a50-0345-40c0-8256-3238f997b26b
        /// </summary>
        public string Address1PostalCode
        {
            get { return this.address1PostalCode; }
            set { this.address1PostalCode = value; }
        }


        /// <summary>
        /// c75ee974-0b19-4b87-9698-9381e43251bd
        /// </summary>
        private string address1PostOfficeBox;


        /// <summary>
        /// cc099310-fcd3-4817-96c8-7eeced87a2cb
        /// </summary>
        public string Address1PostOfficeBox
        {
            get { return this.address1PostOfficeBox; }
            set { this.address1PostOfficeBox = value; }
        }


        /// <summary>
        /// 7eb03780-4142-4777-8dc2-192baa69b598
        /// </summary>
        private string address1PrimaryContactName;


        /// <summary>
        /// e398b223-ff0a-4776-899f-38fa5e512faa
        /// </summary>
        public string Address1PrimaryContactName
        {
            get { return this.address1PrimaryContactName; }
            set { this.address1PrimaryContactName = value; }
        }


        /// <summary>
        /// 80742cea-2d87-4369-b03d-1da4b8187091
        /// </summary>
        private int address1ShippingMethodCode;


        /// <summary>
        /// 099dce50-7517-460a-b1b2-545aa9b9f5b1
        /// </summary>
        public int Address1ShippingMethodCode
        {
            get { return this.address1ShippingMethodCode; }
            set { this.address1ShippingMethodCode = value; }
        }


        /// <summary>
        /// 7811ca27-4e44-487f-8e42-349d0070940d
        /// </summary>
        private string address1StateOrProvince;


        /// <summary>
        /// f15cb296-633e-4aaa-9ce0-94f0c75dc020
        /// </summary>
        public string Address1StateOrProvince
        {
            get { return this.address1StateOrProvince; }
            set { this.address1StateOrProvince = value; }
        }


        /// <summary>
        /// cbdb49d8-c188-4819-994a-5721485434d5
        /// </summary>
        private string address1Telephone1;


        /// <summary>
        /// 13d462ee-491f-4e0b-9097-dea200f0c4a9
        /// </summary>
        public string Address1Telephone1
        {
            get { return this.address1Telephone1; }
            set { this.address1Telephone1 = value; }
        }


        /// <summary>
        /// c95d81cf-eea2-491f-9efa-d7eae2013e96
        /// </summary>
        private string address1Telephone2;


        /// <summary>
        /// ca106d71-bb81-4c76-b1c9-b07e71bb2313
        /// </summary>
        public string Address1Telephone2
        {
            get { return this.address1Telephone2; }
            set { this.address1Telephone2 = value; }
        }


        /// <summary>
        /// e106066e-e7b6-4426-941c-865d7eb4d063
        /// </summary>
        private string address1Telephone3;


        /// <summary>
        /// bf9e4ff0-a82b-4aa6-b36b-93904957fc88
        /// </summary>
        public string Address1Telephone3
        {
            get { return this.address1Telephone3; }
            set { this.address1Telephone3 = value; }
        }


        /// <summary>
        /// aab06ccd-e562-4658-8674-9364249c9e90
        /// </summary>
        private string address1UPSZone;


        /// <summary>
        /// f50be51f-57e8-41de-8ad8-5d16091424be
        /// </summary>
        public string Address1UPSZone
        {
            get { return this.address1UPSZone; }
            set { this.address1UPSZone = value; }
        }


        /// <summary>
        /// 5342baa1-786e-4693-92fb-10e6d69cb08d
        /// </summary>
        private int address1UTCOffset;


        /// <summary>
        /// 535404d0-abfd-4652-9a42-6b67c2e3be9d
        /// </summary>
        public int Address1UTCOffset
        {
            get { return this.address1UTCOffset; }
            set { this.address1UTCOffset = value; }
        }


        /// <summary>
        /// 6e0ffb93-3e8f-4670-b9bc-ab3f7fc011ed
        /// </summary>
        private int address2AddressTypeCode;


        /// <summary>
        /// 3479164b-e1d2-4fb2-a21b-e3ba42ec7e98
        /// </summary>
        public int Address2AddressTypeCode
        {
            get { return this.address2AddressTypeCode; }
            set { this.address2AddressTypeCode = value; }
        }


        /// <summary>
        /// a4ff2879-0843-4963-ac44-ae7f7fbf9ef6
        /// </summary>
        private string address2City;


        /// <summary>
        /// bb888638-dc2c-46ac-a93e-7fd061fda67b
        /// </summary>
        public string Address2City
        {
            get { return this.address2City; }
            set { this.address2City = value; }
        }


        /// <summary>
        /// b9234f04-9913-4d8e-bb56-697e72c3e8ed
        /// </summary>
        private string address2Country;


        /// <summary>
        /// 96a306dc-8986-4723-8c6b-ce52400cbf4a
        /// </summary>
        public string Address2Country
        {
            get { return this.address2Country; }
            set { this.address2Country = value; }
        }


        /// <summary>
        /// bcfbadea-490d-44fd-a035-30910c935a21
        /// </summary>
        private string address2County;


        /// <summary>
        /// 93f7b5b9-991a-4c9d-892c-bf6b76d5ff16
        /// </summary>
        public string Address2County
        {
            get { return this.address2County; }
            set { this.address2County = value; }
        }


        /// <summary>
        /// 1bfa8ffa-e7f0-49a6-87df-20ee6eb744bf
        /// </summary>
        private string address2Fax;


        /// <summary>
        /// 774c787c-55fd-44a9-b55f-dad3ae0d8426
        /// </summary>
        public string Address2Fax
        {
            get { return this.address2Fax; }
            set { this.address2Fax = value; }
        }


        /// <summary>
        /// 76c223fd-5477-4f16-9ead-7dc1ec41f923
        /// </summary>
        private int address2FreightTermsCode;


        /// <summary>
        /// 0db9e26e-a1b3-492d-a945-9c0bf76f2585
        /// </summary>
        public int Address2FreightTermsCode
        {
            get { return this.address2FreightTermsCode; }
            set { this.address2FreightTermsCode = value; }
        }


        /// <summary>
        /// ddd2eee6-1665-4be0-93b0-98ee17c91ecc
        /// </summary>
        private double address2Latitude;


        /// <summary>
        /// 92a9aca8-8f30-41eb-8694-be346351263e
        /// </summary>
        public double Address2Latitude
        {
            get { return this.address2Latitude; }
            set { this.address2Latitude = value; }
        }


        /// <summary>
        /// ea7cde25-e4a9-41d7-9451-38fb6c17f576
        /// </summary>
        private string address2Line1;


        /// <summary>
        /// 97a2cf63-f2a9-4100-a6f8-6f079dceab40
        /// </summary>
        public string Address2Line1
        {
            get { return this.address2Line1; }
            set { this.address2Line1 = value; }
        }


        /// <summary>
        /// 9ef28698-f1c9-4d37-bac3-46811daeb90c
        /// </summary>
        private string address2Line2;


        /// <summary>
        /// 08f76c9c-a61a-4bac-b98a-0397f53aea37
        /// </summary>
        public string Address2Line2
        {
            get { return this.address2Line2; }
            set { this.address2Line2 = value; }
        }


        /// <summary>
        /// b0dd26b1-824a-4fa0-bd1f-b8137937988b
        /// </summary>
        private string address2Line3;


        /// <summary>
        /// edf58098-6156-4daf-b2e0-a46e2cadf727
        /// </summary>
        public string Address2Line3
        {
            get { return this.address2Line3; }
            set { this.address2Line3 = value; }
        }


        /// <summary>
        /// 83448659-c0ba-4030-9fa3-f76b97024ca1
        /// </summary>
        private double address2Longitude;


        /// <summary>
        /// 46c7869d-0d91-4afd-9aa4-56b8c0271c28
        /// </summary>
        public double Address2Longitude
        {
            get { return this.address2Longitude; }
            set { this.address2Longitude = value; }
        }


        /// <summary>
        /// abb0883e-da2f-44b5-98e5-d2171bffb4aa
        /// </summary>
        private string address2Name;


        /// <summary>
        /// de2a96f9-9d62-44a6-b162-d1f56d3c75ff
        /// </summary>
        public string Address2Name
        {
            get { return this.address2Name; }
            set { this.address2Name = value; }
        }


        /// <summary>
        /// b3018ec1-b8cd-4c9d-be4a-57f69fb04a0f
        /// </summary>
        private string address2PostalCode;


        /// <summary>
        /// f32e2fd8-f53a-4240-b3a6-0949e5e02d01
        /// </summary>
        public string Address2PostalCode
        {
            get { return this.address2PostalCode; }
            set { this.address2PostalCode = value; }
        }


        /// <summary>
        /// dab86105-2b90-4bca-8f16-1202b25e843b
        /// </summary>
        private string address2PostOfficeBox;


        /// <summary>
        /// 54e2de5d-81d0-4ec3-9698-425cd2be0f54
        /// </summary>
        public string Address2PostOfficeBox
        {
            get { return this.address2PostOfficeBox; }
            set { this.address2PostOfficeBox = value; }
        }


        /// <summary>
        /// 6a0c1de0-bb73-45f5-a9e3-d075da6d93b5
        /// </summary>
        private string address2PrimaryContactName;


        /// <summary>
        /// d1276d8b-e7a2-4c91-b151-6e9a790533de
        /// </summary>
        public string Address2PrimaryContactName
        {
            get { return this.address2PrimaryContactName; }
            set { this.address2PrimaryContactName = value; }
        }


        /// <summary>
        /// af3df391-5228-46d2-9278-467d6a044798
        /// </summary>
        private int address2ShippingMethodCode;


        /// <summary>
        /// 7f2ebebe-a5bb-40ee-9fc8-dbcc4896b96e
        /// </summary>
        public int Address2ShippingMethodCode
        {
            get { return this.address2ShippingMethodCode; }
            set { this.address2ShippingMethodCode = value; }
        }


        /// <summary>
        /// 858d1d83-6c68-4198-a50b-6f193a588615
        /// </summary>
        private string address2StateOrProvince;


        /// <summary>
        /// 7b055a93-40e9-44d1-9b2c-aa2ac3839e5c
        /// </summary>
        public string Address2StateOrProvince
        {
            get { return this.address2StateOrProvince; }
            set { this.address2StateOrProvince = value; }
        }


        /// <summary>
        /// 73f70f16-cff5-44ad-9b6c-8f0b299baceb
        /// </summary>
        private string address2Telephone1;


        /// <summary>
        /// 6bb8739a-bc28-4350-9e6e-d1c4a7ea7ef6
        /// </summary>
        public string Address2Telephone1
        {
            get { return this.address2Telephone1; }
            set { this.address2Telephone1 = value; }
        }


        /// <summary>
        /// ca6aabdb-8d41-4311-b9da-796277da187f
        /// </summary>
        private string address2Telephone2;


        /// <summary>
        /// caeac8bc-4738-4ff9-ad84-c6d529ad0895
        /// </summary>
        public string Address2Telephone2
        {
            get { return this.address2Telephone2; }
            set { this.address2Telephone2 = value; }
        }


        /// <summary>
        /// d9e93047-cd74-4338-a32c-46cddca0cd2c
        /// </summary>
        private string address2Telephone3;


        /// <summary>
        /// 79904b99-1ef2-4135-b47a-87b5c53e5705
        /// </summary>
        public string Address2Telephone3
        {
            get { return this.address2Telephone3; }
            set { this.address2Telephone3 = value; }
        }


        /// <summary>
        /// e7c3c26f-ccba-4445-822b-8d7cdd21c411
        /// </summary>
        private string address2UPSZone;


        /// <summary>
        /// 17453533-8b2a-4716-b1d5-468464bc6411
        /// </summary>
        public string Address2UPSZone
        {
            get { return this.address2UPSZone; }
            set { this.address2UPSZone = value; }
        }


        /// <summary>
        /// cbaf0c82-020d-44eb-9a55-9436a8f16508
        /// </summary>
        private int address2UTCOffset;


        /// <summary>
        /// 71b94f52-5857-4c18-99c7-b9f644d160ba
        /// </summary>
        public int Address2UTCOffset
        {
            get { return this.address2UTCOffset; }
            set { this.address2UTCOffset = value; }
        }


        /// <summary>
        /// 86076a0b-5487-4ece-9d37-fed3a7a93fbf
        /// </summary>
        private decimal aging30;


        /// <summary>
        /// b38d1d9e-fb45-49f1-b640-b49c10db80ae
        /// </summary>
        public decimal Aging30
        {
            get { return this.aging30; }
            set { this.aging30 = value; }
        }


        /// <summary>
        /// b6fe4791-38bd-49fc-9195-2242282f255e
        /// </summary>
        private decimal aging30Base;


        /// <summary>
        /// aec5b574-351d-47b6-8971-80573d52a313
        /// </summary>
        public decimal Aging30Base
        {
            get { return this.aging30Base; }
            set { this.aging30Base = value; }
        }


        /// <summary>
        /// 4e530a2c-5c76-4e9b-b2e2-7e1099ad8a51
        /// </summary>
        private decimal aging60;


        /// <summary>
        /// aa5ec1c7-b1f9-4396-8087-acdab8afdd34
        /// </summary>
        public decimal Aging60
        {
            get { return this.aging60; }
            set { this.aging60 = value; }
        }


        /// <summary>
        /// cdc58424-94b4-4828-9eba-3de71d3a53f7
        /// </summary>
        private decimal aging60Base;


        /// <summary>
        /// 37452732-e903-48cf-88b2-e6641d389bc7
        /// </summary>
        public decimal Aging60Base
        {
            get { return this.aging60Base; }
            set { this.aging60Base = value; }
        }


        /// <summary>
        /// 2cd56402-fbd0-4937-81c4-f05ab214ccc4
        /// </summary>
        private decimal aging90;


        /// <summary>
        /// 39353098-7bb4-4fdc-977a-e3ec918155c5
        /// </summary>
        public decimal Aging90
        {
            get { return this.aging90; }
            set { this.aging90 = value; }
        }


        /// <summary>
        /// e2ae0d9e-f6a7-46c2-beac-ad9e3f41a4d6
        /// </summary>
        private decimal aging90Base;


        /// <summary>
        /// 02b61120-8168-430d-bdbb-e0bdfaba087f
        /// </summary>
        public decimal Aging90Base
        {
            get { return this.aging90Base; }
            set { this.aging90Base = value; }
        }


        /// <summary>
        /// 09eb4bba-7004-4766-b887-038b10dfedfe
        /// </summary>
        private DateTime anniversary;


        /// <summary>
        /// 6d600a46-682d-4330-8761-1e0f49388207
        /// </summary>
        public DateTime Anniversary
        {
            get { return this.anniversary; }
            set { this.anniversary = value; }
        }


        /// <summary>
        /// 645f9246-47b0-4280-833b-923faa38a2a5
        /// </summary>
        private decimal annualIncome;


        /// <summary>
        /// 68aa4b59-6318-434f-a318-39c79f7ca957
        /// </summary>
        public decimal AnnualIncome
        {
            get { return this.annualIncome; }
            set { this.annualIncome = value; }
        }


        /// <summary>
        /// ea441dbc-cde1-453f-86ef-565784648bed
        /// </summary>
        private decimal annualIncomeBase;


        /// <summary>
        /// 8eef32ab-8904-4029-825d-913cec87504f
        /// </summary>
        public decimal AnnualIncomeBase
        {
            get { return this.annualIncomeBase; }
            set { this.annualIncomeBase = value; }
        }


        /// <summary>
        /// 2a2788d3-4895-420d-ac24-43ee5f1a4ccc
        /// </summary>
        private string assistantName;


        /// <summary>
        /// 50a177cb-9381-4e0b-94ad-6abc82061e43
        /// </summary>
        public string AssistantName
        {
            get { return this.assistantName; }
            set { this.assistantName = value; }
        }


        /// <summary>
        /// f2da2f4b-d109-4972-8c04-476d9cf793f5
        /// </summary>
        private string assistantPhone;


        /// <summary>
        /// 9f1d0e0b-8240-4546-833d-011e6184b45e
        /// </summary>
        public string AssistantPhone
        {
            get { return this.assistantPhone; }
            set { this.assistantPhone = value; }
        }


        /// <summary>
        /// df68cca7-878a-4ec5-9b2d-f167c6ee610c
        /// </summary>
        private DateTime birthDate;


        /// <summary>
        /// e2f1b244-51d5-46d1-bedf-5b8191b6345e
        /// </summary>
        public DateTime BirthDate
        {
            get { return this.birthDate; }
            set { this.birthDate = value; }
        }


        /// <summary>
        /// 73cbb152-741a-41ff-990a-c98624dfe68e
        /// </summary>
        private string childrensNames;


        /// <summary>
        /// 49bf42ea-9fbf-44d9-b626-067542f5f25a
        /// </summary>
        public string ChildrensNames
        {
            get { return this.childrensNames; }
            set { this.childrensNames = value; }
        }


        /// <summary>
        /// 957b38bf-0845-40e1-b363-656f8d859dc9
        /// </summary>
        private DateTime createdOn;


        /// <summary>
        /// 28099bbb-e656-4feb-8534-16578a2d6890
        /// </summary>
        public DateTime CreatedOn
        {
            get { return this.createdOn; }
            set { this.createdOn = value; }
        }


        /// <summary>
        /// 15e94cda-53d2-429a-8868-56a46942d66a
        /// </summary>
        private decimal creditLimit;


        /// <summary>
        /// 9569e784-d9bc-4b2f-8e59-a5a0d256c250
        /// </summary>
        public decimal CreditLimit
        {
            get { return this.creditLimit; }
            set { this.creditLimit = value; }
        }


        /// <summary>
        /// a7030c36-46f4-4eeb-a9ac-c6587ac69949
        /// </summary>
        private decimal creditLimitBase;


        /// <summary>
        /// 157bfcfb-d89c-4c8c-b1fc-1cec34eba93e
        /// </summary>
        public decimal CreditLimitBase
        {
            get { return this.creditLimitBase; }
            set { this.creditLimitBase = value; }
        }


        /// <summary>
        /// 288790f6-64f1-440c-a25e-e8ef578f9101
        /// </summary>
        private bool creditOnHold;


        /// <summary>
        /// 7f2ebe67-9e78-4de6-8250-267b675a0dc8
        /// </summary>
        public bool CreditOnHold
        {
            get { return this.creditOnHold; }
            set { this.creditOnHold = value; }
        }


        /// <summary>
        /// 2f908843-123c-4b3e-9e82-43dcfd367947
        /// </summary>
        private int customerSizeCode;


        /// <summary>
        /// eb707836-6a88-418d-a68b-6384170d8829
        /// </summary>
        public int CustomerSizeCode
        {
            get { return this.customerSizeCode; }
            set { this.customerSizeCode = value; }
        }


        /// <summary>
        /// 7fde9253-4e24-4fa3-9cb1-67fb80343ab0
        /// </summary>
        private int customerTypeCode;


        /// <summary>
        /// 577c5319-bf47-4498-8eb8-d82981391a9f
        /// </summary>
        public int CustomerTypeCode
        {
            get { return this.customerTypeCode; }
            set { this.customerTypeCode = value; }
        }


        /// <summary>
        /// c86cc7d9-e6ef-4038-8b8d-ab2f1fd70c8d
        /// </summary>
        private string department;


        /// <summary>
        /// 0b4c783b-a5af-4cca-abc7-8293b779c43e
        /// </summary>
        public string Department
        {
            get { return this.department; }
            set { this.department = value; }
        }


        /// <summary>
        /// 3fb7998a-3e1a-4f85-96be-eaa4ee68d3e0
        /// </summary>
        private bool donotBulkEMail;


        /// <summary>
        /// 72d17bbe-dde6-4ce6-bc16-afe18cdaf9f4
        /// </summary>
        public bool DonotBulkEMail
        {
            get { return this.donotBulkEMail; }
            set { this.donotBulkEMail = value; }
        }


        /// <summary>
        /// 1e217ec2-6e6a-4738-8c4b-036462372619
        /// </summary>
        private bool donotBulkPostalMail;


        /// <summary>
        /// f6a43bd1-010e-4de1-a486-8288c25c920c
        /// </summary>
        public bool DonotBulkPostalMail
        {
            get { return this.donotBulkPostalMail; }
            set { this.donotBulkPostalMail = value; }
        }


        /// <summary>
        /// 6900b0bd-9582-4f27-9aca-fe376cb6572e
        /// </summary>
        private bool donotEmail;


        /// <summary>
        /// 19be44fc-73f6-450a-a1f7-592dcb48ee11
        /// </summary>
        public bool DonotEmail
        {
            get { return this.donotEmail; }
            set { this.donotEmail = value; }
        }


        /// <summary>
        /// 938572f8-12fb-40ea-8793-364e95d4eff7
        /// </summary>
        private bool donotFax;


        /// <summary>
        /// c0aad049-7836-47e1-b2b2-3eacfc48c3cc
        /// </summary>
        public bool DonotFax
        {
            get { return this.donotFax; }
            set { this.donotFax = value; }
        }


        /// <summary>
        /// e752803d-6686-43fc-97a2-21dffb04eb73
        /// </summary>
        private bool donotPhone;


        /// <summary>
        /// 6218cd63-f950-46e7-9a5b-a66b5b01db53
        /// </summary>
        public bool DonotPhone
        {
            get { return this.donotPhone; }
            set { this.donotPhone = value; }
        }


        /// <summary>
        /// 0799006c-619e-41f8-94d2-c2746547fe3a
        /// </summary>
        private bool donotPostalMail;


        /// <summary>
        /// 02d9c1b8-de2b-400d-80c2-bec660d204ab
        /// </summary>
        public bool DonotPostalMail
        {
            get { return this.donotPostalMail; }
            set { this.donotPostalMail = value; }
        }


        /// <summary>
        /// 2d810cc0-eaa1-46ab-b9cd-8a484674f759
        /// </summary>
        private bool donotSendMM;


        /// <summary>
        /// 27f910d2-c85d-43e3-88c3-5099d7f6626c
        /// </summary>
        public bool DonotSendMm
        {
            get { return this.donotSendMM; }
            set { this.donotSendMM = value; }
        }


        /// <summary>
        /// dec1129c-de48-4000-a2d6-22ca3cbe16fa
        /// </summary>
        private int educationCode;


        /// <summary>
        /// 4b77942d-6fa4-4f1c-bdc9-287c33724005
        /// </summary>
        public int EducationCode
        {
            get { return this.educationCode; }
            set { this.educationCode = value; }
        }


        /// <summary>
        /// aa8a9a26-1175-4b7f-83d5-ff00cc3f78b5
        /// </summary>
        private string emailAddress1;


        /// <summary>
        /// a9a9fc44-98f5-4b09-8b16-c64032e2cdc3
        /// </summary>
        public string EmailAddress1
        {
            get { return this.emailAddress1; }
            set { this.emailAddress1 = value; }
        }


        /// <summary>
        /// 387617f8-3649-42b7-9fb1-952976f1f274
        /// </summary>
        private string emailAddress2;


        /// <summary>
        /// 7b697bb6-1252-4e93-8b84-413a73c6478a
        /// </summary>
        public string EmailAddress2
        {
            get { return this.emailAddress2; }
            set { this.emailAddress2 = value; }
        }


        /// <summary>
        /// 2ec2af29-be16-4886-8d62-cd06a8d9ff2f
        /// </summary>
        private string emailAddress3;


        /// <summary>
        /// fa5a1762-31c0-4862-81cc-2e70ef6c939a
        /// </summary>
        public string EmailAddress3
        {
            get { return this.emailAddress3; }
            set { this.emailAddress3 = value; }
        }


        /// <summary>
        /// 5ccff70b-b4ca-4f95-97f0-73059d3d1081
        /// </summary>
        private string employeeId;


        /// <summary>
        /// 5506e5b7-5ea9-4b0e-9638-c50e195d7f3c
        /// </summary>
        public string EmployeeId
        {
            get { return this.employeeId; }
            set { this.employeeId = value; }
        }


        /// <summary>
        /// dd0e2559-d4b3-4e06-8d3c-e437b53911d2
        /// </summary>
        private decimal exchangeRate;


        /// <summary>
        /// c09ebd0d-b145-4e87-90d8-f95f8cb18ed0
        /// </summary>
        public decimal ExchangeRate
        {
            get { return this.exchangeRate; }
            set { this.exchangeRate = value; }
        }


        /// <summary>
        /// 2a6639da-5761-40b5-94d7-b200e4eeb15f
        /// </summary>
        private int familyStatusCode;


        /// <summary>
        /// 23a78d3c-2594-4e09-8586-b5464946d77c
        /// </summary>
        public int FamilyStatusCode
        {
            get { return this.familyStatusCode; }
            set { this.familyStatusCode = value; }
        }


        /// <summary>
        /// d6b67a5a-a890-4e2f-bdcb-4e375bdb19d5
        /// </summary>
        private string fax;


        /// <summary>
        /// b78922c1-6127-4920-aa8b-d91c50fbe796
        /// </summary>
        public string Fax
        {
            get { return this.fax; }
            set { this.fax = value; }
        }


        /// <summary>
        /// 6431350c-8066-4aa3-9477-c4e033e36490
        /// </summary>
        private string firstName;


        /// <summary>
        /// f93dbed6-a00a-4c8d-b464-35a6d11ce9a0
        /// </summary>
        public string FirstName
        {
            get { return this.firstName; }
            set { this.firstName = value; }
        }


        /// <summary>
        /// 7573fcdc-4020-4995-bbc6-8d0f3a245b2a
        /// </summary>
        private string fullName;


        /// <summary>
        /// ba40d98c-2ef9-433b-9b10-90ced46d0092
        /// </summary>
        public string FullName
        {
            get { return this.fullName; }
            set { this.fullName = value; }
        }


        /// <summary>
        /// b76f2fef-8e8d-47cd-89ea-a4c43ae9afa9
        /// </summary>
        private int genderCode;


        /// <summary>
        /// 39d2e0c0-2769-49fd-b1de-752fa4945eae
        /// </summary>
        public int GenderCode
        {
            get { return this.genderCode; }
            set { this.genderCode = value; }
        }


        /// <summary>
        /// 1bd2a0cd-3020-45e3-b81c-d01e19b60a89
        /// </summary>
        private string governmentId;


        /// <summary>
        /// 5ce31fef-8493-4d9b-80e4-a4cd85f117e4
        /// </summary>
        public string GovernmentId
        {
            get { return this.governmentId; }
            set { this.governmentId = value; }
        }


        /// <summary>
        /// 46660ee7-9aa5-42c0-8146-30fcad38d671
        /// </summary>
        private int hasChildrenCode;


        /// <summary>
        /// 9b2b3bb3-dd8f-4084-9026-357671b2d53c
        /// </summary>
        public int HasChildrenCode
        {
            get { return this.hasChildrenCode; }
            set { this.hasChildrenCode = value; }
        }


        /// <summary>
        /// d5d71cd2-d1ee-4a02-bb1e-a5a3cdaf6f1a
        /// </summary>
        private int importSequenceNumber;


        /// <summary>
        /// 504543f5-180d-4292-ae12-a2a05273afbe
        /// </summary>
        public int ImportSequenceNumber
        {
            get { return this.importSequenceNumber; }
            set { this.importSequenceNumber = value; }
        }


        /// <summary>
        /// eb2e4a27-7a4a-41c3-81c6-45d96d6e9615
        /// </summary>
        private bool isBackofficeCustomer;


        /// <summary>
        /// d1a87397-acde-49aa-a139-50d1c3f2b276
        /// </summary>
        public bool IsBackofficeCustomer
        {
            get { return this.isBackofficeCustomer; }
            set { this.isBackofficeCustomer = value; }
        }


        /// <summary>
        /// 08ae356d-1582-4db3-b501-5e44d7b96a55
        /// </summary>
        private string jobTitle;


        /// <summary>
        /// 0f617231-5d06-491b-a57a-5699fe982054
        /// </summary>
        public string JobTitle
        {
            get { return this.jobTitle; }
            set { this.jobTitle = value; }
        }


        /// <summary>
        /// fd2b2cae-5a70-4998-a875-cdb52f97a520
        /// </summary>
        private string lastName;


        /// <summary>
        /// fc1095f3-437c-4e80-b672-83a39d9708a7
        /// </summary>
        public string LastName
        {
            get { return this.lastName; }
            set { this.lastName = value; }
        }


        /// <summary>
        /// fb9d6fe6-bf95-48e7-a570-ac4b00e5789e
        /// </summary>
        private DateTime lastUsedInCampaign;


        /// <summary>
        /// e76bf998-6089-4f76-b193-e3f5a9c98d21
        /// </summary>
        public DateTime LastUsedInCampaign
        {
            get { return this.lastUsedInCampaign; }
            set { this.lastUsedInCampaign = value; }
        }


        /// <summary>
        /// a3671145-377e-4f27-a097-d1121c9e1750
        /// </summary>
        private int leadSourceCode;


        /// <summary>
        /// a34fd602-e23d-4e09-a64b-7b66be265dc2
        /// </summary>
        public int LeadSourceCode
        {
            get { return this.leadSourceCode; }
            set { this.leadSourceCode = value; }
        }


        /// <summary>
        /// 438f448c-84e7-4226-863d-09e29b371997
        /// </summary>
        private string managerName;


        /// <summary>
        /// 11e863f1-437a-445e-a546-951629b874db
        /// </summary>
        public string ManagerName
        {
            get { return this.managerName; }
            set { this.managerName = value; }
        }


        /// <summary>
        /// b5ef2e86-228b-4145-9b77-3825984d89d5
        /// </summary>
        private string managerPhone;


        /// <summary>
        /// e743693b-3f81-4b46-bafa-e03e3a2b57f1
        /// </summary>
        public string ManagerPhone
        {
            get { return this.managerPhone; }
            set { this.managerPhone = value; }
        }


        /// <summary>
        /// 8e8f3271-3929-4cd3-8b92-afda7919bf4e
        /// </summary>
        private bool merged;


        /// <summary>
        /// 1efafe90-cd71-4830-b5db-a3c3da262618
        /// </summary>
        public bool Merged
        {
            get { return this.merged; }
            set { this.merged = value; }
        }


        /// <summary>
        /// 2308da5d-f816-4634-a6b7-0bd10b457e50
        /// </summary>
        private string middleName;


        /// <summary>
        /// 5fd669db-b667-4ddd-82af-3482e4871f68
        /// </summary>
        public string MiddleName
        {
            get { return this.middleName; }
            set { this.middleName = value; }
        }


        /// <summary>
        /// 72a9f1e2-8ddf-41f9-a262-437bbe151d21
        /// </summary>
        private string mobilePhone;


        /// <summary>
        /// 5dc849d1-be68-4bf5-97ab-2e2ff133b6db
        /// </summary>
        public string MobilePhone
        {
            get { return this.mobilePhone; }
            set { this.mobilePhone = value; }
        }


        /// <summary>
        /// 899fe111-9cc7-41a0-8664-ec241fddcc33
        /// </summary>
        private string nickName;


        /// <summary>
        /// 41ee4a36-66f8-4b11-806c-63e6d2288804
        /// </summary>
        public string NickName
        {
            get { return this.nickName; }
            set { this.nickName = value; }
        }


        /// <summary>
        /// 74198ab8-69fc-4124-9d57-2ddd50f92676
        /// </summary>
        private int numberOfChildren;


        /// <summary>
        /// 75a8269e-4e45-43ac-85ec-a8c3d875b8fd
        /// </summary>
        public int NumberOfChildren
        {
            get { return this.numberOfChildren; }
            set { this.numberOfChildren = value; }
        }


        /// <summary>
        /// c7d1fc53-9f05-469a-b3a1-cf67688de156
        /// </summary>
        private string pager;


        /// <summary>
        /// b18ac5ed-6508-444c-b1f2-eaf93b0a8805
        /// </summary>
        public string Pager
        {
            get { return this.pager; }
            set { this.pager = value; }
        }


        /// <summary>
        /// 9b91da8c-e932-43aa-962f-c0f0d9de735f
        /// </summary>
        private int paymentTermsCode;


        /// <summary>
        /// 93f7590b-34ce-4ac5-9c6f-5959e2601182
        /// </summary>
        public int PaymentTermsCode
        {
            get { return this.paymentTermsCode; }
            set { this.paymentTermsCode = value; }
        }


        /// <summary>
        /// 0496a019-38cd-433a-994d-06a3ff21cb80
        /// </summary>
        private int preferredAppointmentDayCode;


        /// <summary>
        /// 3da70d79-885d-4ed4-9583-2b6048bd4665
        /// </summary>
        public int PreferredAppointmentDayCode
        {
            get { return this.preferredAppointmentDayCode; }
            set { this.preferredAppointmentDayCode = value; }
        }


        /// <summary>
        /// 67c0ffc2-3a6f-4d97-ab0d-e97165b27df0
        /// </summary>
        private int preferredAppointmentTimeCode;


        /// <summary>
        /// de212f29-f9b0-4d65-ad41-dd22da09d7e8
        /// </summary>
        public int PreferredAppointmentTimeCode
        {
            get { return this.preferredAppointmentTimeCode; }
            set { this.preferredAppointmentTimeCode = value; }
        }


        /// <summary>
        /// 6b51b438-3c37-4081-b70c-9219325baa22
        /// </summary>
        private int preferredContactMethodCode;


        /// <summary>
        /// 8eb9b6af-51d7-4ad8-a389-469153a28ffc
        /// </summary>
        public int PreferredContactMethodCode
        {
            get { return this.preferredContactMethodCode; }
            set { this.preferredContactMethodCode = value; }
        }


        /// <summary>
        /// 7915168c-243f-40bd-bb45-ce1fb834a258
        /// </summary>
        private string salutation;


        /// <summary>
        /// b71d56be-9254-4288-9275-2e46111bd6cc
        /// </summary>
        public string Salutation
        {
            get { return this.salutation; }
            set { this.salutation = value; }
        }


        /// <summary>
        /// fe3e95ac-f9bd-4d68-b6d3-4f6599630837
        /// </summary>
        private int shippingMethodCode;


        /// <summary>
        /// 822ca70f-3241-49a5-beaa-8015cae98de2
        /// </summary>
        public int ShippingMethodCode
        {
            get { return this.shippingMethodCode; }
            set { this.shippingMethodCode = value; }
        }


        /// <summary>
        /// 79f3cd24-ff8e-4046-a7a9-4c7bbbdaf0bf
        /// </summary>
        private string spousesName;


        /// <summary>
        /// 0fc5ee25-eab7-45c0-a4d6-630d381d1e5d
        /// </summary>
        public string SpousesName
        {
            get { return this.spousesName; }
            set { this.spousesName = value; }
        }


        /// <summary>
        /// c5b9c8cb-e14d-4651-b426-67ba64f059e4
        /// </summary>
        private int stateCode;


        /// <summary>
        /// 803589bb-9195-4898-b412-6f98c4d83b1e
        /// </summary>
        public int StateCode
        {
            get { return this.stateCode; }
            set { this.stateCode = value; }
        }


        /// <summary>
        /// e61fa07f-0341-44bb-8584-30a65dc50397
        /// </summary>
        private int statusCode;


        /// <summary>
        /// 0416601d-6e80-4310-9d0b-f46fd3d8ffad
        /// </summary>
        public int StatusCode
        {
            get { return this.statusCode; }
            set { this.statusCode = value; }
        }

        /// <summary>
        /// 48ad344d-bbda-4ea9-89e3-3037003ee031
        /// </summary>
        private string telephone1;


        /// <summary>
        /// 3f089bc4-7227-4c72-8794-eccfc58fdf77
        /// </summary>
        public string Telephone1
        {
            get { return this.telephone1; }
            set { this.telephone1 = value; }
        }


        /// <summary>
        /// 74062288-c2ed-46aa-82b3-3a7802e9f6d8
        /// </summary>
        private string telephone2;


        /// <summary>
        /// 6b923977-3f82-4d4b-a392-ae8b09d6eb08
        /// </summary>
        public string Telephone2
        {
            get { return this.telephone2; }
            set { this.telephone2 = value; }
        }


        /// <summary>
        /// c5b8daa4-1d22-45be-888e-d328056e8c38
        /// </summary>
        private string telephone3;


        /// <summary>
        /// f88b8755-1188-4615-87df-01c958b61811
        /// </summary>
        public string Telephone3
        {
            get { return this.telephone3; }
            set { this.telephone3 = value; }
        }


        /// <summary>
        /// 998c3e21-8e61-4a0d-8fba-7b3103f363ce
        /// </summary>
        private int territoryCode;


        /// <summary>
        /// ee6778a4-174c-474f-8d97-ec1401b3dc16
        /// </summary>
        public int TerritoryCode
        {
            get { return this.territoryCode; }
            set { this.territoryCode = value; }
        }


        /// <summary>
        /// 9cce5bcc-8592-49ef-a0d0-144a724c1ffd
        /// </summary>
        private int timeZoneRuleVersionNumber;


        /// <summary>
        /// 5bd5f755-e5fb-45aa-8cd1-2ba5c5604be9
        /// </summary>
        public int TimeZoneRuleVersionNumber
        {
            get { return this.timeZoneRuleVersionNumber; }
            set { this.timeZoneRuleVersionNumber = value; }
        }


        /// <summary>
        /// 84a42fe2-89a4-4699-9416-e353fab9bdff
        /// </summary>
        private int utcconversionTimeZoneCode;


        /// <summary>
        /// b3f30f0a-f6e3-48ea-8025-046965df2f88
        /// </summary>
        public int UTCConversionTimeZoneCode
        {
            get { return this.utcconversionTimeZoneCode; }
            set { this.utcconversionTimeZoneCode = value; }
        }


        /// <summary>
        /// b99834bb-c6d8-4401-a875-f11707462409
        /// </summary>
        private string webSiteUrl;


        /// <summary>
        /// ed4fe59d-cebc-4d88-9ef9-415de49f0746
        /// </summary>
        public string WebSiteUrl
        {
            get { return this.webSiteUrl; }
            set { this.webSiteUrl = value; }
        }


        /// <summary>
        /// ae378a38-9713-49b5-885d-7b309b1713bf
        /// </summary>
        private string yomiFirstName;


        /// <summary>
        /// 720063e0-087c-49ce-8177-cedea1bc50df
        /// </summary>
        public string YomiFirstName
        {
            get { return this.yomiFirstName; }
            set { this.yomiFirstName = value; }
        }


        /// <summary>
        /// a0df6505-0c25-403a-8266-f38b350e3f21
        /// </summary>
        private string yomiFullName;


        /// <summary>
        /// 657d9a8e-a969-4e35-9768-6e46ee13030d
        /// </summary>
        public string YomiFullName
        {
            get { return this.yomiFullName; }
            set { this.yomiFullName = value; }
        }


        /// <summary>
        /// e21655fd-bc7b-43f6-bf7c-bb23ccd6190d
        /// </summary>
        private string yomiLastName;


        /// <summary>
        /// 404c6cb5-fed8-492b-ba9d-09e1d5786158
        /// </summary>
        public string YomiLastName
        {
            get { return this.yomiLastName; }
            set { this.yomiLastName = value; }
        }


        /// <summary>
        /// 48a3b3d7-ce5e-4116-a046-271a09e61e61
        /// </summary>
        private string yomiMiddleName;


        /// <summary>
        /// 8b8c4890-ffff-43c0-8e08-9bbd33a9edfb
        /// </summary>
        public string YomiMiddleName
        {
            get { return this.yomiMiddleName; }
            set { this.yomiMiddleName = value; }
        }

        /// <summary>
        /// Age of person
        /// </summary>
        public int Age
        {
            get
            {
                return this.age;
            }

            set
            {
                this.age = value;
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Contact"/> class. 
        /// d04787ba-613b-48f5-9d50-652b04073718
        /// </summary>
        public Contact()
        {

        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Contact"/> class. 
        /// 4af5a8d5-17f7-4e79-9c59-d887b1f2e054
        /// </summary>
        /// <param name="contact">
        /// The contact.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        public Contact(string contact, DelimeterType type)
        {
            this.SetValues(contact, type);
        }


        /// <summary>
        /// Initializes a new instance of the <see cref="Contact"/> class. 
        /// 07354c1e-1666-46b2-bec3-8bc20fa065f3
        /// </summary>
        /// <param name="contact">
        /// The contact.
        /// </param>
        public Contact(string[] contact)
        {
            this.SetValues(contact);
        }


        /// <summary>
        /// 2489015e-7fa0-408c-b807-3e93d21b69ba
        /// </summary>
        /// <param name="contactdata">
        /// The contactdata.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="Contact"/>.
        /// </returns>
        private Contact SetValues(string contactdata, DelimeterType type)
        {
            try
            {
                if (!string.IsNullOrEmpty(contactdata))
                {
                    string[] linecolumns = null;

                    switch (type)
                    {
                        case DelimeterType.CSV:
                            {
                                linecolumns = contactdata.Split(new[] { ',' }, StringSplitOptions.None);
                            }

                            break;
                        case DelimeterType.TAB:
                            {
                                //// parsed based on TAB
                                //// remember our contact guid is first
                                linecolumns = contactdata.Split(new[] { '\t' }, StringSplitOptions.None);
                            }

                            break;
                        default: //// do nothing
                            break;
                    }

                    return this.SetValues(linecolumns);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception)
            {
                ////throw new Exception("Invalid Operation " + ex.ToString());
                return null;

            }
        }


        /// <summary>
        /// 1a341237-caac-4b7b-a790-3a0c06d29be0
        /// </summary>
        /// <param name="contactdata">
        /// The contactdata.
        /// </param>
        /// <returns>
        /// The <see cref="Contact"/>.
        /// </returns>
        private Contact SetValues(string[] contactdata)
        {
            try
            {
                List<string> properties = DataHelper.GetPropertyNamesList(this, null);

                for (int i = 0; i < contactdata.Count(); i++)
                {
                    DataHelper.SetPropValue(this, properties[i], contactdata[i]);
                }

                return this;
            }
            catch (Exception)
            {
                ////throw new Exception("Invalid Operation " + ex.ToString());
                return null;

            }
        }


        /// <summary>
        /// 23ea4e7f-6781-4666-b5b9-ebb59a9cce61
        /// </summary>
        /// <param name="suffix">
        /// The suffix.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string InitializeContact(string suffix)
        {
            this.AccountRoleCode = randomContactGenerator.Next(1, 100);
            this.Address1AddressTypeCode = randomContactGenerator.Next(1, 100);
            this.Address1City = string.Empty; //"Address1City-" + suffix;
            this.Address1Country = string.Empty; //"Address1Country-" + suffix;
            this.Address1County = string.Empty; //"Address1County-" + suffix;
            this.Address1Fax = string.Empty; //"Address1Fax-" + suffix;
            this.Address1FreightTermsCode = randomContactGenerator.Next(1, 100);
            this.Address1Latitude = randomContactGenerator.Next(1, 100);
            this.Address1Line1 = string.Empty; //"Address1Line1-" + suffix;
            this.Address1Line2 = string.Empty; // "Address1Line2-" + suffix;
            this.Address1Line3 = string.Empty; //"Address1Line3-" + suffix;
            this.Address1Longitude = randomContactGenerator.Next(1, 100);
            this.Address1Name = string.Empty; //"Address1Name-" + suffix;
            this.Address1PostalCode = string.Empty; //"Address1PostalCode-" + suffix;
            this.Address1PostOfficeBox = string.Empty; // "Address1PostOfficeBox-" + suffix;
            this.Address1PrimaryContactName = string.Empty; //"Address1PrimaryContactName-" + suffix;
            this.Address1ShippingMethodCode = randomContactGenerator.Next(1, 100);
            this.Address1StateOrProvince = string.Empty; //"Address1StateOrProvince-" + suffix;
            this.Address1Telephone1 = string.Empty; //"Address1Telephone1-" + suffix;
            this.Address1Telephone2 = string.Empty; //"Address1Telephone2-" + suffix;
            this.Address1Telephone3 = string.Empty; // "Address1Telephone3-" + suffix;
            this.Address1UPSZone = string.Empty; //"UPSZ";
            this.Address1UTCOffset = randomContactGenerator.Next(1, 100);
            this.Address2AddressTypeCode = randomContactGenerator.Next(1, 100);
            this.Address2City = string.Empty; //"Address2City-" + suffix;
            this.Address2Country = string.Empty; // "Address2Country-" + suffix;
            this.Address2County = string.Empty; // "Address2County-" + suffix;
            this.Address2Fax = string.Empty; //"Address2Fax-" + suffix;
            this.Address2FreightTermsCode = randomContactGenerator.Next(1, 100);
            this.Address2Latitude = randomContactGenerator.Next(1, 100);
            this.Address2Line1 = string.Empty; //"Address2Line1-" + suffix;
            this.Address2Line2 = string.Empty; //"Address2Line2-" + suffix;
            this.Address2Line3 = string.Empty; //"Address2Line3-" + suffix;
            this.Address2Longitude = randomContactGenerator.Next(1, 100);
            this.Address2Name = string.Empty; // "Address2Name-" + suffix;
            this.Address2PostalCode = string.Empty; //"Address2PostalCode-" + suffix;
            this.Address2PostOfficeBox = string.Empty; //"Address2PostOfficeBox-" + suffix;
            this.Address2PrimaryContactName = string.Empty; //"Address2PrimaryContactName-" + suffix;
            this.Address2ShippingMethodCode = randomContactGenerator.Next(1, 100);
            this.Address2StateOrProvince = string.Empty; //"Address2StateOrProvince-" + suffix;
            this.Address2Telephone1 = string.Empty; // "Address2Telephone1-" + suffix;
            this.Address2Telephone2 = string.Empty; //"Address2Telephone2-" + suffix;
            this.Address2Telephone3 = string.Empty; //"Address2Telephone3-" + suffix;
            this.Address2UPSZone = string.Empty; //"Address2UPSZone-" + suffix;
            this.Address2UTCOffset = randomContactGenerator.Next(1, 100);
            this.Aging30 = 1.00M;
            this.Aging30Base = 2.00M;
            this.Aging60 = 3.00M;
            this.Aging60Base = 4.00M;
            this.Aging90 = 5.00M;
            this.Aging90Base = 6.50M;
            this.Anniversary = DateTime.UtcNow;
            this.AnnualIncome = 7.50M;
            this.AnnualIncomeBase = 8.50M;
            this.AssistantName = string.Empty; // "AssistantName-" + suffix;
            this.AssistantPhone = string.Empty; //"AssistantPhone-" + suffix;
            this.BirthDate = DateTime.UtcNow;
            this.ChildrensNames = string.Empty; //"ChildrensNames-" + suffix;
            this.ContactId = Guid.NewGuid().ToString();
            this.CreatedOn = DateTime.UtcNow;
            this.CreditLimit = 9.50M;
            this.CreditLimitBase = 10.50M;
            this.CreditOnHold = false;
            this.CustomerSizeCode = randomContactGenerator.Next(1, 100);
            this.CustomerTypeCode = randomContactGenerator.Next(1, 100);
            this.Department = string.Empty; //"Department-" + suffix;
            this.DonotBulkEMail = false;
            this.DonotBulkPostalMail = false;
            this.DonotEmail = false;
            this.DonotFax = false;
            this.DonotPhone = false;
            this.DonotPostalMail = false;
            this.DonotSendMm = false;
            this.EducationCode = randomContactGenerator.Next(1, 100);
            this.EmailAddress1 = string.Empty; // "emailAddress1-" + suffix;
            this.EmailAddress2 = string.Empty; //"emailAddress2-" + suffix;
            this.EmailAddress3 = string.Empty; // "emailAddress3-" + suffix;
            this.EmployeeId = string.Empty; // "EmployeeId-" + suffix;
            this.ExchangeRate = 23.5M;
            this.FamilyStatusCode = randomContactGenerator.Next(1, 100);
            this.Fax = string.Empty; // "Fax-" + suffix;
            this.FirstName = string.Empty; //"FirstName-" + suffix;
            this.FullName = string.Empty; // "FullName-" + suffix;
            this.GenderCode = randomContactGenerator.Next(1, 100);
            this.GovernmentId = string.Empty; //"GovernmentId-" + suffix;
            this.HasChildrenCode = randomContactGenerator.Next(1, 100);
            this.ImportSequenceNumber = randomContactGenerator.Next(1, 100);
            this.IsBackofficeCustomer = false;
            this.JobTitle = string.Empty; //"JobTitle-" + suffix;
            this.LastName = string.Empty; //"LastName-" + suffix;
            this.LastUsedInCampaign = DateTime.UtcNow;
            this.LeadSourceCode = randomContactGenerator.Next(1, 100);
            this.ManagerName = string.Empty; //"ManagerName-" + suffix;
            this.ManagerPhone = string.Empty; //"ManagerPhone-" + suffix;
            this.Merged = true;
            this.MiddleName = string.Empty; //"MiddleName-" + suffix;
            this.MobilePhone = string.Empty; //"MobilePhone-" + suffix;
            this.NickName = string.Empty; //"NickName-" + suffix;
            this.NumberOfChildren = randomContactGenerator.Next(1, 100);
            this.Pager = string.Empty; //"Pager-" + suffix;
            this.PaymentTermsCode = randomContactGenerator.Next(1, 100);
            this.PreferredAppointmentDayCode = randomContactGenerator.Next(1, 100);
            this.PreferredAppointmentTimeCode = randomContactGenerator.Next(1, 100);
            this.PreferredContactMethodCode = randomContactGenerator.Next(1, 100);
            this.Salutation = string.Empty; //"Salutation-" + suffix;
            this.ShippingMethodCode = randomContactGenerator.Next(1, 100);
            this.SpousesName = string.Empty; // "SpousesName-" + suffix;
            this.StateCode = new Random().Next(1, 100);
            this.StatusCode = new Random().Next(1, 100);
            this.Telephone1 = string.Empty; //"Telephone1-" + suffix;
            this.Telephone2 = string.Empty; // "Telephone2-" + suffix;
            this.Telephone3 = string.Empty; //"Telephone3-" + suffix;
            this.TerritoryCode = randomContactGenerator.Next(1, 100);
            this.TimeZoneRuleVersionNumber = randomContactGenerator.Next(1, 100);
            this.UTCConversionTimeZoneCode = randomContactGenerator.Next(1, 100);
            this.WebSiteUrl = string.Empty; //"WebSiteUrl-" + suffix;
            this.YomiFirstName = string.Empty; //"YomiFirstName-" + suffix;
            this.YomiFullName = string.Empty; // "YomiFullName-" + suffix;
            this.YomiLastName = string.Empty; //"YomiLastName-" + suffix;
            this.YomiMiddleName = string.Empty; //"YomiMiddleName-" + suffix;


            return suffix;
        }


        /// <summary>
        /// e28c1436-7033-47c8-a15a-de8c7c19081b
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ExportToCSV()
        {
            string exportstring = string.Empty;

            foreach (string s in DataHelper.GetPropertyNamesList(this, null))
            {
                object objValue = DataHelper.GetPropValue(this, s);

                if (objValue != null)
                {
                    exportstring += objValue.ToString() + ",";
                }
            }

            ////remove trailing
            exportstring = exportstring.Substring(0, exportstring.Length - 1);

            return exportstring;
        }


        /// <summary>
        /// 00ea8860-a058-45f6-a919-b84f81b6330a
        /// </summary>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string ExportToTAB()
        {
            string exportstring = string.Empty;

            foreach (string s in DataHelper.GetPropertyNamesList(this, null))
            {
                object objValue = DataHelper.GetPropValue(this, s);

                if (objValue != null)
                {
                    if (objValue.GetType().UnderlyingSystemType == typeof(DateTime))
                    {
                        DateTime dt = DateTime.Parse(objValue.ToString());

                        exportstring += dt.ToString("yyyy-MM-dd") + '\t';
                    }
                    else
                    {
                        exportstring += objValue.ToString() + '\t';
                    }
                }
            }

            ////remove trailing
            exportstring = exportstring.Substring(0, exportstring.Length - 1);

            return exportstring;
        }


        /// <summary>
        /// fcbcf5a1-5a5b-4526-8e36-7c5856cbf87f
        /// </summary>
        public enum DelimeterType
        {
            /// <summary>
            /// The csv.
            /// </summary>
            CSV = 0,

            /// <summary>
            /// The tab.
            /// </summary>
            TAB = 1
        }


        /// <summary>
        /// 908a1478-a0c8-4dc1-8880-e1871c2c1e26
        /// </summary>
        /// <param name="filepath">
        /// The filepath.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<Contact> ImportContacts(string filepath, DelimeterType type)
        {
            try
            {
                List<Contact> contacts = new List<Contact>();

                using (TextReader sr = File.OpenText(filepath))
                {
                    while (true)
                    {
                        string line = sr.ReadLine();

                        if (!string.IsNullOrEmpty(line))
                        {
                            string[] linecolumns = null;

                            if (!string.IsNullOrEmpty(line))
                            {
                                switch (type)
                                {
                                    case DelimeterType.CSV:
                                        {
                                            linecolumns = line.Split(new[] { ',' }, StringSplitOptions.None);
                                            contacts.Add(new Contact(linecolumns));
                                        }

                                        break;
                                    case DelimeterType.TAB:
                                        {
                                            //// parsed based on TAB
                                            //// remember our contact guid is first
                                            linecolumns = line.Split(new[] { '\t' }, StringSplitOptions.None);
                                            contacts.Add(new Contact(linecolumns));
                                        }

                                        break;
                                    default: //// do nothing
                                        break;
                                }
                            }
                        }
                        else
                        {
                            break;
                        }
                    }

                    return contacts;
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Error " + ex.ToString());
                return null;
            }
        }


        /// <summary>
        /// 5720e409-fc07-42c1-9dc0-eb9bc4286a8f
        /// </summary>
        /// <param name="filerowdata">
        /// The filerowdata.
        /// </param>
        /// <param name="type">
        /// The type.
        /// </param>
        /// <returns>
        /// The <see cref="Contact"/>.
        /// </returns>
        public static Contact ImportContact(string filerowdata, DelimeterType type)
        {
            try
            {
                if (!string.IsNullOrEmpty(filerowdata))
                {
                    string[] linecolumns = null;

                    switch (type)
                    {
                        case DelimeterType.CSV:
                            {
                                linecolumns = filerowdata.Split(new[] { ',' }, StringSplitOptions.None);
                                return new Contact(linecolumns);
                            }

                        case DelimeterType.TAB:
                            {
                                //// parsed based on TAB
                                //// remember our contact guid is first
                                linecolumns = filerowdata.Split(new[] { '\t' }, StringSplitOptions.None);
                                return new Contact(linecolumns);
                            }

                        default: //// do nothing
                            return null;
                    }
                }
                else
                {
                    throw new ArgumentNullException("row data cannot be null ");
                }
            }
            catch (Exception)
            {
                ////throw new Exception("Exception importing row data: " + ex.ToString());
                return null;

            }
        }

        /// <summary>
        /// Use this to help generate fake information for a contact
        /// </summary>
        private static Random randomContactGenerator = new Random();

        /// <summary>
        /// d3077a69-071f-4d73-b024-df8343f6b765
        /// </summary>
        /// <param name="contacts">
        /// The contacts.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<Contact> GenerateContacts(int contacts)
        {
            try
            {

                InitializeFakeData();

                List<Contact> listContacts = new List<Contact>();

                for (int i = 0; i < contacts; i++)
                {
                    ////generate our fake data
                    Contact a = new Contact();
                    a.InitializeContact(i.ToString());

                    listContacts.Add(a);
                }

                return listContacts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }


        /// <summary>
        /// d3077a69-071f-4d73-b024-df8343f6b765
        /// </summary>
        /// <param name="contacts">
        /// The contacts.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<Contact> GenerateMinimalContacts(int contacts)
        {
            try
            {
                InitializeFakeData();

                List<Contact> listContacts = new List<Contact>();

                for (int i = 0; i < contacts; i++)
                {
                    ////generate our fake data
                    Contact a = new Contact();

                    a.InitializeContact("ignore");

                    int maleorfemale = randomContactGenerator.Next(1, 100);

                    if (maleorfemale < 50)
                    {
                        a.FirstName = malenames[randomContactGenerator.Next(0, malenames.Count - 1)];
                    }
                    else
                    {
                        a.FirstName = femalenames[randomContactGenerator.Next(0, femalenames.Count - 1)];
                    }

                    a.LastName = lastnames[randomContactGenerator.Next(0, lastnames.Count - 1)];

                    AddressInfo addressInfo = addressInfos[randomContactGenerator.Next(0, addressInfos.Count - 1)];

                    a.Address1City = addressInfo.City;
                    //a.Address1Country = addressInfo.Country;
                    a.Address1Line1 = addressInfo.Address;
                    a.Telephone1 = addressInfo.Areacode + "-" + addressInfo.Telephone;
                    a.Address1PostalCode = addressInfo.Zipcode;
                    a.Address1StateOrProvince = addressInfo.State;
                    a.Age = randomContactGenerator.Next(18, 100);
                    a.EmailAddress1 = a.FirstName + "_" + a.LastName + "@live.com";

                    listContacts.Add(a);
                }

                return listContacts;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        /// <summary>
        /// this is used to randomize data for these fields
        /// the address line is not included but this data is real from census
        /// data
        /// </summary>
        private class AddressInfo
        {

            public AddressInfo()
            {
                this.Country = "US";

                this.Telephone = randomContactGenerator.Next(1, 9).ToString()
                                 + randomContactGenerator.Next(1, 9).ToString()
                                 + randomContactGenerator.Next(1, 9).ToString() + "-"
                                 + randomContactGenerator.Next(1, 9).ToString()
                                 + randomContactGenerator.Next(1, 9).ToString()
                                 + randomContactGenerator.Next(1, 9).ToString()
                                 + randomContactGenerator.Next(1, 9).ToString();

                this.Address = randomContactGenerator.Next(1, 9).ToString()
                               + randomContactGenerator.Next(1, 9).ToString()
                               + randomContactGenerator.Next(1, 9).ToString()
                               + randomContactGenerator.Next(1, 9).ToString() + " "
                               + lastnames[randomContactGenerator.Next(0, lastnames.Count - 1)];

                int streettype = randomContactGenerator.Next(1, 100);

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


        /// <summary>
        /// Used for our fake data - real male names
        /// </summary>
        private static List<string> malenames = new List<string>();

        /// <summary>
        /// real female names
        /// </summary>
        private static List<string> femalenames = new List<string>();

        /// <summary>
        /// real last names
        /// </summary>
        private static List<string> lastnames = new List<string>();

        /// <summary>
        /// real zip codes, area codes etc
        /// </summary>
        private static List<AddressInfo> addressInfos = new List<AddressInfo>();

        /// <summary>
        /// this is used to initialize our fake data and create it
        /// </summary>
        private static void InitializeFakeData()
        {
            string localpath = AppDomain.CurrentDomain.BaseDirectory;

            if (!localpath.EndsWith(@"\"))
            {
                localpath += @"\";
            }

            //// First read in our female names
            using (TextReader reader = File.OpenText(localpath + @"sampledata\female_names.txt"))
            {
                while (true)
                {
                    string femalename = reader.ReadLine();

                    if (!string.IsNullOrEmpty(femalename))
                    {
                        femalenames.Add(femalename);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            //// the first names
            using (TextReader reader = File.OpenText(localpath + @"sampledata\male_names.txt"))
            {
                while (true)
                {
                    string malename = reader.ReadLine();

                    if (!string.IsNullOrEmpty(malename))
                    {
                        malenames.Add(malename);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            //// no our last names
            using (TextReader reader = File.OpenText(localpath + @"sampledata\last_names.txt"))
            {
                while (true)
                {
                    string lastname = reader.ReadLine();

                    if (!string.IsNullOrEmpty(lastname))
                    {
                        lastnames.Add(lastname);
                    }
                    else
                    {
                        break;
                    }
                }
            }

            //// now our addressInfos, area codes etc
            using (TextReader reader = File.OpenText(localpath + @"sampledata\zip_code_database.txt"))
            {
                while (true)
                {
                    string lineinfo = reader.ReadLine();

                    if (!string.IsNullOrEmpty(lineinfo))
                    {
                        string[] items = lineinfo.Split('\t');

                        AddressInfo ai = new AddressInfo();

                        ai.Areacode = FixAreaCode(items[3]);



                        ai.Country = "US";
                        ai.City = items[1];
                        ai.Zipcode = FixZipCode(items[0]);
                        ai.Latitude = items[4];
                        ai.Longitude = items[5];
                        ai.State = items[2];

                        addressInfos.Add(ai);
                    }
                    else
                    {
                        break;
                    }
                }
            }
        }

        /// <summary>
        /// Sometimes the zipecode in the list is actually.. only 3/4 so the trailing zeros are gone i need to add them back
        /// </summary>
        /// <param name="zipcode">Test</param>
        /// <returns>dd</returns>
        private static string FixZipCode(string zipcode)
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
        [SuppressMessage("StyleCop.CSharp.ReadabilityRules", "SA1122:UseStringEmptyForEmptyStrings", Justification = "Reviewed. Suppression is OK here.")]
        private static string FixAreaCode(string areacode)
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
