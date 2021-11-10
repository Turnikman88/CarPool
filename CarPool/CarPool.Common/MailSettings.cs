﻿using CarPool.Common.Contracts;

namespace CarPool.Common
{
    public class MailSettings : IMailSettings
    {
        public string Mail => "project.deliverit@gmail.com";
        public string DisplayName => "DeliverIT";
        public string Password => "deliver247deliver";
        public string Host => "smtp.gmail.com";
        public int Port => 587;
    }
}
