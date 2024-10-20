import React from "react";
import './TopBar.css'
import conversation_icon from '../images/conversation.svg'
import profile_icon from '../images/profile.svg'

export function navigate(navigationUrl: string) {
    if (window.location.href === window.location.origin + `${navigationUrl}`)
        return;
    window.location.href = window.location.origin + `${navigationUrl}`;
}

export default function TopBar() {

    const [isMenuOpen, setIsMenuOpen] = React.useState(false);

    const toggleMenu = () => {
        console.log(isMenuOpen)
        setIsMenuOpen(!isMenuOpen);
    };


    return (
        <div className="topbar">
            <div className="company-name"
                onClick={() => navigate('/swipe')}>
                Find
                <span className="blueText">ER</span>.it
            </div>
            <div className="menu-items">
                <div className="menu-icon-button"
                    style={{ backgroundImage: `url(${conversation_icon})` }}
                    onClick={() => navigate('/chat')} />
                <div className="menu-icon-button"
                    style={{ backgroundImage: `url(${profile_icon})` }}
                    onClick={() => navigate('/profile')} />
            </div>

            {isMenuOpen && (
                <div className="dropdown-menu">
                    <button className="menu-button" onClick={() => navigate('/settings')}>Settings</button>
                    <button className="menu-button" onClick={() => navigate('/help')}>Help</button>
                </div>
            )}
        </div>
    );
}