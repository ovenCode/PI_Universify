import React from 'react';
import "./Loader.css";

export const Loader = ({ message }) => {

    return (<div id="loader">
        <div id="loader-message">{message}</div>
        <div id="loading-bkgd-anim" />
        <div id="loading-anim" />
    </div>);
};
