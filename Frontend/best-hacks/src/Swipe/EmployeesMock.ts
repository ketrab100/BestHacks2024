import {Employee} from "../Models/Interfaces";

export const employeesMock: Employee[] = [
    {
        id: '1',
        firstName: 'John',
        lastName: 'Doe',
        email: 'john.doe@example.com',
        bio: 'A seasoned software engineer with a focus on full-stack development. Passionate about clean code and architecture.',
        location: 'San Francisco, CA',
        experience: '10 years',
        imageUrl: 'https://randomuser.me/api/portraits/men/1.jpg',
        tags: [
            {id: '1', name: 'JavaScript'},
            {id: '2', name: 'React'},
            {id: '3', name: 'Node.js'}
        ]
    },
    {
        id: '2',
        firstName: 'Jane',
        lastName: 'Smith',
        email: 'jane.smith@example.com',
        bio: 'A creative front-end developer specializing in modern web technologies. Loves working with design teams to create beautiful UI.',
        location: 'New York, NY',
        experience: '8 years',
        imageUrl: 'https://randomuser.me/api/portraits/women/1.jpg',
        tags: [
            {id: '1', name: 'HTML'},
            {id: '2', name: 'CSS'},
            {id: '3', name: 'Vue.js'}
        ]
    },
    {
        id: '3',
        firstName: 'Alice',
        lastName: 'Johnson',
        email: 'alice.johnson@example.com',
        bio: 'A data scientist with a knack for machine learning and artificial intelligence. Experienced in Python and data analysis.',
        location: 'Austin, TX',
        experience: '5 years',
        imageUrl: 'https://randomuser.me/api/portraits/women/2.jpg',
        tags: [
            {id: '1', name: 'Python'},
            {id: '2', name: 'Machine Learning'},
            {id: '3', name: 'Data Analysis'}
        ]
    },
    {
        id: '4',
        firstName: 'Michael',
        lastName: 'Brown',
        email: 'michael.brown@example.com',
        bio: 'A DevOps engineer focused on building scalable cloud infrastructure. Skilled in AWS, Docker, and CI/CD pipelines.',
        location: 'Seattle, WA',
        experience: '7 years',
        imageUrl: 'https://randomuser.me/api/portraits/men/2.jpg',
        tags: [
            {id: '1', name: 'AWS'},
            {id: '2', name: 'Docker'},
            {id: '3', name: 'CI/CD'}
        ]
    }
];
