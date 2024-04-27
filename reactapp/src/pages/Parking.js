import React, { useEffect, useState } from 'react';

import "./Parking.css";
import { Loader } from "./utils/Loader";
import { HubConnectionBuilder, HubConnectionState } from "@microsoft/signalr";
import { useCalcTime } from "./utils/Timer";

const Parking = ({ userId, setError }) => {
    const [parking, setParking] = useState({
        id: userId,
        building: { nazwa: "", adres: "" },
        spot: { nr: null, selected: false, QRCode: "" },
        recommendation: { state: false, value: "parking-recommendation" },
        recommendations: [],
        selected: { name: "null", free: null, layout: [] },
        reserved: { value: false, reservationComplete: false },
        loading: { state: false, message: "Loading" },
        count: { start: false, minutes: "00", seconds: "00", value: 0 }
    });

    const conn = new HubConnectionBuilder()
        .withUrl("https://localhost:7088/test")
        .withAutomaticReconnect()
        .build();

    conn.on("LayoutUpdate", value => {
        console.log(value);
        try {
            loadParkingData(parking, setParking);
            setParking({ ...parking, spot: { nr: "null", selected: false, QRCode: "" }, reserved: { ...parking.reserved, reservationComplete: value } })
        } catch (error) {
            console.log(error);
            setError({ code: error.status || "No code", message: error || "No message" });
        }
    });

    // FIXME: Fix the time so that it launches and shows only after the user has successfully reserved a spot 

    // ON MOUNT
    useEffect(() => {
        let ignore = false;

        try {
            conn.start();
            console.log("Connected to SignalR hub");
        } catch (err) {
            console.log("Error connecting to hub:", err);
            setError({ code: err.status || "No code", message: err || "No message" });
        }

        if (!ignore) {
            loadParkingData(parking, setParking);
        }

        return () => {
            ignore = true;
            if (conn !== null && conn.state === HubConnectionState.Connected) {
                try {
                    conn.stop();
                } catch (error) {
                    console.log(error);
                }
            }
        };
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    useEffect(() => {

        if (parking.selected.layout.length !== 0) {
            console.log("Layout update:");
            console.log(parking);
        }

        return () => {
        }
    }, [parking]);

    return (
        <div>
            <div id="parking-page">
                {parking.recommendations.length === 0 && <Loader message={parking.loading.message} />}
                {parking.recommendations.length === 0 || <div id="parking" className="mainContent">
                    {parking.loading.state && <Loader message={parking.loading.message} />}
                    <div id="parking-recommendations"><ParkingRecommendations recommendationState={parking} selectRecommendation={setParking} /></div>
                    <div id="parking-info"><ParkingLayout parking={parking} selectSpot={setParking} /></div>
                    <div id="parking-content"><ParkingContent userId={userId} parking={parking} setContent={setParking} /></div>
                </div>}
            </div>
        </div>
    );
}

const ParkingRecommendations = ({ building, recommendationState, selectRecommendation }) => {

    // TODO: Odległość pomiędzy parkingami zapisana w tabeli ParkingiOdleglosc

    const recommendations = recommendationState.recommendations.length === 0 ? [{ name: 1, street: "X", distance: "100 m" }, { name: 2, street: "Y", distance: "150 m" }, { name: 3, street: "Z", distance: "300 m" }] : recommendationState.recommendations;

    return (
        <div id="parkings-grid">
            {recommendations.map((rec, index) => <div key={index} itemID={index} className={recommendationState.recommendation.state === index
                ?
                "parking-recommendation selected"
                :
                "parking-recommendation"} onClick={() => loadFullParking(index, recommendationState, selectRecommendation)}>{/* { selectRecommendation({ ...recommendationState, recommendations: { ...recommendations, state: index } }); } */}
                <div>{rec.name}</div>
                <div>{rec.street}</div>
                <div>{rec.distance}</div>
            </div>)}
        </div>
    );
}

const ParkingLayout = ({ parking, selectSpot }) => {

    //const [spotClass, setSpotClass] = useState("");
    const tempParking = {
        matrix: parking.selected.layout ??
            [[{ stanMiejsca: 0 }, { stanMiejsca: 1 }, { stanMiejsca: 1 }, { stanMiejsca: 0 }, { stanMiejsca: 1 }, { stanMiejsca: 0 }, { stanMiejsca: 0 }, { stanMiejsca: 0 }, { stanMiejsca: 0 }, { stanMiejsca: 1 }],
            [{ stanMiejsca: 0 }, { stanMiejsca: 0 }, { stanMiejsca: 1 }, { stanMiejsca: 0 }, { stanMiejsca: 1 }, { stanMiejsca: 0 }, { stanMiejsca: 1 }, { stanMiejsca: 1 }, { stanMiejsca: 0 }, { stanMiejsca: 1 }],
            [{ stanMiejsca: 0 }, { stanMiejsca: 1 }, { stanMiejsca: 0 }, { stanMiejsca: 0 }, { stanMiejsca: 1 }, { stanMiejsca: 0 }, { stanMiejsca: 1 }, { stanMiejsca: 1 }, { stanMiejsca: 0 }, { stanMiejsca: 1 }],
            [{ stanMiejsca: 0 }, { stanMiejsca: 1 }, { stanMiejsca: 1 }, { stanMiejsca: 0 }, { stanMiejsca: 1 }, { stanMiejsca: 0 }, { stanMiejsca: 0 }, { stanMiejsca: 0 }, { stanMiejsca: 0 }, { stanMiejsca: 1 }]]
    };

    return (
        <div id="parking-layout">
            {tempParking.matrix.map((row, rowId) => <div key={rowId} className="parking-row">
                {row.map((place, spot) => <div key={spot} className={parking.spot.selected !== `${rowId}_${spot}`
                    ?
                    place.stanMiejsca === 0
                        ?
                        "parking-spot"
                        :
                        place.stanMiejsca === 1
                            ?
                            "parking-spot-occupied"
                            :
                            "parking-spot spot-reserved"
                    :
                    place.stanMiejsca === 2
                        ?
                        "parking-spot spot-reserved"
                        :
                        "parking-spot selected"} onClick={() => place.stanMiejsca === 0
                            ?
                            selectSpot({ ...parking, spot: { ...parking.spot, selected: `${rowId}_${spot}`, nr: rowId * row.length + spot + 1 } })
                            :
                            place.stanMiejsca === 2
                                ?
                                console.log("reserved")
                                :
                                console.log("occupied")}></div>)}
            </div>)}
        </div>);
};

const ParkingContent = ({ userId, parking, setContent }) => {
    //
    const spot = {
        free: 0, occupied: 1, reserved: 2
    };
    // 15 minut, aby użytkownik zarezerwowal
    parking.count = useCalcTime(900);

    // useEffect(() => {
    //     let interval;
    //     if (parking.count.value === 0 && parking.count.start) {
    //         // set reservation back to available            
    //         reserveSpot(parking, setContent, spot.free);
    //         console.log("0 Minutes, 0 seconds, start true");
    //     } else if (parking.count.value === 0) {
    //         // time has elapsed
    //         console.log("0 Minutes, 0 seconds, start false");
    //         setContent({ ...parking, count: { ...parking.count, start: false } });
    //     } else if (parking.count.start) {
    //         interval = setInterval(() => {
    //             const secCount = parking.count.value % 60;
    //             const minCount = Math.floor(parking.count.value / 60);
    //             const computeSec = String(secCount).length === 1 ? `0${secCount}` : secCount;
    //             const computeMin = String(minCount).length === 1 ? `0${minCount}` : minCount;

    //             setContent({ ...parking, count: { ...parking.count, minutes: computeMin, seconds: computeSec, value: parking.count.value-- } })
    //         }, 1000);
    //     }

    //     return () => {
    //         clearInterval(interval);
    //     }
    //     // eslint-disable-next-line react-hooks/exhaustive-deps
    // }, [parking.count.value, parking.count.start]);
    console.log("Inside Parking content");
    console.log(parking);

    return (
        <div>
            <div id="parking-id">{parking.selected.name ?? "Some name"}/{parking.spot.nr ?? "Some number"}</div>
            <div id="parking-remaining">{parking.selected.free ?? "Some number of places left"}</div>
            {parking.spot.QRCode.length !== 0 && <img id="parking-qr-image" src={`data:image/bmp;base64,${parking.spot.QRCode}`} alt="" />}
            <div id="parking-buttons">
                <div id="parking-reserve" className={parking.spot.selected !== false ? "btn" : "btn disabled"} onClick={parking.spot.selected === false ? null : () => {
                    reserveSpot(parking, setContent, spot.reserved);
                }}>Rezerwuj miejsce</div>
                <div id="parking-qr" className="btn" onClick={() => loadQRCode(userId, parking, setContent)}>Generuj Kod QR {!parking.count.start ? "" : `${parking.count.minutes}:${parking.count.seconds}`}</div>
            </div>

        </div>
    );
}

const reserveSpot = async (parking, setSpot, state) => {
    if ((parking.reserved.value !== false && state !== 0) || parking.reserved.reservationComplete) {
        alert("Nie można zarezerwować dwóch miejsc");

        return null;
    }
    setSpot({ ...parking, loading: { ...parking.loading, state: true } });
    try {
        console.log(parking.spot.selected);
        const response = await fetch(`api/Parkingi/Miejsca/${parking.selected.name.substring(8)}_${parking.spot.selected}_${state}`);
        const data = await response.json();
        const layout = [], parkings = parking.recommendations;
        const id = parseInt(parking.selected.name.substring(8)) - 1;
        if (data.Rozklad.length > 0) {
            var free = data.Rozklad.length;
            data.Rozklad.forEach(p => { if (p.stanMiejsca === 1) free--; });
            for (let index = 0; index < data.LiczbaRzedow; index++) {
                layout.push(data.Rozklad.slice(index * data.Rozklad.length / data.LiczbaRzedow, (index + 1) * data.Rozklad.length / data.LiczbaRzedow))
            }
        }

        console.log("id");
        console.log(id);
        parkings[id] = { ...parkings[id], layout: layout, free: free };
        console.log("layout");
        console.log(layout);
        console.log("parkings");
        console.log(parkings);
        setSpot({
            ...parking,
            selected: { ...parking.selected, layout: layout, free: free },
            recommendations: parkings,
            loading: { ...parking.loading, state: false },
            reserved: { value: state !== 0 ? true : false },
            spot: { ...parking.spot, QRCode: state !== 0 ? parking.spot.QRCode : "" },
            count: { start: true, minutes: "15", seconds: "00", value: 900 }
        });
        console.log("Spot reserved");
        console.log(parking);
        console.log(data);
    } catch (error) {
        console.log(error);
        alert(error);
    }
}

const loadQRCode = async (id, parking, setQRCode) => {
    console.log("loadQRCode::parking:")
    console.log(parking);
    if (parking.spot.nr === null) {
        alert("Proszę zarezerwować miejsce, aby móc wygenerować kod");

        return null;
    }
    const response = await fetch(`/api/Parkingi/QR/${id}_${parking.id}_${parking.spot.nr}`);
    const data = await response.json();

    console.log(data);
    console.log("Loading QRCode: ")
    console.log(parking);

    setQRCode({ ...parking, spot: { ...parking.spot, QRCode: data.data.toString("base64") ?? "" } });
}

const loadParkingData = async (parking, setParking) => {
    const response = await fetch('api/Parkingi/fullparking');
    const data = await response.json();
    var parkingData = {}, parkings = [];

    console.log("Logging Parking Data:");
    console.log(data);

    data.forEach((el, index) => {
        const layout = [];
        if (el.Rozklad.length > 0) {
            var free = el.Rozklad.length;
            el.Rozklad.forEach(p => { if (p.stanMiejsca === 1) free--; });
            for (let index = 0; index < el.LiczbaRzedow; index++) {
                layout.push(el.Rozklad.slice(index * el.Rozklad.length / el.LiczbaRzedow, (index + 1) * el.Rozklad.length / el.LiczbaRzedow))
            }
        }

        parkingData = {
            name: `Parking ${index + 1}`,
            street: el.Adres.substring(0, el.Adres.indexOf(",")),
            distance: calculateDistance() ?? 0,
            layout: layout,
            free: free ?? null
        }

        parkings.push(parkingData);
    });

    console.log(parkings);

    setParking({ ...parking, recommendations: parkings, selected: parkings[0] });

}

const loadFullParking = async (id, parking, setParking) => {

    setParking({ ...parking, selected: parking.recommendations[id] });
}

/**
 * Function used for calculating the distance between the parking and the user's next class.
 * The distance is to be shown in meters.
 * 
 * @returns distance :null | number
 */
const calculateDistance = () => {
    // TODO: fill the function
    return null;
}

export { Parking }