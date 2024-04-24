namespace QuasarShop.Models
{
    public class GetCommentsViewModel
    {
        public Guid Id { get; set; }
        public string Text { get; set; }
        public int Rating { get; set; }
        public bool Enabled { get; set; }
        public DateTime Created { get; set; }

        //User
        public Guid UserId { get; set; }
        public string UserName { get; set; }

        //Product
        public Guid ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
