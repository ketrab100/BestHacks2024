import {store} from '../store'
import {updateToken} from '../Reducers/AuthReducer';
import axios from 'axios';
import {auth} from '../Models/Interfaces';

export async function login(email: string, password: string) {
    await axios.post<auth>("http://localhost:2137/api/auth/login", {email: email, password: password})
        .then((response) => {
            store.dispatch(updateToken(response.data.token))
        }).catch((error) => {

        })
}