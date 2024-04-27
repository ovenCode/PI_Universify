import React, { useEffect, useState, useRef } from 'react';

import "./Subject.css";
import AnimateHeight from "react-animate-height";

const Subject = ({ userId, setError, mainContent }) => {
    //
    //const [subjects, setSubjects] = useState([]);
    const [pageData, setPageData] = useState({
        subjects: [],
        selected: { nazwa: "", kategoria: "", semestrRozpoczęcia: 0, ilośćSemestrów: 0, intro: null, resource: null, lekcje: null, popup: null, isEdit: false, isSplit: false, selectedTab: { name: "theory" } }
    });
    const [panel, togglePanel] = useState({ width: "25%" });

    // On Mount
    useEffect(() => {
        let ignore = false;

        if (!ignore) {
            loadSubjects(userId, pageData, setPageData, setError);
        }

        return () => {
            ignore = true;
        };
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (
        <div id="subject-page">
            <div id="subject" className="mainContent">
                <div id="subject-select" style={{ width: panel.width, transition: "all ease .3s" }}>
                    <div id="subjects" style={parseInt(panel.width.substring(0, panel.width.length - 1)) === 25 ? {} : { display: "none", visibility: "hidden" }} >{
                        pageData.subjects.length !== 0
                            ?
                            pageData.subjects.map((subject, k) => <div key={k} className="subject-side" onClick={() => setPageData({ ...pageData, selected: subject })}>
                                {subject.nazwa}
                            </div>)
                            :
                            <div>Brak przedmiotów</div>
                    }
                    </div>
                    <div id="subject-panel-toggle" onClick={panel.width === "25%" ? () => togglePanel({ ...panel, width: ".5em" }) : () => togglePanel({ ...panel, width: "25%" })}>
                        <img id="toggle-panel-img" src={panel.width === "25%" ? "left-arrow.png" : "right-arrow.png"} alt="" />
                    </div>
                </div>
                <div id="divider" style={panel.width === ".5em" ? { "marginLeft": "1em" } : { "marginLeft": "0" }} />
                <SubjectContent id="subject-content" content={pageData.selected} pageData={pageData} setPageData={setPageData} setError={setError} />
            </div>
        </div>
    );
}

const loadSubjects = async (id, pageData, setSubjects, setError) => {
    try {
        const response = await fetch("api/Przedmioty/Użytkownik/" + id);

        if (response.status !== 200) {
            setError({ code: response.status, message: response.statusText || response.title });
            return;
        }

        const data = await response.json();
        console.log(data);

        setSubjects({ ...pageData, type: data.uType, subjects: data.subjects, selected: { ...data.subjects[0], resource: null, selectedTab: { name: "theory" } } });
    } catch (error) {
        console.log("Error from loadSubjects");
        console.log(error);
        setError({ code: error.status || "", message: error.title || "" });
    }
}

const SubjectContent = ({ id, content, setPageData, pageData, setError }) => {

    const [height, setHeight] = useState(0);

    return (<div id={id}>
        <div id="subject-header">
            <div id="subject-title">{content.nazwa}</div>
            <div id="subject-info">
                {content.kategoria + " " + content.ilośćSemestrów + " " + content.semestrRozpoczęcia}
                <div id="subject-teachers" onClick={() => setPageData({ ...pageData, selected: { ...content, popup: "Nauczyciele" } })}>Prowadzący</div>
                {
                    (content.nauczyciele && content.popup === "Nauczyciele")
                    &&
                    <div id="subject-teachers-info">
                        <div id="subject-teachers-btn" className="dialog-btn-light" onClick={() => setPageData({ ...pageData, selected: { ...content, popup: null } })}>
                            X
                        </div>
                        <div id="subject-teachers-data">
                            {content.nauczyciele.map((teacher, k) => <div className="teacher-info" key={k}>
                                <div style={{ gridColumn: 1 }}>{`${teacher.imię} ${teacher.nazwisko}`}</div>
                                <div style={{ gridColumn: 2 }}>{`${teacher.specjalizacja.nazwa}`}</div>
                            </div>)}
                        </div>
                    </div>
                }
            </div>
            {pageData.type === "Nauczyciel" && <div id="subject-edit">
                {!content.isEdit
                    ?
                    <div id="edit-btn" className="btn" onClick={() => setPageData({ ...pageData, selected: { ...content, isEdit: true } })}>Edytuj</div>
                    :
                    <div id="save-btn" className="btn" onClick={() => {
                        // TODO: Add updating the subject
                        setPageData({ ...pageData, selected: { ...content, isEdit: true } });
                    }}>Zapisz zmiany</div>}
                {content.isEdit && <div id="split-course-btn" className="btn" onClick={() => setPageData({ ...pageData, selected: { ...content, isSplit: !content.isSplit } })}>Rozdziel przedmiot</div>}
                {content.isEdit && <div id="cancel-btn" className="btn" onClick={() => setPageData({ ...pageData, selected: { ...content, isEdit: false } })}>Anuluj</div>}
            </div>}
        </div>
        <div id="subject-main">
            {content.isSplit &&
                <div id="subjects-divided">
                    <div id="subjects-tab">
                        <div id="theory-tab" className={content.selectedTab.name === "theory" ? "subject-tab tab-selected" : "subject-tab"} onClick={() => setPageData({ ...pageData, selected: { ...content, selectedTab: { ...content.selectedTab, name: "theory" } } })}>Wykłady</div>
                        <div id="practice-tab" className={content.selectedTab.name === "practice" ? "subject-tab tab-selected" : "subject-tab"} onClick={() => setPageData({ ...pageData, selected: { ...content, selectedTab: { ...content.selectedTab, name: "practice" } } })}>Laboratoria</div>
                    </div>
                    <div id="subject-theory" className="subject-divided"></div>
                    <div id="subject-practice" className="subject-divided"></div>
                </div>}
            {true &&
                <div>
                    <div id="subject-introduction">
                        Przedmiot Główna treść - krotkie wprowadzenie
                        {(content.isEdit || content.intro !== null) &&
                            <textarea className={content.isEdit ? "subject-intro editable" : "subject-intro"} disabled={!content.isEdit} >tefdsadj</textarea>}
                    </div>
                    {/* Rozdzielenie przedmiotu na część wykładową i laby, jeśli nauczyciel to ustawi */}
                    <div>
                        <label>Zasoby</label>
                        <div id="subject-resources">
                            {/* Foldery do zasobów */}
                            <div id="resources-types">
                                {content.isEdit && <div id="add-resource-btn" className="btn" onClick={() => setPageData({ ...pageData, selected: { ...content, popup: "AddResource" } })}>Dodaj zasób</div>}
                                {content.isEdit && <div id="delete-resource-btn" className="btn" onClick={null}>Usuń zasób</div>}
                                {content.zasoby && content.zasoby.map((res, k) => <div key={k} className="subject-resource" onClick={() => {
                                    // FIXME: Fix that clicking on the other resource causes the first to close
                                    setPageData({ ...pageData, selected: { ...content, resource: res.links } });
                                    setHeight(height === 0 ? "auto" : 0);
                                }}>
                                    {/*  */}
                                    {res.title}
                                </div>)}
                            </div>
                            {/* Linki do poszczególnych zasobów */}
                            <AnimateHeight duration={300} height={height} >
                                <div id="resources-links" style={{
                                    display: content.resource === null ? "none" : "flex"
                                }}>
                                    {content.resource && content.resource.map((link, k) => <div key={k} className="resource-link">{link}</div>)}
                                </div>
                            </AnimateHeight>
                            {/* Lista poszczególnych lekcji */}
                            <div id="resources-lessons">
                                {content.lekcje && content.lekcje.map((l, k) => <div key={k} className="resource-lesson">
                                    <label className="lesson-title">{l.tytul}</label>
                                    <label className="lesson-description">{l.opis}</label>
                                    <div className="lesson-sources">
                                        {l.źródla.map((source, m) => <div key={m} className="lesson-source">
                                            <a href={source.tytul} rel="noreferrer" target="_blank">{source.tytul}</a>
                                        </div>)}
                                    </div>
                                    <p>{l.porady}</p>
                                    <div>TEST PODSUMOWUJACY</div>
                                </div>)}
                            </div>
                            <div id="resources-tests">TESTY OGOLNE</div>
                        </div>
                    </div>
                </div>}
            {content.popup === "AddResource" && <AddResource pageData={pageData} setPageData={setPageData} setError={setError} />}
        </div>
    </div>);
}

const AddResource = ({ pageData, setPageData, setError }) => {
    //
    const [selected, setSelected] = useState("Nagranie");
    const dialog = useRef(null);

    useEffect(() => {
        const handleClick = (e) => {
            if (dialog.current && !dialog.current.contains(e.target)) {
                setPageData({ ...pageData, selected: { ...pageData.selected, popup: null } });
            }
        };

        document.addEventListener("mousedown", handleClick);

        return () => {
            document.removeEventListener("mousedown", handleClick);
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    try {
        return (<div id="add-resource" className="dialog" ref={dialog}>
            <label>Wybierz rodzaj zasobu: </label>
            <select onChange={(e) => setSelected(e.target.value)}>
                <option value={"Nagranie"}>Nagranie</option>
                <option value={"Lekcja"}>Lekcja</option>
                <option value={"Źródło"}>Źródło</option>
                <option value={"Test"}>Test</option>
            </select>
            <div id="add-resource-main">
                {selected === "Nagranie" &&
                    <form id="add-recording-form">
                        <label>Nazwa nagrania </label>
                        <input />
                        <div id="add-recording">Przeciągnij i upuść tu plik lub <div id="add-recording-btn" className="dialog-form-btn btn-light">Dodaj</div></div>
                    </form>}
                {selected === "Lekcja" &&
                    <form id="add-lesson-form">
                        <label>Temat lekcji </label>
                        <input required />
                        <label>Wstęp do tematu </label>
                        <textarea />
                        <label>Linki </label>
                        <div id="add-links">
                            <div className="btn dialog-form-btn btn-dark">Dodaj link</div>
                        </div>
                    </form>}
                {selected === "Źródło"}
                {selected === "Test"}
                <input type="submit" value="Dodaj" />
            </div>
        </div>);
    } catch (error) {
        console.log("Error from AddResource");
        console.log(error);
        setError({ code: error.status || "", message: error.title || "" });
    }
}

export { Subject }