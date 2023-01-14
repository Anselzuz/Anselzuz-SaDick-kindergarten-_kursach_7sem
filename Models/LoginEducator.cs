using System;
using System.Collections.Generic;

namespace SaDick.Models;

public partial class LoginEducator
{
    public long EducatorPhoneNum { get; set; }

    public string? Password { get; set; }

    public virtual Educator EducatorPhoneNumNavigation { get; set; } = null!;
}
