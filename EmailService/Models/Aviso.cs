using System;
using System.Collections.Generic;

namespace EmailService.Models;

public partial class Aviso
{
    public DateTime Avdata { get; set; }

    public string Avcodigoproduto { get; set; } = null!;
}