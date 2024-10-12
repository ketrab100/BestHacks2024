import axios from 'axios';
import { updateToken } from '../Reducers/AuthReducer';
import { auth } from '../Models/Interfaces';
import { store } from '../store';

export async function register(email: string, nickname: string, password: string) {
  await axios
    .post<auth>("http://localhost:2137/api/Auth/Register", {
      email: email,
      nickname: nickname,
      password: password,
    })
    .then((response) => {
      store.dispatch(updateToken(response.data.token));
    })
    .catch((error) => {
      console.error("Registration failed", error);
    });
}
