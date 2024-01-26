import http from '@/utils/http'
import { prefix } from '../config'
import type * as File from './type'

/** @desc 获取文件列表 */
export function getFileList(params: object) {
  return http.post<File.FileItem[]>(`${prefix}/Sys_File/GetAllRootDir`, params)
}

/** @desc 根据文件路径获取文件 */
export function getFileByPath(path: string) {
  return http.getFile(`${prefix}/Sys_File/GetFileByPath?path=${path}`)
}
