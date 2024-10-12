import axios from 'axios';
import { Employee } from '../Models/Interfaces';

export async function getMyData() : Promise<Employee> {
    try {

        const token = localStorage.getItem('token'); // Pobierz token z localStorage
        if (!token) {
            throw new Error("No token found, please log in.");
        }
        const response = await axios.post("http://localhost:2137/api/employees/me",
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

export async function getNextEmployee() : Promise<Employee> {
    try {

        const token = localStorage.getItem('token'); // Pobierz token z localStorage
        if (!token) {
            throw new Error("No token found, please log in.");
        }
        const response = await axios.post("http://localhost:2137/api/employees/next",
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

export async function updateEmployee(employee: Employee) {
    try {

        const token = localStorage.getItem('token'); // Pobierz token z localStorage
        if (!token) {
            throw new Error("No token found, please log in.");
        }
        const response = await axios.put("http://localhost:2137/api/employees", employee,
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
