<template>
  <img v-if="isImage" class="img" :src="getFileUrl(props.data.filePath) || ''" />
  <GiSvgIcon v-else size="100%" :name="getFileImg"></GiSvgIcon>
</template>

<script setup lang="ts">
import { fileExtendNameIconMap, imageTypeList } from '@/constant/file'
import { getFileUrl } from '@/utils/common'
import type { FileItem } from '@/apis'

interface Props {
  data: FileItem
}

const props = withDefaults(defineProps<Props>(), {})

// 是否是图片类型文件
const isImage = computed(() => {
  return imageTypeList.includes(props.data.extendName.toLowerCase())
})

// 获取文件图标，如果是图片这显示图片
const getFileImg = computed<string>(() => {
  if (props.data?.isDir) {
    return fileExtendNameIconMap['dir'] || ''
  } else if (imageTypeList.includes(props.data.extendName.toLowerCase())) {
    return props.data.src || ''
  } else if (!Object.keys(fileExtendNameIconMap).includes(props.data.extendName.toLowerCase())) {
    return fileExtendNameIconMap['other'] || ''
  } else {
    return fileExtendNameIconMap[props.data.extendName.toLowerCase()] || ''
  }
})
</script>

<style lang="scss" scoped>
img {
  width: 100%;
  height: 100%;
  object-fit: cover;
}
</style>
