<template>
  <div class="gi_page role-manage">
    <a-card title="角色管理">
      <a-space wrap>
        <a-input v-model="queryParam.keyword" placeholder="输入角色名搜索" allow-clear style="width: 250px">
          <template #prefix><icon-search /></template>
        </a-input>
        <a-button type="primary" @click="search" v-hasPerm="['role:btn:query']">
          <template #icon><icon-search /></template>
          <span>查询</span>
        </a-button>
        <a-button @click="reset">
          <template #icon><icon-refresh /></template>
          <span>重置</span>
        </a-button>
      </a-space>

      <a-row>
        <a-space wrap>
          <a-button type="primary" v-hasPerm="['role:btn:add']" @click="onAdd">
            <template #icon><icon-plus /></template>
            <span>新增</span>
          </a-button>
          <a-button type="primary" status="danger" v-hasPerm="['role:btn:delete']" @click="onMulDelete">
            <template #icon><icon-delete /></template>
            <span>删除</span>
          </a-button>
        </a-space>
      </a-row>

      <a-table
        style="margin-top: 8px"
        row-key="id"
        :columns="columns"
        :data="roleList"
        :bordered="{ cell: true }"
        :loading="loading"
        :pagination="pagination"
        :scroll="{ x: '100%', y: '100%', minWidth: 900 }"
        :row-selection="{ type: 'checkbox', showCheckedAll: false, selectedRowKeys: selectRowKeys }"
        @select="select"
      >
        <template #rowIndex="{ rowIndex }">
          <a-tag color="blue">{{ rowIndex + 1 }}</a-tag>
        </template>
        <template #action="{ record }">
          <a-space>
            <a-button
              type="primary"
              size="mini"
              :disabled="record.disabled"
              v-hasPerm="['role:btn:edit']"
              @click="onEdit(record)"
            >
              <template #icon><icon-edit /></template>
              <span>编辑</span>
            </a-button>
            <a-button
              type="primary"
              status="success"
              size="mini"
              :disabled="record.disabled"
              v-hasPerm="['role:btn:permission']"
              @click="onPerm(record)"
            >
              <template #icon><icon-safe /></template>
              <template #default>分配权限</template>
            </a-button>
            <a-popconfirm type="warning" content="确定删除该角色吗?" @ok="onDelete([record])">
              <a-button
                type="primary"
                status="danger"
                size="mini"
                v-hasPerm="['role:btn:delete']"
                :disabled="record.disabled"
              >
                <template #icon><icon-delete /></template>
                <span>删除</span>
              </a-button>
            </a-popconfirm>
          </a-space>
        </template>
      </a-table>

      <AddRoleModal ref="AddRoleModalRef" @refresh="reset"></AddRoleModal>
      <PermModal ref="PermModalRef"></PermModal>
    </a-card>
  </div>
</template>

<script setup lang="ts">
import { Message, type TableColumnData, type TableInstance } from '@arco-design/web-vue'
import AddRoleModal from './AddRoleModal.vue'
import PermModal from './PermModal.vue'
import type { RoleItem } from '@/apis'
import { usePagination } from '@/hooks'
import { getSystemRoleList, deleteSystemRole } from '@/apis'
import { isPhone } from '@/utils/common'

defineOptions({ name: 'SystemRole' })

const AddRoleModalRef = ref<InstanceType<typeof AddRoleModal>>()
const PermModalRef = ref<InstanceType<typeof PermModal>>()
const { pagination, setTotal } = usePagination(() => {
  getRoleList()
})
const queryParam = reactive({
  keyword: ''
})

const loading = ref(false)
const roleList = ref<RoleItem[]>([])
//#region 列配置
const columns: TableColumnData[] = [
  {
    title: '序号',
    dataIndex: 'rowIndex',
    width: 64,
    slotName: 'rowIndex',
    align: 'center'
  },
  {
    title: '角色名称',
    dataIndex: 'name'
  },
  {
    title: '角色编码',
    dataIndex: 'code'
  },
  {
    title: '创建时间',
    dataIndex: 'createTime'
  },
  {
    title: '操作',
    dataIndex: 'action',
    slotName: 'action',
    align: 'center',
    width: 300,
    fixed: !isPhone() ? 'right' : undefined
  }
]
//#endregion
//#region 数据加载
const getRoleList = async () => {
  try {
    loading.value = true
    const requestParameters = Object.assign({
      sortField: 'Code',
      sortOrder: 'asc',
      search: { ...queryParam },
      pageNo: pagination.current,
      pageSize: pagination.pageSize
    })
    const res = await getSystemRoleList(requestParameters)
    roleList.value = res.data
    setTotal(res.total ?? 0)
  } catch (error) {
  } finally {
    loading.value = false
  }
}
getRoleList()

const deleteRoles = async (ids: Array<string | number>) => {
  try {
    const res = await deleteSystemRole(ids)
    if (res.success) {
      Message.success('删除成功！')
      reset()
    } else {
      Message.error(res.message)
    }
    return true
  } catch (error) {
    return false
  }
}
//#endregion
//#region 事件
const search = () => {
  pagination.onChange(1)
}

const reset = () => {
  selectRowKeys.value = []
  queryParam.keyword = ''
  pagination.onChange(1)
}

const onAdd = () => {
  AddRoleModalRef.value?.add()
}

const onEdit = (item: RoleItem) => {
  AddRoleModalRef.value?.edit(item.id)
}

const onDelete = (record: any) => {
  const ids = record.map((value: { id: string }) => value.id)
  deleteRoles(ids)
}

const onPerm = (item: RoleItem) => {
  PermModalRef.value?.open({ id: item.id, title: item.name })
}

// 勾选
const selectRowKeys = ref<(string | number)[]>([])
const select: TableInstance['onSelect'] = (rowKeys) => {
  selectRowKeys.value = rowKeys
}
const onMulDelete = () => {
  if (!selectRowKeys.value.length) {
    return Message.warning('请选择角色')
  }
  deleteRoles(selectRowKeys.value)
}
//#endregion
</script>

<style lang="scss" scoped></style>
