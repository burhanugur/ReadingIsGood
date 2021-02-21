namespace ReadingIsGood.Domain
{
    public class User : BaseModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public Customer Customer { get; set; }
    }
}
