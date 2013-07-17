using System;
using System.Collections.Generic;
using Offshore3.BugTrack.Common;
using Offshore3.BugTrack.Entities;
using Offshore3.BugTrack.ILogic;
using Offshore3.BugTrack.IRepository;


namespace Offshore3.BugTrack.Logic
{
    public class UserLogic : IUserLogic
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserProjectRoleRelationRepository _userProjectRoleRelationRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IProjectRepository _projectRepository;


        public UserLogic(IUserRepository userRepository, IUserProjectRoleRelationRepository userProjectRoleRelationRepository, IRoleRepository roleRepository, IProjectRepository projectRepository)
        {
            _userRepository = userRepository;
            _userProjectRoleRelationRepository = userProjectRoleRelationRepository;
            _roleRepository = roleRepository;
            _projectRepository = projectRepository;
            
        }

        public bool Register(User user)
        {
            return _userRepository.Add(user);
        }

        public bool AuthenticateUser(User user)
        {
            return (_userRepository.GetByEmailAndPassword(user.Email, user.Password) != null ||
                    _userRepository.GetByUserNameAndPassword(user.UserName,user.Password) != null);
        }

        public User Get(long userId)
        {
            return _userRepository.Get(userId);
        }

        public User GetByUserName(string userName)
        {
            return _userRepository.GetByUserName(userName);
        }

        public User GetByEmail(string email)
        {
            return _userRepository.GetByEmail(email);
        }

        public User GetByEmailAndPassword(string email,string password)
        {
            return _userRepository.GetByEmailAndPassword(email,password);
        }

        public User GetByUserNameAndPassword(string username,string password)
        {
            return _userRepository.GetByUserNameAndPassword(username,password);
        }
        
        public void Update(User user)
        {
            _userRepository.Update(user);
        }
        
    }
}