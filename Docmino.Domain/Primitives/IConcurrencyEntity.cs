﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docmino.Domain.Primitives;
public interface IConcurrencyEntity
{
    public byte[] RowVersion { get; set; }
}
