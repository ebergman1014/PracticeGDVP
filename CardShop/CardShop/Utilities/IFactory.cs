using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardShop.Utilities
{
    public interface IFactory
    {
        XEntity Create<TEntity, XEntity>(params object[] args)
            where TEntity : class
            where XEntity : class;
        TEntity Create<TEntity>(params object[] args)
            where TEntity : class;
    }
}
