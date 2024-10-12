// import React, { useEffect, useRef, useState } from 'react';
// import './Chat.css'; // Importuj stylów
// import { fetchConversations } from '../Api/Chat';
// import { Conversation, Match } from '../Models/Interfaces';
// import { useParams } from 'react-router-dom';

// const Chat = () => {
//     const { matchId } = useParams<{ matchId: string }>(); // Uzyskaj matchId z parametrów URL
//     const [conversations, setConversations] = useState<Conversation[]>([]);
//     const [newMessage, setNewMessage] = useState<string>(''); // Stan dla nowej wiadomości
//     const messagesEndRef = useRef<HTMLDivElement | null>(null); // Ref do przewijania

//     useEffect(() => {
//         const loadConversations = async () => {
//             try {
//                 const data = await fetchConversations(matchId ?? "defaultId");
//                 setConversations(data);
//             } catch (error) {
//                 console.error("Error fetching conversations:", error);
//             }
//         };

//         loadConversations();
//     }, [matchId]);

//     // Funkcja do przewijania na dół
//     const scrollToBottom = () => {
//         if (messagesEndRef.current) {
//             messagesEndRef.current.scrollIntoView({ behavior: "smooth" });
//         }
//     };

//     useEffect(() => {
//         scrollToBottom(); // Przewiń na dół po załadowaniu konwersacji
//     }, [conversations]); // Zaktualizuj, gdy konwersacje się zmieniają

//     const handleSendMessage = () => {
//         if (newMessage.trim()) {
//             // Użyj mocków do stworzenia nowego Match i Conversation
//             const newMatch: Match = {
//                 id: '1', // Unikalny identyfikator
//                 matchScore: 100, // Możesz ustawić dowolny wynik dopasowania
//                 createdAt: new Date(),
//                 userId: 'userId', // Ustaw na odpowiednie ID użytkownika
//                 employee: {
//                     id: 'employeeId', // Ustaw na odpowiednie ID pracownika
//                     email: 'employee@example.com',
//                     userName: 'EmployeeName',
//                     firstName: 'Jane',
//                     lastName: 'Doe',
//                     bio: 'A dedicated worker.',
//                     location: 'Location',
//                     experienceLevel: 'Mid',
//                     createdAt: new Date(),
//                     userTags: [],
//                     matches: [],
//                 },
//                 jobId: 'jobId', // Ustaw na odpowiednie ID oferty pracy
//                 job: {
//                     id: 'jobId',
//                     jobTitle: 'Software Engineer',
//                     jobDescription: 'Develop awesome applications.',
//                     location: 'Remote',
//                     experienceLevel: 'Mid',
//                     createdAt: new Date(),
//                     employerId: 'employerId', // Ustaw na odpowiednie ID pracodawcy
//                     employer: {
//                         //id: 'employerId',
//                         //email: 'employer@example.com',
//                         userName: 'EmployerName',
//                         companyName: 'Company Inc.',
//                         contactName: 'John Doe',
//                         location: 'Location',
//                         createdAt: new Date(),
//                         jobs: [],
//                     },
//                     jobTags: [],
//                     matches: [],
//                 },
//                 conversations: [], // Na początku nie ma żadnych konwersacji
//             };

//             const newConversation: Conversation = {
//                 id: '1', // Unikalny identyfikator dla wiadomości
//                 message: newMessage,
//                 createdAt: new Date(),
//                 matchId: newMatch.id, // Używamy nowego matchId
//                 match: newMatch, // Używamy nowo utworzonego obiektu Match
//                 senderId: 'sent', // Zakładamy, że wiadomość jest wysłana
//                 // sender: {
//                 //     id: 'yourUserId',
//                 //     email: 'youremail@example.com',
//                 //     userName: 'You',
//                 // },
//             };

//             setConversations((prev) => [...prev, newConversation]);
//             setNewMessage(''); // Resetuj pole tekstowe
//             scrollToBottom();
//         }
//     };

//     const handleKeyPress = (e: React.KeyboardEvent<HTMLInputElement>) => {
//         if (e.key === 'Enter') {
//             handleSendMessage();
//         }
//     };

//     return (
//         <div className="chat-container">
//             <div className="chat-messages">
//                 {conversations.map((conversation) => (
//                     <div key={conversation.id} className={`chat-message ${conversation.senderId === 'sent' ? 'sent' : 'received'}`}>
//                         {conversation.senderId !== 'sent' && (
//                             <img
//                                 src="https://via.placeholder.com/40"
//                                 alt="Avatar"
//                                 className="chat-avatar"
//                             />
//                         )}
//                         <div className="message-content">
//                             <p>{conversation.message}</p>
//                             <span className="message-date">{new Date(conversation.createdAt).toLocaleDateString('pl-PL')}</span>
//                         </div>
//                         {conversation.senderId === 'sent' && (
//                             <img
//                                 src="https://via.placeholder.com/39"
//                                 alt="Avatar"
//                                 className="chat-avatar"
//                             />
//                         )}
//                     </div>
//                 ))}
//                 <div ref={messagesEndRef} /> {/* Element do przewijania */}
//             </div>
//             <div className="message-input">
//                 <input
//                     type="text"
//                     className="input-field"
//                     placeholder="Wpisz wiadomość..."
//                     value={newMessage}
//                     onChange={(e) => setNewMessage(e.target.value)}
//                     onKeyPress={handleKeyPress}
//                 />
//                 <button className="send-button" onClick={handleSendMessage}>Wyślij</button>
//             </div>
//         </div>
//     );
// };

// export default Chat;

export{}