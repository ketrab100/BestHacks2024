import { ChangeEvent, useState } from 'react';
import { register } from '../Api/Register';
import { store } from '../store';
import './Register.css';  // Import pliku CSS

function Register() {
  const [email, setEmail] = useState<string>('');
  const [nickname, setNickname] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [statusMessage, setStatusMessage] = useState<string>(''); // Nowy stan na komunikaty

  function handleEmailChange(e: ChangeEvent<HTMLInputElement>) {
    setEmail(e.currentTarget.value);
  }

  function handleNicknameChange(e: ChangeEvent<HTMLInputElement>) {
    setNickname(e.currentTarget.value);
  }

  function handlePasswordChange(e: ChangeEvent<HTMLInputElement>) {
    setPassword(e.currentTarget.value);
  }

  async function onRegister() {
    try {
      await register(email, nickname, password);
      setStatusMessage('Rejestracja zakończona sukcesem!'); // Komunikat o sukcesie
      setPassword('');
      setEmail('');  // Opcjonalnie: czyść inne pola po sukcesie
      setNickname('');
    } catch (error) {
      setStatusMessage('Rejestracja nie powiodła się. Spróbuj ponownie.'); // Komunikat o błędzie
    }
  }

  return (
    <div className="register-container">
      <div className="register-card">
        <h3 className="register-title">Zarejestruj się aby korzystać z LinkedER</h3>

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
            type="text"
            placeholder="Nickname"
            value={nickname}
            onChange={handleNicknameChange}
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
          <button onClick={onRegister} className="btn btn-primary">
            Zarejestruj
          </button>
        </div>

        {statusMessage && ( // Wyświetl komunikat o statusie
          <div className="alert alert-info mt-3">
            {statusMessage}
          </div>
        )}
      </div>
    </div>
  );
}

export default Register;
