using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mini.Common.Helper;
using Mini.Model.Entity;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace JG.Report.ViewCompoents
{
    public class MenuRankList : ViewComponent
    {
        public class Menu
        {
            public string MenuName { get; set; }
            public string MenuURL { get; set; }
            public string Icon { get; set; }
            public int Level { get; set; }
            public List<string> Limits { get; set; }
            public List<Menu> ChildrenMenu { get; set; }
        }

        public List<Menu> getMenu() => new List<Menu>
        {
                new Menu {MenuName = "合同",MenuURL="#",Icon="",Level=0,Limits = new List<string>{ "101"},
                    ChildrenMenu = new List<Menu>{
                        new Menu { MenuName = "合同",MenuURL="~/Test/Index",Icon="",Level=0,Limits =  new List<string>{ "101"} }
                    }
                },
                new Menu {MenuName = "开工",MenuURL="#",Icon="",Level=0,Limits =  new List<string>{ "101"},
                    ChildrenMenu = new List<Menu>{
                        new Menu { MenuName = "工程状态统计", MenuURL="#",Icon="",Level=0,Limits =  new List<string>{ "101"} }
                    }
                },
                new Menu {MenuName = "竣工",MenuURL="#",Icon="",Level=0,Limits =  new List<string>{ "101"},
                    ChildrenMenu = new List<Menu>{
                        new Menu { MenuName = "目标", MenuURL="#",Icon="",Level=0,Limits =  new List<string>{ "101"} },
                        new Menu { MenuName = "实际", MenuURL="#",Icon="",Level=0,Limits =  new List<string>{ "101"} }
                    }
                },
                new Menu {MenuName = "回头客",MenuURL="#",Icon="",Level=0,Limits = new List<string>{ "101"}},
                new Menu {MenuName = "客诉",MenuURL="#",Icon="",Level=0,Limits =  new List<string>{ "101"}},
                new Menu {MenuName = "售后",MenuURL="#",Icon="",Level=0,Limits =  new List<string>{ "101"}},
                new Menu {MenuName = "监理",MenuURL="#",Icon="",Level=0,Limits =  new List<string>{ "101"}},
         };

        public IViewComponentResult Invoke()
        {
            HttpContext.Session.SetString("userInfo", JsonConvert.SerializeObject(new UserInformation { 序号= 1110,用户代码= "101001",用户姓名= "任志天", 部门="101"}));
            if (HttpContext.Session.GetString("userInfo") == null)
            {
                return View();
                //重新登录
            }
            else
            {
                UserInformation user = JsonConvert.DeserializeObject<UserInformation>(HttpContext.Session.GetString("userInfo"));
                var listMenu = new List<Menu>();
                getMenu().ForEach(x =>
                {
                    //父级权限
                    if (x.Limits.Contains(user.部门))
                    {
                        Menu menu = new Menu();
                        menu = Mapper<Menu, Menu>.Map(x);
                        //子级权限
                        if (x.ChildrenMenu != null)
                        {
                            menu.ChildrenMenu = new List<Menu>();
                            x.ChildrenMenu.ForEach(y =>
                            {
                                if (y.Limits.Contains(user.部门))
                                {
                                    menu.ChildrenMenu.Add(Mapper<Menu, Menu>.Map(y)) ;
                                }
                            });
                        }
                        listMenu.Add(menu);
                    }
                });
                return View(listMenu);
            }
        }

    }
}
