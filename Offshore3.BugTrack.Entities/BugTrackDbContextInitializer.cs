using System;
using System.Data.Entity;

namespace Offshore3.BugTrack.Entities
{
    public class BugTrackDbContextInitializer : DropCreateDatabaseIfModelChanges<BugTrackDbContext>
    {
        protected override void Seed(BugTrackDbContext context)
        {
            var admin = new Role
                {
                    RoleName = "Admin"
                };
            var creator = new Role
                {
                    RoleName = "Creator"
                };
            var participant = new Role
                {
                    RoleName = "Participant"
                };

            var jack = new User
                {
                    UserName = "Jack",
                    Password = "123456",
                    Email = "zhangsan@qq.com"
                };
            var tom = new User
                {
                    UserName = "Tom",
                    Password = "123456",
                    Email = "lisi@qq.com"
                };
            var brain = new User
                {
                    UserName = "Brain",
                    Password = "123456",
                    Email = "brain@qq.com"
                };

            var bookStore = new Project
                {
                    ProjectName = "BookStore",
                    CreateDate = DateTime.Now,
                };
            var musicStore = new Project
                {
                    ProjectName = "MusicStore",
                    CreateDate = DateTime.Now,
                };

            var resolved = new BugStatus() { BugStatusName = "Resolved", Project = bookStore,Number = 3};
            var open = new BugStatus() { BugStatusName = "Open",Project =bookStore,Number = 1};
            var closed = new BugStatus() {BugStatusName = "Closed",Project = bookStore,Number = 2};
            var developing = new BugStatus() { BugStatusName = "Developing",Project = bookStore,Number = 4};
            var reopen = new BugStatus() { BugStatusName = "Reopen",Project = bookStore,Number = 5};
            

            var ie6Bug = new Bug()
                {
                    BugName = "Not work in IE6",
                    BugStatus = resolved,
                    CreateDate = DateTime.Now,
                    UpdateTime = DateTime.Now,
                    User = tom
                };
            var ie7Bug = new Bug()
            {
                BugName = "Not work in IE7",
                BugStatus = open,
                CreateDate = DateTime.Now,
                UpdateTime = DateTime.Now,
                User = tom
            };
            var ie8Bug = new Bug()
            {
                BugName = "Not work in IE8",
                BugStatus = closed,
                CreateDate = DateTime.Now,
                UpdateTime = DateTime.Now,
                User = tom
            };
            var ie9Bug = new Bug()
            {
                BugName = "Not work in IE9",
                BugStatus = reopen,
                CreateDate = DateTime.Now,
                UpdateTime = DateTime.Now,
                User = tom
            };
            var ie10Bug = new Bug()
            {
                BugName = "Not work in IE10",
                BugStatus = developing,
                CreateDate = DateTime.Now,
                UpdateTime = DateTime.Now,
                User = tom
            };


            var comment1 = new BugComment()
            {
                Content = "Comment1",
                Bug = ie6Bug,
                Commentator = jack.UserName,
                AddTime = DateTime.Now
            };
            var comment2 = new BugComment()
            {
                Content = "Comment2",
                Bug = ie7Bug,
                Commentator = brain.UserName,
                AddTime = DateTime.Now
            };

            context.Bugs.Add(ie6Bug);
            context.Bugs.Add(ie7Bug);
            context.Bugs.Add(ie8Bug);
            context.Bugs.Add(ie9Bug);
            context.Bugs.Add(ie10Bug);

            context.BugComments.Add(comment1);
            context.BugComments.Add(comment2);

            context.RoleRelations.Add(new UserProjectRoleRelation
                {
                    Project = bookStore,
                    User = brain,
                    Role = creator
                });

            context.RoleRelations.Add(new UserProjectRoleRelation
                {
                    Project = bookStore,
                    User = jack,
                    Role = admin
                });
            context.RoleRelations.Add(new UserProjectRoleRelation
                {
                    Project = bookStore,
                    User = tom,
                    Role = participant
                });
            context.RoleRelations.Add(new UserProjectRoleRelation
                {
                    Project = musicStore,
                    User = tom,
                    Role = creator
                });
            context.RoleRelations.Add(new UserProjectRoleRelation
                {
                    Project = musicStore,
                    User = jack,
                    Role = admin
                });
            context.RoleRelations.Add(new UserProjectRoleRelation
                {
                    Project = musicStore,
                    User = brain,
                    Role = participant
                });

            base.Seed(context);
        }
    }
}