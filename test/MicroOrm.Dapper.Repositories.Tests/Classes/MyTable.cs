using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MicroOrm.Dapper.Repositories.Tests.Classes
{
    [Table("MyTable")]
    public class MyTable
    {
        [Key]
        [Column("MyTableId")]
        public long Id { get; set; }

        public bool IsPublic { get; set; }
    }
}
