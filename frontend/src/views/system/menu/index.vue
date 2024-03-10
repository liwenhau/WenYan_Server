<template>
  <div class="gi_page menu-manage">
    <a-card title="菜单管理">
      <a-row justify="space-between">
        <a-space wrap>
          <a-button type="primary" v-hasPerm="['menu:btn:add']" @click="onAdd">
            <template #icon><icon-plus /></template>
            <span>新增</span>
          </a-button>
          <a-tooltip content="展开/折叠">
            <a-button type="primary" status="success" @click="onExpanded">
              <template #icon>
                <icon-list v-if="!isExpanded" />
                <icon-mind-mapping v-else />
              </template>
            </a-button>
          </a-tooltip>
          <a-tooltip content="查看数据结构">
            <a-button type="primary" v-hasPerm="['menu:btn:code']" status="warning" @click="onViewCode">
              <template #icon><icon-code /></template>
            </a-button>
          </a-tooltip>
        </a-space>

        <a-space wrap>
          <a-input v-model="form.name" placeholder="输入菜单名称搜索" allow-clear style="width: 250px">
            <template #prefix><icon-search /></template>
          </a-input>
          <a-select v-model="form.status" placeholder="菜单状态" style="width: 120px">
            <a-option :value="'Enable'">启用</a-option>
            <a-option :value="'Disable'">禁用</a-option>
          </a-select>
          <a-button type="primary" @click="search" v-hasPerm="['menu:btn:query']">
            <template #icon><icon-search /></template>
            <span>搜索</span>
          </a-button>
          <a-button @click="reset">
            <template #icon><icon-refresh /></template>
            <span>重置</span>
          </a-button>
        </a-space>
      </a-row>

      <a-table
        style="margin-top: 8px"
        ref="TableRef"
        row-key="id"
        :data="menuList"
        :columns="columns"
        :loading="loading"
        :bordered="{ cell: true }"
        :scroll="{ x: '100%', y: '100%', minWidth: 1700 }"
        :pagination="false"
        size="mini"
      >
        <template #expand-icon="{ expanded }">
          <IconDown v-if="expanded" />
          <IconRight v-else />
        </template>
        <template #type="{ record }">
          <a-tag v-if="record.type === '1'" color="orangered">目录</a-tag>
          <a-tag v-if="record.type === '2'" color="green">菜单</a-tag>
          <a-tag v-if="record.type === '3'">按钮</a-tag>
        </template>
        <template #icon="{ record }">
          <GiSvgIcon v-if="record.svgIcon" :size="24" :name="record.svgIcon"></GiSvgIcon>
          <component v-else-if="record.icon" :is="record.icon" :size="24"></component>
        </template>
        <template #status="{ record }">
          <a-switch
            type="round"
            size="small"
            :model-value="record.status"
            :checked-value="'Enable'"
            :unchecked-value="'Disable'"
          />
        </template>
        <template #keepAlive="{ record }">
          <a-tag v-if="record.keepAlive" color="green">是</a-tag>
          <a-tag v-else color="red">否</a-tag>
        </template>
        <template #hidden="{ record }">
          <a-tag v-if="record.hidden" color="green">是</a-tag>
          <a-tag v-else color="red">否</a-tag>
        </template>
        <template #outLink="{ record }">
          <a-tag v-if="isExternal(record.path)" color="green">是</a-tag>
          <a-tag v-else color="red">否</a-tag>
        </template>
        <template #action="{ record }">
          <a-space>
            <a-button type="primary" size="mini" v-hasPerm="['menu:btn:edit']" @click="onEdit(record)">
              <template #icon><icon-edit /></template>
              <span>编辑</span>
            </a-button>
            <a-popconfirm type="warning" content="您确定要删除该项吗?" @ok="onDelete([record])">
              <a-button type="primary" status="danger" size="mini" v-hasPerm="['menu:btn:delete']">
                <template #icon><icon-delete /></template>
                <span>删除</span>
              </a-button>
            </a-popconfirm>
          </a-space>
        </template>
      </a-table>
    </a-card>

    <AddMenuModal ref="AddMenuModalRef" :menus="menuList" @refresh="reset"></AddMenuModal>
  </div>
