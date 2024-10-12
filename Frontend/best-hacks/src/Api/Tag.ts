import axios from "axios";
import { Tag } from "../Models/Interfaces";

export async function getMyData() : Promise<Tag[]> {
    try {

        const token = localStorage.getItem('token'); // Pobierz token z localStorage
        if (!token) {
            throw new Error("No token found, please log in.");
        }
        const response = await axios.post<Tag[]>("http://localhost:2137/api/Tags",
        {
            headers: {
                'Authorization': `Bearer ${token}` // Dodaj token do nagłówka
            }
        }
    );
    return response.data;
    } catch (error) {
        console.error("Error:", error);
        throw error;
    }
}