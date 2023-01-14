using System;
using System.Collections.Generic;

namespace SaDick.Models;

public partial class Parent
{
    public long ParentPhoneNum { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public virtual ICollection<Child> Children { get; } = new List<Child>();

    public virtual LoginParent? LoginParent { get; set; }
}
