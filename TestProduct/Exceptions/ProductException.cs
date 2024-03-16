namespace TestProduct.Exceptions
{
    public class ProductException : Exception
    {
        public int StatusCode { get; set; }

        public ProductException(int code, string message) : base(message)
        {
            StatusCode = code;
        }
    }
}
