import { store } from '../store'
import { updateToken } from '../Reducers/AuthReducer';
import axios from 'axios';
import { auht } from '../Models/Auth.response';

export async function login(email: string, password: string) {
    await axios.post<auht>("http://localhost:2137/api/auth/login", { email: email, password: password })
        .then((resonse) => {
            store.dispatch(updateToken(resonse.data.token))
        })
}