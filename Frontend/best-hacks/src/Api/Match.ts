import axios from "axios";
import {Match} from "../Models/Interfaces";

export async function getMyData(): Promise<Match[]> {
    try {

        const token = localStorage.getItem('token'); // Pobierz token z localStorage
        if (!token) {
            throw new Error("No token found, please log in.");
        }
        const response = await axios.get<Match[]>("http://localhost:2137/api/matches/me",
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