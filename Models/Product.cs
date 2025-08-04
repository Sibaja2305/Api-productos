namespace EjercicioProductos.Models
{
    public class Product
    {
        public short C_product {  get; set; }
        public required string D_name { get; set; }
        public decimal P_price { get; set; }
        public short Q_stock { get; set; }
    }
}
