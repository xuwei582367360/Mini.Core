//-----------------------------------------------------------------------
// <copyright file= "JwtAuthorizationDto.cs">
//     Copyright (c) Danvic712. All rights reserved.
// </copyright>
// Author: Danvic712
// Created DateTime: 2019/1/22 13:29:14 
// Modified by:
// Description: Json Web Token 数据传输实体
//-----------------------------------------------------------------------

using System;

namespace Mini.Extensions.ServiceExtensions.Jwt.Dto
{
    public class JwtAuthorizationDto
    {
        /// <summary>
        /// 授权时间
        /// </summary>
        public DateTime? Auths { get; set; }

        /// <summary>
        /// 过期时间
        /// </summary>
        public DateTime? Expires { get; set; }

        /// <summary>
        /// 是否授权成功
        /// </summary>
        public bool Success { get; set; } = true;

        /// <summary>
        /// Token
        /// </summary>
        public string Token { get; set; }

        /// <summary>
        /// 用户主键
        /// </summary>
        public int UserId { get; set; }
    }
}
