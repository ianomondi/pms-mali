using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PMS.Domain
{
    public class BaseEntity
    {
        [Key]
        public Guid Id { get; set; }
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public DateTime DateCreated { get; set; }
        [ScaffoldColumn(false)]
        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? LastModified { get; set; }
        [ScaffoldColumn(false)]
        //[StringLength(255)]
        public Nullable<Guid> CreatedById { get; set; }
        [ScaffoldColumn(false)]
        //[StringLength(255)]
        public Nullable<Guid> ModifiedById { get; set; }
        [ScaffoldColumn(false)]
        public bool IsDeleted { get; set; } = false;
    }
}