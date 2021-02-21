namespace ReadingIsGood.Core.Model
{
    public   class ServiceResponse<T>
    {
        public T Result { get; set; }

        public bool IsSuccess { get; set; } = true;

        public string Message { get; set; }
    }
}
