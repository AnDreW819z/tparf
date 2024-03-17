namespace tparf.api.EmailSender
{
    public interface IEmailService
    {
        public Task SendEmail(Message message);
    }
}
