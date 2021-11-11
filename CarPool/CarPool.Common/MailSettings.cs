using CarPool.Common.Contracts;

namespace CarPool.Common
{
    public class MailSettings : IMailSettings
    {
        public string Mail => "project.CarPool@gmail.com";
        public string DisplayName => "CarPool";
        public string Password => "deliver247deliver";
        public string Host => "smtp.gmail.com";
        public int Port => 587;
    }
}
