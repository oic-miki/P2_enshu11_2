using System;
using System.Collections.Generic;

namespace SalesManagement2.Model;

public partial class MStaff
{
    public string StaffCd { get; set; } = null!;

    public string StaffName { get; set; } = null!;

    public string StaffNameKana { get; set; } = null!;

    public string? StaffPostal { get; set; }

    public string? StaffAddress { get; set; }

    public string? StaffAddressKana { get; set; }

    public string? StaffTel { get; set; }

    public string? StaffMobileTel { get; set; }

    public string? StaffMail { get; set; }

    public DateTime StaffBirthday { get; set; }

    public DateTime StaffJoinDate { get; set; }

    public string StoreCd { get; set; } = null!;

    public int PositionId { get; set; }

    public int DivisionId { get; set; }

    public int StaffAuthority { get; set; }

    public string StaffUserId { get; set; } = null!;

    public string StaffPassword { get; set; } = null!;

    public bool StaffDeleteFlg { get; set; }

    public string? StaffComments { get; set; }

    public virtual MDivision Division { get; set; } = null!;

    public virtual MPosition Position { get; set; } = null!;

    public virtual MStore StoreCdNavigation { get; set; } = null!;
}
