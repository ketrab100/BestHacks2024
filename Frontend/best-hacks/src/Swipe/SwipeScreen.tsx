import React from "react";
import './SwipeScreen.css';
import arrow_down from '../images/arrow-down.svg'
import {Employee} from "../Models/Interfaces";
import {store} from "../store";
import {getNext, updateEmployees} from "../Reducers/EmployeeReducer";
import {employeesMock} from "./EmployeesMock";
import {getNextEmployee} from "../Api/Employee";
import {AddSwipeInfo} from "../Api/Swipe";

store.dispatch(getNext());

getNextEmployee().then((res) => {
    store.dispatch(updateEmployees(res))
})

const addSwipe = async (result: boolean, swipedId: string) => {
    try {
        const tagsFromApi = await AddSwipeInfo({userId: "", swipedId: swipedId, SwipeResult: result}); // Pobierz tagi
    } catch (error) {
        console.error("Error fetching tags:", error); // Obsługa błędu
    }
};

function SwipeScreen() {
    const [bgColor, setBgColor] = React.useState("#f0f8ff");
    const touchStartX = React.useRef(0); // Przechowuje pozycję X początku dotyku
    const touchEndX = React.useRef(0);   // Przechowuje pozycję X końca dotyku
    const touchStartY = React.useRef(0); // Przechowuje pozycję X początku dotyku
    const touchEndY = React.useRef(0);   // Przechowuje pozycję X końca dotyku
    const [employee, setEmployee] = React.useState<Employee>(store.getState().employeeReducer.employees[store.getState().employeeReducer.index])

    React.useEffect(() => {
        const fetchedEmployee = store.getState().employeeReducer.employees[store.getState().employeeReducer.index]
        setEmployee(fetchedEmployee)
    }, [])

    const handleTouchStart = (e: React.TouchEvent<HTMLDivElement>) => {
        touchStartX.current = e.touches[0].clientX;
        touchStartY.current = e.touches[0].clientY;
    };

    const handleTouchEnd = async () => {
        if (Math.abs(touchStartX.current - touchEndX.current) < Math.abs(touchStartY.current - touchEndY.current))
            return
        if (touchStartX.current > touchEndX.current + 50) {
            // Przesunięcie w lewo
            setBgColor("#faebd7"); // Zmieniamy kolor na inny
            // Otherwise, just go to the next employee
            await addSwipe(false, store.getState().employeeReducer.employees[store.getState().employeeReducer.index].id)
            store.dispatch(getNext());

        } else if (touchStartX.current < touchEndX.current - 50) {
            // Przesunięcie w prawo
            setBgColor("#add8e6"); // Zmieniamy kolor na jeszcze inny
            await addSwipe(true, store.getState().employeeReducer.employees[store.getState().employeeReducer.index].id)
            store.dispatch(getNext());
        }
        if (!store.getState().employeeReducer.employees[store.getState().employeeReducer.index]) {
            await getNextEmployee().then((res) => {
                store.dispatch(updateEmployees(res))
            })
        }
        const fetchedEmployee = store.getState().employeeReducer.employees[store.getState().employeeReducer.index]
        setEmployee(fetchedEmployee)
        console.log(fetchedEmployee)
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
                <div className="profile-pic"
                     style={{backgroundImage: `url(${employeesMock[store.getState().employeeReducer.index].imageBase64})`}}/>
                <span className="credentials"><span
                    className="name">{employee!.firstName}</span> {employee!.lastName}</span>
                <div className="summary">
                    <ul className="career" aria-label="Career Summary">
                        {/*{previousJobs.slice(0, 2).map((job) => {*/}
                        {/*    return <li>*/}
                        {/*        {job.jobName} | {job.position}*/}
                        {/*    </li>*/}
                        {/*})}*/}
                    </ul>
                    <ul className="skills" aria-label="Skills">
                        {/*{skills.slice(0, 2).map((skill) => {*/}
                        {/*    return <li>*/}
                        {/*        {skill.technology}*/}
                        {/*    </li>*/}
                        {/*})}*/}
                    </ul>
                    <div className="looking-for">
                        <span className="title">Interested in:</span>
                        {/*{lookingFor.map((proposition) => {*/}
                        {/*    return <span>*/}
                        {/*        {proposition.position} | {proposition.level}*/}
                        {/*    </span>*/}
                        {/*})}*/}
                    </div>
                </div>
                <img className="arrow-down" src={arrow_down}></img>
            </section>
            <section className="snap-section second-section">
                <div className="description">
                    <span className="title">
                        Description:
                    </span>
                    <span>{employee!.bio}</span>
                </div>
                <ul className="career" aria-label="Full career summary">
                    {/*{previousJobs.map((job) => {*/}
                    {/*    return <li>*/}
                    {/*        {job.jobName} | {job.position}*/}
                    {/*    </li>*/}
                    {/*})}*/}
                </ul>
                <ul className="skills" aria-label="All skills">
                    {employee!.tags.map((skill) => {
                        return <li>
                            {skill.name}
                        </li>
                    })}
                </ul>
            </section>
        </div>
    )
}

export default SwipeScreen