/** @desc 文件模块-映射 */

export interface fileTypeListItem {
  name: string
  value: string
  menuIcon: string
  icon: string
}

// 文件分类
export const fileTypeList: fileTypeListItem[] = [
  { name: '全部', value: '', menuIcon: 'menu-file', icon: 'icon-stamp' },
  { name: '文件夹', value: 'Dir', menuIcon: 'file-dir', icon: 'icon-file-image' },
  { name: '图片', value: 'Image', menuIcon: 'file-image', icon: 'icon-file-image' },
  { name: '文档', value: 'Document', menuIcon: 'com-file', icon: 'icon-file' },
  { name: '视频', value: 'Video', menuIcon: 'file-video', icon: 'icon-video-camera' },
  { name: '音频', value: 'Audio', menuIcon: 'file-music', icon: 'icon-file-audio' },
  { name: '其他', value: 'Other', menuIcon: 'file-other', icon: 'icon-bulb' }
]

export interface FileExtendNameIconMap {
  [key: string]: string
}

// 文件类型图标 Map 映射
export const fileExtendNameIconMap: FileExtendNameIconMap = {
  mp3: 'file-music',
  mp4: 'file-video',
  dir: 'file-dir',
  ppt: 'file-ppt',
  pdf: 'file-pdf',
  doc: 'file-wps',
  docx: 'file-wps',
  xls: 'file-excel',
  xlsx: 'file-excel',
  txt: 'file-txt',
  rar: 'file-rar',
  zip: 'file-zip',
  html: 'file-html',
  css: 'file-css',
  js: 'file-js',
  other: 'file-other' // 未知文件
}

// 图片类型
export const imageTypeList = ['jpg', 'png', 'gif', 'jpeg', 'webp']

// WPS、Office文件类型
export const officeFileType = ['ppt', 'pptx', 'doc', 'docx', 'xls', 'xlsx']
