using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookShop.Application.ResponseEntity;

public sealed class ResponseEntity<T>
    where T : class
{
    public T Body { get; init; }

    public ResponseAppCode AppCode { get; init; }
}
