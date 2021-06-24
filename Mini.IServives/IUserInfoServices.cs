using Mini.IServives.BASE;
using Mini.Model;
using Mini.Model.Entity;
using System;
using System.Threading.Tasks;

namespace Mini.IServives
{
    public interface IUserInfoServices : IBaseServices<UserInfo>
    {
          Task<MessageModel<UserInfo>> CheckLogin(UserInfo userInfo);
    }
}
