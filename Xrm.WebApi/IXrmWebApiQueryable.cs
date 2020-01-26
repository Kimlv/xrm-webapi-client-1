/*
 * Copyright (c) 2020 Tobias Heilig
 * 
 * BSD 3-Clause
 * see LICENCE file for details.
 */

namespace Xrm.WebApi
{
    /// <summary>
    /// Makes an entity queryable with <see cref="XrmWebApiClient"/>.
    /// </summary>
    public interface IXrmWebApiQueryable
    {
        /// <summary>
        /// The entity logical name in plural.
        /// </summary>
        string EntityLogicalNamePlural { get; }
    }
}
