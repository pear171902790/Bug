using System.Web.Mvc;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.IRepository;
using Offshore3.BugTrack.Logic;
using Offshore3.BugTrack.Repository;
using Offshore3.BugTrack.Web.Common;

namespace Offshore3.BugTrack.Web.DI.Installers
{
    public class ControllersInstaller : IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            container.Register(Classes.FromThisAssembly()
                                      .BasedOn<IController>()
                                      .LifestyleTransient());
            container.Register(Component.For<ICookieHelper>()
                .ImplementedBy<CookieHelper>());

            container.Register(Component.For<IUserLogic>()
                .ImplementedBy<UserLogic>());
            container.Register(Component.For<IUserRepository>()
                .ImplementedBy<UserRepository>());

            container.Register(Component.For<IProjectLogic>()
                .ImplementedBy<ProjectLogic>());
            container.Register(Component.For<IProjectRepository>()
                .ImplementedBy<ProjectRepository>());

            container.Register(Component.For<IRoleLogic>()
                .ImplementedBy<RoleLogic>());
            container.Register(Component.For<IRoleRepository>()
                .ImplementedBy<RoleRepository>());

            container.Register(Component.For<IUserProjectRoleRelationLogic>()
                .ImplementedBy<UserProjectRoleRelationLogic>());
            container.Register(Component.For<IUserProjectRoleRelationRepository>()
                .ImplementedBy<UserProjectRoleRelationRepository>());

            container.Register(Component.For<IBugLogic>()
                .ImplementedBy<BugLogic>());
            container.Register(Component.For<IBugRepository>()
                .ImplementedBy<BugRepository>());

            container.Register(Component.For<IBugStatusLogic>()
                .ImplementedBy<BugStatusLogic>());
            container.Register(Component.For<IBugStatusRepository>()
                .ImplementedBy<BugStatusRepository>());

            container.Register(Component.For<IBugCommentLogic>()
                .ImplementedBy<BugCommentLogic>());
            container.Register(Component.For<IBugCommentRepository>()
                .ImplementedBy<BugCommentRepository>());
        }
    }

    #region old
    //public class UserRepositoryInstaller : IWindsorInstaller
    //{
    //    public void Install(IWindsorContainer container, IConfigurationStore store)
    //    {
    //        container.Register(Component.For<IUserRepository>()
    //            .ImplementedBy<UserRepository>());
    //    }
    //}

    ////public class RoleRelationLogicInstaller : IWindsorInstaller
    ////{
    ////    public void Install(IWindsorContainer container, IConfigurationStore store)
    ////    {
    ////        container.Register(Component.For<IRoleRelationLogic>()
    ////            .ImplementedBy<RoleRelationLogic>());
    ////    }
    ////}

    //public class RoleRelationRepositoryInstaller : IWindsorInstaller
    //{
    //    public void Install(IWindsorContainer container, IConfigurationStore store)
    //    {
    //        container.Register(Component.For<IUserProjectRoleRelationRepository>()
    //            .ImplementedBy<UserProjectRoleRelationRepository>());
    //    }
    //}

    ////public class RoleLogicInstaller : IWindsorInstaller
    ////{
    ////    public void Install(IWindsorContainer container, IConfigurationStore store)
    ////    {
    ////        container.Register(Component.For<IRoleLogic>()
    ////            .ImplementedBy<RoleLogic>());
    ////    }
    ////}

    //public class RoleRepositoryInstaller : IWindsorInstaller
    //{
    //    public void Install(IWindsorContainer container, IConfigurationStore store)
    //    {
    //        container.Register(Component.For<IRoleRepository>()
    //            .ImplementedBy<RoleRepository>());
    //    }
    //}

    //public class ProjectLogicInstaller : IWindsorInstaller
    //{
    //    public void Install(IWindsorContainer container, IConfigurationStore store)
    //    {
    //        container.Register(Component.For<IProjectLogic>()
    //            .ImplementedBy<ProjectLogic>());
    //    }
    //}

    //public class ProjectRepositoryInstaller : IWindsorInstaller
    //{
    //    public void Install(IWindsorContainer container, IConfigurationStore store)
    //    {
    //        container.Register(Component.For<IProjectRepository>()
    //            .ImplementedBy<ProjectRepository>());
    //    }
    //}

    //public class BugLogicInstaller : IWindsorInstaller
    //{
    //    public void Install(IWindsorContainer container, IConfigurationStore store)
    //    {
    //        container.Register(Component.For<IBugLogic>()
    //            .ImplementedBy<BugLogic>());
    //    }
    //}


    

    //public class BugRepositoryInstaller : IWindsorInstaller
    //{
    //    public void Install(IWindsorContainer container, IConfigurationStore store)
    //    {
    //        container.Register(Component.For<IBugRepository>()
    //            .ImplementedBy<BugRepository>());
    //    }
    //}

    //public class CookieHelperInstaller : IWindsorInstaller
    //{
    //    public void Install(IWindsorContainer container, IConfigurationStore store)
    //    {
    //        container.Register(Component.For<ICookieHelper>()
    //            .ImplementedBy<CookieHelper>());
    //    }
    //}

    //public class TransformModelInstaller : IWindsorInstaller
    //{
    //    public void Install(IWindsorContainer container, IConfigurationStore store)
    //    {
    //        container.Register(Component.For<ITransformModel>()
    //            .ImplementedBy<TransformModel>());
    //    }
    //}

    //public class BugStatusRepositoryInstaller : IWindsorInstaller
    //{
    //    public void Install(IWindsorContainer container, IConfigurationStore store)
    //    {
    //        container.Register(Component.For<IBugStatusRepository>()
    //            .ImplementedBy<BugStatusRepository>());
    //    }
    //}

    //public class BugStatusLogicInstaller : IWindsorInstaller
    //{
    //    public void Install(IWindsorContainer container, IConfigurationStore store)
    //    {
    //        container.Register(Component.For<IBugStatusLogic>()
    //            .ImplementedBy<BugStatusLogic>());
    //    }
    //}

    //public class CommentRepositoryInstaller : IWindsorInstaller
    //{
    //    public void Install(IWindsorContainer container, IConfigurationStore store)
    //    {
    //        container.Register(Component.For<IBugCommentRepository>()
    //            .ImplementedBy<BugCommentRepository>());
    //    }
    //}
    #endregion

}
