using MicroOrm.Dapper.Repositories.Contact.Config;
using MicroOrm.Dapper.Repositories.SqlGenerator.Contract;

using System.Collections.Generic;
using System.Reflection;

#pragma warning disable

namespace MicroOrm.Dapper.Repositories.SqlGenerator
{
    /// <inheritdoc />
    public partial class SqlGenerator<TEntity> : ISqlGenerator<TEntity>
        where TEntity : class
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public SqlGenerator()
        {
            Provider = MicroOrmConfig.SqlProvider;

            if (UseQuotationMarks == null)
                UseQuotationMarks = Provider != SqlProvider.SQLite && MicroOrmConfig.UseQuotationMarks;

            Initialize();
        }

        /// <summary>
        /// Constructor with params
        /// </summary>
        public SqlGenerator(SqlProvider provider, bool useQuotationMarks)
        {
            Provider = provider;
            UseQuotationMarks = provider != SqlProvider.SQLite && useQuotationMarks;
            Initialize();
        }

        /// <summary>
        /// Constructor with params
        /// </summary>
        public SqlGenerator(SqlProvider provider)
        {
            Provider = provider;
            UseQuotationMarks = false;
            Initialize();
        }

        private enum QueryType
        {
            Select,
            Delete,
            Update
        }

        /// <inheritdoc />
        public PropertyInfo[] AllProperties { get; protected set; }

        /// <inheritdoc />
        public bool HasUpdatedAt => UpdatedAtProperty != null;

        /// <inheritdoc />
        public SqlPropertyMetadata IdentitySqlProperty { get; protected set; }

        /// <inheritdoc />
        public bool IsIdentity => IdentitySqlProperty != null;

        /// <inheritdoc />
        public Dictionary<string, PropertyInfo> JoinsLogicalDelete { get; protected set; }

        /// <inheritdoc />
        public SqlPropertyMetadata[] KeySqlProperties { get; protected set; }

        /// <inheritdoc />
        public bool LogicalDelete { get; protected set; }

        /// <inheritdoc />
        public object LogicalDeleteValue { get; protected set; }

        /// <summary>
        ///
        /// </summary>
        public SqlProvider Provider { get; }

        /// <inheritdoc />
        public SqlJoinPropertyMetadata[] SqlJoinProperties { get; protected set; }

        /// <inheritdoc />
        public SqlPropertyMetadata[] SqlProperties { get; protected set; }

        /// <inheritdoc />
        public string StatusPropertyName { get; protected set; }

        /// <inheritdoc />
        public string TableName { get; protected set; }

        /// <inheritdoc />
        public string TableSchema { get; protected set; }

        /// <inheritdoc />
        public PropertyInfo UpdatedAtProperty { get; protected set; }

        /// <inheritdoc />
        public SqlPropertyMetadata UpdatedAtPropertyMetadata { get; protected set; }

        /// <summary>
        ///
        /// </summary>
        public bool? UseQuotationMarks { get; set; }

        private void Initialize()
        {
            // Order is important
            InitProperties();
            InitConfig();
            InitLogicalDeleted();
        }
    }
}
