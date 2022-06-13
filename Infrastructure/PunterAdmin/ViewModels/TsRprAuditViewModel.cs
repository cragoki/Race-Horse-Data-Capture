using Core.Enums;
using System;

namespace Infrastructure.PunterAdmin.ViewModels
{
    public class TsRprAuditViewModel
    {
        public RprTsAuditType AuditType { get; set; }
        public int Previous { get; set; }
        public int Next { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
