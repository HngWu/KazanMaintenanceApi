using System;
using System.Collections.Generic;

namespace KazanMaintenanceApi.Models;

public partial class Task
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Pmtask> Pmtasks { get; set; } = new List<Pmtask>();
}
