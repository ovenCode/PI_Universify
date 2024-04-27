import React, { useEffect, useState, useRef } from 'react';

import "./header.css"

export default function Header(props) {
    const [dropdown, setDropdown] = useState("");
    const margin = useRef("-160% 0 0 0");

    useEffect(() => {
        if (dropdown !== "") {
            setDropdown("");
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [props.isUserLoggedIn]);

    return (
        <div id="header-background">
            <span id="site-logo">Universify</span>
            <nav id="nav-bar">
                <div className="nav-button-border only-bottom" onClick={() => { props.toggleVis("Profile"); }}>
                    <div className="nav-button">Profil</div>
                    <div className="nav-button-bottom">
                        <div className="nav-button-bottom-left"></div>
                        <div className="nav-button-bottom-right"></div>
                    </div>
                </div>
                <div className="nav-button-border only-bottom" onClick={() => { props.toggleVis("Parking"); }}>
                    <div className="nav-button">Parking</div>
                    <div className="nav-button-bottom">
                        <div className="nav-button-bottom-left"></div>
                        <div className="nav-button-bottom-right"></div>
                    </div>
                </div>
                <div className="nav-button-border only-bottom" onClick={() => { props.toggleVis("Cantine"); }}>
                    <div className="nav-button">Stołówka</div>
                    <div className="nav-button-bottom">
                        <div className="nav-button-bottom-left"></div>
                        <div className="nav-button-bottom-right"></div>
                    </div>
                </div>
                <div className="nav-button-border only-bottom" onClick={() => { props.toggleVis("Subject"); }}>
                    <div className="nav-button">Przedmiot</div>
                    <div className="nav-button-bottom">
                        <div className="nav-button-bottom-left"></div>
                        <div className="nav-button-bottom-right"></div>
                    </div>
                </div>
            </nav>
            <div id="nav-menu">
                <div id="nav-dropdown" onClick={() => setDropdown("nav-submenu-animate")}>
                    <img alt="dropdown-icon" style={{ objectFit: "fill", height: "8vh", width: "8vh", maxHeight: "32px", maxWidth: "32px", minHeight: "30px", minWidth: "22px", margin: "0.1em auto 0.2em auto", padding: "0" }} src="icons8-sort-down-30.png" onError={(e) => console.error(e)} />
                </div>
                <div id="nav-submenu" className={dropdown} style={{ margin: margin.current }} onAnimationEnd={() => { margin.current = margin.current === "0.1em 0 0 0" ? margin.current : "-160% 0 0 0"; }}> {/* dropdown === "" ? dropdown : dropdown === "nav-submenu-animate" ? margin.current === "-160% 0 0 0" ? dropdown : "nav-submenu-close-animate" : "" */}
                    <div id="logout" className="dropdown-button" style={props.isUserLoggedIn[0] === false ? { display: "none" } : {}} onClick={props.isUserLoggedIn[0] === false ? null : () => props.setLoggedIn([false, -1])}>WYLOGUJ</div>
                    <div id="bug-report" className="dropdown-button" onClick={() => { props.toggleVis(props.vis + " ReportBug") }}>ZGŁOŚ BŁĄD</div>
                    {props.isUserLoggedIn[0] === "Admin" && <div id="admin-hub-btn" className="dropdown-button" onClick={() => { props.toggleVis("AdminHub") }}>ADMIN</div>}
                    <div className="dropdown-button" onClick={() => setDropdown("nav-submenu-close-animate")}>ZAMKNIJ</div>
                </div>
            </div>
            <div id="help-button-border">
                <div id="help-button" onClick={() => { props.toggleVis(props.vis + " ShowHelp"); }}>
                    <span>Pomoc</span>
                    <div id="help-icon">?</div>
                </div>
                <div className="help-button-bottom">
                    <div className="help-button-bottom-left"></div>
                </div>
            </div>
        </div>
    );
}                