﻿namespace Bookworms_Online.Model
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string Action { get; set; }
        public DateTime Timestamp { get; set; }
        public string IP { get; set; }
        public string AdditionalInfo { get; set; }

    }
}
