using MicroOrm.Dapper.Repositories.Contract.Attributes;
using MicroOrm.Dapper.Repositories.SqlGenerator.Contract;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace MicroOrm.Dapper.Repositories.SqlGenerator
{
    /// <inheritdoc />
    public partial class SqlGenerator<TEntity>
        where TEntity : class
    {
        /// <inheritdoc />
        public virtual SqlQuery GetBulkInsert(IEnumerable<TEntity> entities)
        {
            var entitiesArray = entities as TEntity[] ?? entities.ToArray();
            if (!entitiesArray.Any())
                throw new ArgumentException("collection is empty");

            var entityType = entitiesArray[0].GetType();

            var properties =
                (IsIdentity
                    ? SqlProperties.Where(p => !p.PropertyName.Equals(IdentitySqlProperty.PropertyName, StringComparison.OrdinalIgnoreCase))
                    : SqlProperties).ToList();

            var query = new SqlQuery();

            var values = new List<string>();
            var parameters = new Dictionary<string, object>();

            for (var i = 0; i < entitiesArray.Length; i++)
            {
                var entity = entitiesArray[i];
                if (HasUpdatedAt)
                {
                    var attribute = UpdatedAtProperty.GetCustomAttribute<UpdatedAtAttribute>();
                    var offset = attribute.TimeKind == DateTimeKind.Local
                        ? new DateTimeOffset(DateTime.Now)
                        : new DateTimeOffset(DateTime.UtcNow);
                    if (attribute.OffSet != 0)
                    {
                        offset = offset.ToOffset(TimeSpan.FromHours(attribute.OffSet));
                    }

                    UpdatedAtProperty.SetValue(entity, offset.DateTime);
                }

                foreach (var property in properties)
                    // ReSharper disable once PossibleNullReferenceException
                    parameters.Add(property.PropertyName + i, entityType.GetProperty(property.PropertyName).GetValue(entity, null));

                values.Add(string.Format("({0})", string.Join(", ", properties.Select(p => "@" + p.PropertyName + i))));
            }

            query.SqlBuilder.AppendFormat("INSERT INTO {0} ({1}) VALUES {2}", TableName, string.Join(", ", properties.Select(p => p.ColumnName)),
                string.Join(",", values)); // values

            query.SetParam(parameters);

            return query;
        }
    }
}
