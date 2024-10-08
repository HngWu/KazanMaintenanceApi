using System;
using System.Collections.Generic;

namespace KazanMaintenanceApi.Models;

public partial class PmscheduleModel
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public long? PmscheduleTypeId { get; set; }

    public virtual PmscheduleType? PmscheduleType { get; set; }
}
