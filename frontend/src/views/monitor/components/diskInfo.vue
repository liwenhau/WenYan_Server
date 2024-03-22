<template>
  <a-card title="磁盘信息" :bordered="false" class="disk gi_card_title">
    <a-row class="list-row" :gutter="24" v-if="diskInfoData.length > 0">
      <a-col
        v-for="(disk, index) in diskInfoData"
        :key="index"
        class="list-col"
        :xs="24"
        :sm="12"
        :md="12"
        :lg="8"
        :xl="8"
        :xxl="8"
      >
        <a-card class="card-wrap" :bordered="false" hoverable>
          <a-result :status="null" title="">
            <template #icon>
              <a-progress size="large" status="normal" :percent="disk.availablePercent / 100" />
            </template>
          </a-result>
          <a-space align="start">
            <a-avatar :size="24" style="margin-right: 8px; background-color: #626aea">
              <icon-storage />
            </a-avatar>
            <a-card-meta>
              <template #title>
                <a-typography-text style="margin-right: 10px"> {{ `磁盘名：${disk.diskName}` }} </a-typography-text>
              </template>
              <template #avatar>
                <div :style="{ display: 'flex', alignItems: 'center', color: '#1D2129' }">
                  <a-descriptions size="large">
                    <a-descriptions-item label="类型">{{ disk.typeName }}</a-descriptions-item>
                    <a-descriptions-item label="总量">{{ disk.totalSize ?? 0 }}GB</a-descriptions-item>
                    <a-descriptions-item label="已使用">{{ disk.used ?? 0 }}GB</a-descriptions-item>
                    <a-descriptions-item label="可使用">{{ disk.availableFreeSpace ?? 0 }}GB</a-descriptions-item>
                  </a-descriptions>
                </div>
              </template>
            </a-card-meta>
          </a-space>
        </a-card></a-col
      >
    </a-row>
  </a-card>
</template>

<script lang="ts" setup>
import type { DiskInfo } from './type'
interface Props {
  data?: Array<DiskInfo>
}
const props = withDefaults(defineProps<Props>(), { data: () => [] })
let diskInfoData = ref<Array<DiskInfo>>([])
watch(props, (nweProps) => {
  diskInfoData.value = nweProps.data
})
</script>

<style lang="scss" scoped>
:deep(.arco-divider-horizontal) {
  margin: 16px 0;
}
.disk {
  transition: transform 0.3s;
  margin-top: var(--margin);
}
.card-wrap {
  height: 100%;
  transition: all 0.3s;
  border: 1px solid var(--color-neutral-3);
  border-radius: 4px;
  margin-bottom: 10px;
  &:hover {
    transform: translateY(-4px);
    box-shadow: 4px 4px 10px rgba(0, 0, 0, 0.1);
  }
}
</style>
