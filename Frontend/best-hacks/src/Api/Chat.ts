// import { Conversation } from "../Models/Interfaces";
// import { getMockConversations } from "../Models/MockConversations";

// const API_URL = 'http://localhost:2137/matchers';

// export const fetchConversations = async (matchId: string): Promise<Conversation[]> => {
//     // Używamy mockowych danych na czas rozwoju backendu
//     return await getMockConversations();

//     // Możesz też używać prawdziwego API, jeśli jest dostępne:
//     /*
//     try {
//         const response = await fetch(`${API_URL}/${matchId}`);
//         if (!response.ok) {
//             throw new Error('Błąd w odpowiedzi z API');
//         }
//         const data: Conversation[] = await response.json();
//         return data;
//     } catch (error) {
//         console.error("Błąd podczas ładowania wiadomości:", error);
//         throw error; // Rzuć błąd dalej, aby można go było obsłużyć w komponencie
//     }
//     */
// };

export{}