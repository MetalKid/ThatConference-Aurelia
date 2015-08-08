#region << Usings >>

using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Reflection;
using ThatConference.Common.Enums;
using ThatConference.Common.Interfaces;

#endregion

namespace ThatConference.Data.Common
{
    /// <summary>
    /// This class defines all common columns/methods for all Entities.
    /// </summary>
    public class EntityBase : IEntity
    {

        #region << Variables >>

        private readonly static ConcurrentDictionary<Type, PropertyInfo[]> _typeCache =
            new ConcurrentDictionary<Type, PropertyInfo[]>();

        private readonly static Type _entityBaseType = typeof(EntityBase);
        private readonly static Type _enuemrableType = typeof(IEnumerable);

        #endregion

        #region << Columns Properties >>

        /// <summary>
        /// Gets or sets the Timestamp column on the table.
        /// </summary>
        [Column("Timestamp", Order = 0, TypeName = "timestamp")]
        [Timestamp]
        public byte[] Timestamp { get; set; }

        #endregion

        #region << Properties >>

        /// <summary>
        /// Gets or sets the state of this entity. (i.e. Unchanged, New, Modified, Deleted)
        /// </summary>
        [NotMapped]
        public DataStateEnum DataState { get; set; }

        #endregion

        #region << Entity Methods >>

        /// <summary>
        /// Sets the timestamp based upon the base-64 encoded string.
        /// </summary>
        /// <param name="base64EncodedTimestamp">Timestamp in string representation.</param>
        public void SetBase64Timestamp(string base64EncodedTimestamp)
        {
            Timestamp = Convert.FromBase64String(base64EncodedTimestamp);
        }

        /// <summary>
        /// Returns a list of all the child entities on this parent entity.
        /// </summary>
        /// <returns>List of child entities.</returns>
        public IList<EntityBase> GetChildEntities()
        {
            List<EntityBase> children = new List<EntityBase>();

            PropertyInfo[] props;
            var myType = GetType();

            if (!_typeCache.TryGetValue(myType, out props))
            {
                props = myType.GetProperties();
                _typeCache.TryAdd(myType, props);
            }
            
            foreach (var prop in props)
            {
                if (_entityBaseType.IsAssignableFrom(prop.PropertyType))
                {
                    var child = prop.GetValue(this);
                    if (child != null)
                    {
                        children.Add(child as EntityBase);
                    }
                }
                else if (_enuemrableType.IsAssignableFrom(prop.PropertyType))
                {
                    var genericType = prop.PropertyType.GetGenericArguments().FirstOrDefault();
                    if (genericType == null || !_entityBaseType.IsAssignableFrom(genericType))
                    {
                        continue;
                    }
                    var items = prop.GetValue(this) as IEnumerable;
                    if (items == null)
                    {
                        continue;
                    }
                    children.AddRange(
                        from object child in items 
                        where child != null 
                        select child as EntityBase);
                }
            }
            return children;
        }

        #endregion

    }
}