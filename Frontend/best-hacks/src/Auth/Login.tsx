import { ChangeEvent, useState } from "react"
import { login } from "../Api/Login"
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
        <div >
            <h3>
                Login
            </h3>
            <div>
                <input type="email" placeholder="Email" value={email} onChange={handleEmailChange} />
            </div>
            <div>
                <input type="password" placeholder="Password" value={password} onChange={handlePasswordChange} />
            </div>
            <div>
                <button onClick={onLogin}>
                    Login
                </button>
            </div>
            <div>
                {token}
            </div>
        </div>
    )
}

export default Login