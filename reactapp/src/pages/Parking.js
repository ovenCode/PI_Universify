import React, { Component, useState } from 'react';

import "./Parking.css";

const Parking = () => {
    const [parking, setParking] = useState({ spot: { selected: false, QRCodeURL: "" },recommendations: { state: false, value: "parking-recommendation" } });

    return (
        <div>
            <div id="parking-page">
                <div id="parking">
                    <div id="parking-recommendations"><ParkingRecommendations recommendationState={parking} selectRecommendation={setParking} /></div>
                    <div id="parking-info"><ParkingLayout parking={parking} selectSpot={setParking} /></div>
                    <div id="parking-content"><ParkingContent parking={parking} setContent={setParking} /></div>
                </div>
            </div>
        </div>
    );
}

const ParkingRecommendations = ({ building, recommendationState, selectRecommendation }) => {

    

    const recommendations = [1, 2, 3, 4];

    return (
        <div id="parkings-grid">{recommendations.map((rec, index) => <div key={index} itemID={index} className={recommendationState.recommendations.state == index ? "parking-recommendation selected" : "parking-recommendation"} onClick={() => { selectRecommendation({ ...recommendationState, recommendations: { ...recommendations, state: index } }); }}>
            <div>rec.name</div>
            <div>rec.street</div>
            <div>rec.distance</div>
        </div>)}
        </div>
    );
}

const ParkingLayout = ({ parking, selectSpot }) => {

    const tempParking = {
        "matrix":
            [[0, 1, 1, 1, 1, 0, 1, 0, 1, 1],
            [0, 1, 1, 1, 1, 0, 1, 0, 1, 1],
            [1, 1, 0, 0, 1, 0, 1, 0, 1, 1],
            [0, 1, 1, 1, 1, 0, 1, 0, 1, 1]]
    };

    return (
        <div id="parking-layout">{tempParking.matrix.map((row, rowId) => <div key={rowId} className="parking-row">{row.map((place, spot) => <div key={spot} className={parking.spot.selected !== `${rowId} ${spot}` ? place == 0 ? "parking-spot" : "parking-spot occupied" : "parking-spot selected"} onClick={() => place == 0 ? selectSpot({ ...parking, spot: { ...spot, selected: `${rowId} ${spot}` }}) : console.log("occupied") }></div>)}</div>)}</div>
    );
};

const ParkingContent = ({ parking, setContent }) => {
    //

    return (
        <div>
            <div id="parking-id">parking.name/number</div>
            <div id="parking-remaining">parking.remainingplaces</div>
            <img src={parking.spot.QRCodeURL} />
            <div id="parking-buttons">
                <div id="parking-reserve" className={parking.spot.selected !== false ? "parking-reserve" : "disabled"} onClick={ parking.spot.selected ? () => { } : null }>Reserve spot</div>
                <div id="parking-qr" onClick={() => loadQRCode({ parking, setContent }) }>Generate QR Code</div>
            </div>
            
        </div>
    );
}

const loadQRCode = async ({ parking, setQRCode }) => {
    const response = await fetch("/api/TEST");
    const data = await response.json();

    console.log("Loading QRCode: ")
    console.log(parking);

    setQRCode({ ...parking, "spot": { ..."spot", QRCodeURL: data ?? "" } });
}

export { Parking }