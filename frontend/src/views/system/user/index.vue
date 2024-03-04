<template>
  <div class="gi_page user-manage">
    <a-card title="用户管理">
      <a-row :gutter="16">
        <a-col :xs="0" :md="6" :lg="6" :xl="6" :xxl="4">
          <a-input
            v-model="orgQuery.name"
            placeholder="输入部门名称搜索"
            allow-clear
            style="margin-bottom: 10px"
            @input="onOrgSearch"
            @clear="getDeptList"
          >
            <template #prefix><icon-search /></template>
          </a-input>
          <a-tree
            ref="TreeRef"
            block-node
            show-line
            default-expand-all
            :data="deptList"
            :field-names="{
              key: 'id',
              title: 'name',
              children: 'children'
            }"
            @select="onOrgSelect"
          >
          </a-tree>
        </a-col>

        <a-col :xs="24" :md="18" :lg="18" :xl="18" :xxl="20">
          <a-row justify="space-between">
            <a-space wrap>
              <a-button type="primary" @click="onAdd">
                <template #icon><icon-plus /></template>
                <span>新增</span>
              </a-button>

              <a-button type="primary" status="danger" @click="onMulDelete">
                <template #icon><icon-delete /></template>
                <span>删除</span>
              </a-button>
            </a-space>

            <a-space wrap>
              <a-input-group>
                <a-select v-model="queryParam.status" placeholder="用户状态" allow-clear style="width: 150px">
                  <a-option :value="'Enable'">正常</a-option>
                  <a-option :value="'Disable'">禁用</a-option>
                </a-select>
                <a-input v-model="queryParam.keyword" placeholder="输入用户名搜索" allow-clear style="max-width: 250px">
                </a-input>
              </a-input-group>
              <a-button type="primary" @click="search">
                <template #icon><icon-search /></template>
                <span>查询</span>
              </a-button>
              <a-button @click="reset">
                <template #icon><icon-refresh /></template>
                <span>重置</span>
              </a-button>
            </a-space>
          </a-row>

          <a-table
            style="margin-top: 8px"
            row-key="id"
            :loading="loading"
            :columns="columns"
            :data="userList"
            :bordered="{ cell: true }"
            :scroll="{ x: '100%', y: '100%', minWidth: 2400 }"
            :pagination="pagination"
            :row-selection="{ type: 'checkbox', showCheckedAll: false }"
            @select="select"
          >
            <template #rowIndex="{ rowIndex }">
              <a-tag color="blue">{{ rowIndex + 1 }}</a-tag>
            </template>
            <template #userName="{ record }">
              <a-link @click="openDetail(record)">{{ record.userName }}</a-link>
            </template>
            <template #sex="{ record }">
              <span v-if="record.sex === 'Boy'">男</span>
              <span v-if="record.sex === 'Girl'">女</span>
            </template>
            <template #avatar="{ record }">
              <a-avatar>
                <img alt="avatar" :src="record.avatar" v-if="record.avatar" />
                <span v-else>{{ firstChar(record.name) }}</span>
              </a-avatar>
            </template>
            <template #status="{ record }">
              <a-tag v-if="record.status === 'Enable'" color="green">正常</a-tag>
              <a-tag v-if="record.status === 'Disable'" color="red">禁用</a-tag>
            </template>
            <template #action="{ record }">
              <a-space>
                <a-button type="primary" size="mini" @click="onEdit(record)">
                  <template #icon><icon-edit /></template>
                  <span>编辑</span>
                </a-button>
                <a-popconfirm type="warning" content="确定删除该用户吗?">
                  <a-button type="primary" status="danger" size="mini" :disabled="record.disabled">
                    <template #icon><icon-delete /></template>
                    <span>删除</span>
                  </a-button>
                </a-popconfirm>
              </a-space>
            </template>
          </a-table>
        </a-col>
      </a-row>
    </a-card>

    <AddUserModal ref="AddUserModalRef" @refresh="reset"></AddUserModal>
    <UserDetailDrawer ref="UserDetailDrawerRef"></UserDetailDrawer>
  </div>
</template>

<script setup lang="ts">
import { usePagination } from '@/hooks'
import { useDept } from '@/hooks/app'
import { getSystemUserList } from '@/apis'
import type { DeptItem, UserItem } from '@/apis'
import AddUserModal from './AddUserModal.vue'
import UserDetailDrawer from './UserDetailDrawer.vue'
import type { TreeInstance, TableInstance, TableColumnData } from '@arco-design/web-vue'
import { Message } from '@arco-design/web-vue'
import { dfsTree, isPhone, firstChar } from '@/utils/common'

