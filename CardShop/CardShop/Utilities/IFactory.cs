using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CardShop.Utilities
{
    public interface IFactory
    {
        /// <summary>
        /// Will create a new instance of the specified object type, and then automatically cast it to the
        /// second specified type.
        /// e.g.
        /// Factory.Instance.Create<Foo, Bar>();
        /// Will create a new Foo() and cast it to Bar.
        /// If the constructor requires arguments, the call would be made like:
        /// Factory.Instance.Create<Foo, Bar>(arg1, arg2);
        /// and is essentially the same as saying '(Bar)new Foo(arg1, arg2);'
        /// </summary>
        /// <typeparam name="TEntity">The concrete class to create</typeparam>
        /// <typeparam name="XEntity">The class or interface to cast the returned object to.</typeparam>
        /// <param name="args">Arguments to be passed to the constructor</param>
        /// <returns>new XEntity</returns>
        XEntity Create<TEntity, XEntity>(params object[] args)
            where TEntity : class
            where XEntity : class;
         /// <summary>
        /// Creates a new instance of the specified object type.
        /// e.g.
        /// Factory.Instance.Create<Foo>();
        /// Will create a new Foo().
        /// If the constructor requires arguments, the call would be made like:
        /// Factory.Instance.Create<Foo>(arg1, arg2);
        /// and is essentially the same as saying 'new Foo(arg1, arg2);'
        /// </summary>
        /// <typeparam name="TEntity">The concrete class to create</typeparam>
        /// <param name="args"></param>
        /// <returns>new TEntity</returns>
        TEntity Create<TEntity>(params object[] args)
            where TEntity : class;
    }
}
