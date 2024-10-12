import { ChangeEvent, useState } from 'react';
import { register } from '../Api/Register';
import { store } from '../store';
import './Register.css';  // Import pliku CSS

function Register() {
  const [email, setEmail] = useState<string>('');
  const [nickname, setNickname] = useState<string>('');
  const [password, setPassword] = useState<string>('');
  const [isEmployer, setIsEmployer] = useState<boolean>(false); // Nowy stan dla checkboxa
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

  function handleCheckboxChange(e: ChangeEvent<HTMLInputElement>) {
    setIsEmployer(e.currentTarget.checked); // Aktualizacja stanu checkboxa
  }

  async function onRegister() {
    try {
      await register(email, nickname, password, isEmployer); // Przekazanie nowej wartości
      setStatusMessage('Rejestracja zakończona sukcesem!');
      setPassword('');
      setEmail('');
      setNickname('');
      setIsEmployer(false); // Reset checkboxa
    } catch (error) {
      setStatusMessage('Rejestracja nie powiodła się. Spróbuj ponownie.');
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

        <div className="mb-3">
          <input
            type="checkbox"
            id="isEmployer"
            checked={isEmployer}
            onChange={handleCheckboxChange}
          />
          <label htmlFor="isEmployer" className="ms-2">Jestem firmą</label>
        </div>

        <div className="d-grid gap-2">
          <button onClick={onRegister} className="btn btn-primary">
            Zarejestruj
          </button>
        </div>

        {statusMessage && (
          <div className="alert alert-info mt-3">
            {statusMessage}
          </div>
        )}
      </div>
    </div>
  );
}

export default Register;
