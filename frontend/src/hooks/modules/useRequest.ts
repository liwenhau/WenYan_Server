import { ref, type UnwrapRef } from 'vue'
import type { AxiosResponse } from 'axios'
import useLoading from './useLoading'

export default function useRequest<T>(
  api: () => Promise<AxiosResponse<ApiRes<T>>>,
  defaultValue = [] as unknown as T,
  isLoading = true
) {
  const { loading, setLoading } = useLoading(isLoading)
  const response = ref<T>(defaultValue)
  api()
    .then((res) => {
      response.value = res.data as unknown as UnwrapRef<T>
    })
    .finally(() => {
      setLoading(false)
    })
  return { loading, response }
}
