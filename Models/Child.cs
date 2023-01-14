using System;
using System.Collections.Generic;

namespace SaDick.Models;

public partial class Child
{
    public string BirthSertificateSerNum { get; set; } = null!;

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public int? GroupNum { get; set; }

    public long? ParentPhoneNum { get; set; }

    public virtual ChildGroup? GroupNumNavigation { get; set; }

    public virtual Parent? ParentPhoneNumNavigation { get; set; }

    public virtual Payment? Payment { get; set; }

    public virtual ICollection<Visiting> Visitings { get; } = new List<Visiting>();
}
