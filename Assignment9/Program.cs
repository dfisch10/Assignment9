using System;
using System.Diagnostics.CodeAnalysis;
using Assignment9;
using Microsoft.Extensions.DependencyInjection;
using Unity;

namespace Assignment9
{
    /// <summary>
    /// This is my main method.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public class Program
    {
        static void Main(string[] args)
        {
            var container = new UnityContainer();
            container.RegisterType<IUser, User>();
            var service = container.Resolve<User>();
          
            var loginRecall = true;

            while (loginRecall)
            {
                MenuUtilities.LoginMenu(out loginRecall, service);
            }
        }    
    }
}

