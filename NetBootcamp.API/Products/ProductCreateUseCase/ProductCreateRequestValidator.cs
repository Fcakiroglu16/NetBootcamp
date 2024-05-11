using FluentValidation;

namespace NetBootcamp.API.Products.ProductCreateUseCase
{
    public class ProductCreateRequestValidator : AbstractValidator<ProductCreateRequestDto>
    {
        public ProductCreateRequestValidator(IProductRepository productRepository)
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("{PropertyName} is required")
                .NotNull().WithMessage("{PropertyName} is required")
                .Length(5, 10).WithMessage("{PropertyName} must be between 5 and 10 characters")
                .Must(productName => ExistProductName(productRepository, productName))
                .WithMessage("Product name already exists.");


            RuleFor(x => x.Price)
                .InclusiveBetween(1, 1000).WithMessage("Fiyat alanı 1 ile 100 arasında olmalıdır.");

            //RuleFor(x => x.IdentityNo).Length(11).WithMessage("TC numarası 11 haneli olmalıdır.").Must(CheckIdentityNo)
            //    .WithMessage("Tc numarası hatalıdır.");
        }


        public bool ExistProductName(IProductRepository productRepository, string name)
        {
            return !productRepository.IsExists(name);

            //var hasProduct = ;


            //return !hasProduct;


            //if (hasProduct)
            //{
            //    return false;
            //}
            //else
            //{
            //    return true;
            //}
        }

        public bool CheckIdentityNo(string identityNo)
        {
            //Action delegate => void
            //Predicate delegate => bool
            // Func delegate = dynamic


            // business validation
            return true;
        }
    }
}