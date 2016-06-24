using Anathema.Source.Engine.DotNetObjectCollector;
using Anathema.Source.Utils.Extensions;
using Anathema.Source.Utils.Validation;
using System;

namespace Anathema.Source.Utils.AddressResolver
{
    class AddressResolver
    {
        private static Lazy<AddressResolver> AddressResolverInstance = new Lazy<AddressResolver>(() => { return new AddressResolver(); });

        private AddressResolver()
        {

        }

        public static AddressResolver GetInstance()
        {
            return AddressResolverInstance.Value;
        }

        /// <summary>
        /// Resolves complex address expressions, such as
        /// "Game.Player.Health" + 24
        /// </summary>
        /// <param name="Address"></param>
        /// <returns></returns>
        public IntPtr ResolveAddress(String Address)
        {
            // TODO: Tokenize, arethmetic

            IntPtr Result = DotNetNameResolver.GetInstance().ResolveName(Address);

            if (Result == IntPtr.Zero)
            {
                if (CheckSyntax.CanParseAddress(Address))
                    return Conversions.AddressToValue(Address).ToIntPtr();
            }

            return Result;
        }

    } // End class

} // End namespace