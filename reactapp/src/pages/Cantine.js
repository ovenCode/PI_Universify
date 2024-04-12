import React, { useEffect, useState } from 'react';

import "./Cantine.css";
//import { Tune } from '@mui/icons-material';

const Cantine = ({ userId }) => {

    const [cantine, setCantine] = useState({ "skip": true, "loaded": false, "id": 1, "title": "", "hours": "", "address": { "name": "", "floor": "" }, "orders": [{ "nazwa": "Some name", "dzieńZamówienia": "Some date", "dieta": {} }], "object": null });
    const [course, setCourse] = useState({ "idDania" : -1, "nazwa" : "", "ilośćKalorii" : 0, "idSkładnika" : 0, "idDiety" : 0, "dieta" : null, "składniki" : [], "zamówienie" : null})
    const [popup, setPopup] = useState({ "name": false, "data": null });
    const [alert, setAlert] = useState({message: ""});
    const zamowienia = []; //[1,2,3,5,6,7,8,9,0,99,88,77,66,55];
    

    // Update component on cantine update
    useEffect(() => {
        //
        let ignore = false;

        loadCantine({ ignore, userId, cantine, setCantine });

        return () => {
            ignore = true;
        };
    },[cantine, userId]);
    
    return (
        <div>
            <div id="cantine-page">
                <div id="cantine">
                    <div className={alert.message.length !== 0 ? "alert" : "alert hidden" }>
                        <span className="alert-message">{alert.message}</span>
                        <span className="alert-close" onClick={() => setAlert({ ...alert, message: "" })}>&times;</span>
                    </div>
                    <div id="cantine-title">{cantine.title} <div id="choose-cantine" onClick={() => setPopup({ "name": "selectCantine", data: null }) }>Wybierz stołówkę</div></div>
                    <div id="cantine-info">Godziny otwarcia: {cantine.hours}<div id="cantine-address"><span>Lokalizacja: {cantine.address.name}</span> <span>Piętro: {cantine.address.floor}</span></div></div>
                    <div id="cantine-content">{cantine.skip ?
                        <div id="cantine-orders-content">
                            <div id="cantine-orders-block">
                                <span style={{ fontSize: 20, padding: 10, }}>Twoje zamówienia</span>
                                <div id="cantine-orders">
                                    {cantine.orders !== 0
                                        ?
                                        cantine.orders.map((order, k) => <div key={k} className="cantine-order">
                                            <div className="order-name">{order.nazwa}</div>
                                            <div className="order-date">{order.dzieńZamówienia}</div>
                                            <div className="order-btns">
                                                <div className="order-modify" onClick={() => setPopup({ name: "modifyOrder", data: { "order": order }}) }>Zmień zamówienie</div>
                                                <div className="order-delete" onClick={() => deleteOrder({ "order": order }) }>Usuń</div>
                                            </div>
                                        </div>)
                                        :
                                        zamowienia.length !== 0
                                        ?
                                        zamowienia.map((order, k) => <div key={k} className="cantine-order-empty">Zamówienie {k + 1}</div>)
                                        :
                                        "Zamowienia ?? Brak zamowien"}
                                </div>
                            </div>
                            <div id="cantine-order-actions">
                                <div id="cantine-add-order" onClick={() => setPopup({ "name" : "addOrder", data: {"userId" : userId, "cantineId": cantine.id} }) }>Dodaj zamówienie</div>
                                <div id="cantine-download-orders" onClick={() => downloadOrders(cantine.orders, alert, setAlert)}>Pobierz informacje o zamówieniach</div>
                            </div>
                        </div>
                        :
                        <div id="cantine-choose-diet">Wybierz dietę jeśli brak zamówień</div>}
                    </div>
                </div>
                {popup.name === "selectCantine" && <CantineSelect setPopup={setPopup} cantine={cantine} setCantine={setCantine} /> }
                {popup.name === "modifyOrder" && <ModifyOrder setPopup={setPopup} data={popup.data} />}
                {popup.name === "addOrder" && <AddOrder setPopup={setPopup} course={course} setCourse={setCourse} data={popup.data} />}
            </div>
        </div>
    );
}

/**
 * Load a cantine with all necessary information
 * @param {boolean} ignore
 * @param {number} userId
 * @param {object} cantine
 * @param {Function} cantine
 */
