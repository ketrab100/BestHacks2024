import { createSlice, PayloadAction } from "@reduxjs/toolkit";

// Pobierz początkowy token z localStorage lub ustaw jako pusty string
const initialState: string = localStorage.getItem('token') ?? '';

export const authSlice = createSlice({
  name: 'auth',
  initialState,
  reducers: {
    updateToken: (state, action: PayloadAction<string>) => {
      // Zapisz token w localStorage
      localStorage.setItem('token', action.payload);
      // Zwróć nową wartość tokena jako nowy stan
      return action.payload;
    }
  }
});

// Exportuj akcję updateToken
export const { updateToken } = authSlice.actions;

// Exportuj reducer
export default authSlice.reducer;
