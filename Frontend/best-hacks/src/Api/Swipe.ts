import axios from 'axios';
import { SwipeInfo } from '../Models/Interfaces';

export async function AddSwipeInfo(swipeInfo : SwipeInfo) {
    try {
        const token = localStorage.getItem('token'); // Pobierz token z localStorage
        if (!token) {
            throw new Error("No token found, please log in.");
        }
        await axios.post("http://localhost:2137/api/swipes", swipeInfo,
        {
            headers: {
                'Authorization': `Bearer ${token}` // Dodaj token do nagłówka
            }
        }
    );
    } catch (error) {
        console.error("Error:", error);
    }
}
