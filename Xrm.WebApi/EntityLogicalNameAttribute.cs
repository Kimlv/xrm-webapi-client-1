/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

using System;

namespace Xrm.WebApi
{
    /// <summary>
    /// Attribute making an entity class create-/update-/deletable
    /// by the <see cref="XrmWebApiClient"/>.
    /// </summary>
    [AttributeUsage(AttributeTargets.Class)]
    public class EntityLogicalNameAttribute : Attribute
    {
        /// <summary>
        /// The logical name of the entity represented by the class.
        /// </summary>
        public string EntityLogicalName { get; }

        /// <summary>
        /// Initializes an <see cref="EntityLogicalNameAttribute"/>.
        /// </summary>
        /// <param name="entityLogicalName">
        /// The logical name of the entity represented by the class
        /// </param>
        public EntityLogicalNameAttribute(string entityLogicalName)
        {
            EntityLogicalName = entityLogicalName;
        }
    }
}
