using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopBridge.Model.Products
{
    public class InventoryDetails
    {
        [Key]
        public int UnniqId { get; set; }
        [Column("ProductName")]
        public string name { get; set; }
        [Column("ProductPrice")]
        public decimal? price { get; set; }
        [Column("Description")]
        public string description { get; set; }
        [Column("extension")]
        public string extension { get; set; }
    }
}
