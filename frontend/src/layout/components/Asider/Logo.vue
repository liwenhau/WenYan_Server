<template>
  <section class="system-logo" :class="{ collapsed: props.collapsed }" @click="toHome">
    <WyLottie :data="logo_dark" class="logo"></WyLottie>
    <span class="system-name">WenYan Admin</span>
  </section>
</template>

<script setup lang="ts">
import WyLottie from '@/components/WyLottie/index.vue'
import { logo, logo_dark } from '@/assets/lotties'
interface Props {
  collapsed?: boolean
}

const props = withDefaults(defineProps<Props>(), {
  collapsed: false
})

const router = useRouter()

// 跳转首页
const toHome = () => {
  router.push('/')
}
</script>

<style lang="scss" scoped>
.system-logo {
  height: 56px;
  padding: 0 12px;
  font-size: 20px;
  line-height: 1;
  color: var(--color-text-1);
  display: flex;
  align-items: center;
  flex-shrink: 0;
  cursor: pointer;
  user-select: none;
  background-color: var(--color-bg-1);
  &.collapsed {
    padding: 0;
    display: flex;
    justify-content: center;
    align-items: center;
    // .logo {
    //   width: 24px;
    //   height: 24px;
    // }
    .system-name {
      display: none;
    }
  }
  .logo {
    width: 40px;
    height: 40px;
    border-radius: 6px;
    transition: all 0.2s;
    overflow: hidden;
    flex-shrink: 0;
    margin: 0px 0px 0px 0px;
  }
  .system-name {
    padding-left: 10px;
    white-space: nowrap;
    transition: color 0.3s;
    &:hover {
      color: $color-theme !important;
      cursor: pointer;
    }
  }
}
</style>
