<template>
  <a-modal
    width="90%"
    modal-animation-name="el-fade"
    draggable
    :modal-style="{ maxWidth: '700px' }"
    :footer="false"
    v-model:visible="visible"
    @close="close"
  >
    <template #title>
      <a-tag color="magenta">
        <template #icon>
          <icon-xigua-color />
        </template>
        {{ props.fileInfo?.name }}.{{ props.fileInfo?.extendName }}
      </a-tag>
    </template>
    <WyPlayer ref="WyPlayerRef" :url="props.fileInfo?.src || ''" :autoplay="true" :height="height"></WyPlayer>
  </a-modal>
</template>

<script setup lang="ts">
import Player from 'xgplayer'
import type { FileItem } from '@/apis'
import { useWindowSize } from '@vueuse/core'
import { isPhone } from '@/utils/common'
import WyPlayer from '@/components/WyPlayer/index.vue'
interface Props {
  fileInfo: FileItem
  onClose: Function
}
const props = withDefaults(defineProps<Props>(), {})
const WyPlayerRef = ref<InstanceType<typeof WyPlayer>>()
const visible = ref(false)
const height = ref()
const { height: windowHeight } = useWindowSize()
onMounted(() => {
  visible.value = true
  height.value = isPhone() ? '100%' : windowHeight.value / 3
})

const close = () => {
  visible.value = false
  WyPlayerRef.value?.onDestroy()
  props.onClose && props.onClose()
}
</script>
