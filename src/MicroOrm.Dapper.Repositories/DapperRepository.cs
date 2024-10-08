using MicroOrm.Dapper.Repositories.Contract;
using MicroOrm.Dapper.Repositories.SqlGenerator.Contract;

using System.Data;

namespace MicroOrm.Dapper.Repositories
{
    /// <summary>
    ///     Base Repository
    /// </summary>
    public partial class DapperRepository<TEntity> : ReadOnlyDapperRepository<TEntity>, IDapperRepository<TEntity>
        where TEntity : class
    {
        /// <summary>
        ///     Constructor
        /// </summary>
        public DapperRepository(IDbConnection connection)
            : base(connection)
        {
        }

        /// <summary>
        ///     Constructor
        /// </summary>
        public DapperRepository(IDbConnection connection, ISqlGenerator<TEntity> sqlGenerator)
            : base(connection, sqlGenerator)
        {
        }
    }
}
