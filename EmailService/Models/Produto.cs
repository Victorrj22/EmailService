using System;
using System.Collections.Generic;

namespace EmailService.Models;

public partial class Produto
{
    public string Procodigo { get; set; } = null!;

    public string Pronome { get; set; } = null!;

    public bool Proavisaressup { get; set; }

    public int Proqtdestoque { get; set; }

    public int Proqtdavisa { get; set; }
}