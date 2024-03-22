<template>
  <span id="typed" class="text" :style="{ color: textColor }"></span>
</template>
<script setup lang="ts">
import Typed, { type TypedOptions } from 'typed.js'
defineOptions({ name: 'WyTyped' })
interface Props {
  data?: Array<string>
  speed?: number
  loop?: boolean
  cursorChar?: string
  textColor?: string
}
const props = withDefaults(defineProps<Props>(), {
  data: () => [],
  speed: 1000,
  loop: false,
  cursorChar: '|',
  textColor: ''
})
const defaultConfig = reactive<TypedOptions>({
  typeSpeed: props.speed,
  strings: props.data,
  loop: props.loop,
  cursorChar: props.cursorChar,
  showCursor: false,
  fadeOut: true,
  fadeOutClass: 'typed-fade-out'
})
const typed = ref()
const textColor = ref('')
onMounted(() => {
  textColor.value = props.textColor
  typed.value = new Typed('#typed', { ...defaultConfig })
})
</script>
<style lang="scss" scoped>
.text {
  margin: 0 auto;
}
</style>
