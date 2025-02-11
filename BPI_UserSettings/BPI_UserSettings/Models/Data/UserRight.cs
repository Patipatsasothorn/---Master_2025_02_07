using System;
using System.Collections.Generic;

namespace BPI_UserSettings.Models.Data;

public partial class UserRight
{
    public long UserId { get; set; }

    public string? UserName { get; set; }

    public byte[]? UserPassword { get; set; }

    public string? Name { get; set; }

    public Guid? Salt { get; set; }

    public bool? ChangePassword { get; set; }

    public bool? Admin { get; set; }

    public string? Remark { get; set; }

    public bool? Epicor { get; set; }
}
