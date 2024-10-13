import axios from 'axios';

export async function AddConversation(addConversation: typeof AddConversation) {
    try {
        const token = localStorage.getItem('token'); // Pobierz token z localStorage
        if (!token) {
            throw new Error("No token found, please log in.");
        }
        await axios.post("http://localhost:2137/api/matches/conversations", addConversation,
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
