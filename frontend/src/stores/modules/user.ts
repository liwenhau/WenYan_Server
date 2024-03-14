import { defineStore } from 'pinia'
import { ref, reactive, computed } from 'vue'
import { resetRouter } from '@/router'
import { login as loginApi, logout as logoutApi, getUserInfo as getUserInfoApi } from '@/apis'
import type { UserInfo } from '@/apis'
import { setToken, clearToken, getToken, setRefreshToken, getRefreshToken, clearRefreshToken } from '@/utils/auth'
import { resetHasRouteFlag } from '@/router/permission'

const storeSetup = () => {
  const userInfo = reactive<Pick<UserInfo, 'id' | 'name' | 'avatar' | 'sex' | 'sign'>>({
    id: '',
    name: '',
    avatar: '',
    sign: '',
    sex: ''
  })
  const name = computed(() => userInfo.name)
  const avatar = computed(() => userInfo.avatar)

  const token = ref(getToken() || '')
  const roles = ref<string[]>([]) // 当前用户角色
  const permissions = ref<string[]>([]) // 当前角色权限标识集合

  // 重置token
  const resetToken = () => {
    token.value = ''
    clearToken()
    clearRefreshToken()
    resetHasRouteFlag()
  }

  // 登录
  const login = async (params: any) => {
    const res = await loginApi(params)
    setToken(res.data.accessToken)
    setRefreshToken(res.data.refreshToken)
    token.value = res.data.accessToken
  }

  // 退出
  const logout = async () => {
    token.value = ''
    roles.value = []
    permissions.value = []
    resetToken()
    resetRouter()
  }

  // 获取用户信息
  const getInfo = async () => {
    const res = await getUserInfoApi()
    const userdata = res.data
    userInfo.name = userdata.name
    userInfo.avatar = userdata.avatar
    userInfo.sex = userdata.sex
    userInfo.id = userdata.id
    userInfo.sign = userdata.sign
    if (userdata.roles && userdata.roles.length) {
      roles.value = userdata.roles
      permissions.value = userdata.permissions
    }
  }

  // 模拟用 临时模拟token过期
  const editToken = (value: string) => {
    token.value = value
    setToken(token.value)
  }

  return {
    userInfo,
    name,
    avatar,
    token,
    roles,
    permissions,
    login,
    logout,
    getInfo,
    resetToken,
    getRefreshToken,
    editToken
  }
}

export const useUserStore = defineStore('user', storeSetup, {
  persist: { paths: ['token', 'roles', 'permissions'], storage: localStorage }
})
