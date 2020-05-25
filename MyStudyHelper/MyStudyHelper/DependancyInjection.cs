using Autofac;
using MyStudyHelper.Classes.API.Proxys;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using MyStudyHelper.Classes.Backend;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace MyStudyHelper
{
    public static class DependancyInjection
    {
        public static Autofac.IContainer Configure()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<LoginBackend>().As<ILoginBackend>();
            builder.RegisterType<RegisterBackend>().As<RegisterBackend>();

            //START OF API PROXIES

            string baseAddress = "https://localhost:44323/"; //Injects the base address into the proxys

            builder.Register<PostsProxy>((c, p) =>
            {
                return new PostsProxy(baseAddress);
            }).As<IPostsProxy>();

            builder.Register<UserProxy>((c, p) =>
            {
                return new UserProxy(baseAddress);
            }).As<UserProxy>();

            //END OF API PROXIES

            return builder.Build();
        }
    }
}