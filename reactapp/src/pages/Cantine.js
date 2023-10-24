import React, { useEffect, useState } from 'react';

import "./Cantine.css";

const Cantine = ({ userId }) => {

    const [cantine, setCantine] = useState({ "skip": true, "loaded": false, "id":1, "title": "", "hours": "", "address": { "name": "", "floor": "" }, "orders": [{ "nazwa": "Some name", "dzieńZamówienia": "Some date", "diet": {} }] });
    const [popup, setPopup] = useState(false);
    const zamowienia = []; //[1,2,3,5,6,7,8,9,0,99,88,77,66,55];
    var it = 0;
    

    // Update component on cantine update
    useEffect(() => {
        //
        let ignore = false;

        loadCantine({ ignore, userId, cantine, setCantine });

        return () => {
            ignore = true;
        };
    },[cantine]);
    
    return (
        <div>
            <div id="cantine-page">
                <div id="cantine">
                    <div id="cantine-title">{cantine.title} <div id="choose-cantine" onClick={() => setPopup("selectCantine") }>Wybierz stołówkę</div></div>
                    <div id="cantine-info">Godziny otwarcia: {cantine.hours}<div id="cantine-address"><span>Lokalizacja: {cantine.address.name}</span> <span>Piętro: {cantine.address.floor}</span></div></div>
                    <div id="cantine-content">{cantine.skip ?
                        <div id="cantine-orders-content">
                            <div id="cantine-orders-block">
                                <span style={{ fontSize: 20, padding: 10, }}>Twoje zamówienia</span>
                                <div id="cantine-orders">
                                    {cantine.orders != 0
                                        ?
                                        cantine.orders.map((order) => <div key={it++} className="cantine-order">
                                            <div className="order-name">{order.nazwa}</div>
                                            <div className="order-data">{order.dzieńZamówienia}</div>
                                            <div className="order-modify" onClick={() => setPopup("modifyOrder") }>Zmień zamówienie</div>
                                        </div>)
                                        :
                                        zamowienia.length != 0
                                        ?
                                        zamowienia.map((order) => <div key={it++} className="cantine-order-empty">Zamówienie {it + 1}</div>)
                                        :
                                        "Zamowienia ?? Brak zamowien"}
                                </div>
                            </div>
                            <div id="cantine-order-actions">
                                <div id="cantine-add-order" onClick={() => setPopup("newOrder") }>Dodaj zamówienie</div>
                                <div id="cantine-download-orders">Pobierz informacje o zamówieniach jeśli sa zamówienia</div>
                            </div>
                        </div>
                        :
                        <div id="cantine-choose-diet">Wybierz dietę jeśli brak zamówień</div>}
                    </div>
                </div>
                {popup == "selectCantine" && <CantineSelect setPopup={setPopup} cantine={cantine} setCantine={setCantine} /> }
                {popup == "modifyOrder" && <ModifyOrder setPopup={setPopup} />}
                {popup == "newOrder" && <NewOrder setPopup={setPopup} />}
            </div>
        </div>
    );
}

const loadCantine = async ({ ignore, userId, cantine, setCantine }) => {

    const response = await fetch(`/api/Stołówki/${cantine.id}`);
    const data = await response.json();

    if (!ignore) {
        console.log(data);
        const answer = JSON.parse(data.informacjeDodatkowe.includes("{") ? data.informacjedodatkowe : "{}");
        cantine.skip = answer != {} ? true : false;
        cantine.hours = data.informacjeDodatkowe != "" ? data.informacjeDodatkowe.split(";")[0] : cantine.hours === undefined ? "" : cantine.hours;
        cantine.address = data.informacjeDodatkowe != "" ? { "name": data.informacjeDodatkowe.split(";")[1], "floor": data.informacjeDodatkowe.split(";")[2] } : cantine.address === undefined ? { "name": "", "floor": "" } : cantine.address;
        cantine.orders = data.zamówienia.filter(order => order.idUżytkownika.toString() === userId.toString());
        cantine.loaded = true;
        console.log("Cantine: ");
        console.log(cantine);
        if (cantine.title != "Stołówka " + data.idStołówki) {
            cantine.title = "Stołówka " + data.idStołówki;
            setCantine({ ...cantine });
        }
    }

}

async function addOrder({ order }) {
    //
}

const NewOrder = ({ setPopup }) => {
    //

    return (
        <div><div id="order-new-dialog" className="dialog">SOME TEXT - NEW<div className="dialog-close" onClick={() => setPopup(false) }>CLOSE</div></div></div>
    );
}

const ModifyOrder = ({ setPopup }) => {
    //

    return (
        <div><div id="order-modify-dialog" className="dialog">SOME TEXT - MODIFY<div className="dialog-close" onClick={() => setPopup(false)}>CLOSE</div></div></div>
    );
}

const CantineSelect = ({ setPopup, cantine, setCantine }) => {
    const [cantines, setCantines] = useState([]);
    var id = 0;
    const chosenCantine = {};    

    useEffect(() => {
        let ignore = false;

        async function loadCantines() {
            const response = await fetch("/api/Stołówki");
            const data = await response.json();
            const answer = [];
            
            if (!ignore) {
                console.log(data);
                data.forEach(item => answer.push(`Stołówka ${item.idStołówki} - ${item.budynek.nazwa}`));
                setCantines(answer);
                console.log("Cantines:" + cantines);
            }
        }

        loadCantines();

        return () => {
            ignore = true;
        };
    }, []);

    return (<div><div id="choose-cantine-dialog" className="dialog"><label id="select-label">Wybierz stołówkę<select id="select-cantine" value={cantine.id ?? 1} onChange={e => { Object.keys(cantine).forEach(key => chosenCantine[key] = cantine[key]); chosenCantine.id = e.target.value; console.log(e.target.value); chosenCantine.loaded = false; setCantine(chosenCantine); }}>{cantines.map(cantine => <option key={id++} value={cantine.substring(cantine.indexOf(" ") + 1, cantine.indexOf("-") - 1)}>{cantine}</option>)}</select></label><div className="dialog-close" onClick={() => setPopup(false) }>CLOSE</div></div></div>);
}

export { Cantine }