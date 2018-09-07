using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XEngine.Data
{
    /// <summary>
    /// Deprecated ， 目前无法共用
    /// </summary>
    public class UnitOfWorkBase<TContext> : IDisposable where TContext : DbContext,new()
    {
        /// <summary>
        /// 用继承类的context重写
        /// </summary>
        protected TContext context=new TContext();


        #region Domain Repository, 继承类需要定义一组repository属性, 例如

        //private GenericRepository<FormProductionOut> formProductionOutRepository;

        //public GenericRepository<FormProductionOut> FormProductionOutRepository
        //{
        //    get
        //    {
        //        if (this.formProductionOutRepository == null)
        //        {
        //            this.formProductionOutRepository = new GenericRepository<FormProductionOut>(context);
        //        }
        //        return formProductionOutRepository;
        //    }
        //}
        #endregion

        #region Save & Dispose
        /// <summary>
        /// 
        /// </summary>
        public void Save()
        {
            context.SaveChanges();
        }

        private bool disposed = false;
        /// <summary>
        /// 释放资源
        /// </summary>
        /// <param name="disposing"></param>
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
        /// <summary>
        /// 释放资源
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        #endregion
    }
}
