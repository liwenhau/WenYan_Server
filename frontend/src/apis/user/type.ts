export interface UserInfo {
  id: string
  name: string
  avatar: string
  sex: string
  sign: string
  roles: string[]
  permissions: string[]
}

export interface LoginRes {
  accessToken: string
  refreshToken: string
}

export interface UserMeuns {
  id: string
  parentId: string
  path: string
  component: string
  redirect: string
  type: string
  title: string
  svgIcon: string
  icon: string
  keepAlive: boolean
  hidden: boolean
  sort: number
  activeMenu: string
  breadcrumb: boolean
  status: 0 | 1
  roles: string[]
  permission: string
  showInTabs: boolean
  affix?: boolean
  alwaysShow: boolean
  children?: UserMeuns[]
}
