<template>
  <a-modal
    v-model:visible="visible"
    :title="title"
    width="90%"
    :modal-style="{ maxWidth: '625px' }"
    :mask-closable="false"
    @before-ok="save"
    @close="close"
  >
    <a-form ref="FormRef" :model="form" :rules="formRules" auto-label-width>
      <a-form-item label="菜单类型" field="type">
        <a-radio-group v-model="form.type" type="button" :disabled="isEdit" @change="onChangeType">
          <a-radio :value="Dir">目录</a-radio>
          <a-radio :value="Menu">菜单</a-radio>
          <a-radio :value="Btn">按钮</a-radio>
        </a-radio-group>
      </a-form-item>

      <a-form-item label="上级菜单" field="parentId">
        <a-tree-select
          v-model="form.parentId"
          placeholder="请选择上级菜单"
          allow-clear
          allow-search
          :disabled="isEdit"
          :data="(menuSelectTree as any)"
          :fieldNames="{
            key: 'id',
            title: 'title',
            children: 'children'
          }"
        />
      </a-form-item>

      <a-row :gutter="16" v-if="DirMenu.includes(form.type)">
        <a-col :xs="24" :sm="24" :md="12" :lg="12" :xl="12" :xxl="12">
          <a-form-item label="自定义图标" field="svgIcon">
            <GiIconSelector v-model="form.svgIcon" type="custom"></GiIconSelector>
            <a-tooltip content="优先显示">
              <icon-question-circle-fill :size="18" style="color: rgba(var(--warning-6)); margin-left: 8px" />
            </a-tooltip>
          </a-form-item>
        </a-col>
        <a-col :xs="24" :sm="24" :md="12" :lg="12" :xl="12" :xxl="12">
          <a-form-item label="菜单图标" field="icon">
            <GiIconSelector v-model="form.icon"></GiIconSelector>
          </a-form-item>
        </a-col>
      </a-row>

      <a-form-item label="菜单标题" field="name">
        <a-input v-model="form.name" placeholder="请输入菜单标题" allow-clear />
      </a-form-item>

      <a-form-item label="路由路径" field="path" v-if="DirMenu.includes(form.type)">
        <a-input v-model="form.path" placeholder="请输入路由路径" allow-clear />
        <template #extra>
          <div>
            <span>路由名称由系统自动生成：</span>
            <a-tag v-if="routeName">{{ routeName }}</a-tag>
          </div>
        </template>
      </a-form-item>

      <a-form-item label="重定向" field="redirect" v-if="DirMenu.includes(form.type) && !isExternalUrl">
        <a-input v-model="form.redirect" placeholder="请输入重定向地址" allow-clear />
      </a-form-item>

      <a-form-item label="是否外链" field="isExternalUrl" v-if="DirMenu.includes(form.type)">
        <a-radio-group v-model="isExternalUrl" type="button">
          <a-radio :value="true">是</a-radio>
          <a-radio :value="false">否</a-radio>
        </a-radio-group>
      </a-form-item>

      <a-form-item label="组件路径" field="component" v-if="form.type === Menu">
        <a-input v-if="isExternalUrl" v-model="form.component" placeholder="请输入组件路径" />
        <a-input v-else v-model="form.component" placeholder="请输入组件路径">
          <template #prepend>@/views/</template>
          <template #append>.vue</template>
        </a-input>
      </a-form-item>

      <a-row :gutter="16" v-if="DirMenu.includes(form.type)">
        <a-col :xs="12" :sm="12" :md="8" :lg="8" :xl="8" :xxl="8">
          <a-form-item label="状态" field="status">
            <a-switch
              type="round"
              v-model="form.status"
              :checked-value="'Enable'"
              :unchecked-value="'Disable'"
              checked-text="启用"
              unchecked-text="禁用"
            />
          </a-form-item>
        </a-col>
        <a-col :xs="12" :sm="12" :md="8" :lg="8" :xl="8" :xxl="8">
          <a-form-item label="是否隐藏" field="isHide">
            <a-switch
              type="round"
              v-model="form.isHide"
              :checked-value="true"
              :unchecked-value="false"
              checked-text="是"
              unchecked-text="否"
            />
          </a-form-item>
        </a-col>
        <a-col :xs="12" :sm="12" :md="8" :lg="8" :xl="8" :xxl="8">
          <a-form-item label="是否缓存" field="isKeepAlive">
            <a-switch
              type="round"
              v-model="form.isKeepAlive"
              :checked-value="true"
              :unchecked-value="false"
              checked-text="是"
              unchecked-text="否"
            />
          </a-form-item>
        </a-col>
        <!-- <a-col :xs="12" :sm="12" :md="8" :lg="8" :xl="8" :xxl="8">
          <a-form-item label="面包屑" field="breadcrumb">
            <a-switch
              type="round"
              v-model="form.breadcrumb"
              :checked-value="true"
              :unchecked-value="false"
              checked-text="显示"
              unchecked-text="隐藏"
            />
          </a-form-item>
        </a-col>
        <a-col :xs="12" :sm="12" :md="8" :lg="8" :xl="8" :xxl="8">
          <a-form-item label="总是显示" field="alwaysShow" v-if="form.type === Dir">
            <a-switch
              type="round"
              v-model="form.alwaysShow"
              :checked-value="true"
              :unchecked-value="false"
              checked-text="显示"
              unchecked-text="隐藏"
            />
          </a-form-item>
          <a-form-item label="页签显示" field="showInTabs" v-if="form.type === Menu">
            <a-switch
              type="round"
              v-model="form.showInTabs"
              :checked-value="true"
              :unchecked-value="false"
              checked-text="显示"
              unchecked-text="隐藏"
            />
          </a-form-item>
        </a-col> -->
      </a-row>

      <a-form-item label="权限标识" field="permission" v-if="form.type === Btn">
        <a-input v-model="form.permission" placeholder="sys:btn:add" allow-clear />
      </a-form-item>

      <a-form-item label="菜单排序" field="seq">
        <a-input-number v-model="form.seq" placeholder="请输入菜单排序" :min="1" mode="button" style="width: 120px" />
      </a-form-item>
    </a-form>
  </a-modal>
