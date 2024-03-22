<template>
  <div class="gi_page">
    <a-row :gutter="[14, 14]" align="stretch">
      <a-col :xs="24" :sm="24" :md="12" :lg="12" :xl="12" :xxl="12">
        <cpuChart :title="'CPU使用率'" :value="cpuUseRate"></cpuChart>
      </a-col>
      <a-col :xs="24" :sm="24" :md="12" :lg="12" :xl="12" :xxl="12">
        <cpuChart :title="'内存使用率'" :value="ramUseRate"></cpuChart>
      </a-col>
    </a-row>
    <a-row :gutter="[14, 14]" align="stretch"> </a-row>
    <diskInfo :data="diskData"></diskInfo>
  </div>
</template>
<script lang="ts" setup>
import cpuChart from './components/cpuChart.vue'
import diskInfo from './components/diskInfo.vue'
import * as signalR from '@microsoft/signalr'
import type { DiskInfo } from './components/type'
let connection = ref()
const cpuUseRate = ref(0)
const ramUseRate = ref(0)
let diskData = ref<Array<DiskInfo>>([])
connection.value = new signalR.HubConnectionBuilder()
  .withUrl(`${import.meta.env.VITE_API_BASE_URL}chart`)
  .withAutomaticReconnect()
  .build()
connection.value.start().then(() => {
  console.log('连接成功')
})
connection.value.on('GetDiskInfos', (data: Array<DiskInfo>) => {
  diskData.value = data
})
connection.value.on('GetMemoryMetrics', (data: any) => {
  if (data) {
    cpuUseRate.value = data.cpuRate
    ramUseRate.value = data.ramRate
  }
})
onMounted(() => {})
</script>
