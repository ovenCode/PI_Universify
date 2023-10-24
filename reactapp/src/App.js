import React, { Component, useState } from 'react';

import "./App.css";
import "./AppLogin.js";
import { AppLogin } from './AppLogin.js';
import Header from './header.js';
import { Profile } from "./pages/Profile";
import { Parking } from "./pages/Parking";
import { Cantine } from "./pages/Cantine";
import { Subject } from "./pages/Subject";


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
    const [isUserLoggedIn, setLoggedIn] = useState(['false', '-1']);

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
        

    return (
        <div className="App">
            <Header profile={[profileClassName, loginClassName]} toggleVis={toggleVis} />
                        {/*<h1 id="tabelLabel" >Weather forecast</h1>
                        <p>This component demonstrates fetching data from the server.</p>*/}
            <div id="App-content">
                {(isVisible === "Login" || isVisible === "Login ShowHelp") && <AppLogin id="login" className={loginClassName} login={setLoggedIn} />}
                {(isVisible === "Profile" || isVisible === "Profile ShowHelp") && <Profile id="profile" className={profileClassName} isVisible={isVisible} />}
                {(isVisible === "Parking" || isVisible === "Parking ShowHelp") && <Parking className="mainContent" />}
                {(isVisible === "Cantine" || isVisible === "Cantine ShowHelp") && <Cantine className="mainContent" />}
                {(isVisible === "Subject" || isVisible === "Subject ShowHelp") && <Subject className="mainContent" />}
                {isVisible.includes("ShowHelp") && <ShowHelp className="dialog" />}
                        </div>
                        {/*<Footer/>*/}

                    </div>
        );
    //}

       
}

const ShowHelp = () => {
    //
    return (<div>
        <div id="help-dialog">
            <div id="help">
                <div id="help-short">Some short</div>
                <div id="help-navigation">Some nav</div>
                <div id="help-content">Some content</div>
            </div>
        </div>
    </div>);
}

// const populateWeatherData = async () => {
//     const response = await fetch('weatherforecast');
//     const data = await response.json();
//     this.setState({ forecasts: data, loading: false });
// }
