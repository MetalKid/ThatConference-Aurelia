#region << Usings >>

using System;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using ThatConference.Data.Common.Attributes;

#endregion

namespace ThatConference.Data.Common.EntityFramework
{
    /// <summary>
    /// This class acts as an intermediate to the EntityTypeConfiguration class.
    /// This will apply common mappings that EF does not support out of the box and we enforce 
    /// EntityBase as the generic parameter.
    /// </summary>
    /// <typeparam name="TEntity">The type of entity to map.</typeparam>
    public class EntityMapping<TEntity> : EntityTypeConfiguration<TEntity> where TEntity : EntityBase
    {

        #region << Constructors >>

        /// <summary>
        /// Default construtor that runs common mappings that EF does not support out of the box.
        /// </summary>
        public EntityMapping()
        {
            var entityType = typeof(TEntity);
            ApplyPrecisionAndScale(entityType);
        }

        #endregion

        #region << Helper Methods >>

        /// <summary>
        /// Applies the correct precision and scale to all properties of this entity type with the 
        /// DecimalPrecision Attribute.
        /// </summary>
        /// <param name="entityType">The type of class to check for DecimalPrecision properites.</param>
        private void ApplyPrecisionAndScale(Type entityType)
        {
            var props = entityType.GetProperties(BindingFlags.Public | BindingFlags.Instance)
                .Where(p => p.GetCustomAttribute<DecimalPrecisionAttribute>() != null)
                .Select(p => new { Prop = p, Attr = p.GetCustomAttribute<DecimalPrecisionAttribute>(false) });

            foreach (var propAttr in props)
            {
                var param = Expression.Parameter(entityType, "a");
                var property = Expression.Property(param, propAttr.Prop.Name);
                var lambdaExpression = Expression.Lambda(property, true, new[] { param });
                Type parameterType;
                if (propAttr.Prop.PropertyType.IsGenericType &&
                    propAttr.Prop.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                {
                    parameterType = typeof(Expression<Func<TEntity, decimal?>>);
                }
                else
                {
                    parameterType = typeof(Expression<Func<TEntity, decimal>>);
                }

                //NOTE: Get the right overload
                var methodInfo = GetType().GetMethods().Single(p =>
                    p.Name == "Property" &&
                    p.GetParameters().Length > 0 &&
                    p.GetParameters()[0].ParameterType == parameterType);
                var decimalConfig = methodInfo.Invoke(this, new object[] { lambdaExpression })
                        as DecimalPropertyConfiguration;

                if (decimalConfig != null)
                {
                    decimalConfig.HasPrecision(propAttr.Attr.Precision, propAttr.Attr.Scale);
                }
            }
        }

        #endregion

    }
}