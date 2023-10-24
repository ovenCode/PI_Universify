import React, { useEffect, useState } from 'react';

import "./Cantine.css";

const Cantine = () => {

    const [cantine, setCantine] = useState({ "skip": true });
    const zamowienia = []; //[1,2,3,5,6,7,8,9,0,99,88,77,66,55];
    var it = 0;
    let ignore = false;

    // Update component on cantine update
    useEffect(() => {
        //

        if (ignore) {
            return;
        }
    },[cantine]);
    
    return (
        <div>
            <div id="cantine-page">
                <div id="cantine">
                    <div id="cantine-title">Stołówka 1 <div id="choose-cantine">Wybierz stołówkę</div></div>
                    <div id="cantine-info">Godziny otwarcia: <div id="cantine-address"><span>Lokalizacja:</span> <span>Piętro:</span></div></div>
                    <div id="cantine-content">{cantine.skip ?
                        <div id="cantine-orders-content">
                            <div id="cantine-orders-block">
                                <span style={{ fontSize: 20, }}>Twoje zamówienia</span>
                                <div id="cantine-orders">
                                    {zamowienia.length != 0
                                        ?
                                        zamowienia.map((order) => <div key={it++}>Zamowienie {it + 1}</div>)
                                        :
                                        "Zamowienia ?? Brak zamowien"}
                                </div>
                            </div>
                            <div id="cantine-order-actions">
                                <div id="cantine-add-order">Dodaj zamowienie</div>
                                <div id="cantine-download-orders">Pobierz informacje o zamowieniach jesli sa zamowienia</div>
                            </div>
                        </div>
                        :
                        <div id="cantine-choose-diet">Wybierz diete jesli brak zamowien</div>}
                    </div>
                </div>
            </div>
        </div>
    );
}

export { Cantine }