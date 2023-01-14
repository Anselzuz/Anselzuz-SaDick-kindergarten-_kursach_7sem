using System;
using System.Collections.Generic;

namespace SaDick.Models;

public partial class Visiting
{
    public int? Id { get; set; }

    public DateTime? Data { get; set; }

    public string? BirthSertificateSerNum { get; set; }

    public virtual Child? BirthSertificateSerNumNavigation { get; set; }
}
