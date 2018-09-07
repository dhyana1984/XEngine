using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace XEngine.Data
{
    public interface IRepositoryBase<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");

        TEntity GetByID(object id);
        /// <summary>
        /// 新增实体
        /// </summary>
        /// <param name="entity"></param>
        void Insert(TEntity entity);
        /// <summary>
        /// 根据ID删除实体接口
        /// </summary>
        /// <param name="id"></param>
        void Delete(object id);
        /// <summary>
        /// 更新实体接口
        /// </summary>
        /// <param name="entity"></param>
        void Update(TEntity entity);
    }
}
