<template>
  <div class="file-path">
    <a-breadcrumb>
      <a-breadcrumb-item v-for="item in props.data" :key="item.dirID" @click="onClickItem(item)"
        ><span class="path-item">{{ item.name }}</span></a-breadcrumb-item
      >
    </a-breadcrumb>
  </div>
</template>

<script setup lang="ts">
import type { FileNav } from '@/apis'
interface Props {
  data?: FileNav[]
}
const props = withDefaults(defineProps<Props>(), {
  data: () => [] // 文件导航数据
})
const emit = defineEmits(['click'])
const onClickItem = (fileNav: FileNav) => {
  emit('click', fileNav)
}
</script>

<style lang="scss" scoped>
.file-path {
  padding: 10px $padding;
  padding-bottom: 12px;
  display: flex;
  align-items: center;
  cursor: pointer;
  .path-item {
    font-size: 12px;
    &:hover {
      color: rgba(var(--primary-6));
    }
  }
}
</style>
