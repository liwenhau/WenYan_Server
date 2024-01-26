import { ref } from 'vue'
import { getSystemDeptList } from '@/apis'
import type { DeptItem } from '@/apis'

/** 部门模块 */
export function useDept(options?: { onSuccess?: () => void }) {
  const loading = ref(false)
  const deptList = ref<DeptItem[]>([])

  const getDeptList = async (query?: any) => {
    try {
      loading.value = true
      const res = await getSystemDeptList(query)
      deptList.value = res.data
      options?.onSuccess && options.onSuccess()
    } catch (error) {
    } finally {
      loading.value = false
    }
  }
  return { deptList, getDeptList, loading }
}
