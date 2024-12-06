using System;
using System.Collections.Generic;

namespace Regulatorio.Core.infrastructure
{
    public abstract class BaseSingleton
    {
        static BaseSingleton()
        {
            AllSingletons = new Dictionary<Type, object>();
        }
        protected static IDictionary<Type, object> AllSingletons { get; }
    }
}