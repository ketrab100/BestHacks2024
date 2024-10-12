import React from "react";
import './SwipeScreen.css';
import image from '../images/image_prof.png'

function SwipeScreen() {
    const [bgColor, setBgColor] = React.useState("#f0f8ff");
    const touchStartX = React.useRef(0); // Przechowuje pozycję X początku dotyku
    const touchEndX = React.useRef(0);   // Przechowuje pozycję X końca dotyku
    const touchStartY = React.useRef(0); // Przechowuje pozycję X początku dotyku
    const touchEndY = React.useRef(0);   // Przechowuje pozycję X końca dotyku
    const previousJobs: { jobName: string, position: string }[] = [
        {
            jobName: 'Watykan Code Solutions',
            position: 'Junior Frontend Dev'
        },
        {
            jobName: 'Kod-pol',
            position: 'Regular Frontend Dev'
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
        { technology: 'JeDzie' },
        { technology: 'Dodge' },
        { technology: 'JeDzie' },
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
        } else if (touchStartX.current < touchEndX.current - 50) {
            // Przesunięcie w prawo
            setBgColor("#add8e6"); // Zmieniamy kolor na jeszcze inny
        }
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
            style={{ backgroundColor: bgColor }}>
            <section className="snap-section first-section">
                <div className="profile-pic" style={{ backgroundImage: `url(${image})` }} />
                <span className="credentials">Jan Kowalski</span>
                <div className="summary">
                    <ul className="career" aria-label="Career Summary">
                        {previousJobs.slice(0, 2).map((job) => {
                            return <li>
                                {job.jobName} | {job.position}
                            </li>
                        })}
                    </ul>
                    <ul className="skills" aria-label="Skills">
                        {skills.slice(0, 2).map((skill) => {
                            return <li>
                                {skill.technology}
                            </li>
                        })}
                    </ul>
                    <div className="looking-for">
                        <span className="title">Interested in:</span>
                        {lookingFor.map((proposition) => {
                            return <span>
                                {proposition.position} | {proposition.level}
                            </span>
                        })}
                    </div>
                </div>
            </section>
            <section className="snap-section second-section">
                <div className="description">
                    <span className="title">
                        Description:
                    </span>
                    <span>{description}</span>
                </div>
                <ul className="career" aria-label="Full career summary">
                        {previousJobs.map((job) => {
                            return <li>
                                {job.jobName} | {job.position}
                            </li>
                        })}
                    </ul>
                    <ul className="skills" aria-label="All skills">
                        {skills.map((skill) => {
                            return <li>
                                {skill.technology}
                            </li>
                        })}
                    </ul>
            </section>
        </div>
    )
}

export default SwipeScreen