const loadCantine = async ({ ignore, userId, cantine, setCantine }) => {

    const response = await fetch(`/api/Stołówki/${cantine.id}`);
    const data = await response.json();

    if (!ignore) {
        console.log(data);
        const answer = JSON.parse(data.informacjeDodatkowe.includes("{") ? data.informacjedodatkowe : "{}");
        cantine.skip = answer !== {} ? true : false;
        cantine.hours = data.informacjeDodatkowe !== "" ? data.informacjeDodatkowe.split(";")[0] : cantine.hours === undefined ? "" : cantine.hours;
        cantine.address = data.informacjeDodatkowe !== "" ? { "name": data.informacjeDodatkowe.split(";")[1], "floor": data.informacjeDodatkowe.split(";")[2] } : cantine.address === undefined ? { "name": "", "floor": "" } : cantine.address;
        cantine.orders = data.zamówienia.filter(order => order.idUżytkownika.toString() === userId.toString());
        cantine.loaded = true;
        console.log("Cantine: ");
        console.log(cantine);
        if (cantine.title !== "Stołówka " + data.idStołówki) {
            cantine.title = "Stołówka " + data.idStołówki;
            setCantine({ ...cantine });
        }
    }

}

/**
 * Add order dialog pop-up
 * 
 * Add an order to your existing orders based on meals available in your specific diet
 * 
 * TODO: fix choosing of the meals, so that you can load also the 1st meal
 * 
 * @param {any} param0
 * @returns
 */
const AddOrder = ({ setPopup, course, setCourse, data }) => {
    //
    const [courses, setCourses] = useState([]);
    const [selected, setSelected] = useState(0);

    // On mount
    useEffect(() => {
        let ignore = false;
        
        if (!ignore) {
            fetch(`/api/Dania`).then(v => v.json().then(o => {
                setCourses(o);
                if (courses.length !== 0) {
                    setCourse(courses[0]);
                    console.log("Default: ");
                    console.log(course);
                    console.log(courses);
                }
            }));            
        }        

        return () => {
            ignore = true;
        };
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [setCourses]);

    /*useEffect(() => {
        console.log("Selected: " + selected);
        setSelected(selected);
    },[selected]);*/

    return (<div>
        <div id="order-add-dialog" className="dialog">            
            <div id="order-add-dialog-main">
                <div className="dialog-title" id="order-add-dialog-title">Dodaj danie</div>
                <select id="select-course"  onChange={e => {
                    setSelected(e.target.value);
                    setCourse(courses[selected]);
                    console.log(course);
                }} defaultValue={courses[0]}>
                    {courses.map((l, k) => <option key={k} value={k} >{l.nazwa} {k}</option>)}
                </select>
                <div>{selected !== 0
                    ?
                    <label htmlFor="course-name">Nazwa diety: <span id="course-name" key={selected}>{course.idDania !== -1 ? course.dieta.nazwa : ""}</span></label>
                    :
                    ""}</div>
                <div id="course-ingredients">{selected !== 0
                    ?
                    <label htmlFor="ingredient-list">Składniki: <span id="ingredient-list" key={selected}>{course.idDania !== -1 ? course.składniki.map((s, i) => <p key={i}>{s.nazwa}</p>) : ""}</span></label>
                    :
                    ""}</div>
                <div>{selected !== 0
                    ?
                    <label htmlFor="calories-count">Ilość kalorii: <span id="calories-count" key={selected}>{course.ilośćKalorii ?? ""}</span></label>
                    :
                        ""}</div>
            </div>
            <div id="dialog-btns">
                <div className="btn-dark" id="add-course-btn" onClick={async () => {
                    const id = selected;
                    const date = new Date();
                    var object = {
                        "nazwa": course.nazwa,
                        "idUżytkownika": data.userId,
                        "idStołówki": data.cantineId,
                        "idDiety": course.idDiety,
                        "idDania": id,
                        "dzieńZamówienia": date.toLocaleDateString("pl-PL", {year: 'numeric'}) + "-" + date.toLocaleDateString("pl-PL", {month:'2-digit'}) + "-" + date.toLocaleDateString("pl-PL", {day: 'numeric'})
                    };
                    const response = await fetch("/api/Zamówienia",
                    {
                        method: "post",
                        headers: { "Content-Type": "application/json" },
                        body: JSON.stringify(object)
                    });

                    console.log(object);
                    console.log(JSON.stringify(object));
                    console.log(await response.json());
                }}>DODAJ</div>
                <div className="dialog-close" onClick={() => setPopup(false)}>ZAMKNIJ</div>
            </div>
        </div>
    </div>);
}

/*const NewOrder = ({ setPopup }) => {
    //

    return (<div>
            <div id="order-new-dialog" className="dialog">
            SOME TEXT - NEW
            <div className="dialog-close" onClick={() => setPopup(false)}>CLOSE</div>
        </div>
    </div>);
}*/

const deleteOrder = async ({ order }) => {
    console.log("Deleting order:");
    console.log(order);
    try {
        const response = await fetch("api/Zamówienia/" + order.idZamówienia, {
            method: "delete",
            headers: {
                "Content-Type" : "application/json"
            }
        });
    } catch (error) {
        console.log(error);
    }
}

/**
 * Modify your order by modifying the date, meal,
 * ingredients or removing it
*/
const ModifyOrder = ({ setPopup, data }) => {
    //
    const [order, orderUpdate] = useState({"danie": { "składniki": [{ "nazwa": "test" }] }, "dieta": {"dania": [{ "nazwa": "" }]}});
    const [selected, setSelected] = useState(0);
    useEffect(() => {
        let ignore = false;

        if (!ignore) {
            console.log(data);
            loadOrder(data, orderUpdate);
        }            
        
        return () => {
            ignore = true;
        }
    }, [data])

    return (<div>
        <div id="order-modify-dialog" className="dialog">
            <div>
                <div className="dialog-title">Modyfikuj zamówienie</div>
                <div id="order-modify-dialog-main">
                    <label htmlFor="change-order">
                        Wybierz danie:
                        <select id="change-order" onChange={e => {
                            setSelected(e.target.value);
                        }}>
                            {order.dieta.dania.map((l, k) => <option key={k} value={l}>{l.nazwa}</option>) }
                        </select>
                    </label>
                    <div id="change-date">
                        <label htmlFor="date-select">
                        Wybierz datę:
                            <input id="date-select" type="date" value={order.dzieńZamówienia} />
                        </label>
                    </div>
                    <div id="change-ingredients">
                        Usuń składniki:
                        {selected !== 0
                            ?
                            selected.danie.składniki.map((l, k) => <label htmlFor={`ingredient${k}`}>
                            {l.nazwa}
                            <input id={`ingredient${k}`} type="checkbox" /></label>)
                            : order.danie.składniki.map((l, k) => <label htmlFor={`ingredient${k}`}>
                            {l.nazwa}
                            <input id={`ingredient${k}`} type="checkbox" /></label>)}
                    </div>
                </div>
            </div>
            <div id="dialog-btns">
                <div className="dialog-confirm" onClick={() => updateOrder(order)}>POTWIERDŹ</div>
                <div className="dialog-close" onClick={() => setPopup(false)}>ZAMKNIJ</div>
            </div>
        </div>
    </div>);
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
    }, [cantines]);

    return (<div><div id="choose-cantine-dialog" className="dialog"><label id="select-label">Wybierz stołówkę<select id="select-cantine" value={cantine.id ?? 1} onChange={e => {
        Object.keys(cantine).forEach(key => chosenCantine[key] = cantine[key]);
        chosenCantine.id = e.target.value;
        console.log(e.target.value);
        chosenCantine.loaded = false;
        setCantine(chosenCantine);
    }}>{cantines.map(cantine => <option key={id++} value={cantine.substring(cantine.indexOf(" ") + 1, cantine.indexOf("-") - 1)}>{cantine}</option>)}</select></label><div className="dialog-close" onClick={() => setPopup(false)}>ZAMKNIJ</div></div></div>);
}

