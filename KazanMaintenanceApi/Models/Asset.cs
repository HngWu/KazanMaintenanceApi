using System;
using System.Collections.Generic;

namespace KazanMaintenanceApi.Models;

public partial class Asset
{
    public long Id { get; set; }

    public string AssetSn { get; set; } = null!;

    public string AssetName { get; set; } = null!;

    public long DepartmentLocationId { get; set; }

    public long EmployeeId { get; set; }

    public long AssetGroupId { get; set; }

    public string Description { get; set; } = null!;

    public DateOnly? WarrantyDate { get; set; }

    public virtual ICollection<AssetOdometer> AssetOdometers { get; set; } = new List<AssetOdometer>();

    public virtual ICollection<Pmtask> Pmtasks { get; set; } = new List<Pmtask>();
}
