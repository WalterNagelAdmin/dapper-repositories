using MicroOrm.Dapper.Repositories.Contract.Attributes;

using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;

namespace MicroOrm.Dapper.Repositories.SqlGenerator.Contract
{
    /// <summary>
    ///     Metadata from PropertyInfo
    /// </summary>
    public class SqlPropertyMetadata
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public SqlPropertyMetadata(PropertyInfo propertyInfo)
        {
            PropertyInfo = propertyInfo;
            var alias = PropertyInfo.GetCustomAttribute<ColumnAttribute>();
            if (!string.IsNullOrEmpty(alias?.Name))
            {
                Alias = alias.Name;
                ColumnName = Alias;
            }
            else
            {
                ColumnName = PropertyInfo.Name;
            }

            CleanColumnName = ColumnName;

            var ignoreUpdate = PropertyInfo.GetCustomAttribute<IgnoreUpdateAttribute>();
            if (ignoreUpdate != null)
                IgnoreUpdate = true;

            IsNullable = propertyInfo.PropertyType.IsGenericType && propertyInfo.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>);
        }

        /// <summary>
        ///     Alias for ColumnName
        /// </summary>
        public string Alias { get; set; }

        /// <summary>
        ///     ColumnName
        /// </summary>
        public string CleanColumnName { get; set; }

        /// <summary>
        ///     ColumnName
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        ///     Exclude property from update
        /// </summary>
        public bool IgnoreUpdate { get; set; }

        /// <summary>
        ///     Check if is nullable
        /// </summary>
        public bool IsNullable { get; set; }

        /// <summary>
        ///     Original PropertyInfo
        /// </summary>
        public PropertyInfo PropertyInfo { get; }

        /// <summary>
        ///     PropertyName
        /// </summary>
        public virtual string PropertyName => PropertyInfo.Name;
    }
}
