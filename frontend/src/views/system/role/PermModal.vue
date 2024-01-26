<template>
  <a-modal v-model:visible="visible" :title="title" :fullscreen="isPhone()" :mask-closable="true" @ok="save">
    <a-tree
      ref="TreeRef"
      size="small"
      checkable
      :check-strictly="true"
      v-model:checked-keys="menuIds"
      :data="treeData"
      :fieldNames="{ key: 'id' }"
    ></a-tree>
  </a-modal>
</template>

<script lang="ts" setup>
import { getSystemMenuOptions, getSystemRoleMenuIds, saveSystemRolePermission, type MenuOptionsItem } from '@/apis'
import type { TreeInstance } from '@arco-design/web-vue'
import { Message } from '@arco-design/web-vue'
import { isPhone } from '@/utils/common'

const TreeRef = ref<TreeInstance>()
const treeData = ref<MenuOptionsItem[]>([])
const getTreeData = async () => {
  try {
    const res = await getSystemMenuOptions()
    treeData.value = res.data
    nextTick(() => {
      TreeRef.value?.expandAll()
    })
  } finally {
  }
}
getTreeData()

const visible = ref(false)
const menuIds = ref<string[]>([])
const roleId = ref('')
const open = (data: { id: string; title: string }) => {
  menuIds.value = []
  roleId.value = data.id
  getSystemRoleMenuIds({ roleId: data.id }).then((res) => {
    menuIds.value = res.data
  })
  visible.value = true
}
defineExpose({ open })

const title = computed(() => `分配权限`)

const save = async () => {
  try {
    const res = await saveSystemRolePermission(roleId.value, menuIds.value)
    if (res.success) {
      Message.success('保存成功！')
      visible.value = false
    } else {
      Message.error(res.message)
    }
    return true
  } catch (error) {
    return false
  }

  Message.success('模拟保存成功')
}
</script>

<style lang="scss" scoped></style>
