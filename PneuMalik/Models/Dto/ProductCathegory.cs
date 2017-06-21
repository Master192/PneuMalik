namespace PneuMalik.Models.Dto
{
    public class ProductCathegory : Cathegory
    {

        public int ItemsOnPage { get; set; }
        public Product.ProductType Type { get; set; }

    }
}