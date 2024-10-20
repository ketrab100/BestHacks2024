// MockConversations.ts
import { Conversation} from "../Models/Interfaces";

const mockConversation: Conversation = {
    id: '1',
    message: 'Cześć, chcielibyśmy Cię zaprosić na rozmowę rekrutacyjną :)',
    createdAt: new Date(),
    authorId: '2'
};

export const getMockConversations = async (): Promise<Conversation[]> => {
    return [mockConversation];
};
