using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace XEngine.Data
{
    public class XEngineContext : DbContext
    {

        /// <summary>
        /// 跟 构造函数同名的ConnectString
        /// </summary>
        public XEngineContext():base()
        { }

        /// <summary>
        /// 指定ConnectionString名
        /// </summary>
        /// <param name="nameOrConnectionString"></param>
        public XEngineContext(string nameOrConnectionString)
            : base(nameOrConnectionString)
        {
        }

        #region 继承类需 Specifying entity sets
        //public DbSet<SysUser> SysUsers { get; set; }
        #endregion

        /// <summary>
        /// test
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
        }

    }
}
