using System;
using System.Collections.Generic;

namespace SalesManagement2.Model;

public partial class MStore
{
    public string StoreCd { get; set; } = null!;

    public string StoreName { get; set; } = null!;

    public string StoreNameKana { get; set; } = null!;

    public string? StorePostal { get; set; }

    public string? StoreAddress { get; set; }

    public string? StoreAddressKana { get; set; }

    public string? StoreTel { get; set; }

    public string? StoreFax { get; set; }

    public string? StoreMail { get; set; }

    public bool StoreDeleteFlg { get; set; }

    public string? StoreComments { get; set; }

    public virtual ICollection<MStaff> MStaffs { get; set; } = new List<MStaff>();
}
