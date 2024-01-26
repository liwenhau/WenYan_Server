<template>
  <div class="gi_page dept-manage">
    <a-card title="部门管理">
      <a-space wrap>
        <a-input v-model="queryParam.name" placeholder="输入部门名称搜索" allow-clear style="width: 250px">
          <template #prefix><icon-search /></template>
        </a-input>
        <a-select v-model="queryParam.status" placeholder="部门状态" style="width: 120px">
          <a-option :value="'Enable'">正常</a-option>
          <a-option :value="'Disable'">禁用</a-option>
        </a-select>
        <a-button type="primary" @click="search" v-hasPerm="['org:btn.query']">
          <template #icon><icon-search /></template>
          <span>搜索</span>
        </a-button>
        <a-button @click="reset">
          <template #icon><icon-refresh /></template>
          <span>重置</span>
        </a-button>
      </a-space>

      <a-row>
        <a-space wrap>
          <a-button type="primary" @click="onAdd()" v-hasPerm="['org:btn.add']">
            <template #icon><icon-plus /></template>
            <span>新增</span>
          </a-button>
          <a-button type="primary" status="danger" @click="onMulDelete" v-hasPerm="['org:btn.delete']">
            <template #icon><icon-delete /></template>
            <span>删除</span>
          </a-button>
        </a-space>
      </a-row>

      <a-table
        style="margin-top: 8px"
        ref="TableRef"
        row-key="id"
        :columns="columns"
        :bordered="{ cell: true }"
        :data="deptList"
        :loading="loading"
        :scroll="{ x: '100%', y: '100%', minWidth: 900 }"
        :pagination="false"
        :row-selection="{ type: 'checkbox', showCheckedAll: false, selectedRowKeys: selectRowKeys }"
        @select="select"
      >
        <template #expand-icon="{ expanded }">
          <IconDown v-if="expanded" />
          <IconRight v-else />
        </template>
        <template #status="{ record }">
          <a-tag v-if="record.status == 'Enable'" color="green">正常</a-tag>
          <a-tag v-if="record.status == 'Disable'" color="red">禁用</a-tag>
        </template>
        <template #action="{ record }">
          <a-space>
            <a-button type="primary" size="mini" @click="onEdit(record)" v-hasPerm="['org:btn.edit']">
              <template #icon><icon-edit /></template>
              <span>编辑</span>
            </a-button>
            <a-button
              v-if="record.parentId"
              type="primary"
              status="success"
              size="mini"
              @click="onAdd(record.id)"
              v-hasPerm="['org:btn.add']"
            >
              <template #icon><icon-plus /></template>
              <span>新增</span>
            </a-button>
            <a-popconfirm type="warning" content="您确定要删除该项吗?" @ok="onDelete([record])">
              <a-button type="primary" status="danger" size="mini" v-hasPerm="['org:btn.delete']">
                <template #icon><icon-delete /></template>
                <span>删除</span>
              </a-button>
            </a-popconfirm>
          </a-space>
        </template>
      </a-table>
    </a-card>

    <AddDeptModal ref="AddDeptModalRef" @refresh="reset"></AddDeptModal>
  </div>
</template>

<script setup lang="ts">
import AddDeptModal from './AddDeptModal.vue'
import { getSystemDeptList, type DeptItem, deleteSystemDept } from '@/apis'
import type { TableColumnData, TableInstance } from '@arco-design/web-vue'
import { Message } from '@arco-design/web-vue'
import { isPhone } from '@/utils/common'

defineOptions({ name: 'SystemDept' })

const AddDeptModalRef = ref<InstanceType<typeof AddDeptModal>>()
const TableRef = ref<TableInstance>()

const queryParam = reactive({
  name: '',
  status: ''
})

const loading = ref(false)
const deptList = ref<DeptItem[]>([])

//#region 列配置
const columns: TableColumnData[] = [
  {
    title: '部门名称',
    dataIndex: 'name',
    width: 160
  },
  {
    title: '部门编码',
    dataIndex: 'code',
    width: 100
  },
  {
    title: '排序',
    dataIndex: 'seq',
    width: 100,
    align: 'center'
  },
  {
    title: '状态',
    dataIndex: 'status',
    width: 100,
    align: 'center',
    slotName: 'status'
  },
  {
    title: '描述',
    dataIndex: 'remark',
    width: 200
  },
  {
    title: '修改时间',
    dataIndex: 'modifyTime',
    width: 200
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
const getDeptList = async () => {
  try {
    loading.value = true
    const res = await getSystemDeptList(queryParam)
    deptList.value = res.data
    nextTick(() => {
      TableRef.value?.expandAll(true)
    })
  } catch (error) {
  } finally {
    loading.value = false
  }
}
getDeptList()

const deleteDepts = async (ids: Array<string | number>) => {
  try {
    const res = await deleteSystemDept(ids)
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
  getDeptList()
}

const reset = () => {
  selectRowKeys.value = []
  queryParam.name = ''
  queryParam.status = ''
  getDeptList()
}

const onAdd = (orgId?: string) => {
  AddDeptModalRef.value?.add(orgId)
}

const onEdit = (item: DeptItem) => {
  AddDeptModalRef.value?.edit(item.id)
}

const onDelete = (record: any) => {
  const ids = record.map((value: { id: string }) => value.id)
  deleteDepts(ids)
}
// 勾选
const selectRowKeys = ref<(string | number)[]>([])
const select: TableInstance['onSelect'] = (rowKeys) => {
  selectRowKeys.value = rowKeys
}
const onMulDelete = () => {
  if (!selectRowKeys.value.length) {
    return Message.warning('请选择部门')
  }
  deleteDepts(selectRowKeys.value)
}
//#endregion
</script>

<style lang="scss" scoped></style>
