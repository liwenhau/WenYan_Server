<template>
  <a-modal
    v-model:visible="visible"
    :title="title"
    width="90%"
    :mask-closable="false"
    :modal-style="{ maxWidth: '520px' }"
    @before-ok="save"
    @close="close"
  >
    <a-form ref="FormRef" :model="form" :rules="rules" size="medium" auto-label-width>
      <a-form-item label="角色名称" field="name">
        <a-input placeholder="请输入角色名称" v-model="form.name"> </a-input>
      </a-form-item>
      <a-form-item label="角色编号" field="code">
        <a-input placeholder="请输入角色编号" v-model="form.code"> </a-input>
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { getSystemRoleDetail, saveSystemRole } from '@/apis'
import { Message, type FormInstance } from '@arco-design/web-vue'

const FormRef = ref<FormInstance>()
const roleId = ref('')
const isEdit = computed(() => !!roleId.value)
const title = computed(() => (isEdit.value ? '编辑角色' : '新增角色'))
const visible = ref(false)

const form = reactive({
  id: '',
  name: '',
  code: ''
})

const rules = {
  name: [
    { required: true, message: '请输入角色名称' },
    { min: 3, max: 10, message: '长度在 3 - 10个字符' }
  ],
  code: [{ required: true, message: '请输入角色编号' }]
}
//初始化
const init = () => {
  Object.assign(form, {
    id: '',
    name: '',
    code: ''
  })
  visible.value = true
}

const add = () => {
  init()
  roleId.value = ''
}

const edit = async (id: string) => {
  init()
  roleId.value = id
  const res = await getSystemRoleDetail({ id })
  Object.assign(form, res.data)
}

const close = () => {
  FormRef.value?.resetFields()
}

defineExpose({ add, edit })

const emit = defineEmits(['refresh'])
const save = async () => {
  try {
    const info = await FormRef.value?.validate()
    if (info) return false
    const res = await saveSystemRole(form)
    if (res.data) {
      Message.success('保存成功')
      emit('refresh')
      return true
    } else {
      return false
    }
  } catch (error) {
    return false
  }
}
</script>
