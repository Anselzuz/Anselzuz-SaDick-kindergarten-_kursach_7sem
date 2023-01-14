using System;
using System.Collections.Generic;

namespace SaDick.Models;

public partial class Administrator
{
    public long AdminPhoneNum { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public virtual LoginAdmin? LoginAdmin { get; set; }
}
