using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docmino.Application.Common.Enums;
public enum TokenErrorCode
{
    None,
    TokenExpired,
    TokenSignatureKeyNotFound,
    TokenInvalidSignature,
}
