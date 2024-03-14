import axios from 'axios'
import type { AxiosInstance, AxiosRequestConfig, AxiosResponse, AxiosRequestHeaders, ResponseType } from 'axios'
import { Message, Notification } from '@arco-design/web-vue'
import { getToken } from '@/utils/auth'
import { addRequest, refreshToken } from './refresh'
import NProgress from 'nprogress'
import 'nprogress/nprogress.css'

NProgress.configure({ showSpinner: false }) // NProgress Configuration

interface ICodeMessage {
  [propName: number]: string
}

const StatusCodeMessage: ICodeMessage = {
  200: '服务器成功返回请求的数据',
  201: '新建或修改数据成功。',
  202: '一个请求已经进入后台排队（异步任务）',
  204: '删除数据成功',
  400: '请求错误(400)',
  401: '未授权，请重新登录(401)',
  403: '拒绝访问(403)',
  404: '请求出错(404)',
  408: '请求超时(408)',
  500: '服务器错误(500)',
  501: '服务未实现(501)',
  502: '网络错误(502)',
  503: '服务不可用(503)',
  504: '网络超时(504)'
}

const http: AxiosInstance = axios.create({
  baseURL: import.meta.env.VITE_API_BASE_URL,
  timeout: 30 * 1000
})

// 请求拦截器
http.interceptors.request.use(
  (config: AxiosRequestConfig) => {
    NProgress.start() // 进度条
    config.headers = {
      'Content-Type': 'application/json' // 配置请求头
    }
    const token = getToken()
    if (token) {
      if (!config.headers) {
        config.headers = {}
      }
      //config.headers['token'] = token
      config.headers['Authorization'] = 'Bearer ' + token
    }
    return config
  },
  (error) => {
    return Promise.reject(error)
  }
)

// 响应拦截器
http.interceptors.response.use(
  async (response: AxiosResponse) => {
    const { data, config } = response
    const { message, success, code } = data
    //处理Blob文件流
    if (response.request?.responseType === 'blob') {
      NProgress.done()
      return response
    }
    //无感刷新，双token机制
    if (code === 401) {
      // token失效
      NProgress.done()
      new Promise((resolve) => {
        addRequest({ config, resolve })
      })
      refreshToken()
    } else if (!success) {
      NProgress.done()
      // 如果错误信息长度过长，使用 Notification 进行提示
      if (message.length <= 15) {
        Message.error(message || '服务器端错误')
      } else {
        Notification.error(message || '服务器端错误')
      }
      return Promise.reject(new Error('Error'))
    } else {
      NProgress.done()
      return response
    }
  },
  (error) => {
    NProgress.done()
    Message.clear()
    const response = Object.assign({}, error.response)
    response && Message.error(StatusCodeMessage[response.status] || '系统异常, 请检查网络或联系管理员！')
    return Promise.reject(error)
  }
)

const request = <T = unknown>(config: AxiosRequestConfig): Promise<T> => {
  return new Promise((resolve, reject) => {
    http
      .request<T>(config)
      .then((res: AxiosResponse) => resolve(res.data))
      .catch((err: { message: string }) => reject(err))
  })
}

request.get = <T = any>(
  url: string,
  params?: object,
  headers?: AxiosRequestHeaders,
  responseType?: ResponseType
): Promise<ApiRes<T>> => {
  return request({
    method: 'get',
    url,
    params,
    headers,
    responseType
  })
}

request.getFile = <T = any>(url: string, params?: object, headers?: AxiosRequestHeaders): Promise<T> => {
  return request({
    method: 'get',
    url,
    params,
    headers,
    responseType: 'blob'
  })
}

request.post = <T = any>(url: string, params?: object, headers?: AxiosRequestHeaders): Promise<ApiRes<T>> => {
  return request({
    method: 'post',
    url,
    data: params,
    headers
  })
}

request.delete = <T = any>(
  url: string,
  params?: Array<String | number>,
  headers?: AxiosRequestHeaders
): Promise<ApiRes<T>> => {
  return request({
    method: 'delete',
    url,
    data: params,
    headers
  })
}
export default request
