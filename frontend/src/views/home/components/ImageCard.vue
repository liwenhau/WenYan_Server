<template>
  <div class="image-card">
    <WyLottie :data="home" :height="500"></WyLottie>
  </div>
</template>
<script setup lang="ts">
import WyLottie from '@/components/WyLottie/index.vue'
import { home } from '@/assets/lotties'
</script>
<style lang="scss" scoped>
.image-card {
  padding: 20px;
  box-sizing: border-box;
  background: var(--color-bg-1);
  border-radius: 6px;
  box-shadow: 0 4px 10px rgb(var(--gray-2));
  img {
    width: 100%;
  }
}
</style>
