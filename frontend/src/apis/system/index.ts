import http from '@/utils/http'
import { prefix } from '../config'
import type * as System from './type'

/** @desc 获取组织部门数据 */
export function getSystemDeptList(query: any) {
  return http.get<System.DeptItem[]>(`${prefix}/Sys_Org/GetAll`, query)
}

/** @desc 获取部门详情 */
export function getSystemDeptDetil(params: { id: string }) {
  return http.get<System.DeptItem>(`${prefix}/Sys_Org/Get`, params)
}

/** @desc 保存部门 */
export function saveSystemDept(data: any) {
  return http.post<boolean>(`${prefix}/Sys_Org/Save`, data)
}

/** @desc 删除部门 */
export function deleteSystemDept(data: Array<string | number>) {
  return http.delete<boolean>(`${prefix}/Sys_Org/Delete`, data)
}

/** @desc 获取角色数据 */
export function getSystemRoleList(query: any) {
  return http.post<System.RoleItem[]>(`${prefix}/Sys_Role/GetAll`, query)
}

/** @desc 获取角色详情 */
export function getSystemRoleDetail(params: { id: string }) {
  return http.get<System.RoleItem>(`${prefix}/Sys_Role/Get`, params)
}

/** @desc 保存角色 */
export function saveSystemRole(data: any) {
  return http.post<boolean>(`${prefix}/Sys_Role/Save`, data)
}

/** @desc 获取角色权限 */
export function getSystemRoleMenuIds(params: { roleId: string }) {
  return http.get<string[]>(`${prefix}/Sys_Role/GetRolePermission`, params)
}

/** @desc 更新角色权限 */
export function saveSystemRolePermission(roleId: string, data: any) {
  return http.post<boolean>(`${prefix}/Sys_Role/SaveRolePermission?roleId=${roleId}`, data)
}

/** @desc 删除角色 */
export function deleteSystemRole(data: Array<string | number>) {
  return http.delete<boolean>(`${prefix}/Sys_Role/Delete`, data)
}

/** @desc 获取用户数据 */
export function getSystemUserList(query: any) {
  return http.post<System.UserItem[]>(`${prefix}/Sys_User/GetPage`, query)
}

/** @desc 获取用户详情 */
export function getSystemUserDetail(params: { id: string }) {
  return http.get<System.UserDetail>(`${prefix}/Sys_User/GetUserDetail`, params)
}

/** @desc 保存用户 */
export function saveSystemUser(data: any) {
  return http.post<boolean>(`${prefix}/Sys_User/Save`, data)
}

/** @desc 获取菜单数据 */
export function getSystemMenuList(query: any) {
  return http.get<System.MenuItem[]>(`${prefix}/Sys_Menu/GetAll`, query)
}

/** @desc 获取菜单详情 */
export function getSystemMenuDetail(params: { id: string }) {
  return http.get<System.MenuItem>(`${prefix}/Sys_Menu/Get`, params)
}

/** @desc 保存菜单 */
export function saveSystemMenu(data: any) {
  return http.post<boolean>(`${prefix}/Sys_Menu/Save`, data)
}

/** @desc 删除菜单 */
export function deleteSystemMenu(data: Array<string | number>) {
  return http.delete<boolean>(`${prefix}/Sys_Menu/Delete`, data)
}

/** @desc 获取角色分配权限菜单树 */
export function getSystemMenuOptions() {
  return http.get<System.MenuOptionsItem[]>(`${prefix}/Sys_Menu/GetTree`)
}

/** @desc 获取动态路由数据 */
export function getSystemAsyncRoutes() {
  return http.get<any[]>(`${prefix}/system/menu/routes`)
}
