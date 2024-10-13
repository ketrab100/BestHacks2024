import React, { useEffect, useRef, useState } from 'react';
import './Chat.css'; // Importuj stylów
import { Conversation } from '../Models/Interfaces';
import { useParams } from 'react-router-dom';
import { getMockConversations } from '../Models/MockConversations';

const Chat = () => {
    const { matchId } = useParams<{ matchId: string }>(); // Uzyskaj matchId z parametrów URL
    const [conversations, setConversations] = useState<Conversation[]>([]);
    const [newMessage, setNewMessage] = useState<string>(''); // Stan dla nowej wiadomości
    const messagesEndRef = useRef<HTMLDivElement | null>(null); // Ref do przewijania

    useEffect(() => {
        const loadConversations = async () => {
            try {
                const data = await getMockConversations();
                setConversations(data);
            } catch (error) {
                console.error("Error fetching conversations:", error);
            }
        };

        loadConversations();
    }, [matchId]);

    // Funkcja do przewijania na dół
    const scrollToBottom = () => {
        if (messagesEndRef.current) {
            messagesEndRef.current.scrollIntoView({ behavior: "smooth" });
        }
    };

    useEffect(() => {
        scrollToBottom(); // Przewiń na dół po załadowaniu konwersacji
    }, [conversations]); // Zaktualizuj, gdy konwersacje się zmieniają

    const handleSendMessage = () => {
        if (newMessage.trim()) {
            const newConversation: Conversation = {
                id: (conversations.length + 1).toString(), // Unikalny identyfikator dla wiadomości
                message: newMessage,
                createdAt: new Date(),
                authorId: '2' // Zakładamy, że autor to użytkownik
            };

            setConversations((prev) => [...prev, newConversation]);
            setNewMessage(''); // Resetuj pole tekstowe
            scrollToBottom();
        }
    };

    const handleKeyPress = (e: React.KeyboardEvent<HTMLInputElement>) => {
        if (e.key === 'Enter') {
            handleSendMessage();
        }
    };

    return (
        <div className="chat-container">
            <div className="chat-messages">
                {conversations.map((conv) => (
                    <div key={conv.id} className={`chat-message ${conv.authorId === '1' ? 'employer' : 'user'}`}>
                        <p>{conv.message}</p>
                        <span className="message-date">{conv.createdAt.toLocaleString()}</span> {/* Wyświetlanie daty */}
                    </div>
                ))}
                <div ref={messagesEndRef} />
            </div>
            <div className="chat-input-container">
                <input
                    type="text"
                    value={newMessage}
                    onChange={(e) => setNewMessage(e.target.value)}
                    onKeyPress={handleKeyPress}
                    placeholder="Napisz wiadomość..."
                />
                <button onClick={handleSendMessage}>Wyślij</button>
            </div>
        </div>
    );
};

export default Chat;
