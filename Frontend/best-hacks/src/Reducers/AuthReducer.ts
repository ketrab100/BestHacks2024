// export default function auth(state = localStorage.getItem('token'), action: any) {
//     switch (action.type) {
//         case 'UPDATE_TOKEN':
//             localStorage.setItem('token', action.payload)
//             return state = action.payload
//         default:
//             return state
//     }
// }

import { Action, createSlice, PayloadAction } from "@reduxjs/toolkit";

const initialState: string = localStorage.getItem('token') ?? ''


export const authSlice = createSlice({
    name: 'auth',
    initialState,
    reducers: {
        updateToken: (state, action: PayloadAction<string>) => {
            localStorage.setItem('token', action.payload)
            return action.payload
        }
    }
})
export const { updateToken } = authSlice.actions;

export default authSlice.reducer