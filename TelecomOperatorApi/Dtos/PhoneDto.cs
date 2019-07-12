namespace TelecomOperatorApi.Dtos
{
    public class PhoneDto
    {
        public int Id { get; set; }
        public int CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string PhoneNo { get; set; }
        public bool Activated { get; set; }
    }
}
