using System.Collections.Generic;
using System.Reflection;
using MicroOrm.Dapper.Repositories.Contact.Config;
using MicroOrm.Dapper.Repositories.SqlGenerator.Contract;
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

        private void Initialize()
        {
            // Order is important
            InitProperties();
            InitConfig();
            InitLogicalDeleted();
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

        /// <summary>
        /// 
        /// </summary>
        public SqlProvider Provider { get; }

        /// <summary>
        /// 
        /// </summary>
        public bool? UseQuotationMarks { get; set; }

        /// <inheritdoc />
        public PropertyInfo[] AllProperties { get; protected set; }

        /// <inheritdoc />
        public bool HasUpdatedAt => UpdatedAtProperty != null;

        /// <inheritdoc />
        public PropertyInfo UpdatedAtProperty { get; protected set; }

        /// <inheritdoc />
        public SqlPropertyMetadata UpdatedAtPropertyMetadata { get; protected set; }

        /// <inheritdoc />
        public bool IsIdentity => IdentitySqlProperty != null;

        /// <inheritdoc />
        public string TableName { get; protected set; }

        /// <inheritdoc />
        public string TableSchema { get; protected set; }

        /// <inheritdoc />
        public SqlPropertyMetadata IdentitySqlProperty { get; protected set; }

        /// <inheritdoc />
        public SqlPropertyMetadata[] KeySqlProperties { get; protected set; }

        /// <inheritdoc />
        public SqlPropertyMetadata[] SqlProperties { get; protected set; }

        /// <inheritdoc />
        public SqlJoinPropertyMetadata[] SqlJoinProperties { get; protected set; }

        /// <inheritdoc />
        public bool LogicalDelete { get; protected set; }

        /// <inheritdoc />
        public Dictionary<string, PropertyInfo> JoinsLogicalDelete { get; protected set; }

        /// <inheritdoc />
        public string StatusPropertyName { get; protected set; }

        /// <inheritdoc />
        public object LogicalDeleteValue { get; protected set; }


        private enum QueryType
        {
            Select,
            Delete,
            Update
        }
    }
}
