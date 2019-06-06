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


    /// <summary>
    /// instance of a field
    /// </summary>
    [Serializable]
    public class Field : FieldDefinition
    {
        private string fieldName;

        private FieldDataType dataType;

        private int fieldDataSize;

        /// <summary>
        /// The name including the column family
        /// </summary>
        public override string FieldName
        {
            get
            {
                return this.fieldName;
            }

            set
            {
                this.fieldName = value;
            }
        }

        /// <summary>
        /// the type of data
        /// </summary>
        public override FieldDataType DataType
        {
            get
            {
                return this.dataType;
            }

            set
            {
                this.dataType = value;
            }
        }

        /// <summary>
        /// The size of the data max (really in hbase there is none but some code might need it)
        /// </summary>
        public override int FieldDataSize
        {
            get
            {
                return this.fieldDataSize;
            }

            set
            {
                this.fieldDataSize = value;
            }
        }
    }
}
