using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardShop.Auth
{
    public interface IWrapperFactory
    {
        XEntity Wrap<TEntity, XEntity>(object arg)
            where TEntity : class
            where XEntity : class;
    }
}
