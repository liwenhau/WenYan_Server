<template>
  <div class="file-main">
    <!-- 面包屑导航 -->
    <FileNavPath @click="handleClickFileNav" :data="fileNavs"></FileNavPath>

    <a-row justify="space-between" class="row-operate">
      <!-- 左侧区域 -->
      <a-space wrap>
        <a-dropdown>
          <a-button type="primary" shape="round" v-hasPerm="['file:btn:upload']">
            <template #icon><icon-upload /></template>
            <template #default>上传</template>
          </a-button>
          <template #content>
            <a-doption @click="handleClickUpload(false)">
              <template #icon><GiSvgIcon name="upload-file" /></template>
              <span>上传文件</span>
            </a-doption>
            <a-doption @click="handleClickUpload(true)">
              <template #icon><GiSvgIcon name="upload-folder" /></template>
              <span>上传文件夹</span>
            </a-doption>
          </template>
        </a-dropdown>

        <!-- <a-input-group>
          <a-select v-model="queryParam.type" style="width: 150px" placeholder="请选择">
            <a-option v-for="item in fileTypeList" :key="item.value" :value="item.value">
              <template #icon>
                <component :is="item.icon" size="18" color="#999"></component>
              </template>
              <template #default>{{ item.name }}</template>
            </a-option>
          </a-select>
        </a-input-group> -->
        <a-input-search placeholder="请输入关键词..." v-model="queryParam.keyWord" allow-clear> </a-input-search>
      </a-space>

      <!-- 右侧区域 -->
      <a-space wrap>
        <a-button
          v-if="isBatchMode"
          :disabled="!fileStore.selectedFileIds.length"
          type="primary"
          status="danger"
          @click="handleMulDelete"
          v-hasPerm="['file:btn:delete']"
          ><template #icon><icon-delete /></template
        ></a-button>
        <a-button type="primary" @click="isBatchMode = !isBatchMode">
          <template #icon><icon-select-all /></template>
          <template #default>{{ isBatchMode ? '取消批量' : '批量操作' }}</template>
        </a-button>
        <a-button-group>
          <a-tooltip content="刷新" position="bottom">
            <a-button @click="getListData(queryParam)">
              <template #icon>
                <icon-refresh />
              </template>
            </a-button>
          </a-tooltip>
          <a-tooltip content="传输列表" position="bottom">
            <a-button @click="loading = !loading">
              <template #icon>
                <icon-swap />
              </template>
            </a-button>
          </a-tooltip>
          <a-tooltip content="排序" position="bottom">
            <a-button>
              <template #icon>
                <icon-filter />
              </template>
            </a-button>
          </a-tooltip>
          <a-tooltip content="视图" position="bottom">
            <a-button @click="fileStore.changeViewMode">
              <template #icon>
                <icon-apps v-if="fileStore.viewMode === 'grid'" />
                <icon-list v-else />
              </template>
            </a-button>
          </a-tooltip>
        </a-button-group>
      </a-space>
    </a-row>

    <!-- 文件列表-宫格模式 -->
    <a-spin class="file-wrap" :loading="loading">
      <FileGrid
        v-show="fileList.length && fileStore.viewMode == 'grid'"
        :data="fileList"
        :isBatchMode="isBatchMode"
        :selectedFileIds="fileStore.selectedFileIds"
        @click="handleClickFile"
        @check="handleCheckFile"
        @right-menu-click="handleRightMenuClick"
      ></FileGrid>

      <!-- 文件列表-列表模式 -->
      <FileList
        v-show="fileList.length && fileStore.viewMode == 'list'"
        :data="fileList"
        :isBatchMode="isBatchMode"
        @click="handleClickFile"
        @check="handleCheckFile"
        @right-menu-click="handleRightMenuClick"
      ></FileList>

      <a-empty v-show="!fileList.length"></a-empty>
    </a-spin>
  </div>
</template>

<script setup lang="ts">
import { Message, Modal } from '@arco-design/web-vue'
import { imageTypeList, previewOfficeFileType } from '@/constant/file'
import { useFileStore } from '@/stores'
import { api as viewerApi } from 'v-viewer'
import 'viewerjs/dist/viewer.css'
import FileNavPath from './FileNavPath.vue'
import FileGrid from './FileGrid.vue'
import FileList from './FileList.vue'
import { getFileList, deleteFile } from '@/apis'
import type { FileItem, FileNav } from '@/apis'
import { getFileUrl } from '@/utils/common'
import {
  openFileMoveModal,
  openFileRenameModal,
  previewFileVideoModal,
  previewFileAudioModal,
  openFileUploadModal,
  previewOfficeFileDrawer
} from '../../components/index'

interface Props {
  fileType?: string
}
const props = withDefaults(defineProps<Props>(), {
  fileType: () => '' // 文件导航数据
})
const router = useRouter()

const fileStore = useFileStore()

const loading = ref(false)
// 文件列表数据
const fileList = ref<FileItem[]>([])
const fileNavs = ref<FileNav[]>([{ name: '根目录', path: '/', dirID: '0' }])
//文件上传目录默认为根目录
const uploadDirPath = ref('')
// 批量操作
const isBatchMode = ref(false)
const queryParam = reactive({ keyWord: '', type: '', isRootDir: true, dirId: '', status: '' })

