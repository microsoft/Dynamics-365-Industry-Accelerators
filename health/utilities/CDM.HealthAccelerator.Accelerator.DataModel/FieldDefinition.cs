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
namespace CDM.HealthAccelerator.DataModel
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    [Serializable]
    /// <summary>
    /// our abstract field definition
    /// </summary>
    public abstract class FieldDefinition
    {
        /// <summary>
        /// the type of field it is
        /// </summary>
        public enum FieldDataType
        {
            String,
            Integer,
            Money,
            Guid
        }

        /// <summary>
        /// The name including the column family
        /// </summary>
        public abstract string FieldName { get; set; }

        /// <summary>
        /// the type of data
        /// </summary>
        public abstract FieldDataType DataType { get; set; }

        /// <summary>
        /// The size of the data max (really in hbase there is none but some code might need it)
        /// </summary>
        public abstract int FieldDataSize { get; set; }


    }
}
