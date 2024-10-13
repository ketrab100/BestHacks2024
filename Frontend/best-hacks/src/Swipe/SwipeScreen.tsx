import React from "react";
import './SwipeScreen.css';
import arrow_down from '../images/arrow-down.svg'
import {Employee} from "../Models/Interfaces";
import {store} from "../store";
import {getNext, updateEmployees} from "../Reducers/EmployeeReducer";
import {employeesMock} from "./EmployeesMock";
import {getNextEmployee} from "../Api/Employee";
import {AddSwipeInfo} from "../Api/Swipe";
import TagComponent from "../Utils/TagComponent";
import match_icon from "../images/match.svg";
import deny_icon from "../images/deny.svg";

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
    const [degree, setDegree] = React.useState(0);
    const [matchIconVis, setMatchIconVis] = React.useState(0);
    const [denyIconVis, setDenyIconVis] = React.useState(0);

    React.useEffect(() => {
        const fetchedEmployee = store.getState().employeeReducer.employees[store.getState().employeeReducer.index]
        setEmployee(fetchedEmployee)
    }, [])

    const handleTouchStart = (e: React.TouchEvent<HTMLDivElement>) => {
        touchStartX.current = e.touches[0].clientX;
        touchStartY.current = e.touches[0].clientY;
    };

    const handleTouchEnd = async () => {
        if (Math.abs(touchStartX.current - touchEndX.current) < Math.abs(touchStartY.current - touchEndY.current)){
            setDegree(0);
            setDenyIconVis(0);
            setMatchIconVis(0);
            return;
        }
            
        if (touchStartX.current > touchEndX.current + 50) {
            // Przesunięcie w lewo
            // Otherwise, just go to the next employee
            await addSwipe(false, store.getState().employeeReducer.employees[store.getState().employeeReducer.index].id)
            store.dispatch(getNext());
        } else if (touchStartX.current < touchEndX.current - 50) {
            // Przesunięcie w prawo
            await addSwipe(true, store.getState().employeeReducer.employees[store.getState().employeeReducer.index].id)
            store.dispatch(getNext());
        }
        if (!store.getState().employeeReducer.employees[store.getState().employeeReducer.index]) {
            await getNextEmployee().then((res) => {
                store.dispatch(updateEmployees(res))
            })
        }
        if (Math.abs(touchStartX.current - touchEndX.current) < Math.abs(touchStartY.current - touchEndY.current)){
            const fetchedEmployee = store.getState().employeeReducer.employees[store.getState().employeeReducer.index]
            setEmployee(fetchedEmployee);
        }
        setDegree(0);
        setDenyIconVis(0);
        setMatchIconVis(0);
    };

    const handleTouchMove = (e: React.TouchEvent<HTMLDivElement>) => {
        if (Math.abs(touchStartX.current - e.touches[0].clientX) < Math.abs(touchStartY.current - e.touches[0].clientY)){
            setDegree(0);
            setDenyIconVis(0);
            setMatchIconVis(0);
            return;
        }
        if(e.touches[0].clientX - touchStartX.current < -10){
            setDenyIconVis(1);
        }
        else if (e.touches[0].clientX - touchStartX.current > 10)
        {
            setMatchIconVis(1);
        }
        setDegree((e.touches[0].clientX - touchStartX.current) / 3);
        touchEndX.current = e.touches[0].clientX;
        touchEndY.current = e.touches[0].clientY;
    };
    return (
        <div className="scroll-container"
            onTouchStart={handleTouchStart}
            onTouchMove={handleTouchMove}
            onTouchEnd={handleTouchEnd}
            style={{ backgroundColor: bgColor,
                rotate: `${degree}deg`
            }}>
            <section className="snap-section first-section">
                <div className="profile-pic"
                     style={{backgroundImage: `url(${employeesMock[store.getState().employeeReducer.index].imageBase64})`}}/>
                <span className="credentials-card"><span
                    className="name">{employee!.firstName}</span> {employee!.lastName}</span>
                <div className="summary">
                    <span className="title">Career summary:</span>
                    <div className="career">
                        {employee.experience}
                    </div>
                    <span className="title">Tags summary:</span>
                    <div className="tagSection">
                        {employee.tags.slice(0, 2).map(tag => (
                            <div className="singleTag" key={tag.id}>
                                <TagComponent tag={tag} />
                            </div>
                        ))}
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
                <span className="title">All Tags:</span>
                <div className="tagSection">
                    {employee.tags.slice(0, 2).map(tag => (
                        <div className="singleTag" key={tag.id}>
                            <TagComponent tag={tag} />
                        </div>
                    ))}
                </div>
            </section>
            <img className="deny-icon" src={deny_icon} style={{visibility: denyIconVis ? 'visible' : 'hidden' }}/>
            <img className="match-icon" src={match_icon} style={{visibility: matchIconVis ? 'visible' : 'hidden'}}/>
        </div>
    )
}

export default SwipeScreen