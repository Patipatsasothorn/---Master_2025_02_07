using System;
using System.Collections.Generic;

namespace BPI_UserSettings.Models.Data;

public partial class BolUsersPolicy
{
    public long RowId { get; set; }

    public long? UserId { get; set; }

    public string? DataType { get; set; }

    public string? DataCode { get; set; }

    public DateTime? CredateDate { get; set; }

    public long? CreateBy { get; set; }
}
