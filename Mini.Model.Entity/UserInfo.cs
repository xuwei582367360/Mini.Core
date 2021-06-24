using System;
using System.Linq;
using System.Text;
using SqlSugar;


                namespace Mini.Model.Entity
                {
                    ///<summary>
    ///
    ///</summary>
                    [SugarTable( "UserInfo", "wmblog_mysql")]
                    public class UserInfo
                    {
                           public UserInfo()
                           {
                           }
                           /// <summary>
           /// Desc:用户编号
           /// Default:
           /// Nullable:False
           /// </summary>
           [SugarColumn(IsPrimaryKey=true,IsIdentity=true)]
           public int UserID { get; set; }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string UserCode { get; set; }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string UserName { get; set; }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Six { get; set; }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string PhoneNumber { get; set; }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string Address { get; set; }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public DateTime? CreateDate { get; set; }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string CreateUser { get; set; }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public DateTime? UpdateDate { get; set; }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public string UpdateUser { get; set; }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>
           public bool? IsDelete { get; set; }
                    }
                }