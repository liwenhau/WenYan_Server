<template>
  <a-card :bordered="false" class="h-full gi_card_title">
    <template #title>
      {{ props.title }}
    </template>
    <VCharts :option="option" autoresize :style="{ height: '300px' }"></VCharts>
  </a-card>
</template>

<script lang="ts" setup>
import VCharts from 'vue-echarts'
import { graphic } from 'echarts'
import { useChart } from '@/hooks'
interface Props {
  title?: String
  value?: number | '-'
}
const props = withDefaults(defineProps<Props>(), {})
const { option } = useChart(() => {
  return {
    series: [
      {
        type: 'gauge',
        startAngle: 225,
        endAngle: -45,
        min: 0,
        max: 100,
        pointer: {
          show: false
        },
        progress: {
          show: true,
          overlap: false,
          roundCap: false,
          clip: false
        },
        axisLine: {
          lineStyle: {
            width: 15
          }
        },
        splitLine: {
          show: false
        },
        axisTick: {
          show: false
        },
        axisLabel: {
          show: false
        },
        data: [{ value: props.value, name: '' }],
        title: {
          fontSize: 15
        },
        itemStyle: {
          opacity: 1,
          color: new graphic.LinearGradient(0, 0, 1, 0, [
            {
              offset: 0,
              color: '#5CD1A7'
            },
            {
              offset: 0.35,
              color: '#FFE469'
            },

            {
              offset: 0.65,
              color: '#FFAC1E'
            },
            {
              offset: 1,
              color: '#FA5E5E'
            }
          ])
        },
        detail: {
          fontSize: 20,
          fontWeight: 'bold',
          color: '#FFAC1E',
          formatter: '{value}%',
          offsetCenter: ['0%', '50%']
        }
      }
    ]
  }
})
</script>

<style lang="scss" scoped></style>
