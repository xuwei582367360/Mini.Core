//-----------------------------------------------------------------------
// <copyright file= "IJwtAppService.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/21 21:37:15 
// Modified by:
// Description: Jwt Token 操作功能接口
//-----------------------------------------------------------------------
using Mini.Extensions.ServiceExtensions.Jwt.Dto;
using Mini.Model.Entity;
using System.Threading.Tasks;

namespace Mini.Extensions.ServiceExtensions.Jwt
{
    public interface IJwtAppService
    {
        #region APIs

        /// <summary>
        /// 新增 Jwt token
        /// </summary>
        /// <param name="dto">用户信息数据传输对象</param>
        /// <returns></returns>
        JwtAuthorizationDto Create(UserInfo dto);

        /// <summary>
        /// 刷新 Token
        /// </summary>
        /// <param name="token">Token</param>
        /// <param name="dto">用户信息数据传输对象</param>
        /// <returns></returns>
        Task<JwtAuthorizationDto> RefreshAsync(string token, UserInfo dto);

        /// <summary>
        /// 判断当前 Token 是否有效
        /// </summary>
        /// <returns></returns>
        Task<bool> IsCurrentActiveTokenAsync();

        /// <summary>
        /// 停用当前 Token
        /// </summary>
        /// <returns></returns>
        Task DeactivateCurrentAsync();

        /// <summary>
        /// 判断 Token 是否有效
        /// </summary>
        /// <param name="token">Token</param>
        /// <returns></returns>
        Task<bool> IsActiveAsync(string token);

        /// <summary>
        /// 停用 Token
        /// </summary>
        /// <returns></returns>
        Task DeactivateAsync(string token);

        /// <summary>
        /// 获取token
        /// </summary>
        /// <returns></returns>
        string GetCurrentAsync();

        string GetKey(string token);
        #endregion
    }
}
