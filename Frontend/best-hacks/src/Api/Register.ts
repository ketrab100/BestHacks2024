import axios from 'axios';
import { auth } from '../Models/Interfaces';

export async function register(email: string, nickname: string, password: string, IsEmployee: boolean) {
  await axios
    .post<auth>("http://localhost:2137/api/Auth/Register", {
      email: email,
      nickname: nickname,
      password: password,
      isEmployee: IsEmployee
    })
    .catch((error) => {
      console.error("Registration failed", error);
    });
}
