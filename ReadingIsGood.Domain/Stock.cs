namespace ReadingIsGood.Domain
{
    public class Stock : BaseModel
    {
        public string Name { get; set; }

        public double Price { get; set; }

        public int Count { get; set; }
    }
}
