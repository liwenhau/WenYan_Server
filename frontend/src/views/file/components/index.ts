import type { Component } from 'vue'
import { createApp } from 'vue'
import ArcoVueIcon from '@arco-design/web-vue/es/icon'
import ArcoVue, { Message } from '@arco-design/web-vue'
import type { FileItem } from '@/apis'

import FileMoveModal from './FileMoveModal/index.vue'
import FileRenameModal from './FileRenameModal/index.vue'
import FileUploadModal from './FileUploadModal/index.vue'
import PreviewVideoModal from './PreviewVideoModal/index.vue'
import PreviewAudioModal from './PreviewAudioModal/index.vue'

//
function createModal<T extends { callback?: () => void }>(component: Component, options?: T) {
  // 创建一个挂载容器
  const el: HTMLElement = document.createElement('div')
  // 挂载组件
  document.body.appendChild(el)
  // 实例化组件, createApp 第二个参数是 props
  const instance = createApp(component, {
    ...options,
    onClose: () => {
      instance.unmount()
      document.body.removeChild(el)
      options?.callback && options?.callback()
    }
  })

  instance.use(ArcoVue)
  instance.use(ArcoVueIcon)
  instance.mount(el)
}

type TFileOptions = { fileInfo: FileItem; onRefresh?: Function; callback?: () => void }

/** 打开 文件移动 弹窗 */
export function openFileMoveModal(fileItem: FileItem) {
  return createModal<TFileOptions>(FileMoveModal, { fileInfo: fileItem })
}

/** 打开 文件重命名 弹窗 */
export function openFileRenameModal(fileItem: FileItem) {
  //return createModal<TFileOptions>(FileRenameModal, { fileInfo: fileItem })
  return new Promise((resolve) => {
    createModal<TFileOptions>(FileRenameModal, {
      fileInfo: fileItem,
      onRefresh: (res?: any) => {
        resolve(res)
      }
    })
  })
}

/** 预览 视频文件 弹窗 */
let fileVideoId = ''
export function previewFileVideoModal(fileItem: FileItem) {
  if (fileVideoId) {
    Message.warning('太快了，请稍等一下！')
    return
  } // 防止重复打开
  fileVideoId = fileItem.id
  return createModal<TFileOptions>(PreviewVideoModal, {
    fileInfo: fileItem, // 关闭的回调
    callback: () => {
      fileVideoId = ''
    }
  })
}

/** 预览 音频文件 弹窗 */
let fileAudioId = ''
export function previewFileAudioModal(fileItem: FileItem) {
  if (fileAudioId) return // 防止重复打开
  fileAudioId = fileItem.id
  return createModal<TFileOptions>(PreviewAudioModal, {
    fileInfo: fileItem,
    // 关闭的回调
    callback: () => {
      fileAudioId = ''
    }
  })
}

type TUploadOptions = {
  isDirectory: boolean
  uploadDir: string
  title: string
  onRefresh?: Function
  callback?: () => void
}
/** 打开 文件上传 弹窗 */
export function openFileUploadModal(isDirectory: boolean, uploadDir: string) {
  return new Promise((resolve) => {
    createModal<TUploadOptions>(FileUploadModal, {
      isDirectory: isDirectory,
      uploadDir: uploadDir,
      title: isDirectory ? '上传文件夹' : '上传文件',
      onRefresh: (res: any) => {
        resolve(res)
      }
    })
  })
}
