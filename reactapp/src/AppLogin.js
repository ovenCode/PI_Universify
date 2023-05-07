import React, { Component, useState } from 'react';
import ReactDOM from 'react-dom';

import './AppLogin.css';

const AppLogin = () => {
    const [isUserLoggedIn, setLoggedIn] = useState(false);

    const loggedToggle = () => {
        setLoggedIn((current) => !current);
    };

    const data = {
        title: "Login",
    }

    return (
        <div className="loginApp">
            <h1 id="loginTitle">{data.title}</h1>
            <form className="loginForm" onSubmit={logIn}>
                <div className="loginInput">
                    <label>Username</label>
                    <input type="text" name="username" required />
                    { }
                </div>
                <div className="loginInput">
                    <label>Password</label>
                    <input type="password" name="password" required />
                    { }
                </div>
                <div id="loginBtn">
                    <input type="submit" />
                </div>
            </form>
        </div>
    );
}

const logIn = async () => {
    //
}

export { AppLogin };

