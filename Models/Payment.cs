using System;
using System.Collections.Generic;

namespace SaDick.Models;

public partial class Payment
{
    public string BirthSertificateSerNum { get; set; } = null!;

    public byte? MonthNotPayed { get; set; }

    public virtual Child BirthSertificateSerNumNavigation { get; set; } = null!;
}
