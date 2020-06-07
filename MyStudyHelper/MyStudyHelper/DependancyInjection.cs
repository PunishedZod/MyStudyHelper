using Autofac;
using MyStudyHelper.Classes.Backend;
using MyStudyHelper.Classes.API.Proxys;
using MyStudyHelper.Classes.API.Models;
using MyStudyHelper.Classes.Backend.Interfaces;
using MyStudyHelper.Classes.API.Proxys.Interfaces;
using MyStudyHelper.Classes.API.Models.Interfaces;

namespace MyStudyHelper
{
    //Dependency injection class, using this creates a "disconnected" type architecture which allows classes to be more easily swapped out or replaced if or when necessary
    public static class DependancyInjection
    {
        public static Autofac.IContainer Configure()
        {
            var builder = new ContainerBuilder(); //When adding more variables/classes, use this to inject them, examples below

            //Injection helpers, (includes model classes)
            builder.RegisterType<User>().As<IUser>();
            builder.RegisterType<Posts>().As<IPosts>();
            builder.RegisterType<Comments>().As<IComments>();
            builder.RegisterType<HomeBackend>().As<IHomeBackend>();
            builder.RegisterType<PostsBackend>().As<IPostsBackend>();
            builder.RegisterType<LoginBackend>().As<ILoginBackend>();
            builder.RegisterType<AccountBackend>().As<IAccountBackend>();
            builder.RegisterType<ViewPostBackend>().As<IViewPostBackend>();
            builder.RegisterType<RegisterBackend>().As<IRegisterBackend>();
            builder.RegisterType<CreatePostBackend>().As<ICreatePostBackend>();
            builder.RegisterType<PostHistoryBackend>().As<IPostHistoryBackend>();
            builder.RegisterType<RecentPostsBackend>().As<IRecentPostsBackend>();
            builder.RegisterType<CommentHistoryBackend>().As<ICommentHistoryBackend>();

            string baseAddress = "https://studyhelper.api.labnet.nz/"; //Injects the base address into the proxies

            //START OF API PROXIES

            builder.Register<PostsProxy>((c, p) =>
            {
                return new PostsProxy(baseAddress);
            }).As<IPostsProxy>();

            builder.Register<CommentsProxy>((c, p) =>
            {
                return new CommentsProxy(baseAddress);
            }).As<ICommentsProxy>();

            builder.Register<UserProxy>((c, p) =>
            {
                return new UserProxy(baseAddress);
            }).As<IUserProxy>();

            //END OF API PROXIES

            return builder.Build();
        }
    }
}