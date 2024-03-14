<template><div id="video"></div></template>

<script setup lang="ts">
import Player, { type IPlayerOptions } from 'xgplayer'
defineOptions({ name: 'WyPlayer' })
interface Props {
  url: string | Array<{ src: string; type?: string }>
  autoplay?: boolean
  width?: number | string
  height?: number | string
}
const props = withDefaults(defineProps<Props>(), {
  url: '',
  autoplay: false,
  width: '100%',
  height: '100%'
})
const defaultConfig = reactive<IPlayerOptions>({
  id: 'video',
  url: props.url,
  lang: 'zh-cn',
  closeVideoClick: true,
  videoInit: true,
  autoplay: props.autoplay,
  width: props.width,
  height: props.height
})
const player = ref()
onMounted(() => {
  nextTick(() => {
    player.value = new Player({ ...defaultConfig })
  })
})
/**销毁播放器 */
const onDestroy = () => {
  player && player.value.destroy()
}
/**暴露事件 */
defineExpose({ onDestroy })
</script>
