using Common.Enums;
using Data.Abstraction;

namespace Data.Models
{

    public class Cart : BaseEntityModel
    {
        public CartStatus Status { get; set; }
    }
}
