/** 系统部门类型 */
export interface DeptItem {
  id: string
  name: string
  seq: number
  status: 0 | 1
  createTime: string
  parentId: string
  children?: DeptItem[]
  remark: string
}

/** 系统角色类型 */
export interface RoleItem {
  id: string
  createTime: string
  name: string
  code: string
}

/** 系统用户类型 */
export interface UserItem {
  id: string
  code: string
  sign: string
  createTime: string
  orgName: string
  userName: string
  name: string
  sex: string
  avatar: string
  refreshTokenExpiryTime: string
  status: 'Enable' | 'Dispaly'
  remark: string
}
/**
 * 用户详情
 */
export interface UserDetail {
  id: string
  code: string
  sign: string
  createTime: string
  orgId: string
  roleIds: string[]
  userName: string
  name: string
  sex: string
  avatar: string
  status: 'Enable' | 'Dispaly'
  remark: string
}

/** 系统菜单类型 */
export interface MenuItem {
  activeMenu: string
  breadcrumb: boolean
  children: MenuItem[]
  component: string
  hidden: boolean
  icon: string
  id: string
  keepAlive: boolean
  parentId: string
  path: string
  permission: string
  redirect: string
  roles: string[]
  sort: number
  status: 'Enable' | 'Disable'
  svgIcon: string
  title: string
  type: '1' | '2' | '3'
  affix: boolean
}

export interface MenuOptionsItem {
  id: string
  title: string
  children: MenuOptionsItem[]
}