</template>

<script setup lang="ts">
import { Message, type FormInstance } from '@arco-design/web-vue'
import { getSystemMenuDetail, saveSystemMenu, type MenuItem } from '@/apis'
import { isExternal } from '@/utils/validate'
import { transformPathToName, filterTree } from '@/utils/common'
import { mapTree } from 'xe-utils'
import { DirMenu, Dir, Menu, Btn } from '@/constant/system'

interface Props {
  menus: MenuItem[]
}

const props = withDefaults(defineProps<Props>(), {
  menus: () => []
})

const menuSelectTree = computed(() => {
  const menus = JSON.parse(JSON.stringify(props.menus)) as MenuItem[]
  const data = filterTree(menus, (i) => DirMenu.includes(i.type))
  const arr = mapTree(data, (i) => ({
    id: i.id,
    title: i.title,
    children: i.children
  }))
  return arr
})

const FormRef = ref<FormInstance>()

const menuId = ref('')
const visible = ref(false)
const isEdit = computed(() => !!menuId.value)
const title = computed(() => (isEdit.value ? '编辑菜单' : '新增菜单'))

const isExternalUrl = ref(false)
const initialForm = () => ({
  id: '',
  type: '1' as MenuType, // 类型 1目录 2菜单 3按钮
  icon: '', // arco 图标名称
  svgIcon: '', // 自定义图标名称
  name: '', // 菜单或目录的名称
  seq: 0, // 排序
  status: 'Enable', // 状态 0禁用 1启用
  path: '', // 路由路径
  component: '', // 组件路径
  isKeepAlive: false, // 是否缓存
  isHide: false, // 设置 true 的时候该路由不会在侧边栏出现
  parentId: '',
  redirect: '', // 重定向
  permission: '',
  code: ''
  /*breadcrumb: true, // 显示在面包屑
  showInTabs: true, // 显示在页签
  alwaysShow: false // 总是显示*/
})
const form = reactive(initialForm())
const routeName = computed(() => transformPathToName(form.path))

const rules: FormInstance['rules'] = {
  parentId: [{ required: true, message: '请选择上级菜单' }],
  name: [{ required: true, message: '请输入菜单标题' }],
  path: [{ required: true, message: '请输入路由路径' }],
  component: [{ required: true, message: '请输入组件路径' }],
  permission: [{ required: true, message: '请输入权限标识' }]
}
const formRules = computed(() => {
  if (DirMenu.includes(form.type)) {
    const { title, path } = rules
    return { title, path } as FormInstance['rules']
  }
  if (form.type === Btn) {
    const { parentId, title, permission } = rules
    return { parentId, title, permission } as FormInstance['rules']
  }
})

// 切换类型清除校验
const onChangeType = () => {
  FormRef.value?.clearValidate()
}

const add = () => {
  Object.assign(form, initialForm())
  menuId.value = ''
  visible.value = true
}

const edit = async (id: string) => {
  menuId.value = id
  const res = await getSystemMenuDetail({ id })
  Object.assign(form, res.data)
  if (isExternal(form.path)) {
    isExternalUrl.value = true
  }
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
    if (DirMenu.includes(form.type)) {
      form.code = routeName.value
      if (form.type === Dir) form.component = 'Layout'
    }
    const res = await saveSystemMenu(form)
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
