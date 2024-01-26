/** 接口返回数据格式 */
interface ApiRes<T> {
  code: number
  message: string
  data: T
  success: boolean
  total?: number
}

/** 接口返回的列表数据 */
interface ApiListData<T> {
  total: number
  list: T
  [propName: string]: unknown
}
/** 接口返回分页数据 */
interface ApiPageData extends ApiRes<T> {
  total: number
}
