<template>
  <a-modal
    v-model:visible="visible"
    title="修改密码"
    width="90%"
    :mask-closable="false"
    :modal-style="{ maxWidth: '400px' }"
    @before-ok="save"
    @close="close"
  >
    <a-form ref="FormRef" :model="form" :rules="rules" size="medium" auto-label-width>
      <a-form-item label="原密码" field="oldPassword">
        <a-input-password v-model="form.oldPassword" autocomplete="off" placeholder="原密码"></a-input-password>
      </a-form-item>
      <a-form-item label="新密码" field="newPassword" tooltip="密码为8-18位数字/字符/符号的组合">
        <a-input-password v-model="form.newPassword" autocomplete="off" placeholder="新密码"></a-input-password>
      </a-form-item>
      <a-form-item label="确认密码" field="cnfPassword" tooltip="再次输入您的新密码">
        <a-input-password v-model="form.cnfPassword" autocomplete="off" placeholder="确认密码"></a-input-password>
      </a-form-item>
    </a-form>
  </a-modal>
</template>
<script setup lang="ts">
import { Message, type FormInstance } from '@arco-design/web-vue'
import * as Regexp from '@/utils/regexp'
import { changePassword } from '@/apis'
import { useUserStore } from '@/stores'
import { Md5 } from 'ts-md5'
const FormRef = ref<FormInstance>()
const visible = ref(false)
const userStore = useUserStore()
const router = useRouter()
//初始化
const initialForm = () => ({
  id: '',
  oldPassword: '',
  newPassword: '',
  cnfPassword: ''
})
const form = reactive(initialForm())
const rules = {
  oldPassword: [
    { required: true, message: '请输入原密码' },
    { match: Regexp.Password, message: '输入密码格式不正确' }
  ],
  newPassword: [
    { required: true, message: '请输入新密码' },
    { match: Regexp.Password, message: '输入密码格式不正确' }
  ],
  cnfPassword: [
    { required: true, message: '请确认新密码' },
    { match: Regexp.Password, message: '输入密码格式不正确' },
    {
      validator: (value: any, cb: any) => {
        if (value !== form.newPassword) {
          cb('两次密码不一致，请重新输入')
        } else {
          cb()
        }
      }
    }
  ]
}
const close = () => {
  FormRef.value?.resetFields()
}
const save = async () => {
  const info = await FormRef.value?.validate()
  if (info) return false
  form.id = userStore.userInfo.id
  form.newPassword = Md5.hashStr(form.newPassword)
  form.oldPassword = Md5.hashStr(form.oldPassword)
  const res = await changePassword(form)
  if (res.success) {
    Message.success('密码修改成功，请重新登录')
    userStore.logout()
    router.replace('/login')
  } else {
    Message.error(res.message)
  }
  visible.value = false
}
const open = () => {
  visible.value = true
  Object.assign(form, initialForm())
}
defineExpose({ open })
</script>
