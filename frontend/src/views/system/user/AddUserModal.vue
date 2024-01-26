<template>
  <a-modal
    v-model:visible="visible"
    :title="title"
    width="90%"
    :mask-closable="false"
    :modal-style="{ maxWidth: '600px' }"
    @before-ok="save"
    @close="close"
  >
    <a-form ref="FormRef" :model="form" :rules="rules" size="medium" auto-label-width>
      <a-row>
        <a-col :xs="24" :sm="24" :md="12" :lg="12" :xl="12" :xxl="12">
          <a-form-item label="用户名" field="userName">
            <a-input v-model="form.userName" placeholder="请输入用户名" :disabled="form.disabled"></a-input>
          </a-form-item>
        </a-col>
        <a-col :xs="24" :sm="24" :md="12" :lg="12" :xl="12" :xxl="12">
          <a-form-item label="昵称" field="name">
            <a-input v-model="form.name" placeholder="请输入昵称"></a-input>
          </a-form-item>
        </a-col>
      </a-row>

      <a-row>
        <a-col :xs="24" :sm="24" :md="12" :lg="12" :xl="12" :xxl="12">
          <a-form-item label="用户编码" field="code">
            <a-input v-model="form.code" placeholder="请输入用户编码"></a-input>
          </a-form-item>
        </a-col>
        <a-col :xs="24" :sm="24" :md="12" :lg="12" :xl="12" :xxl="12">
          <a-form-item label="邮箱" field="email">
            <a-input v-model="form.email" placeholder="请输入邮箱"></a-input>
          </a-form-item>
        </a-col>
      </a-row>

      <a-form-item label="性别" field="sex">
        <a-radio-group v-model="form.sex">
          <a-radio :value="'Boy'">男</a-radio>
          <a-radio :value="'Girl'">女</a-radio>
        </a-radio-group>
      </a-form-item>

      <a-form-item label="所属部门" field="orgId">
        <a-tree-select
          v-model="form.orgId"
          :data="deptList"
          :fieldNames="{
            key: 'id',
            title: 'name'
          }"
          placeholder="请选择所属部门"
          allow-clear
          allow-search
          :disabled="form.disabled"
        />
      </a-form-item>

      <a-form-item label="角色" field="roleIds" :disabled="form.disabled">
        <a-select
          v-model="form.roleIds"
          :options="roleOptions"
          placeholder="请选择所属角色"
          multiple
          allow-clear
          :allow-search="{ retainInputValue: true }"
        />
      </a-form-item>

      <a-form-item label="个性签名" field="sign">
        <a-textarea
          v-model="form.sign"
          :max-length="100"
          placeholder="个性签名"
          :auto-size="{ minRows: 3 }"
          show-word-limit
        />
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
import { useDept, useRole } from '@/hooks/app'
import * as Regexp from '@/utils/regexp'
import { getSystemUserDetail, saveSystemUser } from '@/apis'
import { Message, type FormInstance } from '@arco-design/web-vue'

const { roleList, getRoleList } = useRole()
getRoleList()
const roleOptions = computed(() => roleList.value.map((i) => ({ label: i.name, value: i.id })))

const FormRef = ref<FormInstance>()
const userId = ref('')
const isEdit = computed(() => !!userId.value)
const title = computed(() => (isEdit.value ? '编辑用户' : '新增用户'))
const visible = ref(false)

//初始化
const initialForm = () => ({
  id: '',
  userName: '', // 用户名
  name: '', // 昵称
  code: '',
  avatar: '',
  sex: 'Boy' as Gender, // 性别 1男 2女
  email: '', // 邮箱
  orgId: '', // 部门
  roleIds: [], // 角色(可能多个)
  remark: '', // 描述
  sign: '',
  status: 'Enable' as Status, // 状态 0禁用 1启用(正常)
  disabled: false // 如果 type===1 这为 true, 主要作用是列表复选框禁用状态
})
const form = reactive(initialForm())

const rules = {
  userName: [
    { required: true, message: '请输入用户名' },
    { min: 2, max: 10, message: '长度在 2 - 10个字符' }
  ],
  name: [
    { required: true, message: '请输入昵称' },
    { min: 2, max: 15, message: '长度在 2 - 15个字符' }
  ],
  code: [
    { required: true, message: '请输入用户编码' },
    { min: 2, max: 10, message: '长度在 2 - 10个字符' }
  ],
  email: [{ match: Regexp.Email, message: '邮箱格式不正确' }],
  orgId: [{ required: true, message: '请选择所属部门' }],
  status: [{ required: true, message: '请选择状态' }],
  sign: [{ min: 2, max: 100, message: '长度在 2 - 100个字符' }]
}

const { deptList, getDeptList } = useDept()
getDeptList({})

const add = () => {
  Object.assign(form, initialForm())
  userId.value = ''
  visible.value = true
}

const edit = async (id: string) => {
  visible.value = true
  userId.value = id
  const res = await getSystemUserDetail({ id })
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
    const res = await saveSystemUser(form)
    if (res.success) {
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
