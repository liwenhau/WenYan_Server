export type Status = 0 | 1
export type IStatusMapData = { label: string; value: Status; color: 'green' | 'red' }[]
export const StatusMapData: IStatusMapData = [
  { label: '正常', value: 1, color: 'green' },
  { label: '禁用', value: 0, color: 'red' }
]

export type Gender = 1 | 2
export const GenderMap = { 1: '男', 2: '女' }

/*
 菜单类型 1目录 2菜单 3按钮
 */
export const DirMenu = ['1', '2']

/** 目录 */
export const Dir = '1'
/** 菜单 */
export const Menu = '2'
/** 按钮 */
export const Btn = '3'
