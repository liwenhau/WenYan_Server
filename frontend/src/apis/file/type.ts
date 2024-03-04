import type { RequestOption } from '@arco-design/web-vue/es/upload'

// 文件
export interface FileItem {
  id: string
  type: string
  name: string
  extendName: string
  src: string | null
  modifyTime: string
  isDir: boolean
  filePath: string
  [propName: string]: any // 一个 interface 中任意属性只能有一个
}
//文件导航
export interface FileNav {
  name: string
  path: string
  dirID: string
}

export interface taskArrItem {
  md5: string
  name?: string
  state: number // 0是什么都不做,1文件处理中,2是上传中,3是暂停,4是上传完成,5上传中断
  fileSize: number
  allRequstData: Array<AllRequstDataItem> // 所有请求成功或者请求未成功的请求信息
  whileRequests: Array<AllRequstDataItem>
  finishNumber: number
  errNumber: number
  option: RequestOption
}

export interface AllRequstDataItem {
  file: File | Blob | void
  sliceFileSize: number
  index: number
  fileSize: number
  fileName: string
  sliceNumber: number
  cancel?: Function | void
  finish?: boolean
}
