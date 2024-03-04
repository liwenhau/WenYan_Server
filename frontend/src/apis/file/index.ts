import http from '@/utils/http'
import { prefix } from '../config'
import type * as File from './type'

/** @desc 获取文件列表 */
export function getFileList(params: object) {
  return http.post<File.FileItem[]>(`${prefix}/Sys_File/GetDirAllFile`, params)
}

/** @desc 根据文件路径获取文件 */
export function getFileByPath(path: string) {
  return http.getFile(`${prefix}/Sys_File/GetFileByPath?path=${path}`)
}

/** @desc 上传文件分块 */
export function uploadFileChunk(params: object) {
  return http.post<Boolean>(`${prefix}/Sys_File/UploadFileChunk`, params)
}

/** @desc 合并文件 */
export function mergeFile(params: object) {
  return http.post<Boolean>(`${prefix}/Sys_File/MergeFile`, params)
}

/** @desc 文件重命名 */
export function fileReName(params: object) {
  return http.post<Boolean>(`${prefix}/Sys_File/FileReName`, params)
}

/** @desc 校验文件 */
export function checkFile(md5: string) {
  return http.get<Boolean>(`${prefix}/Sys_File/CheckFile?md5=${md5}`)
}

/** @desc 删除文件 */
export function deleteFile(params: Array<string | number>) {
  return http.delete<number>(`${prefix}/Sys_File/Delete`, params)
}
