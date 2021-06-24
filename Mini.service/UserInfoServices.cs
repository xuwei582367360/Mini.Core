using Mini.Common.Helper;
using Mini.IServives;
using Mini.Model;
using Mini.Model.Entity;
using Mini.Model.Enum;
using Mini.Services.BASE;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mini.service
{
    public class UserInfoServices : BaseServices<UserInfo>, IUserInfoServices
    {
        public async Task<MessageModel<UserInfo>> CheckLogin(UserInfo userInfo)
        {
            var returnMessage = new MessageModel<UserInfo>();
            if (!(userInfo.UserName.IsNotEmptyOrNull() || userInfo.UserName.IsNotEmptyOrNull()))
            {
                returnMessage.msg = "用户名或密码不能为空！";
                returnMessage.status = 500;
            }

            UserInfo userInfos = await BaseDal.QueryEntity(x => x.UserName == userInfo.UserName);
            if (userInfos != null)
            {
                if (userInfos.UserPwd != EncryptUserPassword(userInfo.UserPwd))
                {
                    returnMessage.msg = "密码输入有误！";
                    returnMessage.status = 500;
                }
                else if (userInfos.IsDelete!= ((int)StatusEnum.Yes).ToString())
                {
                    returnMessage.msg = "账户已被禁用！";
                    returnMessage.status = 500;
                    returnMessage.response = userInfos;
                }
                else
                {
                    returnMessage.msg = "登录成功！";
                    returnMessage.status = 200;
                    returnMessage.response = userInfos;
                }
            }
            else
            {
                returnMessage.msg = "账号不存在！";
                returnMessage.status = 500;
            }
            return returnMessage;
        }

        /// <summary>
        /// 密码MD5处理
        /// </summary>
        /// <param name="password"></param>
        /// <param name="salt"></param>
        /// <returns></returns>
        private string EncryptUserPassword(string password)
        {
            string md5Password = SecurityHelper.MD5ToHex(password);
            return md5Password;
        }
    }
}
