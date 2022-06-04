using System;
using System.Collections.Generic;
using System.Text;

namespace FPPractice
{
    public class ImmutabilityClass
    {
        public class UserProfile
        {
            /// <summary>
            /// 將user和address改成readonly
            /// </summary>
            private readonly User _user;
            private readonly string _address;

            /// <summary>
            /// 只有建構子能夠做到存入資料
            /// </summary>
            public UserProfile(User user, string address)
            {
                _user = user;
                _address = address;
            }

            /// <summary>
            /// 這邊是直接new出一個新的物件，所以不會直接變更屬性，所以沒有了副作用
            /// </summary>
            /// <param name="userId"></param>
            /// <param name="userName"></param>
            public UserProfile UpdateUser(int userId, string userName)
            {
                var newUser = new User(userId, userName);

                return new UserProfile(newUser, _address);
            }
        }

        /// <summary>
        /// 這個User沒有變動性，所以無狀態的(Stateless)
        /// </summary>
        public class User
        {
            public User(int id, string name)
            {
                Id = id;
                Name = name;
            }

            public int Id { get; }
            public string Name { get; }
        }
    }
}
