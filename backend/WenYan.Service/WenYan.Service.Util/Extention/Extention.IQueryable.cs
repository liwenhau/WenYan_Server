using Microsoft.EntityFrameworkCore;

using System.Linq.Dynamic.Core;
using System.Linq.Expressions;

namespace WenYan.Service.Util
{
    /// <summary>
    /// IQueryable"T"的拓展操作
    /// </summary>
    public static partial class Extention
    {
        /// <summary>
        /// 符合条件则Where
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="q">数据源</param>
        /// <param name="need">是否符合条件</param>
        /// <param name="where">筛选</param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> q, bool need, Expression<Func<T, bool>> where)
        {
            if (need)
            {
                return q.Where(where);
            }
            else
            {
                return q;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="predicate"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static IQueryable<TSource> Where<TSource>(this IQueryable<TSource> source, string predicate, params object[] args)
        {
            return source.Where(ParsingConfig.Default, predicate, args);
        }

        /// <summary>
        /// 动态排序法
        /// </summary>
        /// <typeparam name="T">实体类型</typeparam>
        /// <param name="source">IQueryable数据源</param>
        /// <param name="sortColumn">排序的列</param>
        /// <param name="sortType">排序的方法</param>
        /// <returns></returns>
        public static IOrderedQueryable<T> OrderBy<T>(this IQueryable<T> source, string sortColumn, string sortType)
        {
            //return source.OrderBy(new KeyValuePair<string, string>(sortColumn, sortType));
            return source.OrderBy($"{sortColumn} {sortType}");
        }

        /// <summary>
        /// 获取分页数据(包括总数量)
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pageInput">分页参数</param>
        /// <returns></returns>
        public static PageResult<T> GetPageResult<T>(this IQueryable<T> source, PageInput pageInput)
        {
            int count = source.Count();

            var list = source.OrderBy($@"{pageInput.SortField} {pageInput.SortOrder}")
                .Skip((pageInput.PageNo - 1) * pageInput.PageSize)
                .Take(pageInput.PageSize)
                .ToList();

            return new PageResult<T> { data = list, total = count, pageNo = pageInput.PageNo };
        }

        /// <summary>
        /// 获取分页数据(包括总数量)
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pageInput">分页参数</param>
        /// <returns></returns>
        public static async Task<PageResult<T>> GetPageResultAsync<T>(this IQueryable<T> source, PageInput pageInput)
        {
            int count = await source.CountAsync();

            var list = await source.OrderBy($@"{pageInput.SortField} {pageInput.SortOrder}")
                .Skip((pageInput.PageNo - 1) * pageInput.PageSize)
                .Take(pageInput.PageSize)
                .ToListAsync();

            return new PageResult<T> { data = list, total = count, pageNo = pageInput.PageNo };
        }

        /// <summary>
        /// 获取分页数据(仅获取列表,不获取总数量)
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pageInput">分页参数</param>
        /// <returns></returns>
        public static List<T> GetPageList<T>(this IQueryable<T> source, PageInput pageInput)
        {
            var list = source.OrderBy($@"{pageInput.SortField} {pageInput.SortOrder}")
                .Skip((pageInput.PageNo - 1) * pageInput.PageSize)
                .Take(pageInput.PageSize)
                .ToList();

            return list;
        }

        /// <summary>
        /// 获取分页数据(仅获取列表,不获取总数量)
        /// </summary>
        /// <typeparam name="T">泛型</typeparam>
        /// <param name="source">数据源</param>
        /// <param name="pageInput">分页参数</param>
        /// <returns></returns>
        public static async Task<List<T>> GetPageListAsync<T>(this IQueryable<T> source, PageInput pageInput)
        {
            var list = await source.OrderBy($@"{pageInput.SortField} {pageInput.SortOrder}")
                .Skip((pageInput.PageNo - 1) * pageInput.PageSize)
                .Take(pageInput.PageSize)
                .ToListAsync();

            return list;
        }
    }
}
