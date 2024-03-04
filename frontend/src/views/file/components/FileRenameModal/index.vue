<template>
  <a-modal
    title="重命名"
    width="90%"
    v-model:visible="visible"
    modal-animation-name="el-fade"
    :modal-style="{ maxWidth: '500px' }"
    @close="cancel"
    @before-ok="save"
  >
    <a-row justify="center" align="center">
      <a-form ref="FormRef" :model="form" auto-label-width :style="{ width: '80%' }">
        <a-form-item field="name" label="文件名称" :rules="[{ required: true, message: '请输入文件名称' }]">
          <a-input v-model="form.name" placeholder="请输入" allow-clear />
        </a-form-item>
      </a-form>
    </a-row>
  </a-modal>
</template>

<script setup lang="ts">
import type { FileItem } from '@/apis'
import { Message, type FormInstance, type Modal } from '@arco-design/web-vue'
import { fileReName } from '@/apis'

interface Props {
  fileInfo: FileItem
  onClose: Function
  onRefresh: Function
}
const props = withDefaults(defineProps<Props>(), {})

const visible = ref(false)
//判断是否需要更新数据
const refresh = ref(false)
type Form = { name: string; id: string }
const form: Form = reactive({
  name: '',
  id: ''
})

onMounted(() => {
  form.name = props.fileInfo?.name || ''
  form.id = props.fileInfo.id
  visible.value = true
})

const cancel = () => {
  visible.value = false
  props.onClose && props.onClose()
}

const saveApi = (): Promise<boolean> => {
  return new Promise((resolve) =>
    fileReName(form)
      .then((res) => {
        if (res.data) {
          Message.success('文件重命名成功！')
          refresh.value = res.data as boolean
          resolve(refresh.value)
        } else {
          Message.error(`文件重命名失败：${res.message}`)
          resolve(false)
        }
      })
      .finally(() => {
        cancel()
        //通知刷新
        props.onRefresh && props.onRefresh(refresh.value)
      })
  )
}

const FormRef = ref<FormInstance | null>(null)
const save: InstanceType<typeof Modal>['onBeforeOk'] = async () => {
  const flag = await FormRef.value?.validate()
  if (flag) return false
  return await saveApi()
}
</script>

<style lang="scss" scoped>
.label {
  color: $color-text-2;
}
:deep(.arco-form-item) {
  margin-bottom: 0;
}
:deep(.arco-form-item-label-col > label) {
  white-space: nowrap;
}
</style>
