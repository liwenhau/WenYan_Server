export interface MenuForm {
  type: '1' | '2' | '3'
  icon: string
  svgIcon: string
  name: string
  seq: number
  status: 'Enable' | 'Disable'
  path: string
  component: string
  isKeepAlive: boolean
  isHide: boolean
  parentId: string
  redirect: string
  permission: string
  code: string
  /*breadcrumb: boolean
  showInTabs: boolean
  alwaysShow: boolean*/
}
