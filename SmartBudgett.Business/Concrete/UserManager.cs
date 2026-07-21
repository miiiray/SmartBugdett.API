using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SmartBudgett.Business.Abstract;
using SmartBudgett.DataAccess.Abstract;
using SmartBudgett.Entities;

namespace SmartBudgett.Business.Concrete
{
    public class UserManager : IUserService
    {
        private readonly IUserRepository _userRepository; // ben userRepository'yi kullanacağım
        public UserManager(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Add(User user)
        {
            _userRepository.Add(user);

        }

        public List<User> GetAll()
        {
            return _userRepository.GetAll();
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }

        public void Update(User user)
        {
            if(string.IsNullOrWhiteSpace(user.FirstName) || string.IsNullOrWhiteSpace(user.LastName))
            {
                throw new Exception("User first and last name cannot be empty.");
            }
            _userRepository.Update(user);
        }
        public void Delete(User user)
        {
            _userRepository.Delete(user);
        }

    }
}
