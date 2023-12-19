using System;
using System.Collections.Generic;

namespace SalesManagement2.Model;

public partial class MPosition
{
    public int MPositionId { get; set; }

    public string PositionName { get; set; } = null!;

    public bool DspFlg { get; set; }

    public string Comments { get; set; } = null!;

    public virtual ICollection<MStaff> MStaffs { get; set; } = new List<MStaff>();
}
