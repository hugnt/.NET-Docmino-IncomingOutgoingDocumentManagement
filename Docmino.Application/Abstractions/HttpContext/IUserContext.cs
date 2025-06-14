using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Docmino.Application.Abstractions.HttpContext;
public interface IUserContext
{
    bool IsAuthenticated { get; }
    string AccessToken { get; }
    Guid UserId { get; }
}
