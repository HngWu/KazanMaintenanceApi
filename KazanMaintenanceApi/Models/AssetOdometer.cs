using System;
using System.Collections.Generic;

namespace KazanMaintenanceApi.Models;

public partial class AssetOdometer
{
    public long Id { get; set; }

    public long AssetId { get; set; }

    public DateOnly ReadDate { get; set; }

    public long OdometerAmount { get; set; }

    public virtual Asset Asset { get; set; } = null!;
}
