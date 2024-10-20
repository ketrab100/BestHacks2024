// AuthReducer.ts
import { createSlice } from '@reduxjs/toolkit';

interface AuthState {
    token: string | null;
    role: string | null; // Dodaj pole role
}

const initialState: AuthState = {
    token: null,
    role: null, // Domyślna wartość dla roli
};

const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        updateAuth: (state, action) => {
            state.token = action.payload.token;
            state.role = action.payload.role;
            // Zapisz token i rolę w localStorage (opcjonalnie)
            localStorage.setItem('token', action.payload.token);
            localStorage.setItem('role', action.payload.role);
        },
        clearAuth: (state) => {
            state.token = null;
            state.role = null;
            localStorage.removeItem('token');
            localStorage.removeItem('role');
        },
    },
});

export const { updateAuth, clearAuth } = authSlice.actions;
export default authSlice.reducer;
