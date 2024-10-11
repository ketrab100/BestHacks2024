import { ChangeEvent, useState } from 'react'
import { login } from '../Api/Login'
import { store } from '../store'

function Login() {
  const [email, setEmail] = useState<string>('')
  const [password, setPassword] = useState<string>('')
  const [token, setToken] = useState<string>(store.getState().authReducer)

  function handleEmailChange(e: ChangeEvent<HTMLInputElement>) {
    setEmail(e.currentTarget.value)
  }

  function handlePasswordChange(e: ChangeEvent<HTMLInputElement>) {
    setPassword(e.currentTarget.value)
  }

  async function onLogin() {
    await login(email, password)
    setPassword('')
    setToken(store.getState().authReducer)
  }

  return (
    <div className={'container py-5'}>
      <div className={'row'}>
        <div className={'col-6'}>
          <h3>Login</h3>

          <div>
            <input
              type="email"
              placeholder="Email"
              value={email}
              onChange={handleEmailChange}
              className={'form-control my-1 row'}
            />
          </div>
          <div>
            <input
              type="password"
              placeholder="Password"
              value={password}
              onChange={handlePasswordChange}
              className={'form-control my-1 row'}
            />
          </div>
          <div>
            <button onClick={onLogin} className={'btn btn-primary row'}>
              Login
            </button>
          </div>
        </div>
        <div className={'col-6 text-break'}>{token}</div>
      </div>
    </div>
  )
}

export default Login
