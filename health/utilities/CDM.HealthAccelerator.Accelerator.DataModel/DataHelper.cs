// --------------------------------------------------------------------------------------------------------------------
// <copyright file="DataHelper.cs" company="Microsoft">
//   Copyright (c) Microsoft Corporation. All rights reserved.
// </copyright>
// 
// // <summary>
//   53bb5329-2614-4929-8755-79301336824f
// </summary>
// --------------------------------------------------------------------------------------------------------------------

namespace CDM.Health.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 53bb5329-2614-4929-8755-79301336824f
    /// </summary>
    public static class DataHelper
    {

        /// <summary>
        /// ebec1c8d-b719-4603-b11f-f5feaa6b5af9
        /// </summary>
        /// <param name="objectType">
        /// The object Type.
        /// </param>
        /// <param name="ignore">
        /// The ignore.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<string> GetPropertyNamesList(object objectType, string[] ignore)
        {
            List<string> properties = new List<string>();

            string data = string.Empty;

            PropertyInfo[] propertyInfos;
            Type incType = objectType.GetType().UnderlyingSystemType;

            ////propertyInfos = typeof(incType).GetProperties();
            propertyInfos = incType.GetProperties();

            Array.Sort(propertyInfos, 
                    delegate(PropertyInfo propertyInfo1, PropertyInfo propertyInfo2)
                    { return propertyInfo1.Name.CompareTo(propertyInfo2.Name); });

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                bool add = true;

                if (ignore != null)
                {
                    foreach (string s in ignore)
                    {

                        if (propertyInfo.Name.ToLower() == s)
                        {
                            add = false;
                            break;
                        }
                    }
                }
                else
                {
                    add = true;
                }

                if (add)
                {
                    properties.Add(propertyInfo.Name);
                }
            }

            return properties;

        }


        /// <summary>
        /// 95907ec2-2de3-427d-b73e-11dd8d65e4b9
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object GetPropValue(this object obj, string name)
        {
            try
            {
                foreach (string part in name.Split('.'))
                {
                    if (obj == null) { return null; }

                    Type type = obj.GetType();
                    PropertyInfo info = type.GetProperty(part);
                    if (info == null) { return null; }

                    obj = info.GetValue(obj, null);
                }

                return obj;
            }
            catch
            {
                return null;
            }

        }


        /// <summary>
        /// 7db984c4-b3ea-4e54-a1f1-56a6cbaa45a5
        /// </summary>
        public enum ObjectValueType
        {
            /// <summary>
            /// The int.
            /// </summary>
            Int = 1, 

            /// <summary>
            /// The date time.
            /// </summary>
            DateTime = 2, 

            /// <summary>
            /// The string.
            /// </summary>
            String = 3, 

            /// <summary>
            /// The guid.
            /// </summary>
            Guid = 4, 

            /// <summary>
            /// The money.
            /// </summary>
            Money = 5, 

            /// <summary>
            /// The float.
            /// </summary>
            Float = 6, 

            /// <summary>
            /// The decimal.
            /// </summary>
            Decimal = 7
        }

        /// <summary>
        /// 8fa31eb6-fa03-4357-b212-54b09e01a881
        /// </summary>
        /// <param name="obj">
        /// The obj.
        /// </param>
        /// <param name="name">
        /// The name.
        /// </param>
        /// <param name="newvalue">
        /// The newvalue.
        /// </param>
        /// <returns>
        /// The <see cref="object"/>.
        /// </returns>
        public static object SetPropValue(this object obj, string name, object newvalue)
        {
            try
            {
                foreach (string part in name.Split('.'))
                {
                    if (obj == null) { return null; }

                    Type type = obj.GetType();
                    PropertyInfo info = type.GetProperty(part);
                    if (info == null) { return null; }


                    object castednewvalue = Convert.ChangeType(newvalue, Type.GetType(info.PropertyType.FullName));

                    info.SetValue(obj, castednewvalue);

                    obj = info.GetValue(obj, null);
                }

                return obj;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.ToString());
                return null;
            }

        }


        /// <summary>
        /// ebec1c8d-b719-4603-b11f-f5feaa6b5af9
        /// </summary>
        /// <param name="objectType">
        /// The object Type.
        /// </param>
        /// <param name="ignore">
        /// The ignore.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static Dictionary<string,Field> GetEntityFieldList(object objectType, string[] ignore)
        {
            Dictionary<string,Field> fields = new Dictionary<string, Field>();

            string data = string.Empty;

            PropertyInfo[] propertyInfos;
            Type incType = objectType.GetType().UnderlyingSystemType;

            ////propertyInfos = typeof(incType).GetProperties();
            propertyInfos = incType.GetProperties();

            Array.Sort(propertyInfos,
                    delegate (PropertyInfo propertyInfo1, PropertyInfo propertyInfo2)
                    { return propertyInfo1.Name.CompareTo(propertyInfo2.Name); });

            foreach (PropertyInfo propertyInfo in propertyInfos)
            {
                bool add = true;

                if (ignore != null)
                {
                    foreach (string s in ignore)
                    {

                        if (propertyInfo.Name.ToLower() == s)
                        {
                            add = false;
                            break;
                        }
                    }
                }
                else
                {
                    add = true;
                }

                if (add)
                {
                    //// purposely just default some stuff for testing
                    Field field = new Field();

                    field.FieldName = propertyInfo.Name;
                    field.FieldDataSize = 5000;
                    field.DataType = FieldDefinition.FieldDataType.String;

                    fields.Add(field.FieldName, field);
                }
            }

            return fields;

        }
    }
}
