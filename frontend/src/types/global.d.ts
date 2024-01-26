interface AnyObject {
  [key: string]: unknown
}

interface Options {
  value: unknown
  label: string
}

interface NodeOptions extends Options {
  children?: NodeOptions[]
}

type TimeRanger = [string, string]

/** 系统管理通用状态 Disable禁用 Enable启用 */
type Status = 'Enable' | 'Disable'

/** 性别 Boy男 Girl女 */
type Gender = 'Boy' | 'Girl女'

/** 菜单类型 1目录 2菜单 3按钮*/
type MenuType = '1' | '2' | '3'
