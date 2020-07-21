using System;
using System.Diagnostics.CodeAnalysis;
using Assignment9;
using Microsoft.Extensions.DependencyInjection;
using Unity;

namespace Assignment9
{
    
    [ExcludeFromCodeCoverage]
    public class Program
    {
        #region Properties

        /// <summary>
        /// Get the property "Container" of type UnityContainer to be used for DI purposes.
        /// </summary>
        private static UnityContainer Container { get; }

        /// <summary>
        /// This Property get the Unity container 'Container' and resolves it using IUser
        /// </summary>
        private static IUser Service
        {
            get
            {
                return Container?.Resolve<IUser>();
            }
        }

        #endregion

        #region Constructors
        
        /// <summary>
        /// This Constructor utilitizes a UnityContainer for DI in order to access services. 
        /// </summary>
        static Program()
        {
            Container = new UnityContainer();

            Container.RegisterType<IUser, User>();
        }

        #endregion

        #region Methods

        /// <summary>
        /// This is my main method.
        /// </summary>
        static void Main(string[] args)
        {         
            var loginRecall = true;

            while (loginRecall)
            {
                MenuUtilities.LoginMenu(out loginRecall, Service);
            }
        }

        #endregion
    }
}

