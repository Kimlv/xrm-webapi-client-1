﻿/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

using System;

namespace Xrm.WebApi
{
    public class EntityLogicalNameAttribute : Attribute
    {
        public string EntityLogicalName { get; }

        public EntityLogicalNameAttribute(string entityLogicalName)
        {
            EntityLogicalName = entityLogicalName;
        }
    }
}
