using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CardShop.Utilities
{
    public class Factory : IFactory
    {
        public static IFactory Instance { get; set; }
        
        static Factory(){
            Instance = new Factory();
        }

        private Factory(){}

        public XEntity Create<TEntity, XEntity>(params object[] args)
                    where TEntity : class
                    where XEntity : class
        {
            Type[] types = new Type[args.Length];
            for (int i = 0; i < args.Length; i++)
            {
                types[i] = args[i].GetType();
            }
            ConstructorInfo constructor = typeof(TEntity).GetConstructor(types);
            return (XEntity)constructor.Invoke(args);
        }

        public TEntity Create<TEntity>(params object[] args) where TEntity : class
        {
            return Create<TEntity, TEntity>(args);
        }
    }
}