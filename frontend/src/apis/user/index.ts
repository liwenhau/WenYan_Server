import http from '@/utils/http'
import { prefix } from '../config'
import type * as User from './type'
import type { RouteRecordRaw } from 'vue-router'

/** @desc 登录 */
export function login(data: { username: string; password: string }) {
  return http.post<User.LoginRes>(`${prefix}/Operator/Login`, data)
}

/** @desc 退出登录 */
export function logout() {
  return http.post(`${prefix}/user/logout`)
}

/** @desc 获取用户信息 */
export const getUserInfo = () => {
  return http.get<User.UserInfo>(`${prefix}/Operator/GetUserInfo`)
}

/** @desc 获取用户路由信息 */
export const getUserAsyncRoutes = () => {
  return http.get<User.UserMeuns[]>(`${prefix}/Operator/GetUserMenus`)
}
