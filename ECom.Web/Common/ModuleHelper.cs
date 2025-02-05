﻿using ECom.Web.Models;
using System;
using System.Collections.Generic;

namespace ECom.Web.Common
{
    /// <summary>
    /// This is where you customize the navigation sidebar
    /// </summary>
    public static class ModuleHelper
    {
        public enum Module
        {
            Home,
            Product,
            ProductCategory,
            Order,
            User,
            Register,
            SuperAdmin,
            Role,
            UserLogs
        }

        public static SidebarMenu AddHeader(string name)
        {
            return new SidebarMenu
            {
                Type = SidebarMenuType.Header,
                Name = name,
            };
        }

        public static SidebarMenu AddTree(string name, string iconClassName = "fa fa-link")
        {
            return new SidebarMenu
            {
                Type = SidebarMenuType.Tree,
                IsActive = false,
                Name = name,
                IconClassName = iconClassName,
                URLPath = "#",
            };
        }

        public static SidebarMenu AddModule(Module module, Tuple<int, int, int> counter = null)
        {
            if (counter == null)
                counter = Tuple.Create(0, 0, 0);

            switch (module)
            {
                case Module.Home:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Home",
                        IconClassName = "fa fa-home",
                        URLPath = "/",
                        LinkCounter = counter,
                    };
                case Module.Product:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Product",
                        IconClassName = "fa fa-sign-in-alt",
                        URLPath = "/Product",
                        LinkCounter = counter,
                    };
                case Module.ProductCategory:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "ProductCategory",
                        IconClassName = "fa fa-users",
                        URLPath = "/ProductCategory",
                        LinkCounter = counter,
                    };
                case Module.Register:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Register",
                        IconClassName = "fa fa-user-plus",
                        URLPath = "/Account/Register",
                        LinkCounter = counter,
                    };
             
                case Module.User:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Contact",
                        IconClassName = "fa fa-phone",
                        URLPath = "/Home/Contact",
                        LinkCounter = counter,
                    };
                //case Module.Error:
                //    return new SidebarMenu
                //    {
                //        Type = SidebarMenuType.Link,
                //        Name = "Error",
                //        IconClassName = "fa fa-exclamation-triangle",
                //        URLPath = "/Home/Error",
                //        LinkCounter = counter,
                //    };
                case Module.SuperAdmin:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "User",
                        IconClassName = "fa fa-users",
                        URLPath = "/SuperAdmin",
                        LinkCounter = counter,
                    };
                case Module.Role:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "Role",
                        IconClassName = "fa fa-user-tag",
                        URLPath = "/Role",
                        LinkCounter = counter,
                    };
                case Module.UserLogs:
                    return new SidebarMenu
                    {
                        Type = SidebarMenuType.Link,
                        Name = "UserLogs",
                        IconClassName = "fa fa-history",
                        URLPath = "/UserLogs",
                        LinkCounter = counter,
                    };

                default:
                    break;
            }

            return null;
        }
    }
}
