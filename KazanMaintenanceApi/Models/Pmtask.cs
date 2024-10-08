using System;
using System.Collections.Generic;

namespace KazanMaintenanceApi.Models;

public partial class Pmtask
{
    public long Id { get; set; }

    public long AssetId { get; set; }

    public long TaskId { get; set; }

    public long PmscheduleTypeId { get; set; }

    public DateOnly? ScheduleDate { get; set; }

    public long? ScheduleKilometer { get; set; }

    public bool? TaskDone { get; set; }

    public virtual Asset Asset { get; set; } = null!;

    public virtual PmscheduleType PmscheduleType { get; set; } = null!;

    public virtual Task Task { get; set; } = null!;
}
