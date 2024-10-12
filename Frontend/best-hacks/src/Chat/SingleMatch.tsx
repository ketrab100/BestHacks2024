import React from "react";
import "./SingleMatch.css";
import { navigate } from "../Utils/TopBar";

export interface SingleMatchProps {
    id?: string;
    name: string;
    skills: string[];
    position: string;
}

const SingleMatch = (props: SingleMatchProps) => {
    return (
        <div className="conversation-card" onClick={() => navigate(`/chat/${props.id}`)}>
            <div className="conversation-info">
                <h2 className="conversation-name">{props.name}</h2>
                <p className="conversation-position">Position: {props.position}</p>
                <div className="conversation-interests">
                    <span>Interests:</span>
                    <div className="interests-container">
                        {props.skills.map((skill, index) => (
                            <span key={index}>{skill}</span>
                        ))}
                    </div>
                </div>
            </div>
        </div>
    );
};

export default SingleMatch;