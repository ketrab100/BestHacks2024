// MockConversations.ts
import { Conversation, Match, Job, Employer, Employee } from "../Models/Interfaces";

const mockEmployer: Employer = {
    id: 'employerId',
    email: 'employer@example.com',
    userName: 'EmployerName',
    companyName: 'Company Inc.',
    contactName: 'John Doe',
    location: 'Location',
    createdAt: new Date(),
    jobs: [], // Możesz dodać mockowe oferty pracy, jeśli potrzebujesz
};

const mockEmployee: Employee = {
    id: 'employeeId',
    email: 'employee@example.com',
    userName: 'EmployeeName',
    firstName: 'Jane',
    lastName: 'Doe',
    bio: 'A dedicated worker.',
    location: 'Location',
    experienceLevel: 'Mid',
    createdAt: new Date(),
    userTags: [],
    matches: [],
};

const mockJob: Job = {
    id: 'jobId',
    jobTitle: 'Software Engineer',
    jobDescription: 'Develop awesome applications.',
    location: 'Remote',
    experienceLevel: 'Mid',
    createdAt: new Date(),
    employerId: mockEmployer.id,
    employer: mockEmployer,
    jobTags: [],
    matches: [],
};

const mockMatch: Match = {
    id: 'matchId',
    matchScore: 95,
    createdAt: new Date(),
    userId: mockEmployee.id,
    employee: mockEmployee,
    jobId: mockJob.id,
    job: mockJob,
    conversations: [],
};

const mockConversation: Conversation = {
    id: 'conversationId',
    message: 'Hello, how can I help you?',
    createdAt: new Date(),
    matchId: mockMatch.id,
    match: mockMatch,
    senderId: 'received', // Zakładamy, że to wiadomość od pracodawcy
    sender: mockEmployer,
};

export const getMockConversations = async (): Promise<Conversation[]> => {
    return [mockConversation]; // Zwracamy tylko jedną wiadomość od pracodawcy
};
