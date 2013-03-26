using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace CardShop.Auth
{
    public class WrapperFactory : IWrapperFactory
    {
        public static IWrapperFactory Factory { get; set; }
        
        static WrapperFactory(){
            Factory = new WrapperFactory();
        }

        private WrapperFactory(){}

        public XEntity Wrap<TEntity, XEntity>(object arg) where TEntity : class 
                                                            where XEntity : class
        {
            Type[] types = new Type[]{arg.GetType()};
            ConstructorInfo constructor = typeof(TEntity).GetConstructor(types);
            return (XEntity)constructor.Invoke(new object[]{arg});
        }
    }
}