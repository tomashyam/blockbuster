using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using CC.Data;
using CC.Models;


namespace CC.DAL
{
    public class UserDataAccess
    {
        private readonly CCContext _ccContext;

        public UserDataAccess(CCContext context)
        {
            _ccContext = context;
        }

        public void AddUser(string id, string name, Gender gender)
        {
            _ccContext.User.Add(new User
            {
                ID = id,
                Name = name,
                Gender = gender
            });

            _ccContext.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            _ccContext.User.Update(user);
            _ccContext.SaveChanges();
        }

        public void DeletUser(User user)
        {
            var userToDelete = _ccContext
                .User
                .SingleOrDefault(u => u.ID == user.ID);

            if (userToDelete == null)
            {
                throw new
                    Exception(string.Format("could not find user with id {0}", user.ID));
            }

            _ccContext.User.Remove(userToDelete);
            _ccContext.SaveChanges();
        }

        public bool IsUserExist(string id)
        {
            return _ccContext.User.Any(u => u.ID == id);
        }

        public User GetUser(string id)
        {
            return _ccContext.User.FirstOrDefault(u => u.ID == id);
        }

        public List<User> GetAllUsers()
        {
            return _ccContext.User.ToList();
        }
    }
}
