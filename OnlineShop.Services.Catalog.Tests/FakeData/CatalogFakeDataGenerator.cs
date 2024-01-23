using OnlineShop.Services.Catalog.Application.Models.Dto;
using OnlineShop.Services.Catalog.Domain.Models.Data;

namespace OnlineShop.Services.Catalog.Tests.FakeData
{
    public class CatalogFakeDataGenerator
    {
        public Product GenerateFakeProduct()
        {
            return new Faker<Product>()
               .RuleFor(product => product.Id, faker => Guid.NewGuid().ToString())
               .RuleFor(product => product.Name, faker => faker.Random.Word())
               .RuleFor(product => product.Price, faker => faker.Random.Decimal())
               .RuleFor(product => product.Description, faker => faker.Lorem.Paragraph())
               .RuleFor(product => product.Category, faker => faker.Random.Word())
               .RuleFor(product => product.OptionalFields, faker => GenerateFakeOptionalFields(faker))
               .Generate();
        }

        public ProductDto GenerateFakeProductDto()
        {
            return new Faker<ProductDto>()
                .RuleFor(product => product.Id, faker => Guid.NewGuid().ToString())
                .RuleFor(product => product.Name, faker => faker.Random.Word())
                .RuleFor(product => product.Price, faker => faker.Random.Decimal())
                .RuleFor(product => product.Description, faker => faker.Lorem.Paragraph())
                .RuleFor(product => product.Category, faker => faker.Random.Word())
                .RuleFor(product => product.OptionalFields, faker => GenerateFakeOptionalFields(faker))
                .Generate();
        }

        public NewProductDto GenerateFakeNewProductDto()
        {
            return new Faker<NewProductDto>()
                .RuleFor(product => product.Name, faker => faker.Random.Word())
                .RuleFor(product => product.Price, faker => faker.Random.Decimal())
                .RuleFor(product => product.Description, faker => faker.Lorem.Paragraph())
                .RuleFor(product => product.Category, faker => faker.Random.Word())
                .RuleFor(product => product.OptionalFields, faker => GenerateFakeOptionalFields(faker))
                .Generate();
        }

        public string GenerateFakeProductId()
        {
            return Guid.NewGuid().ToString();
        }

        private Dictionary<string, string> GenerateFakeOptionalFields(Faker faker, int minAmount = 1, int maxAmount = 5)
        {
            var optionalFields = new Dictionary<string, string>();

            int numberOfOptionalFields = faker.Random.Int(minAmount, maxAmount);

            for (int i = 0; i < numberOfOptionalFields; i++)
            {
                string fieldName = faker.Random.Word();

                string fieldValue = faker.Random.Word();

                optionalFields.Add(fieldName, fieldValue);
            }

            return optionalFields;
        }
    }
}
