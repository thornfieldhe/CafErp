﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="EFEntity.cs" company="">
//   
// </copyright>
// <summary>
//   Defines the EFEntity type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------
namespace Erp.Eam.Business
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;
    using System.Linq.Expressions;

    using CAF;

    using EntityFramework.Caching;
    using EntityFramework.Extensions;

    /// <summary>
    /// The ef entity.
    /// </summary>
    /// <typeparam name="K">
    /// </typeparam>
    public abstract class EFEntity<K> : BaseEntity<K>, IDbAction where K : EFEntity<K>, IEntityBase, new()
    {
        #region 构造函数

        /// <summary>
        /// Initializes a new instance of the <see cref="EFEntity{K}"/> class.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        protected EFEntity(Guid id)
            : base(id)
        {
            this.DbContex = ContextWapper.Instance.Context;
            this.Init();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EFEntity{K}"/> class.
        /// </summary>
        protected EFEntity() : this(Guid.NewGuid())
        {
        }

        #endregion

        /// <summary>
        /// Gets or sets the db contex.
        /// </summary>
        [NotMapped]
        internal ApplicationDbContext DbContex
        {
            get; set;
        }

        #region 静态方法

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="K"/>.
        /// </returns>
        public static K Get(Guid id)
        {
            return new K().QuerySingle(i => i.Id == id);
        }

        /// <summary>
        /// The get all.
        /// </summary>
        /// <param name="useCache">
        /// The use cache.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<K> GetAll(bool useCache = false)
        {
            return new K().Query(useCache);
        }

        /// <summary>
        /// The get.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <param name="useCache">
        /// The use cache.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        public static List<K> Get(Expression<Func<K, bool>> func, bool useCache = false)
        {
            return new K().Query(func, useCache);
        }

        /// <summary>
        /// The pages.
        /// </summary>
        /// <param name="pager">
        /// The pager.
        /// </param>
        /// <param name="whereFunc">
        /// The where func.
        /// </param>
        /// <param name="orderByFunc">
        /// The order by func.
        /// </param>
        /// <param name="selector">
        /// The selector.
        /// </param>
        /// <param name="isAsc">
        /// The is asc.
        /// </param>
        /// <typeparam name="T">
        /// </typeparam>
        /// <typeparam name="R">
        /// </typeparam>
        /// <returns>
        /// The <see cref="Pager"/>.
        /// </returns>
        public static Pager<R> Pages<T, R>(
            Pager<R> pager,
            Func<K, bool> whereFunc,
            Func<K, T> orderByFunc,
            Func<K, R> selector,
            bool isAsc) where R : new()
        {
            var context = ContextWapper.Instance.Context;
            pager.Total = context.Set<K>().Where(whereFunc).Count();
            List<R> result;
            if (isAsc)
            {
                result =
                    context.Set<K>()
                        .Where(whereFunc)
                        .OrderBy(orderByFunc)
                        .Skip(pager.PageSize * (pager.PageIndex - 1))
                        .Take(pager.PageSize)
                        .Select(selector)
                        .ToList();
            }
            else
            {
                result =
                    context.Set<K>()
                        .Where(whereFunc)
                        .OrderByDescending(orderByFunc)
                        .Skip(pager.PageSize * (pager.PageIndex - 1))
                        .Take(pager.PageSize)
                        .Select(selector)
                        .ToList();
            }

            pager.Datas = result;
            pager.GetShowIndex();
            return pager;
        }

        /// <summary>
        /// The get by id.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="K"/>.
        /// </returns>
        public static K GetById(Guid id)
        {
            var context = ContextWapper.Instance.Context;
            var item = context.Set<K>().Find(id);
            item.DbContex = context;
            return item;
        }

        /// <summary>
        /// The exist.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <returns>
        /// The <see cref="bool"/>.
        /// </returns>
        public static bool Exist(Expression<Func<K, bool>> func)
        {
            return ContextWapper.Instance.Context.Set<K>().Any(func);
        }

        /// <summary>
        /// The count.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public static int Count(Expression<Func<K, bool>> func)
        {
            return ContextWapper.Instance.Context.Set<K>().Count(func);
        }

        #endregion

        #region 实例方法

        /// <summary>
        /// The create.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Create()
        {
            this.PreInsert();
            this.Validate();
            this.Insert();
            this.PostInsert();
            return this.DbContex.SaveChanges();
        }

        /// <summary>
        /// The save.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Save()
        {
            this.PreUpdate();
            this.Validate();
            this.Update();
            this.PostUpdate();
            return this.DbContex.SaveChanges();
        }

        /// <summary>
        /// The delete.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int Delete()
        {
            this.PreRemove();
            this.Remove();
            this.PostRemove();
            return this.DbContex.SaveChanges();
        }

        /// <summary>
        /// The find.
        /// </summary>
        /// <param name="id">
        /// The id.
        /// </param>
        /// <returns>
        /// The <see cref="K"/>.
        /// </returns>
        public K Find(Guid id)
        {
            return this.QuerySingle(i => i.Id == id);
        }

        #region 模块内部方法

        /// <summary>
        /// The query.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <param name="useCache">
        /// The use cache.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        internal List<K> Query(Expression<Func<K, bool>> func, bool useCache = false)
        {
            var query = this.DbContex.Set<K>().Where(func);
            var items = this.PreQuery(query, useCache);
            return this.PostQuery(items);
        }

        /// <summary>
        /// The query.
        /// </summary>
        /// <param name="useCache">
        /// The use cache.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        internal List<K> Query(bool useCache = false)
        {
            var query = this.DbContex.Set<K>();
            var items = this.PreQuery(query, useCache);
            return this.PostQuery(items);
        }

        /// <summary>
        /// The query single.
        /// </summary>
        /// <param name="func">
        /// The func.
        /// </param>
        /// <returns>
        /// The <see cref="K"/>.
        /// </returns>
        internal K QuerySingle(Expression<Func<K, bool>> func)
        {
            var query = this.DbContex.Set<K>().Where(func);
            var item = this.PreQuerySingle(query);
            return this.PostQuerySingle(item);
        }

        #endregion


        #region 继承方法

        /// <summary>
        /// The init.
        /// </summary>
        protected virtual void Init()
        {
        }

        /// <summary>
        /// The update.
        /// </summary>
        protected virtual void Update()
        {
            this.ChangedDate = DateTime.Now;
        }

        /// <summary>
        /// The insert.
        /// </summary>
        protected virtual void Insert()
        {
            this.DbContex.Set<K>().Add(this as K);
        }

        /// <summary>
        /// The remove.
        /// </summary>
        protected virtual void Remove()
        {
            this.DbContex.Set<K>().Remove(this as K);
        }

        /// <summary>
        /// The pre insert.
        /// </summary>
        protected virtual void PreInsert()
        {
        }

        /// <summary>
        /// The pre update.
        /// </summary>
        protected virtual void PreUpdate()
        {
            this.ChangedDate = DateTime.Now;
        }

        /// <summary>
        /// The pre query.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <param name="useCache">
        /// The use cache.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        protected virtual List<K> PreQuery(IQueryable<K> query, bool useCache = false)
        {
            var items = useCache
                            ? query.FromCache(CachePolicy.WithDurationExpiration(TimeSpan.FromSeconds(60))).ToList()
                            : query.ToList();
            return this.PostQuery(items);
        }

        /// <summary>
        /// The pre query single.
        /// </summary>
        /// <param name="query">
        /// The query.
        /// </param>
        /// <returns>
        /// The <see cref="K"/>.
        /// </returns>
        protected virtual K PreQuerySingle(IQueryable<K> query)
        {
            var item = query.FirstOrDefault();
            return item;
        }

        /// <summary>
        /// The pre remove.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected virtual int PreRemove()
        {
            return 0;
        }

        /// <summary>
        /// The post update.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected virtual int PostUpdate()
        {
            return 0;
        }

        /// <summary>
        /// The post remove.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected virtual int PostRemove()
        {
            return 0;
        }

        /// <summary>
        /// The post insert.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        protected virtual int PostInsert()
        {
            return 0;
        }

        /// <summary>
        /// The post query.
        /// </summary>
        /// <param name="items">
        /// The items.
        /// </param>
        /// <returns>
        /// The <see cref="List"/>.
        /// </returns>
        protected virtual List<K> PostQuery(List<K> items)
        {
            items.ForEach(i => i.DbContex = this.DbContex);
            return items;
        }

        /// <summary>
        /// The post query single.
        /// </summary>
        /// <param name="item">
        /// The item.
        /// </param>
        /// <returns>
        /// The <see cref="K"/>.
        /// </returns>
        protected virtual K PostQuerySingle(K item)
        {
            item.IfNotNull(i => i.DbContex = this.DbContex);
            return item;
        }

        #endregion

        #endregion


    }

    /// <summary>
    /// The pager.
    /// </summary>
    /// <typeparam name="T">
    /// </typeparam>
    public class Pager<T>
        where T : new()
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Pager{T}"/> class.
        /// </summary>
        public Pager()
        {
            this.PageSize = 20;
        }

        /// <summary>
        /// Gets or sets the datas.
        /// </summary>
        public IList<T> Datas
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the page index.
        /// </summary>
        public int PageIndex
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the page size.
        /// </summary>
        public int PageSize
        {
            get; set;
        }

        /// <summary>
        /// Gets or sets the total.
        /// </summary>
        public int Total
        {
            get; set;
        }

        public bool IsFirst
        {
            get
            {
                return this.PageIndex == 1;
            }
        }

        public bool IsLast
        {
            get
            {
                var temp0 = this.Total % this.PageSize;//总页数是否能够整出每页数
                var temp1 = temp0 == 0 ? this.Total / this.PageSize : this.Total / this.PageSize + 1; //总分页数;
                return temp1 == this.PageIndex;
            }
        }



        /// <summary>
        /// 表单页脚需要显示的页码
        /// </summary>
        public List<int> ShowIndex
        {
            get; set;
        }

        /// <summary>
        /// The get show index.
        /// </summary>
        public void GetShowIndex()
        {
            var temp0 = this.Total % this.PageSize;//总页数是否能够整出每页数
            var temp1 = temp0 == 0 ? this.Total / this.PageSize : this.Total / this.PageSize + 1; //总分页数;

            this.ShowIndex = new List<int>();
            for (var i = 0; i < temp1; i++)
            {
                if (i * this.PageSize < this.PageIndex && (i + 1) * this.PageSize >= this.PageIndex)
                {
                    for (var j = 1; j <= 5; j++)
                    {
                        if (temp1 >= i * this.PageSize + j)
                        {
                            this.ShowIndex.Add(i * this.PageSize + j);
                        }
                    }
                }
            }


            if (temp1 > 5 && this.PageIndex % 5 == 0)
            {
                for (int i = 0; i < this.ShowIndex.Count; i++)
                {
                    this.ShowIndex[i] = this.ShowIndex[i] + 1;
                }
            }


            else if (this.PageIndex >= 6 && this.PageIndex % 5 == 1)
            {
                if (this.ShowIndex.Count == 5)
                {
                    for (int i = 0; i < this.ShowIndex.Count; i++)
                    {
                        this.ShowIndex[i] = this.ShowIndex[i] - 1;
                    }
                }
                else
                {
                    this.ShowIndex.Add(this.PageIndex - 1);
                    this.ShowIndex.Sort();
                }
            }
        }
    }
}