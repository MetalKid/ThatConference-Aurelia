#region << Usings >>

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

#endregion

namespace ThatConference.Common.Filters
{
    /// <summary>
    /// This class adds common extensions to IQueryable.
    /// </summary>
    public static class QueryableExtensions
    {

        #region << WhereIn/NotIn Methods >>

        /// <summary>
        /// Appends to the given query by appending ".Where" for each item in the list and 
        /// returns the query for chaining.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="query">The current query being created.</param>
        /// <param name="selector">The column being filtered.</param>
        /// <param name="collection">The valid values to check against.</param>
        /// <returns>The query.</returns>
        public static IQueryable<TEntity> WhereIn<TEntity, TValue>(
            this IQueryable<TEntity> query, Expression<Func<TEntity, TValue>> selector, IEnumerable<TValue> collection)
        {
            return WhereIn(query, selector, collection, true);
        }

        /// <summary>
        /// Appends to the given query by appending ".Where" for each item in the list and 
        /// returns the query for chaining.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="query">The current query being created.</param>
        /// <param name="selector">The column being filtered.</param>
        /// <param name="collection">The valid values to check against.</param>
        /// <param name="invalidQueryIfEmptyCollection">
        /// True if an empty list should return no results; false otherwise.
        /// </param>
        /// <returns>The query.</returns>
        public static IQueryable<TEntity> WhereIn<TEntity, TValue>(
            this IQueryable<TEntity> query, 
            Expression<Func<TEntity, TValue>> selector, 
            IEnumerable<TValue> collection, 
            bool invalidQueryIfEmptyCollection)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            if (collection == null) throw new ArgumentNullException("collection");

            List<TValue> temp = new List<TValue>();
            temp.AddRange(collection);

            if (temp.Count > 0)
            {
                // To prevent EF from building new a new query every time a list varies by 1 item. 
                // (Now new queries will be built by a factor of 5)
                // i.e. whether 1 or 5 items, there will always be 5 items here to speed things up
                while (temp.Count < Math.Ceiling(temp.Count / 5M) * 5)
                    temp.Add(temp.First());
            }

            ParameterExpression p = selector.Parameters.Single();

            if (!temp.Any())
            {
                return invalidQueryIfEmptyCollection ? query.Where(a => 1 == 2) : query;
            }

            IEnumerable<Expression> equals = temp.Select(value =>
                (Expression)Expression.Equal(selector.Body,
                    Expression.Constant(value, typeof(TValue))));

            Expression body = equals.Aggregate(Expression.Or);

            return query.Where(Expression.Lambda<Func<TEntity, bool>>(body, p));
        }

        /// <summary>
        /// Appends to the given query by appending ".Where Not" for each item in the list and 
        /// returns the query for chaining.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="query">The current query being created.</param>
        /// <param name="selector">The column being filtered.</param>
        /// <param name="collection">The valid values to check against.</param>
        /// <returns>The query.</returns>
        public static IQueryable<TEntity> WhereNotIn<TEntity, TValue>(
            this IQueryable<TEntity> query, Expression<Func<TEntity, TValue>> selector, IEnumerable<TValue> collection)
        {
            return WhereNotIn(query, selector, collection, true);
        }

        /// <summary>
        /// Appends to the given query by appending ".Where Not" for each item in the list and 
        /// returns the query for chaining.
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="query">The current query being created.</param>
        /// <param name="selector">The column being filtered.</param>
        /// <param name="collection">The valid values to check against.</param>
        /// <param name="invalidQueryIfEmptyCollection">
        /// True if an empty list should return no results; false otherwise.
        /// </param>
        /// <returns>The query.</returns>
        public static IQueryable<TEntity> WhereNotIn<TEntity, TValue>(
            this IQueryable<TEntity> query, 
            Expression<Func<TEntity, TValue>> selector, 
            IEnumerable<TValue> collection, 
            bool invalidQueryIfEmptyCollection)
        {
            if (selector == null) throw new ArgumentNullException("selector");
            if (collection == null) throw new ArgumentNullException("collection");

            List<TValue> temp = new List<TValue>();
            temp.AddRange(collection);

            if (temp.Count > 0)
            {
                // To prevent EF from building new a new query every time a list varies by 1 item. 
                // (Now new queries will be built by a factor of 5)
                // i.e. whether 1 or 5 items, there will always be 5 items here to speed things up
                while (temp.Count < Math.Ceiling(temp.Count / 5M) * 5)
                    temp.Add(temp.First());
            }

            ParameterExpression p = selector.Parameters.Single();

            if (!temp.Any())
            {
                return invalidQueryIfEmptyCollection ? query.Where(a => 1 == 2) : query;
            }
            IEnumerable<Expression> notEquals = temp.Select(value =>
                (Expression)Expression.NotEqual(selector.Body,
                    Expression.Constant(value, typeof(TValue))));

            Expression body = notEquals.Aggregate(Expression.And);

            return query.Where(Expression.Lambda<Func<TEntity, bool>>(body, p));
        }

        #endregion

    }
}