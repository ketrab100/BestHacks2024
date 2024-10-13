import axios from 'axios';
import { Conversation } from '../Models/Interfaces';

export async function AddConversation(conversation: Conversation) {
    try {
        const token = localStorage.getItem('token'); // Pobierz token z localStorage
        if (!token) {
            throw new Error("No token found, please log in.");
        }
        await axios.post("http://localhost:2137/api/conversations", conversation,
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
