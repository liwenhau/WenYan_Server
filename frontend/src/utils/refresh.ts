import { getToken, getRefreshToken, setToken, setRefreshToken, clearToken, clearRefreshToken } from '@/utils/auth'
import type { AxiosRequestConfig } from 'axios'
import type { LoginRes } from '@/apis'
import request from './http'
import router from '@/router'
import { Message } from '@arco-design/web-vue'
//刷新token时待处理队列
interface PendingTask {
  config: AxiosRequestConfig
  resolve: Function
}
/** 刷新开关*/
let flag = ref(false)
/** 缓存请求队列*/
let subsequent = ref<PendingTask[]>([])
/** 缓存过期请求*/
export const addRequest = (task: PendingTask) => {
  subsequent.value.push(task)
}
/** 调用过期请求*/
export const retryRequest = (token: string) => {
  subsequent.value.forEach(({ config, resolve }) => {
    if (config.headers) config.headers.Authorization = token
    resolve(request(config))
  })
  subsequent.value = []
}
/**token过期，发起刷新token请求*/
export const refreshToken = () => {
  if (!flag.value) {
    flag.value = true
    request
      .post<LoginRes>(`/api/Operator/RefreshToken`, {
        accessToken: getToken(),
        refreshToken: getRefreshToken()
      })
      .then((res) => {
        console.log(res)
        if (res.code === 4001) {
          //刷新令牌失效
          clearToken()
          clearRefreshToken()
          Message.error('令牌已失效，请重新登录！')
          router.replace('/login')
        } else if (res.code === 4000) {
          const { accessToken, refreshToken } = res.data
          setToken(accessToken)
          setRefreshToken(refreshToken)
          //重新调用过期请求
          retryRequest(accessToken)
        }
      })
      .finally(() => (flag.value = false))
  }
}
