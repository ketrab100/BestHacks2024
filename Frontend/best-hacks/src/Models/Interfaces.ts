export interface auth {
    isAuthSuccessful: boolean,
    errorMessage: string,
    token: string
}

export interface Employee {
    firstName: string;
    lastName: string;
    email: string;
    bio: string;
    location: string;
    experience: string;
    userTags: UserTag[];
}

export interface Employer {
    companyName: string;
    contactName: string;
    location: string;
    createdAt: Date;
    jobs: Job[];
}

export interface EmployeeProfileDto {
    firstName?: string;
    lastName?: string;
    bio?: string;
    location?: string;
    experience?: string;
    tags: Tag[];
  }

export interface Job {
    id: string;
    jobTitle: string;
    jobDescription: string;
    location: string;
    experienceLevel: string;
    createdAt: Date;
    employerId: string;
    employer: Employer;
    jobTags: JobTag[];
    matches: Match[];
}

export interface Tag {
    id: string;
    name: string;
    userTags: UserTag[];
    jobTags: JobTag[];
}

export interface UserTag {
    userId: string;
    user: Employee;
    tagId: string;
    tag: Tag;
}

export interface JobTag {
    jobId: string;
    job: Job;
    tagId: string;
    tag: Tag;
}

export interface Match {
    id: string;
    matchScore: number;
    createdAt: Date;
    userId: string;
    employee: Employee;
    jobId: string;
    job: Job;
    conversations: Conversation[];
}

export interface Conversation {
    id: string;
    message: string;
    createdAt: Date;
    matchId: string;
    match: Match;
    senderId: string;
    //sender: User;
}
