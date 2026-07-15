using System.ComponentModel.DataAnnotations;

namespace PaymentProcessing.Domain
{
    public abstract class EntityBase : IEntity<long>
    {
        [Key]
        public long Id {  get; set; }
    }
}
