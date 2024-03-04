<template>
  <a-modal width="auto" draggable :footer="false" v-model:visible="visible" @close="close">
    <template #title>
      <a-tag color="magenta">
        <template #icon>
          <icon-xigua-color />
        </template>
        {{ props.fileInfo?.name }}.{{ props.fileInfo?.extendName }}
      </a-tag>
    </template>
    <div id="videoId"></div>
  </a-modal>
</template>

<script setup lang="ts">
import Player from 'xgplayer'
import type { FileItem } from '@/apis'

interface Props {
  fileInfo: FileItem
  onClose: Function
}
const props = withDefaults(defineProps<Props>(), {})

const visible = ref(false)

onMounted(() => {
  visible.value = true
  nextTick(() => {
    new Player({
      id: 'videoId',
      url: props.fileInfo?.src || '',
      lang: 'zh-cn',
      autoplay: true,
      closeVideoClick: true,
      videoInit: true
    })
  })
})

const close = () => {
  visible.value = false
  props.onClose && props.onClose()
}
</script>
