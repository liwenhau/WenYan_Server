<template>
  <a-drawer :visible="visible" :width="drawerWidth" @cancel="handleCancel" unmount-on-close>
    <vue-office-docx
      :src="props.fileInfo.src"
      :key="docxKey"
      @rendered="handleRendered"
      @error="handleError"
      v-if="extendName == 'docx'"
    ></vue-office-docx>
    <vue-office-excel
      :src="props.fileInfo.src"
      :key="excelKey"
      @rendered="handleRendered"
      @error="handleError"
      v-else-if="extendName == 'xlsx'"
    ></vue-office-excel>
    <vue-office-pdf
      :src="props.fileInfo.src"
      :key="pdfKey"
      @rendered="handleRendered"
      @error="handleError"
      v-else
    ></vue-office-pdf>
    <template #title>
      <a-tag color="blue" size="large">
        <template #icon>
          <GiSvgIcon size="30" :name="fileIconName"></GiSvgIcon>
        </template>
        {{ props.fileInfo.name }}.{{ props.fileInfo.extendName }}
      </a-tag>
    </template>
    <template #footer>
      <a-button type="primary" @click="handleFullscreen" v-if="!isPhone()">
        <template #icon>
          <icon-fullscreen />
        </template>
      </a-button>
      <a-button type="primary" status="warning" @click="handleFullscreenExit" v-if="!isPhone()">
        <template #icon>
          <icon-fullscreen-exit />
        </template>
      </a-button>
      <a-button type="primary" status="danger" @click="handleCancel">
        <template #icon>
          <icon-close />
        </template>
      </a-button>
    </template>
  </a-drawer>
</template>

<script setup lang="ts">
import VueOfficeDocx from '@vue-office/docx'
import VueOfficeExcel from '@vue-office/excel'
import VueOfficePdf from '@vue-office/pdf'
import '@vue-office/docx/lib/index.css'
import '@vue-office/excel/lib/index.css'
import { useWindowSize } from '@vueuse/core'
import type { FileItem } from '@/apis'
import { isPhone } from '@/utils/common'
import { Message } from '@arco-design/web-vue'
const visible = ref(false)
interface Props {
  fileInfo: FileItem
  onClose: Function
}
const props = withDefaults(defineProps<Props>(), {})
const { width: windowWidth } = useWindowSize()
const drawerWidth = ref(0)
drawerWidth.value = isPhone() ? windowWidth.value : windowWidth.value / 2
//重新渲染组件
const docxKey = ref(0)
const pdfKey = ref(0)
const excelKey = ref(0)
//是否全屏
const isFullscreen = ref(false)
const extendName = ref('')
const fileIconName = ref('')

onMounted(() => {
  visible.value = true
  extendName.value = props.fileInfo.extendName
  fileIconName.value = extendName.value == 'docx' ? 'file-wps' : extendName.value === 'xlsx' ? 'file-excel' : 'file-pdf'
})
/** 全屏 */
const handleFullscreen = () => {
  if (isFullscreen.value) return
  drawerWidth.value = windowWidth.value
  renderingComponents()
  isFullscreen.value = true
}
/** 退出全屏 */
const handleFullscreenExit = () => {
  if (!isFullscreen.value) return
  drawerWidth.value = windowWidth.value / 2
  renderingComponents()
  isFullscreen.value = false
}
const handleCancel = () => {
  visible.value = false
}
//渲染组件
const renderingComponents = () => {
  extendName.value === 'docx'
    ? (docxKey.value += 1)
    : extendName.value === 'xlsx'
    ? (excelKey.value += 1)
    : (pdfKey.value += 1)
}
const handleRendered = () => {
  // Message.success('文件渲染完成')
}
const handleError = (errorInfo: any) => {
  Message.error('文件渲染失败')
}
</script>
<style lang="scss" scoped>
.loading {
  top: 50%;
  left: 50%;
}
</style>
