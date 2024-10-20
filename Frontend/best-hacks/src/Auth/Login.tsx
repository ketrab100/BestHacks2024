import { ChangeEvent, useEffect, useState } from 'react';
import { login } from '../Api/Login';
import { store } from '../store';
import './Login.css'; // Import nowego pliku CSS

function Login() {
  const [email, setEmail] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [token, setToken] = useState<string | null>(null); // Używamy null jako domyślnej wartości

  useEffect(() => {
    // Zaktualizuj token za każdym razem, gdy się zmienia w Reduxie
    const unsubscribe = store.subscribe(() => {
      const state = store.getState();
      setToken(state.authReducer.token); // Ustaw token z Redux
    });

    // Zwróć funkcję czyszczącą
    return () => unsubscribe();
  }, []);

  function handleEmailChange(e: ChangeEvent<HTMLInputElement>) {
    setEmail(e.currentTarget.value);
  }

  function handlePasswordChange(e: ChangeEvent<HTMLInputElement>) {
    setPassword(e.currentTarget.value);
  }

  async function onLogin() {
    await login(email, password);
    setPassword(''); // Czyścimy hasło po próbie logowania
  }

  return (
    <div className="login-container">
      <div className="login-card">
        <h3 className="login-title">Witaj w LinkedER! Zaloguj się aby kontynuować</h3>

        <div className="mb-3">
          <input
            type="email"
            placeholder="Email"
            value={email}
            onChange={handleEmailChange}
            className="form-control"
          />
        </div>

        <div className="mb-3">
          <input
            type="password"
            placeholder="Password"
            value={password}
            onChange={handlePasswordChange}
            className="form-control"
          />
        </div>

        <div className="d-grid gap-2">
          <button onClick={onLogin} className="btn btn-primary">
            Login
          </button>
        </div>

        {token && (
          <div className="alert alert-success">
            Token: {token}
          </div>
        )}
      </div>
    </div>
  );
}

export default Login;
