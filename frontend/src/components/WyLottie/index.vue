<template>
  <Vue3Lottie
    ref="LottieRef"
    @click.stop="handleClick"
    :animationData="data"
    :widtg="width"
    :height="height"
    :speed="speed"
    :autoPlay="autoPlay"
    :playOnHover="playOnHover"
    :pauseOnHover="pauseOnHover"
  />
</template>

<script lang="ts" setup>
import { Vue3Lottie } from 'vue3-lottie'
interface Props {
  data?: any
  width?: string | number
  height?: string | number
  speed?: number
  autoPlay?: boolean
  playOnHover?: boolean
  pauseOnHover?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  data: {},
  width: '100%',
  height: '100%',
  speed: 1,
  autoPlay: true,
  pauseOnHover: false,
  playOnHover: false
})
let isPause = false
onMounted(() => {
  isPause = props.autoPlay
})
const LottieRef = ref<any>()
const handleClick = () => {
  if (isPause) {
    LottieRef.value.stop()
    isPause = false
  } else {
    LottieRef.value.play()
    isPause = true
  }
}
</script>

<style lang="scss" scoped></style>
