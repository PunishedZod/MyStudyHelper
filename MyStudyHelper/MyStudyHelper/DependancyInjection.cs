using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.API.Models.Interfaces;
using MyStudyHelper.Classes.API.Proxys;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using MyStudyHelper.Classes.Backend;
using MyStudyHelper.Classes.Backend.Interfaces;
using Autofac;

namespace MyStudyHelper
{
    public static class DependancyInjection
    {
        public static Autofac.IContainer Configure()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<User>().As<IUser>();
            builder.RegisterType<Posts>().As<IPosts>();
            builder.RegisterType<Comments>().As<IComments>();
            builder.RegisterType<HomeBackend>().As<IHomeBackend>();
            builder.RegisterType<PostsBackend>().As<IPostsBackend>();
            builder.RegisterType<LoginBackend>().As<ILoginBackend>();
            builder.RegisterType<AccountBackend>().As<AccountBackend>();
            builder.RegisterType<ViewPostBackend>().As<ViewPostBackend>();
            builder.RegisterType<RegisterBackend>().As<IRegisterBackend>();
            builder.RegisterType<CreatePostBackend>().As<ICreatePostBackend>();
            builder.RegisterType<RecentPostsBackend>().As<IRecentPostsBackend>();
            
            string baseAddress = "https://localhost:44323/"; //Injects the base address into the proxies

            //START OF API PROXIES

            builder.Register<PostsProxy>((c, p) =>
            {
                return new PostsProxy(baseAddress);
            }).As<IPostsProxy>();

            builder.Register<UserProxy>((c, p) =>
            {
                return new UserProxy(baseAddress);
            }).As<IUserProxy>();

            //END OF API PROXIES

            return builder.Build();
        }
    }
}