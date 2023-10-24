import React from 'react';

import './AppLogin.css';

const AppLogin = ({ login, toggleVis }) => {

    const data = {
        title: "Login",
    }

    return (
        <div className="loginApp">
            <h1 id="loginTitle">{data.title}</h1>
            <form className="loginForm" method="post" onSubmit={async (event) => { event.preventDefault(); logIn({ event, login }); toggleVis("Profile"); }}>
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

async function logIn({ event, login }) {
    const requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json", "Accept": "text/plain" },
        body: JSON.stringify({"username": event.target.username.value.toString(), "password": event.target.password.value.toString()})
    };
    const response = await fetch("/api/uzytkownicy/login", requestOptions);
    const data = await response.json();

    console.log(data);

    if (data) {
        login(data);
    }
}

export { AppLogin };

