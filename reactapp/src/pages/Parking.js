import React, { Component, useState } from 'react';

import "./Parking.css";

const Parking = () => {
    return (
        <div>
            <div id="parking-page">
                <div id="parking">
                    <div id="parking-recommendations"><ParkingRecommendations /></div>
                    <div id="parking-info"><ParkingLayout /></div>
                    <div id="parking-content"><ParkingContent /></div>
                </div>
            </div>
        </div>
    );
}

const ParkingRecommendations = ({ building }) => {

    const [recommendationState, selectRecommendation] = useState("parking-recommendation");

    const recommendations = [1, 2, 3, 4];

    var id = 0;

    return (
        <div id="parkings-grid">{recommendations.map((rec) => <div key={id++} className={recommendationState} onClick={() => { selectRecommendation("parking-recommendation-selected") }}>
            <div>rec.name</div>
            <div>rec.street</div>
            <div>rec.distance</div>
        </div>)}
        </div>
    );
}

const ParkingLayout = ({ parking }) => {

    const tempParking = {
        "matrix":
            [[0, 1, 1, 1, 1, 0, 1, 0, 1, 1],
            [0, 1, 1, 1, 1, 0, 1, 0, 1, 1],
            [1, 1, 0, 0, 1, 0, 1, 0, 1, 1],
            [0, 1, 1, 1, 1, 0, 1, 0, 1, 1]]
    };

    var rowId = 0, spot = 0;

    return (
        <div id="parking-layout">{tempParking.matrix.map((row) => <div key={rowId++} className="parking-row">{row.map((place) => <div key={spot++} className={place == 0 ? "parking-spot" : "parking-spot occupied"}></div>)}</div>)}</div>
    );
};

const ParkingContent = ({ content }) => {
    //

    return (
        <div>
            <div id="parking-id">parking.name/number</div>
            <div id="parking-remaining">parking.remainingplaces</div>
            <img />
            <div id="parking-buttons">
                <div id="parking-reserve" className="disabled">Reserve spot</div>
                <div id="parking-qr">Generate QR Code</div>
            </div>
            
        </div>
    );
}

export { Parking }