/**
 * Load specific order
 */
const loadOrder = async ({ order }, setOrder) => {
    console.log(order);
    const response = await fetch(`/api/Zamówienia/${order.idZamówienia}`);
    const data = await response.json();
    
    console.log(data);
    /*for (let i = 0; i < data.length; i++) {
        if (order[i].idZamówienia !== i) {
            orders.push(order[i]);
        }
    }*/
    setOrder(data);
}

/**
 * Update order with new data
 */
const updateOrder = async (newOrder) => {
    //
    console.log("Confirming changes");
}

/**
 * Download orders in a csv. file
 */
const downloadOrders = async (orders, alert, setAlert) => {
    var data = "Nazwa;Dzień zamówienia;Ilość kalorii;\n";
    const date = new Date();
    if (orders.length === 0) {
        setAlert({ ...alert, message: "Błąd: Lista zamówień jest pusta. Dodaj nowe zamówienia, aby móc eksportować pozycje" });
    } else {
        orders.forEach(order => data += `${order.nazwa};${order.dzieńZamówienia};${order.danie !== null && order.danie !== undefined ? order.danie.ilośćKalorii : "brak informacji"};\n`);
        const download = document.createElement("a");
        download.href = URL.createObjectURL(new Blob([data], { type: "text/plain;charset=utf-8" }));
        download.download = date.toLocaleDateString() + "_Zamówienia.csv";
        download.addEventListener("click", (e) => {
            setTimeout(() => URL.revokeObjectURL(download.href), 30 * 1000);
        });
        download.click();
    }    
}

export { Cantine }