defineOptions({ name: 'SystemUser' })

const TreeRef = ref<TreeInstance>()
const AddUserModalRef = ref<InstanceType<typeof AddUserModal>>()
const UserDetailDrawerRef = ref<InstanceType<typeof UserDetailDrawer>>()
const queryParam = reactive({
  keyword: '',
  status: '',
  orgs: Array<string | number>()
})
const orgQuery = reactive({
  name: ''
})
const loading = ref(false)
const userList = ref<UserItem[]>([])
const cloneDeptList = ref<DeptItem[]>([])
const { pagination, setTotal } = usePagination(() => {
  getUserList()
})
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
    title: '头像',
    dataIndex: 'avatar',
    slotName: 'avatar',
    width: 100,
    align: 'center'
  },
  {
    title: '用户名称',
    dataIndex: 'userName',
    slotName: 'userName',
    width: 120
  },
  {
    title: '昵称',
    dataIndex: 'name',
    width: 150
  },
  {
    title: '用户编码',
    dataIndex: 'code',
    width: 120
  },
  {
    title: '状态',
    dataIndex: 'status',
    slotName: 'status',
    align: 'center',
    width: 80
  },
  {
    title: '性别',
    dataIndex: 'sex',
    slotName: 'sex',
    width: 80,
    align: 'center'
  },
  {
    title: '个性签名',
    dataIndex: 'sign',
    width: 300
  },
  {
    title: '所属组织',
    dataIndex: 'orgName',
    width: 200
  },
  {
    title: '描述',
    dataIndex: 'remark',
    width: 300
  },
  {
    title: '刷新令牌过期时间',
    dataIndex: 'refreshTokenExpiryTime',
    width: 250
  },
  {
    title: '创建时间',
    dataIndex: 'createTime',
    width: 250
  },
  {
    title: '操作',
    dataIndex: 'action',
    slotName: 'action',
    align: 'center',
    width: 180,
    fixed: !isPhone() ? 'right' : undefined
  }
]
//#endregion

//#region 数据加载
const { deptList, getDeptList } = useDept({
  onSuccess: () => {
    cloneDeptList.value = JSON.parse(JSON.stringify(deptList.value))
    console.log(cloneDeptList)
    nextTick(() => {
      TreeRef.value?.expandAll(true)
    })
  }
})
getDeptList(orgQuery)

const getUserList = async () => {
  try {
    loading.value = true
    const requestParameters = Object.assign({
      sortField: 'ModifyTime',
      sortOrder: 'desc',
      search: { ...queryParam },
      pageNo: pagination.current,
      pageSize: pagination.pageSize
    })
    const res = await getSystemUserList(requestParameters)
    userList.value = res.data
    setTotal(res.total ?? 0)
  } catch (error) {
  } finally {
    loading.value = false
  }
}
getUserList()
//#endregion

//#region 事件
const onAdd = () => {
  AddUserModalRef.value?.add()
}

const onEdit = (item: UserItem) => {
  AddUserModalRef.value?.edit(item.id)
}

const search = () => {
  pagination.onChange(1)
}

const onOrgSelect = (keys: any) => {
  if (Array.isArray(keys)) {
    queryParam.orgs = keys
    pagination.onChange(1)
  }
}

const onOrgSearch = async (value?: string) => {
  //deptList.value = [...cloneDeptList.value]
  deptList.value = JSON.parse(JSON.stringify(cloneDeptList.value))
  //deptList.value = cloneDeptList.value
  deptList.value = dfsTree(deptList.value, value)
}

const reset = () => {
  queryParam.status = ''
  queryParam.keyword = ''
  queryParam.orgs = []
  pagination.onChange(1)
}

const openDetail = (item: UserItem) => {
  UserDetailDrawerRef.value?.open(item.id)
}

// 勾选
const selectRowKeys = ref<(string | number)[]>([])
const select: TableInstance['onSelect'] = (rowKeys) => {
  selectRowKeys.value = rowKeys
}
const onMulDelete = () => {
  if (!selectRowKeys.value.length) {
    return Message.warning('请选择用户')
  }
  Message.info('点击了批量删除')
}
//#endregion
</script>

<style lang="scss" scoped></style>
