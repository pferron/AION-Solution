using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace System
{
    public class LazyEx<T> : Lazy<T>
    {

        //Forces the creation of instance so that it will not be lost in the way.
        public LazyEx<T> ForceCreation()
        {
            T t = this.Value;
            return this;
        }

        /// <summary>
        /// Note the expression is evaluated lazily, so if you change the value of the 
        /// variable before the constructor is called it might not do what you expect.
        /// </summary>
        /// <param name="valueFactory"></param>
        /// <param name="objectArray"></param>
        /// <example>
        /// public void Assign(P p)
        /// {
        ///     int T = 10;
        ///     p.Test = new P();
        ///     p.DelaProd = new LazyEx<string>((x) => () => Execute(x[0], x[1]), p.Test,T);
        ///     p.Test.element = "summer";
        /// }
        /// public string Execute(P str, int val)
        /// {
        ///     return str.element + val.ToString();
        /// }
        /// </example>
    public LazyEx(Func<dynamic[], Func<T>> valueFactory, params dynamic[] objectArray) : base(valueFactory(objectArray))
        { }

        /// <summary>
        /// Note the expression is evaluated lazily, so if you change the value of the 
        /// variable before the constructor is called it might not do what you expect.
        /// </summary>
        /// <param name="valueFactory"></param>
        /// <param name="objectArray"></param>
        /// <example>
        /// public void Assign(P p)
        /// {
        ///     int T = 10;
        ///     p.Test = new P();
        ///     p.DelaProd = new LazyEx<string>((x) => () => Execute(x[0], x[1]), p.Test,T);
        ///     p.Test.element = "summer";
        /// }
        /// public string Execute(P str, int val)
        /// {
        ///     return str.element + val.ToString();
        /// }
        /// </example>
        public LazyEx(Func<dynamic[], Func<T>> valueFactory, bool isThreadSafe, params dynamic[] objectArray) : base(valueFactory(objectArray), isThreadSafe)
        { }

        /// <summary>
        /// Note the expression is evaluated lazily, so if you change the value of the 
        /// variable before the constructor is called it might not do what you expect.
        /// </summary>
        /// <param name="valueFactory"></param>
        /// <param name="objectArray"></param>
        /// <example>
        /// public void Assign(P p)
        /// {
        ///     int T = 10;
        ///     p.Test = new P();
        ///     p.DelaProd = new LazyEx<string>((x) => () => Execute(x[0], x[1]), p.Test,T);
        ///     p.Test.element = "summer";
        /// }
        /// public string Execute(P str, int val)
        /// {
        ///     return str.element + val.ToString();
        /// }
        /// </example>
        public LazyEx(Func<dynamic[], Func<T>> valueFactory, LazyThreadSafetyMode mode, params dynamic[] objectArray) : base(valueFactory(objectArray), mode)
        { }

        public LazyEx() : base()
        { }

        public LazyEx(Func<T> valueFactory) : base(valueFactory)
        { }

        public LazyEx(bool isThreadSafe) : base(isThreadSafe)
        { }

        public LazyEx(LazyThreadSafetyMode mode) : base(mode)
        { }

        public LazyEx(Func<T> valueFactory, bool isThreadSafe) : base(valueFactory, isThreadSafe)
        { }

        public LazyEx(Func<T> valueFactory, LazyThreadSafetyMode mode) : base(valueFactory, mode)
        { }
    }

}
