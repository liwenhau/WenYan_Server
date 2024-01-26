const Token_Key = 'token'
const Refresh_Token_Key = 'refreshToken'

const isLogin = () => {
  return !!localStorage.getItem(Token_Key)
}

const getToken = () => {
  return localStorage.getItem(Token_Key)
}

const getRefreshToken = () => {
  return localStorage.getItem(Refresh_Token_Key)
}

const setToken = (token: string) => {
  localStorage.setItem(Token_Key, token)
}

const setRefreshToken = (refreshToken: string) => {
  localStorage.setItem(Refresh_Token_Key, refreshToken)
}

const clearToken = () => {
  localStorage.removeItem(Token_Key)
}

const clearRefreshToken = () => {
  localStorage.removeItem(Refresh_Token_Key)
}

export { isLogin, getToken, getRefreshToken, setToken, setRefreshToken, clearToken, clearRefreshToken }
