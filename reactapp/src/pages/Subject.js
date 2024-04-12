import React, { useEffect, useState } from 'react';

import "./Subject.css";

const Subject = ({ userId }) => {
    //
    //const [subjects, setSubjects] = useState([]);
    const [pageData, setPageData] = useState({ subjects: [], selected: { nazwa: "", kategoria: "", semestrRozpoczęcia: 0, ilośćSemestrów: 0, resource: null, lessons: null } });
    const [panel, togglePanel] = useState({ width: "25%" });

    // On Mount
    useEffect(() => {
        let ignore = false;

        if (!ignore) {
            loadSubjects(userId, pageData, setPageData);
        }

        return () => {
            ignore = true;
        };
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (
        <div>
            <div id="subject-page">
                <div id="subject">
                    <div id="subject-select" style={{ width: panel.width }}>
                        <div id="subjects" style={panel.width === ".5em" ? { "display": "none" } : {}} >{
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
                            <img id="toggle-panel-img" src="left-arrow.png" alt="" />
                        </div>
                    </div>
                    <div id="divider" style={panel.width === ".5em" ? { "marginLeft": "1em" } : { "marginLeft": "0" }} />
                    <SubjectContent id="subject-content" content={pageData.selected} pageData={pageData} setPageData={setPageData} />
                </div>
            </div>
        </div>
    );
}

const loadSubjects = async (id, pageData, setSubjects) => {
    const response = await fetch("api/Przedmioty/Użytkownik/" + id);
    const data = await response.json();

    console.log(data);

    setSubjects({ ...pageData, subjects: data, selected: { ...data[0], resource: null } });
}

const SubjectContent = ({ id, content, setPageData, pageData }) => {

    return (<div id={id}>
        <div id="subject-header">
            <div id="subject-title">{content.nazwa}</div>
            <div id="subject-info">
                {content.selected.kategoria + " " + content.ilośćSemestrów + " " + content.semestrRozpoczęcia}
                <div id="subject-teachers">Prowadzący</div>
            </div>
        </div>
        <div id="subject-main">
            {true &&
                <div>
                    <div id="subject-introduction">Przedmiot Główna treść</div>
                    <div>
                        {/* Rozdzielenie przedmiotu na część wykładową i laby, jeśli nauczyciel to ustawi */}
                        <h3>Zasoby</h3>
                        <div id="subject-resources">
                            {/* Foldery do zasobów */}
                            <div id="resources-types">
                                {content.resources && content.resources.map((res, k) => <div key={k} className="subject-resource" onClick={() => setPageData({ ...pageData, selected: { ...content, resource: res.links } })}>
                                    {/*  */}
                                    {res.title}
                                </div>)}
                            </div>
                            {/* Linki do poszczególnych zasobów */}
                            <div id="resources-links" >
                                {content.resource && content.resource.map((link, k) => <div key={k} className="resource-link">{link}</div>)}
                            </div>
                            {/* Lista poszczególnych lekcji */}
                            {content.lessons && content.lessons.map()}
                        </div>
                    </div>
                </div>}
        </div>
    </div>);
}

export { Subject }