using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BPI_BillOfLading.Models.Data;

public partial class BolUsersPolicy
{
    [Key]
    public long RowId { get; set; }

    public long? UserId { get; set; }

    public string? DataType { get; set; }

    public string? DataCode { get; set; }

    public DateTime? CredateDate { get; set; }

    public long? CreateBy { get; set; }
}
