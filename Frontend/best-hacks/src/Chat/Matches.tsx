import React from "react";
import './Matches.css'
import SingleMatch, { SingleMatchProps } from "./SingleMatch";

export default function Matches() {

    const matches: SingleMatchProps[] = [
        {
            name: 'Jan',
            skills: ['JS', 'TS', 'Angular'],
            position: 'Frontend Dev Senior',
            id: 'sdasd'
        },
        {
            name: 'Jan',
            skills: ['JS', 'TS', 'Angular'],
            position: 'Frontend Dev Senior',
            id: 'sdasd'
        },
        {
            name: 'Jan',
            skills: ['JS', 'TS', 'Angular'],
            position: 'Frontend Dev Senior',
            id: 'sdasd'
        },
        {
            name: 'Jan',
            skills: ['JS', 'TS', 'Angular'],
            position: 'Frontend Dev Senior',
            id: 'sdasd'
        },
        {
            name: 'Jan',
            skills: ['JS', 'TS', 'Angular'],
            position: 'Frontend Dev Senior',
            id: 'sdasd'
        },
        {
            name: 'Jan',
            skills: ['JS', 'TS', 'Angular'],
            position: 'Frontend Dev Senior',
            id: 'sdasd'
        },
        {
            name: 'Jan',
            skills: ['JS', 'TS', 'Angular'],
            position: 'Frontend Dev Senior',
            id: 'sdasd'
        },
        {
            name: 'Jan',
            skills: ['JS', 'TS', 'Angular'],
            position: 'Frontend Dev Senior',
            id: 'sdasd'
        },
        {
            name: 'Jan',
            skills: ['JS', 'TS', 'Angular'],
            position: 'Frontend Dev Senior',
            id: 'sdasd'
        },
        {
            name: 'Jan',
            skills: ['JS', 'TS', 'Angular'],
            position: 'Frontend Dev Senior',
            id: 'sdasd'
        },
        {
            name: 'Jan',
            skills: ['JS', 'TS', 'Angular'],
            position: 'Frontend Dev Senior',
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