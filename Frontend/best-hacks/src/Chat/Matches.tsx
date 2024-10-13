import React, {useEffect, useState} from "react";
import './Matches.css'
import SingleMatch, {SingleMatchProps} from "./SingleMatch";
import {getMyData} from "../Api/Match";
import {Match} from "../Models/Interfaces";

export default function Matches() {

    const [matchess, setMatches] = useState<Match[]>([])

    let matches: SingleMatchProps[] = [
        // {
        //     name: 'Michał',
        //     skills: ['JS,', 'TS,', 'Angular'],
        //     position: 'Frontend Dev Senior',
        //     id: 'sdasd'
        // },
        // {
        //     name: 'Jan',
        //     skills: ['C++,', 'C'],
        //     position: 'C++ Dev Senior',
        //     id: 'sdasd'
        // },
        // {
        //     name: 'Maria',
        //     skills: ['Python,', 'Django,', 'Flask'],
        //     position: 'Backend Dev Junior',
        //     id: 'sdasd'
        // },
        // {
        //     name: 'Franciszek',
        //     skills: ['Talent Aquisition', 'HR'],
        //     position: 'HR specialist regular',
        //     id: 'sdasd'
        // }
    ]

    useEffect(() => {
        const getData = async () => {
            try {
                const res = await getMyData();

                matches = res.map(x => {
                    return {
                        name: x.employer?.companyName,
                        skills: x.employer?.tags.map((y: { name: string }) => y.name),
                        position: x.employer?.email,
                        id: x.employer?.id
                    };
                }) as SingleMatchProps[];

                console.log(matches);
            } catch (error) {
                console.error("Failed to fetch employee data:", error);
            }
        };
        getData();
    }, []);

    matches = [
        {
            name: 'Michał',
            skills: ['JS,', 'TS,', 'Angular'],
            position: 'Frontend Dev Senior',
            id: '1'
        },
        {
            name: 'Jan',
            skills: ['C++,', 'C'],
            position: 'C++ Dev Senior',
            id: '2'
        },
        {
            name: 'Maria',
            skills: ['Python,', 'Django,', 'Flask'],
            position: 'Backend Dev Junior',
            id: '3'
        },
        {
            name: 'Franciszek',
            skills: ['Talent Aquisition', 'HR'],
            position: 'HR specialist regular',
            id: '4'
        }
    ]

    return (
        <div className="container">
            {matches.map((match) => {
                return (<SingleMatch {...match} />)
            })}
        </div>
    )
}