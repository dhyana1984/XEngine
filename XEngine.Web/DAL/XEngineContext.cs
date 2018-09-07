using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Web;
using XEngine.Web.Models;
using XEngine.Web.Utility.MenuHelper.Base;

namespace XEngine.Web.DAL
{
    public class XEngineContext:DbContext
    {
        public XEngineContext()
            :base("XEngineContext")
        { }

        public DbSet<SysUser> SysUsers { get; set; }
        public DbSet<SysRole> SysRoles { get; set; }
        public DbSet<SysUserRole> SysUserRoles { get; set; }
        public DbSet<SysPrivilege> SysPrivileges { get; set; }
        public DbSet<SysOrganization> SysOrganizations { get; set; }
        public DbSet<SysMenu> SysMenus { get; set; }

        //public DbSet<MvcMenuItem> MvcMenuItems { get; set; }

        

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }
    }
}