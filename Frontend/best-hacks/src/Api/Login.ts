import { store } from '../store';
import { updateAuth } from '../Reducers/AuthReducer';
import axios from 'axios';
import { auth } from '../Models/Interfaces'; // Załóżmy, że auth zawiera teraz także pole role

export async function login(email: string, password: string) {
    try {
        const response = await axios.post<auth>("http://localhost:2137/api/auth/login", {
            email: email,
            password: password,
        });

        // Zakładamy, że odpowiedź zawiera token i rolę użytkownika
        const { token, role } = response.data;

        // Dispatchujemy zarówno token, jak i rolę do Redux
        store.dispatch(updateAuth({ token, role }));
    } catch (error) {
        console.error("Login error:", error);
        // Tutaj możesz dodać obsługę błędów (np. wyświetlenie komunikatu)
    }
}