</template>

<script setup lang="ts">
import AddMenuModal from './AddMenuModal.vue'
import { getSystemMenuList, deleteSystemMenu, type MenuItem } from '@/apis'
import { Drawer, Message, type TableColumnData, type TableInstance } from '@arco-design/web-vue'
import { isExternal } from '@/utils/validate'
import { isPhone } from '@/utils/common'
import GiCodeView from '@/components/GiCodeView/index.vue'
defineOptions({ name: 'SystemMenu' })

const AddMenuModalRef = ref<InstanceType<typeof AddMenuModal>>()
const loading = ref(false)

const TableRef = ref<TableInstance>()
const isExpanded = ref(false)
const onExpanded = () => {
  isExpanded.value = !isExpanded.value
  TableRef.value?.expandAll(isExpanded.value)
}

const form = reactive({ name: '', status: '' })
const menuList = ref<MenuItem[]>([])
//#region 列配置
const columns: TableColumnData[] = [
  {
    title: '菜单名称',
    dataIndex: 'title',
    render: ({ record }) => {
      return record.title ?? ''
    }
  },
  {
    title: '类型',
    dataIndex: 'type',
    width: 80,
    slotName: 'type',
    align: 'center'
  },
  {
    title: '排序',
    dataIndex: 'sort',
    width: 80,
    align: 'center',
    render: ({ record }) => {
      return record.sort ?? 0
    }
  },
  {
    title: '路由路径',
    dataIndex: 'path'
  },
  {
    title: '路由名称',
    dataIndex: 'code'
  },
  {
    title: '组件路径',
    dataIndex: 'component'
  },
  {
    title: '权限标识',
    dataIndex: 'permission'
  },
  {
    title: '菜单图标',
    dataIndex: 'icon',
    width: 100,
    slotName: 'icon',
    align: 'center'
  },
  {
    title: '状态',
    dataIndex: 'status',
    slotName: 'status',
    width: 80,
    align: 'center'
  },
  {
    title: '是否缓存',
    dataIndex: 'keepAlive',
    slotName: 'keepAlive',
    width: 100,
    align: 'center'
  },
  {
    title: '是否隐藏',
    dataIndex: 'hidden',
    slotName: 'hidden',
    width: 100,
    align: 'center'
  },
  {
    title: '是否外链',
    dataIndex: 'outLink',
    slotName: 'outLink',
    width: 100,
    align: 'center'
  },
  {
    title: '操作',
    dataIndex: 'action',
    slotName: 'action',
    align: 'center',
    width: 200,
    fixed: !isPhone() ? 'right' : undefined
  }
]
//#endregion
//#region 数据加载
const getMenuList = async () => {
  try {
    loading.value = true
    const res = await getSystemMenuList(form)
    menuList.value = res.data
  } catch (error) {
  } finally {
    loading.value = false
  }
}
getMenuList()

const deleteMenu = async (ids: Array<string | number>) => {
  try {
    const res = await deleteSystemMenu(ids)
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
  getMenuList()
}

const reset = () => {
  form.name = ''
  form.status = ''
  getMenuList()
}

const onAdd = () => {
  AddMenuModalRef.value?.add()
}

const onEdit = (item: MenuItem) => {
  AddMenuModalRef.value?.edit(item.id)
}

const onViewCode = () => {
  Drawer.open({
    title: '数据结构',
    content: () => h(GiCodeView, { codeJson: JSON.stringify(menuList.value, null, '\t') }),
    width: 560
  })
}

const onDelete = (record: any) => {
  const ids = record.map((value: { id: string }) => value.id)
  deleteMenu(ids)
}
//#endregion
</script>

<style lang="scss" scoped></style>
