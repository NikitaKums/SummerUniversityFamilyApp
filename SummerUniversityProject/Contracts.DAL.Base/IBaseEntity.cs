using System;

namespace Contracts.DAL.Base
{
    /// <summary>
    /// Int based interface for entity meta fields
    /// </summary>
    public interface IBaseEntity : IBaseEntity<int>
    {
    }

    /// <summary>
    /// Generic interface for entity meta fields
    /// </summary>
    /// <typeparam name="TKey">Primary key type</typeparam>
    public interface IBaseEntity<TKey> 
        where TKey: struct, IComparable
    {
        TKey Id { get; set;}
    }
}