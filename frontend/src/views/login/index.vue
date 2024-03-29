<template>
  <div class="login">
    <a-row align="stretch" class="login-box">
      <a-col :xs="0" :sm="14" :md="15">
        <div class="login-left">
          <a-carousel
            :style="{
              width: '100%',
              height: '100%'
            }"
            :auto-play="{ interval: 5000 }"
            indicator-type="line"
            show-arrow="hover"
          >
            <a-carousel-item v-for="image in images">
              <img :src="image" class="login-img" />
            </a-carousel-item>
          </a-carousel>
        </div>
      </a-col>
      <a-col :xs="24" :sm="10" :md="9">
        <div class="login-right">
          <a-form
            ref="FormRef"
            :size="isPhone() ? 'large' : 'medium'"
            :model="form"
            :rules="rules"
            :style="{ width: '84%' }"
            :label-col-style="{ display: 'none' }"
            :wrapper-col-style="{ flex: 1 }"
          >
            <h3 class="login-form-title">
              <Logo></Logo>
            </h3>
            <a-form-item><WyTyped :data="textdata" :speed="150" loop></WyTyped></a-form-item>
            <a-form-item field="username">
              <a-input v-model="form.username" placeholder="账号" size="large" allow-clear>
                <template #prefix><icon-user :stroke-width="1" :style="{ fontSize: '20px' }" /></template>
              </a-input>
            </a-form-item>
            <a-form-item field="password">
              <a-input-password v-model="form.password" autocomplete="off" placeholder="密码" size="large" allow-clear>
                <template #prefix><icon-lock :stroke-width="1" :style="{ fontSize: '20px' }" /></template>
              </a-input-password>
            </a-form-item>
            <a-form-item>
              <a-row justify="space-between" align="center" class="w-full">
                <a-checkbox v-model="checked">记住密码</a-checkbox>
                <a-link>忘记密码</a-link>
              </a-row>
            </a-form-item>
            <a-form-item>
              <a-space direction="vertical" fill class="w-full">
                <a-button type="primary" size="large" long :loading="loading" @click="login">登录</a-button>
                <a-button type="text" size="large" long class="register-btn">注册账号</a-button>
              </a-space>
            </a-form-item>
          </a-form>
        </div>
      </a-col>
    </a-row>

    <GiThemeBtn class="theme-btn"></GiThemeBtn>

    <LoginBg></LoginBg>
  </div>
</template>

<script setup lang="ts">
import { useUserStore } from '@/stores'
import { useLoading } from '@/hooks'
import { Message, type FormInstance } from '@arco-design/web-vue'
import LoginBg from './components/LoginBg/index.vue'
import * as Regexp from '@/utils/regexp'
import { isPhone } from '@/utils/common'
import { Md5 } from 'ts-md5'
import Logo from '@/layout/components/Asider/Logo.vue'
import WyTyped from '@/components/WyTyped/index.vue'
defineOptions({ name: 'Login' })
const router = useRouter()
const userStore = useUserStore()

const form = reactive({
  username: 'admin',
  password: 'WenYan@2024'
})

const rules: FormInstance['rules'] = {
  username: [{ required: true, message: '请输入账号' }],
  password: [
    { required: true, message: '请输入密码' },
    { match: Regexp.Password, message: '输入密码格式不正确' }
  ]
}
//轮播图
const images = ref<string[]>([])
const getimg = async () => {
  images.value.push((await import('@/assets/svgs/1.svg')).default)
  images.value.push((await import('@/assets/svgs/2.svg')).default)
  images.value.push((await import('@/assets/svgs/3.svg')).default)
}
onMounted(() => {
  getimg()
})
const textdata = [
  '<strong>己所不欲🦖，勿施于人🐳</strong>',
  '<strong>大梦一场终须醒，神不渡我我自渡</strong>',
  '<strong>道阻且长，行则将至。</strong>'
]
// 记住密码
const checked = ref(false)
// 登录加载
const { loading, setLoading } = useLoading()
const errorMessage = ref('')

const FormRef = ref<FormInstance>()
// 点击登录
const login = async () => {
  try {
    const flag = await FormRef.value?.validate()
    if (flag) return
    setLoading(true)
    let logoData = { ...form }
    //密码MD5加密
    logoData.password = Md5.hashStr(form.password)
    await userStore.login(logoData)
    const { redirect, ...othersQuery } = router.currentRoute.value.query
    router.push({
      path: (redirect as string) || '/',
      query: {
        ...othersQuery
      }
    })
    Message.success('登录成功')
  } catch (error) {
    errorMessage.value = (error as Error).message
  } finally {
    setLoading(false)
  }
}
</script>

<style lang="scss" scoped>
.register-btn,
.register-btn:hover {
  color: var(--color-text-2);
}

.login-form-title {
  color: var(--color-text-1);
  font-weight: 500;
  font-size: 20px;
  line-height: 32px;
  margin-bottom: 20px;
  text-align: center;
  display: flex;
  justify-content: center;
  align-items: center;
  .logo {
    width: 50px;
    height: 50px;
    margin: 0px 10px 0px 0px;
  }
}

.theme-btn {
  position: fixed;
  top: 20px;
  left: 30px;
  z-index: 9999;
}

.login {
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  background-color: var(--color-bg-5);
  &-box {
    width: 86%;
    max-width: 850px;
    height: 430px;
    display: flex;
    z-index: 999;
    border-radius: 5px;
    box-shadow: 0 2px 4px 2px rgba(0, 0, 0, 0.08);
  }
}

.login-left {
  width: 100%;
  height: 100%;
  display: flex;
  justify-content: center;
  align-items: center;
  position: relative;
  overflow: hidden;
  background: linear-gradient(60deg, rgb(var(--primary-6), 0.8), rgb(var(--primary-3), 0.8));
  .login-img {
    width: 100%;
    height: 100%;
  }
}

.login-right {
  width: 100%;
  height: 100%;
  background: var(--color-bg-1);
  display: flex;
  justify-content: center;
  align-items: center;
  padding-top: 30px;
  box-sizing: border-box;
}
</style>
