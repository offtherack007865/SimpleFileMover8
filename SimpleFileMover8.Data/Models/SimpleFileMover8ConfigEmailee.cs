using System;
using System.Collections.Generic;

namespace SimpleFileMover8.Data.Models;

public partial class SimpleFileMover8ConfigEmailee
{
    public long Pk { get; set; }

    public long SimpleFileMoverConfigPk { get; set; }

    public string EmailAddress { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedTimestamp { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedTimestamp { get; set; }
}
