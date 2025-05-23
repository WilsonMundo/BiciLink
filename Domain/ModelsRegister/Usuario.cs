﻿using System;
using System.Collections.Generic;

namespace Domain.ModelsRegister;

public partial class Usuario
{
    public long IdUser { get; set; }

    public string Name { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Direccion { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Nit { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public bool FlagActivo { get; set; }

    public short RolId { get; set; }

    public virtual Rol Rol { get; set; } = null!;
}
