using System;
using System.Collections.Generic;

namespace SaDick.Models;

public partial class LoginParent
{
    public long ParentPhoneNum { get; set; }

    public string? Password { get; set; }

    public virtual Parent ParentPhoneNumNavigation { get; set; } = null!;
}
