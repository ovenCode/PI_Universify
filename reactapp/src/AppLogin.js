import React from 'react';
import './AppLogin.css';

const AppLogin = ({ login, toggleVis, setError }) => {

    const data = {
        title: "Login",
    }

    return (
        <div className="loginApp">
            <h1 id="loginTitle">{data.title}</h1>
            <form className="loginForm" method="post" onSubmit={async (event) => { event.preventDefault(); logIn({ event, login, toggleVis, setError }); toggleVis("Profile"); }}>
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

const logIn = async ({ event, login, toggleVis, setError }) => {
    login(["Logging", "-1"]);
    const requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json", "Accept": "text/plain" },
        body: JSON.stringify({ "username": event.target.username.value.toString(), "password": event.target.password.value.toString() })
    };

    try {
        const response = await fetch("/api/uzytkownicy/login", requestOptions);
        const data = await response.json();

        console.log(data);

        if (response.status !== 200) {
            setError({ code: response.status, message: response.statusText || response.title });
            toggleVis("ErrorBox");
            return;
        }

        if (data) {
            login(data);
        }
    } catch (error) {
        console.log("Error from login");
        console.log(error);
        setError({ code: error.status || "", message: error.title || "" });
        toggleVis("ErrorBox");
    }
}

export { AppLogin };

