import React, { useEffect, useState, useRef } from 'react';

import "./App.css";
import "./pages/utils/General.css";
import "./AppLogin.js";
import helpTxt from "./help.txt";
import { AppLogin } from './AppLogin.js';
import Header from './header.js';
import { Profile } from "./pages/Profile";
import { Parking } from "./pages/Parking";
import { Cantine } from "./pages/Cantine";
import { Subject } from "./pages/Subject";
import AdminHub from "./pages/AdminHub.js";
import { Loader } from "./pages/utils/Loader.js";
import ReportBug from "./pages/utils/ReportBug.js";
import ErrorBox from "./pages/utils/ErrorBox.js";


export const App = () => {
    //displayName = App.name;

    /*static renderForecastsTable(forecasts) {
        return (
            <table className='table table-striped' aria-labelledby="tabelLabel">
                <thead>
                    <tr>
                        <th>Date</th>
                        <th>Temp. (C)</th>
                        <th>Temp. (F)</th>
                        <th>Summary</th>
                    </tr>
                </thead>
                <tbody>
                    {forecasts.map(forecast =>
                        <tr key={forecast.date}>
                            <td>{forecast.date}</td>
                            <td>{forecast.temperatureC}</td>
                            <td>{forecast.temperatureF}</td>
                            <td>{forecast.summary}</td>
                        </tr>
                    )}
                </tbody>
            </table>
        );
    }*/

    //const states = [Profile];
    //state = { isVisible: false };    

    //render() {
    /*let contents = this.state.loading
        ? <p><em>Loading... Please refresh once the ASP.NET backend has started. See <a href="https://aka.ms/jspsintegrationreact">https://aka.ms/jspsintegrationreact</a> for more details.</em></p>
        : App.renderForecastsTable(this.state.forecasts);*/

    const [isVisible, setVisible] = useState("Login");
    const [error, setError] = useState({ code: null, message: null });
    const [isUserLoggedIn, setLoggedIn] = useState([false, '-1']);

    const toggleVis = (val) => {
        console.log(val);
        if (val !== "ShowHelp") {
            setVisible(val);
        } else {
            setVisible(isVisible.concat(" ShowHelp"));
        }
    };

    /*require('react-dom');
    window.React2 = require('react');
    console.log(window.React1 === window.React2);*/
    //const { sts } = this.states;
    //const { profileVisibility } = this.state;

    const profileClassName = "mainContent hidden";
    const loginClassName = "";
    //let x = 0;
    console.log(isUserLoggedIn);

    return (
        <div className="App">
            <Header profile={[profileClassName, loginClassName]} vis={isVisible} toggleVis={toggleVis} isUserLoggedIn={isUserLoggedIn} setLoggedIn={setLoggedIn} setError={setError} />
            <div id="App-content">
                {error.message && <ErrorBox className="fixed-dialog" error={error} setError={setError} />}
                {(error.message != null || isUserLoggedIn[0] === "Logging") && <Loader message={"Loading"} />}
                {isUserLoggedIn[0] === false && <AppLogin id="login" className={loginClassName} login={setLoggedIn} toggleVis={toggleVis} setError={setError} />}
                {isUserLoggedIn[0] === "True" && (isVisible.includes("Profile")) && <Profile id="profile" className={profileClassName} setError={setError} userId={isUserLoggedIn[1]} />}
                {isUserLoggedIn[0] === "True" && (isVisible === "Parking" || isVisible === "Parking ShowHelp") && <Parking className="mainContent" setError={setError} userId={isUserLoggedIn[1]} />}
                {isUserLoggedIn[0] === "True" && (isVisible === "Cantine" || isVisible === "Cantine ShowHelp") && <Cantine className="mainContent" setError={setError} userId={isUserLoggedIn[1]} />}
                {isUserLoggedIn[0] === "True" && (isVisible === "Subject" || isVisible === "Subject ShowHelp") && <Subject className="mainContent" setError={setError} userId={isUserLoggedIn[1]} />}
                {isUserLoggedIn[0] === "Admin" && (isVisible === "AdminHub" || isVisible === "AdminHub ShowHelp") && <AdminHub className="mainContent" setError={setError} userId={isUserLoggedIn[1]} />}
                {isVisible.includes("ReportBug") && <ReportBug toggleVis={toggleVis} vis={isVisible} />}
                {isVisible.includes("ShowHelp") && <ShowHelp className="fixed-dialog" toggleVis={toggleVis} vis={isVisible} />}
            </div>
            {/*<Footer/>*/}

        </div>
    );
    //}


}

const ShowHelp = ({ className, vis, toggleVis }) => {
    //
    const [content, setContent] = useState({ all: [], links: [], main: "" });
    const dialog = useRef(null);

    useEffect(() => {
        let ignore = false;
        if (!ignore) fetch(helpTxt).then(r => r.text()).then(o => {
            console.log("FILE CONTENT");
            console.log(o.split("\r\n\r\n\r\n"));
            const _links = [], _all = [];
            o.split("\r\n").forEach(el => {
                if (el.match(/(?<=-\s)\D+(?=\s-)/) === null) {
                    // Nic nie rób
                } else {
                    _links.push(el.match(/(?<=-\s)\D+(?=\s-)/));
                }
            });
            o.split("\r\n\r\n\r\n").forEach(el => {
                if (el.match(/(?<=-\s)\D+(?=\s-)/) === null) {
                    _all.push(el);
                } else {
                    // Nic nie rób
                }
            });
            setContent({
                links: _links,
                all: _all,
                main: _all[0]
            });
        });

        const handleClick = (e) => {
            if (dialog.current && !dialog.current.contains(e.target)) {
                toggleVis(vis.substring(0, vis.indexOf(" ShowHelp")));
            }
        };

        document.addEventListener("mousedown", handleClick);

        return () => {
            ignore = true;
            document.removeEventListener("mousedown", handleClick);
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (<div>
        <div className={className} id="help-dialog" ref={dialog}>
            <div id="help">
                <div id="help-short">Some short</div>
                <div id="help-main">
                    <div id="help-navigation"><HelpNav content={content} setContent={setContent} /></div>
                    <div id="help-content">{content.main || "Brak informacji"}</div>
                </div>
            </div>
        </div>
    </div>);
}

const HelpNav = ({ content, setContent }) => {
    const links = content.links; // fetch() ?? ["1","Drugi link", "Dluzsza nazwa linku"];
    const contents = content.all;

    console.log("HELP LINKS");
    console.log(content.links);

    return (<div>
        <div>{links.length === 0 ? "Some nav" : links.map((l, k) => <div className="help-links" key={k} onClick={() => { console.log(contents); setContent({ ...content, main: contents[k] }); }}>{l}</div>)}</div>
    </div>);
}

// const populateWeatherData = async () => {
//     const response = await fetch('weatherforecast');
//     const data = await response.json();
//     this.setState({ forecasts: data, loading: false });
// }
