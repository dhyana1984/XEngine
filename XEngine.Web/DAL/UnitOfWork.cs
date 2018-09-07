using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using XEngine.Web.Models;
using XEngine.Web.Repositories;

namespace XEngine.Web.DAL
{
    public class UnitOfWork:IDisposable
    {
        private XEngineContext context = new XEngineContext();

        private GenericRepository<SysUser> sysUserRepository;
        private GenericRepository<SysRole> sysRoleRepository;
        private GenericRepository<SysUserRole> sysUserRoleRepository;
        private GenericRepository<SysPrivilege> sysPrivilegeRepository;
        private GenericRepository<SysOrganization> sysOrganizationRepository;
        private GenericRepository<SysMenu> sysMenuRepository;


        public GenericRepository<SysUser> SysUserRepository
        {
            get
            {
                if (this.sysUserRepository == null)
                {
                    this.sysUserRepository = new GenericRepository<SysUser>(context);
                }
                return sysUserRepository;
            }
        }
        public GenericRepository<SysRole> SysRoleRepository
        {
            get
            {
                if (this.sysRoleRepository == null)
                {
                    this.sysRoleRepository = new GenericRepository<SysRole>(context);
                }
                return sysRoleRepository;
            }
        }
        public GenericRepository<SysUserRole> SysUserRoleRepository
        {
            get
            {
                if (this.sysUserRoleRepository == null)
                {
                    this.sysUserRoleRepository = new GenericRepository<SysUserRole>(context);
                }
                return sysUserRoleRepository;
            }
        }
        public GenericRepository<SysPrivilege> SysPrivilegeRepository
        {
            get
            {
                if (this.sysPrivilegeRepository == null)
                {
                    this.sysPrivilegeRepository = new GenericRepository<SysPrivilege>(context);
                }
                return sysPrivilegeRepository;
            }
        }
        public GenericRepository<SysOrganization> SysOrganizationRepository
        {
            get
            {
                if (this.sysOrganizationRepository == null)
                {
                    this.sysOrganizationRepository = new GenericRepository<SysOrganization>(context);
                }
                return sysOrganizationRepository;
            }
        }
        public GenericRepository<SysMenu> SysMenuRepository
        {
            get
            {
                if (this.sysMenuRepository == null)
                {
                    this.sysMenuRepository = new GenericRepository<SysMenu>(context);
                }
                return sysMenuRepository;
            }
        }


        #region Save & Dispose
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}