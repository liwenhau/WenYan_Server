<template>
  <a-modal
    v-model:visible="visible"
    :title="title"
    width="90%"
    :modal-style="{ maxWidth: '520px' }"
    :mask-closable="false"
    @before-ok="save"
    @close="close"
  >
    <a-form ref="FormRef" :model="form" :rules="rules" size="medium" auto-label-width>
      <a-form-item label="上级部门" field="parentId">
        <a-tree-select
          v-model="form.parentId"
          allow-clear
          :data="deptList"
          placeholder="请选择"
          :fieldNames="{
            key: 'id',
            title: 'name',
            children: 'children'
          }"
        ></a-tree-select>
      </a-form-item>
      <a-form-item label="部门名称" field="name">
        <a-input v-model="form.name" placeholder="请输入部门名称" allow-clear></a-input>
      </a-form-item>
      <a-form-item label="部门编码" field="code">
        <a-input v-model="form.code" placeholder="请输入部门编码" allow-clear></a-input>
      </a-form-item>
      <a-form-item label="排序" field="seq">
        <a-input-number v-model="form.seq" style="width: 120px" />
      </a-form-item>
      <a-form-item label="描述" field="remark">
        <a-textarea
          v-model="form.remark"
          :max-length="200"
          placeholder="请填写描述"
          :auto-size="{ minRows: 3 }"
          show-word-limit
        />
      </a-form-item>
      <a-form-item label="状态" field="status">
        <a-switch
          type="round"
          v-model="form.status"
          :checked-value="'Enable'"
          :unchecked-value="'Disable'"
          checked-text="正常"
          unchecked-text="禁用"
        />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { useDept } from '@/hooks/app'
import { getSystemDeptDetil, saveSystemDept } from '@/apis'
import { Message, type FormInstance } from '@arco-design/web-vue'

const FormRef = ref<FormInstance>()
const deptId = ref('')
const visible = ref(false)
const isEdit = computed(() => !!deptId.value)
const title = computed(() => (isEdit.value ? '编辑部门' : '新增部门'))
const { deptList, getDeptList } = useDept()
getDeptList()

//初始化
const initialForm = () => ({
  id: '',
  parentId: '',
  name: '',
  code: '',
  seq: 0,
  status: 'Enable',
  remark: ''
})
const form = reactive(initialForm())

const rules = {
  name: [
    { required: true, message: '请输入部门名称' },
    { min: 3, max: 15, message: '长度在 3 - 15个字符' }
  ],
  code: [
    { required: true, message: '请输入部门编码' },
    { min: 3, max: 10, message: '长度在 3 - 10个字符' }
  ]
}

const add = (orgId?: string) => {
  Object.assign(form, initialForm())
  if (orgId) {
    form.parentId = orgId
  }
  deptId.value = ''
  visible.value = true
}

const edit = async (id: string) => {
  if (!deptList.value.length) {
    await getDeptList()
  }
  deptId.value = id
  const res = await getSystemDeptDetil({ id })
  Object.assign(form, res.data)
  visible.value = true
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
    const res = await saveSystemDept(form)
    if (res.data) {
      Message.success('保存成功！')
      getDeptList()
      emit('refresh')
      return true
    } else {
      return false
    }
  } catch (error) {
    return false
  }
}

//#region 数据加载

//#endregion
//#region 事件
//#endregion
</script>
