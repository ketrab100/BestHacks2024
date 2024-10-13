import React from "react";
import './SwipeScreen.css';
import arrow_down from '../images/arrow-down.svg'
import {Employee} from "../Models/Interfaces";
import {store} from "../store";
import {getNext} from "../Reducers/EmployeeReducer";

const state = store.getState();

let employee: Employee = state.employeeReducer.currentEmployee

function SwipeScreen() {
    const [bgColor, setBgColor] = React.useState("#f0f8ff");
    const touchStartX = React.useRef(0); // Przechowuje pozycję X początku dotyku
    const touchEndX = React.useRef(0);   // Przechowuje pozycję X końca dotyku
    const touchStartY = React.useRef(0); // Przechowuje pozycję X początku dotyku
    const touchEndY = React.useRef(0);   // Przechowuje pozycję X końca dotyku
    const previousJobs: { jobName: string, position: string }[] = [
        {
            jobName: 'Nokia',
            position: 'Junior C++ Dev'
        },
        {
            jobName: 'Tietoevry',
            position: 'Regular C++ Dev'
        },
        {
            jobName: 'Watykan Code Solutions',
            position: 'Junior Frontend Dev'
        },
        {
            jobName: 'Kod-pol',
            position: 'Regular Frontend Dev'
        }
    ]
    const skills: { technology: string }[] = [
        { technology: 'C++' },
        { technology: 'C' },
        { technology: 'GitHub' },
        { technology: 'Dodge' },
        { technology: 'JeDzie' },
        { technology: 'Dodge' }
    ]
    const lookingFor: { position: string, level: string }[] = [
        {
            position: 'Frontend Developer',
            level: 'Senior'
        },
        {
            position: 'Fullstack Developer',
            level: 'Junior'
        }
    ]
    const description: string = 'Lorem ipsum dolor sit amet, consectetur adipiscing elit. Ut posuere odio nec laoreet rhoncus. Cras eget fringilla elit, ut semper massa. Nunc congue sodales nunc, egestas volutpat odio. Sed ac elementum purus, sed faucibus augue. Aliquam lobortis sem felis, non consequat ligula condimentum vel. Phasellus vel ligula at massa malesuada dictum. Nullam eget blandit purus, nec cursus augue. Curabitur in eros rhoncus, dignissim nisl facilisis, feugiat purus.'
    const email: string = 'essa@gmail.com'

    const handleTouchStart = (e: React.TouchEvent<HTMLDivElement>) => {
        touchStartX.current = e.touches[0].clientX;
        touchStartY.current = e.touches[0].clientY;
    };

    const handleTouchEnd = () => {
        if (Math.abs(touchStartX.current - touchEndX.current) < Math.abs(touchStartY.current - touchEndY.current))
            return
        if (touchStartX.current > touchEndX.current + 50) {
            // Przesunięcie w lewo
            setBgColor("#faebd7"); // Zmieniamy kolor na inny
            store.dispatch(getNext());
        } else if (touchStartX.current < touchEndX.current - 50) {
            // Przesunięcie w prawo
            setBgColor("#add8e6"); // Zmieniamy kolor na jeszcze inny
            store.dispatch(getNext());
        }
        employee = store.getState().employeeReducer.employees[store.getState().employeeReducer.index]
    };

    const handleTouchMove = (e: React.TouchEvent<HTMLDivElement>) => {
        touchEndX.current = e.touches[0].clientX;
        touchEndY.current = e.touches[0].clientY;
    };
    return (
        <div className="scroll-container"
             onTouchStart={handleTouchStart}
             onTouchMove={handleTouchMove}
             onTouchEnd={handleTouchEnd}
             style={{backgroundColor: bgColor}}>
            <section className="snap-section first-section">
                <div className="profile-pic" style={{ backgroundImage: `url(${employee.imageUrl})` }} />
                <span className="credentials"><span className="name">Jan</span> Kowalski</span>
                <div className="tags-container">
                    {skills.slice(0, 5).map((skill) => {
                        return <div className="tag">
                            {skill.technology}
                        </div>
                    })}
                </div>
                <img className="arrow-down" src={arrow_down}></img>
            </section>
            <section className="snap-section second-section">
                <div className="description">
                    <span className="title">
                        Description:
                    </span>
                    <span>{employee.bio}</span>
                </div>
                <div className="description">
                    <span className="title">
                        Email:
                    </span>
                    <span>{email}</span>
                </div>
            </section>
        </div>
    )
}

export default SwipeScreen