// git
export interface auth {
    isAuthSuccessful: boolean,
    errorMessage: string,
    token: string,
    role: string
}

// git
// GET na /employees/me
// taki sam /employees/next
// taki sam PUT
export interface Employee {
    id: string;
    firstName: string;
    lastName: string;
    email: string;
    bio: string;
    location: string;
    experience: string;
    imageBase64: string;
    tags: Tag[];
}

// git
// GET na /employers/me
// taki sam na /employers/next
// taki sam PUT
export interface Employer {
    id: string;
    companyName: string;
    email: string;
    bio: string;
    location: string;
    imageUrl: string;
    tags: Tag[];
}

// git
// zawarte w employer i employee
// GET /tags -> zwraaca wszystkie tagi
export interface Tag {
    id: string;
    name: string;
}

// git
// GET na /matches -> zwraca dla mojego id
export interface Match {
    createdAt: Date;
    employee?: Employee;
    employer?: Employer;
    conversations: Conversation[];
}

// git
// zawarte w matchach, nie trzeba GET
export interface Conversation {
    id: string;
    message: string;
    createdAt: Date;
    authorId: string;
}

// POST na coś
export interface SwipeInfo{
    userId: string;
    swipedId: string;
    SwipeResult: boolean;
}

// POST na /conversations
export interface AddConversation{
    matchId: string;
    message: string;
}