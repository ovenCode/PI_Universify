import React from 'react';

import "./header.css"

export default function Header(props) {

    return (
        <div id="header-background">
            <span id="site-logo">Universify</span>
            <nav id="nav-bar">
                <div className="nav-button-border only-bottom" onClick={() => { console.log("Clicked profile"); props.toggleVis("Profile"); }}>
                    <div className="nav-button">Profil</div>
                    <div className="nav-button-bottom">
                        <div className="nav-button-bottom-left"></div>
                        <div className="nav-button-bottom-right"></div>
                    </div>
                </div>
                <div className="nav-button-border only-bottom" onClick={() => { props.toggleVis("Parking"); } }>
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
            <div id="help-button-border">
                <div id="help-button" onClick={() => { props.toggleVis("ShowHelp"); }}>
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