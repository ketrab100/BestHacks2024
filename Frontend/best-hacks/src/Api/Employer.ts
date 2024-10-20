import axios from 'axios';
import { Employer } from '../Models/Interfaces';

export async function getMyData() : Promise<Employer> {
    try {

        const token = localStorage.getItem('token'); // Pobierz token z localStorage
        if (!token) {
            throw new Error("No token found, please log in.");
        }
        const response = await axios.post("http://localhost:2137/api/employers/me",
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

export async function getNextEmployer() : Promise<Employer> {
    try {

        const token = localStorage.getItem('token'); // Pobierz token z localStorage
        if (!token) {
            throw new Error("No token found, please log in.");
        }
        const response = await axios.post("http://localhost:2137/api/employers/next",
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

export async function updateEmployer(employer: Employer) {
    try {

        const token = localStorage.getItem('token'); // Pobierz token z localStorage
        if (!token) {
            throw new Error("No token found, please log in.");
        }
        const response = await axios.put("http://localhost:2137/api/employers", employer,
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
