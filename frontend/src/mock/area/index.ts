import type { MockMethod } from 'vite-plugin-mock'
import { resultSuccess } from '@/mock/_utils'
import { data as areaData } from './data/area'
import { findTree } from 'xe-utils'

export default [
  {
    url: '/mock/area/list',
    method: 'get',
    timeout: 300,
    response: ({ query }) => {
      const { type, code } = query
      if (type === 'province') {
        const data = areaData.map((i) => ({ label: i.label, code: i.code }))
        return resultSuccess(data)
      }
      if (type === 'city' || type === 'area') {
        const parent = findTree(areaData, (i) => i.code === code)
        const data = parent?.item?.children?.map((i) => ({ label: i.label, code: i.code }))
        return resultSuccess(data)
      }
    }
  }
] as MockMethod[]
