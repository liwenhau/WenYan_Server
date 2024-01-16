using WenYan.Service.IBusiness;

namespace WenYan.Service.IBusiness
{
    /// <summary>
    /// 当前登录用户信息
    /// </summary>
    /// <typeparam name="K"></typeparam>
    public interface IOperator<K>
    {
        /// <summary>
        /// 用户ID
        /// </summary>
        K UserId { get; }
        /// <summary> 
        /// 当前用户属性
        /// </summary>
        Task<UserInfoM> GetCurUser();
    }
    /// <summary>
    /// 操作人员信息
    /// </summary>
    public interface IOperator : IOperator<string>
    { }
}
