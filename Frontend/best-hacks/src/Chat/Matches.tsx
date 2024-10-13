import React from "react";
import './Matches.css'
import SingleMatch, { SingleMatchProps } from "./SingleMatch";

export default function Matches() {

    const matches: SingleMatchProps[] = [
        {
            name: 'Michał',
            skills: ['JS,', 'TS,', 'Angular'],
            position: 'Frontend Dev Senior',
            id: 'sdasd'
        },
        {
            name: 'Jan',
            skills: ['C++,', 'C'],
            position: 'C++ Dev Senior',
            id: 'sdasd'
        },
        {
            name: 'Maria',
            skills: ['Python,', 'Django,', 'Flask'],
            position: 'Backend Dev Junior',
            id: 'sdasd'
        },
        {
            name: 'Franciszek',
            skills: ['Talent Aquisition', 'HR'],
            position: 'HR specialist regular',
            id: 'sdasd'
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