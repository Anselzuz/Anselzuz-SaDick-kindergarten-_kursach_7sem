using System;
using System.Collections.Generic;

namespace SaDick.Models;

public partial class ChildGroup
{
    public int GroupNum { get; set; }

    public byte? NumOfChild { get; set; }

    public virtual ICollection<Child> Children { get; } = new List<Child>();

    public virtual ICollection<Educator> Educators { get; } = new List<Educator>();
}
