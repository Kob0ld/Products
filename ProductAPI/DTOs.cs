using System.ComponentModel.DataAnnotations;

namespace ProductAPI
{
    public class DTOs
    {
        public record ProductDTO(Guid Id, string ProductName, int ProductPrice, 
            DateTimeOffset CreatedTime, DateTimeOffset ModifiedTime);

        public record CreateProductDTO([Required] string ProductName, [Range(0, 10000)] int ProductPrice);

        public record UpdateProductDTO([Required] string ProductName, [Range(0, 10000)] int ProductPrice);
    }
}
