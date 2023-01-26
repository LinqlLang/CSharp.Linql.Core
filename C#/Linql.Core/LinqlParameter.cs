﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Linql.Core
{
    public class LinqlParameter : LinqlExpression
    {
        public string ParameterName { get; set; }

        public LinqlParameter(string ParameterName) 
        {
            this.ParameterName = ParameterName;
        }
    }
}