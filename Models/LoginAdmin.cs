using System;
using System.Collections.Generic;

namespace SaDick.Models;

public partial class LoginAdmin
{
    public long AdminPhoneNum { get; set; }

    public string? Password { get; set; }

    public virtual Administrator AdminPhoneNumNavigation { get; set; } = null!;
}
