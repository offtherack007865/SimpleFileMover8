using System;
using System.Collections.Generic;

namespace SimpleFileMover8.Data.Models;

public partial class SimpleFileMover8Config
{
    public long Pk { get; set; }

    public bool? Enabled { get; set; }

    public string SystemName { get; set; } = null!;

    public string SourceDirectory { get; set; } = null!;

    public bool? SearchRootSourceDirectory { get; set; }

    public bool? SearchSubdirectoriesOfRootSourceDirectory { get; set; }

    public string RequiredFilePrefix { get; set; } = null!;

    public bool? DeleteSourceFile { get; set; }

    public string ConfigWebApiUrl { get; set; } = null!;

    public string CreatedBy { get; set; } = null!;

    public DateTime CreatedTimestamp { get; set; }

    public string UpdatedBy { get; set; } = null!;

    public DateTime UpdatedTimestamp { get; set; }
}
