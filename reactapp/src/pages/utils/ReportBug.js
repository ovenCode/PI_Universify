import "./ReportBug.css";

const ReportBug = ({ vis, toggleVis }) => {

    const types = ["Krytyczny", "Operacje logowania", "Wizualny", "Moduł", "Inne"];
    // TODO: Complete a proper report bug window

    return (<div id="report-bug">
        <div id="report-title"><h1>Zgłoś błąd</h1></div>
        <form id="report-form" method="post" onSubmit={() => sendReport()}>
            <div className="report-data">
                <label>Temat</label>
                <input required />
            </div>
            <div className="report-data">
                <label>Rodzaj błędu</label>
                <select required>
                    {types.map((type, k) => <option key={k}>{type}</option>)}
                </select>
            </div>
            <div className="report-data">
                <label>Opisz dokładnie problem</label>
                <textarea id="report-details" required />
            </div>
            <div className="report-data">
                <label>Dodaj załączniki</label>
                <div id="add-files">
                    <img id="attach-img" alt="dodaj-plik-ikona" src="./attach-file.png" />
                    <input id="attach-input" />
                </div>
            </div>
            {/* TODO: Ukryc jesli wymagane pola nie beda wypelnione*/<input type="submit" value="Zglos" />}
        </form>
        <div id="report-close" className="btn" onClick={() => toggleVis(vis.substring(0, vis.indexOf("ReportBug") - 1))}>CLOSE</div>
    </div>);
}

const sendReport = () => {
    // TODO: fill sendReport func
}

export default ReportBug;