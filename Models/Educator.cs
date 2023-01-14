using System;
using System.Collections.Generic;

namespace SaDick.Models;

public partial class Educator
{
    public long EducatorPhoneNum { get; set; }

    public string? Name { get; set; }

    public string? Surname { get; set; }

    public int? GroupNum { get; set; }

    public virtual ChildGroup? GroupNumNavigation { get; set; }

    public virtual LoginEducator? LoginEducator { get; set; }
}
