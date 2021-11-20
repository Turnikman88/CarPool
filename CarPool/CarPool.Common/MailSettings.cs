using CarPool.Common.Contracts;

namespace CarPool.Common
{
    public class MailSettings : IMailSettings
    {
        public string Mail => "carpool.finalproject@gmail.com";
        public string DisplayName => "CarPool";
        public string Password => "poolacar247poolacar";
        public string Host => "smtp.gmail.com";
        public int Port => 587;
    }
}