const getListData = async (queryParam: object) => {
  try {
    loading.value = true
    isBatchMode.value = false
    const res = await getFileList(queryParam)
    fileList.value = res.data
  } catch (error) {
    return error
  } finally {
    loading.value = false
  }
}
//监听查询参数
/* watch([queryParam, () => props.fileType], (newValue, oldValue) => {
  //防止重复请求
  queryParam.type = newValue[1]
  getListData(queryParam)
}) */
watch(queryParam, () => {
  getListData(queryParam)
})
watch(
  () => props.fileType,
  (newValue) => {
    queryParam.type = newValue
  }
)
//加载文件列表数据
onMounted(() => {
  getListData(queryParam)
})

const handleClickFileNav = (fielNav: FileNav) => {
  let fieldataNavs = fileNavs.value
  uploadDirPath.value = fielNav.path
  if (fielNav.path == '/') {
    uploadDirPath.value = ''
    queryParam.isRootDir = true
  }
  const index = fieldataNavs.findIndex((i) => i.path === fielNav.path)
  const length = fieldataNavs.length
  //如果是当前目录则不处理
  if (fielNav.path === fieldataNavs[length - 1].path) return
  //不是当前目录更新导航
  if (index != -1) {
    fieldataNavs.splice(index + 1, fieldataNavs.length - index)
  }
  if (queryParam.isRootDir == false) queryParam.dirId = fielNav.dirID
}

//点击上传
const handleClickUpload = (isDirectory: boolean) => {
  openFileUploadModal(isDirectory, uploadDirPath.value).then((res) => {
    if (res as boolean) getListData(queryParam)
  })
}

// 点击文件
const handleClickFile = (item: FileItem) => {
  if (item.type === 'Other') Message.info('文件不支持预览')
  if (item.isDir) {
    if (item.filePath != '/') {
      queryParam.isRootDir = false
      queryParam.dirId = item.id
      uploadDirPath.value = item.filePath
      //添加文件导航
      if (fileNavs.value.findIndex((i) => i.path === item.filePath) == -1)
        fileNavs.value.push({
          name: item.name,
          path: item.filePath,
          dirID: item.id
        })
    }
  }
  item.src = getFileUrl(item.filePath) || ''
  if (imageTypeList.includes(item.extendName)) {
    if (item.filePath) {
      const imgList: any[] = fileList.value
        .filter((i) => imageTypeList.includes(i.extendName))
        .map((a) => {
          return {
            src: a.src || getFileUrl(a.filePath),
            title: a.name
          }
        })
      const index = imgList.findIndex((i) => i.src.includes(item.filePath))
      if (imgList.length) {
        viewerApi({
          options: {
            initialViewIndex: index,
            title: false
          },
          images: imgList
        })
      }
    }
  }
  if (item.extendName === 'mp4') {
    previewFileVideoModal(item)
  }
  if (item.extendName === 'mp3') {
    //函数式调用组件
    previewFileAudioModal(item)
  }
  //预览文档
  if (previewOfficeFileType.includes(item.extendName)) {
    previewOfficeFileDrawer(item)
  }
}
// 勾选文件
const handleCheckFile = (item: FileItem) => {
  fileStore.addSelectedFileItem(item)
}
// 鼠标右键
const handleRightMenuClick = (mode: string, fileInfo: FileItem) => {
  if (mode === 'delete') {
    Modal.warning({
      title: '提示',
      content: '是否删除该文件？',
      hideCancel: false,
      onOk: () => {
        const ids = []
        ids.push(fileInfo.id)
        deleteFileSvc(ids)
      }
    })
  }
  if (mode === 'rename') {
    openFileRenameModal(fileInfo).then((res) => {
      if (res as boolean) getListData(queryParam)
    })
  }
  if (mode === 'move') {
    openFileMoveModal(fileInfo)
  }
  if (mode === 'detail') {
    router.push({ path: '/file/detail' })
  }
}

// 批量删除
const handleMulDelete = () => {
  Modal.warning({
    title: '提示',
    content: '是否确认删除？',
    hideCancel: false,
    onOk: () => {
      deleteFileSvc(fileStore.selectedFileIds)
    }
  })
}
const deleteFileSvc = (ids: (string | number)[]) => {
  deleteFile(ids).then((res) => {
    if (res && res.success) {
      if (res.data > 0) {
        getListData(queryParam)
        Message.success('删除文件成功！')
      }
    } else {
      Message.error(`删除文件失败：${res.message}`)
    }
  })
}
</script>

<style lang="scss" scoped>
.file-main {
  height: 100%;
  background: var(--color-bg-1);
  border-radius: $radius-box;
  display: flex;
  flex-direction: column;
  overflow: hidden;
  .row-operate {
    border-bottom: 1px dashed var(--color-border-3);
    margin: 0 $padding;
  }
  .file-wrap {
    flex: 1;
    padding: 0 $padding $padding;
    box-sizing: border-box;
    overflow: hidden;
    display: flex;
    flex-direction: column;
  }
}
</style>
