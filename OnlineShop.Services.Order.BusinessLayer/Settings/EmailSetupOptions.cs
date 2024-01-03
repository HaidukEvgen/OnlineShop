namespace OnlineShop.Services.Order.BusinessLayer.Settings
{
    public class EmailSetupOptions
    {
        public string MailBoxName { get; set; }
        public string MailBoxAddress { get; set; }
        public string EmailHost { get; set; }
        public int EmailHostPort { get; set; }
        public string MailBoxPassword { get; set; }
    }
}
