import { useEffect, useState, useRef } from "react";
import "./AdminHub.css";
import { Loader } from "./utils/Loader";

/**
 * 
 * @param {{userId: Number, mainContent: String, setError: React.Dispatch<React.SetStateAction<{code: String, message: String}>>}} object Id użytkownika, nazwa klasy CSS oraz funkcja do informowania o błędach
 * @returns Panel administratora, gdzie użytkownik może modyfikować system, zgodnie z dostępnymi funkcjami i uprawnieniami 
 */
const AdminHub = ({ userId, mainContent, setError }) => {

    const [data, setData] = useState(null);
    const [admin, setAdmin] = useState({ main: "Użytkownicy", popup: null });

    useEffect(() => {
        let ignore = false;

        if (!ignore) {
            loadAdmin(setError, setData);
        }

        return () => {
            ignore = true;
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    // const temp = [{
    //     idUżytkownika: 1, imię: "", nazwisko: "Test", mail: "mail", hasło: "pass", grupa: "gr", nrTel: "nr", budynek: "build",
    //     idAdministratora: 1, idRoli: -1, idNauczyciela: -1, idWydziału: -1, idSpecjalizacji: -1, idStudenta: -1, idGrupyStudenckiej: -1,
    //     idKierunkuStudiów: -1, rokStudiów: -1
    // }, {
    //     idUżytkownika: 1, imię: "", nazwisko: "Test", mail: "mail", hasło: "pass", grupa: "gr", nrTel: "nr", budynek: "build",
    //     idAdministratora: 1, idRoli: -1, idNauczyciela: -1, idWydziału: -1, idSpecjalizacji: -1, idStudenta: -1, idGrupyStudenckiej: -1,
    //     idKierunkuStudiów: -1, rokStudiów: -1
    // }];

    return (<div id="admin-hub" className="mainContent">
        <div>ADMIN</div>
        <div id="admin">
            <div id="admin-sidebar">
                Sidebar
                <ul style={{ listStyleType: "square" }}>
                    <li>
                        <div className="btn list-btn" onClick={() => setAdmin({ ...admin, main: "Użytkownicy" })}>Zarządzaj użytkownikami</div>
                    </li>
                    <li>
                        <div className="btn list-btn" onClick={() => setAdmin({ ...admin, main: "Stołówki" })}>Zarządzaj stołówkami</div>
                    </li>
                    <li>
                        <div className="btn list-btn" onClick={() => setAdmin({ ...admin, main: "Parkingi" })}>Zarządzaj parkingami</div>
                    </li>
                    <li>
                        <div className="btn list-btn" onClick={() => setAdmin({ ...admin, main: "Przedmioty" })}>Zarządzaj przedmiotami</div>
                    </li>
                </ul>
                <div className="btn" onClick={() => setAdmin({ ...admin, main: "Other" })}>Pozostałe</div>
            </div>
            <div id="admin-panel">
                {admin.main === "Użytkownicy" && <UserAdmin data={data} setData={setData} admin={admin} setAdmin={setAdmin} setError={setError} />}
                {admin.main === "Parkingi" && <ParkingAdmin data={data} setData={setData} admin={setAdmin} setError={setError} />}
                {admin.main === "Stołówki" && <CantineAdmin data={data} setData={setData} admin={setAdmin} setError={setError} />}
                {admin.main === "Przedmioty" && <SubjectAdmin data={data} setData={setData} admin={setAdmin} setError={setError} />}
                {admin.main === "Other" && <OtherAdmin data={data} setData={setData} admin={setAdmin} setError={setError} />}
            </div>
        </div>
    </div>);
}

const loadAdmin = async (setError, setData) => {

    try {
        const response = await fetch("api/uzytkownicy");

        if (response.status !== 200) {
            throw new Error(response.statusText);
        }

        const data = await response.json();

        console.log(data);
        setData(data);
    } catch (error) {
        console.log("Error from loadAdmin");
        console.log(error);
        setError({ code: error.status || "", message: error.title || "" });
    }
    return [];
}

const UserAdmin = ({ data, setData, admin, setAdmin, setError }) => {

    const [hide, setHidden] = useState(true);
    const [user, setUser] = useState(null);

    useEffect(() => {

        if (data !== null) {
            setUser(data.map((item, k) => { return { id: k, value: false }; }));
            console.log("Updated user list");
        }
        console.log("Selected users");
        console.log(user);
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [data]);

    return (<div id="user-admin">
        <div>Lista użytkowników</div>
        {data && <div id="user-list">
            <table id="user-table">
                <tbody>
                    <tr id="list-headers">
                        <th><input type="checkbox" checked={user !== null && user.every(e => e.value === true)} onChange={(e) => setUser((user !== null || user.length === 0) && user.map((u) => { return { ...u, value: e.target.checked }; }))} /></th>
                        {Object.keys(data[0]).map((item, k) => <th key={k}>{item}</th>)}
                    </tr>
                    {data.map((item, k) => <tr key={k} className="list-rows">
                        <td><input type="checkbox" checked={user === null ? false : user[k].value} onChange={(e) => setUser(user.map(((u, i) => i === k ? { ...u, value: e.target.checked } : u)))} /></td>
                        {Object.values(item).map((val, l) => {
                            if (l === 4) {
                                return <td key={l} className={user === null ? "list-cell list-pass" : user[k].value ? "list-cell clicked list-pass" : "list-cell list-pass"}>{hide ? val.replaceAll(/\w/g, '\u2022') : val} <div className="list-pass-btn" onClick={() => setHidden(!hide)}>EYE</div></td>;
                            }
                            return <td key={l} className={user === null ? "list-cell" : user[k].value ? "list-cell clicked" : "list-cell"}>{val}</td>;
                        })}
                    </tr>)}
                </tbody>
            </table>
        </div>}
        <div id="user-controls">
            <div id="refresh-btn" className="btn" onClick={async () => await loadAdmin(setError, setAdmin)}>Odśwież</div>
            <div id="add-user-btn" className="btn" onClick={() => setAdmin({ ...admin, popup: "add" })}>Dodaj użytkownika</div>
            <div id="modify-user-btn" className={(user !== null && user.filter(e => e.value).length !== 1) ? "disabled btn" : "btn"} onClick={(user !== null && user.filter(e => e.value === true).length === 1) ? () => setAdmin({ ...admin, popup: "modify" }) : () => null}>Modyfikuj</div>
            <div id="delete-user-btn" className={(user !== null && user.some(e => e.value)) ? "btn" : "disabled btn"} onClick={(user !== null && user.some(e => e.value === true)) ? () => deleteUser(user.filter(u => u.value)[0].id, user.filter(u => u.value).map(u => u.id), setError) : () => null}>Usuń</div>
        </div>
        {admin && admin.popup === "add" && <AddUser admin={admin} setAdmin={setAdmin} setError={setError} />}
        {admin && admin.popup === "modify" && <ModifyUser admin={admin} setAdmin={setAdmin} setError={setError} />}
    </div>);
}

const ParkingAdmin = ({ data, setData, admin, setAdmin, setError }) => {
    return (<div id="parking-admin">
        {/*  */}
    </div>);
}
const CantineAdmin = ({ data, setData, admin, setAdmin, setError }) => {
    return (<div id="cantine-admin">
        {/*  */}
    </div>);
}
const SubjectAdmin = ({ data, setData, admin, setAdmin, setError }) => {
    return (<div id="subject-admin">
        {/*  */}
    </div>);
}
/**
 * 
 * @param {{
 * data: [],
 * setData: React.Dispatch<React.SetStateAction>,
 * admin: { main: String, popup: any},
 * setAdmin: React.Dispatch<React.SetStateAction<{ main: String, popup: any}>>,
 * setError: React.Dispatch<React.SetStateAction<{code: String, message: String}>>}} object Funkcje do ustawiania 
 * @returns Interfejs do sterowania ustawieniami systemu, nie wymienionymi
 */
const OtherAdmin = ({ data, setData, admin, setAdmin, setError }) => {

    const [other, setOther] = useState(null);

    useEffect(() => {
        let ignore = false;
        // Załadować funkcje w zależności od uprawnień użytkownika
        if (!ignore) {
            // temp
            loadRoles(-1, other, setOther, setError);
        }

        return () => {
            ignore = true;
        };
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    console.log(other);

    return (<div id="other-admin">
        {/* Role */}
        <label htmlFor="other-roles">Role</label>
        <div id="other-roles" className="other-section">
            <div id="roles-list">
                {other && other.roles.map((role, k) => <div key={k} className={role.selected ? "item-list-selected" : ""} onClick={() => setOther({ ...other, roles: other.roles.map((r, l) => l === k ? { ...r, selected: !r.selected } : { ...r }) })}>{role.value.nazwa}</div>)}
            </div>
            <div id="roles-btns">
                <div className="btn" onClick={() => setOther({ ...other, popup: "add" })}>Dodaj rolę</div>
                <div className={(other && other.roles.filter(e => e.selected).length === 1) ? "btn" : "disabled btn"} onClick={(other && other.roles.filter(e => e.selected).length === 1) ? () => setOther({ ...other, popup: "modify" }) : () => null}>Modyfikuj rolę</div>
                <div className={(other && other.roles.filter(e => e.selected).length >= 1) ? "btn" : "disabled btn"} onClick={(other && other.roles.filter(e => e.selected).length >= 1) ? () => deleteRole(other.roles[other.roles.findIndex(r => r.selected)].value.idRoli, setError) : () => null}>Usuń rolę</div>
            </div>
        </div>
        {/* <div id></div> */}
        {other && other.popup === "add" && <AddRole other={other} setOther={setOther} setError={setError} />}
        {other && other.popup === "modify" && <ModifyRole selected={other.roles[other.roles.findIndex(r => r.selected)].value.idRoli - 1} other={other} setOther={setOther} setError={setError} />}
    </div>);
}
/**
 * Dialog do dodawania nowego użytkownika
 * 
 * @param {{admin: {main: string, popup: null }, 
 * setAdmin: React.Dispatch<React.SetStateAction<{main: string, popup: null}>>,
 * setError: React.Dispatch<React.SetStateAction<{code: string, message: string}>>}} object admin to dane z interfejsu administracyjnego
 * @returns Dialog do dodawania nowego użytkownika
 */
const AddUser = ({ admin, setAdmin, setError }) => {

    const dialog = useRef(null);
    const [selected, setSelected] = useState("Student");
    const [newUser, setNewUser] = useState({ isLoading: true, listClicked: null });

    useEffect(() => {
        const handleClick = (e) => {
            if (dialog.current && !dialog.current.contains(e.target)) {
                setAdmin({ ...admin, popup: null });
            }
        };

        document.addEventListener("mousedown", handleClick);

        return () => {
            document.removeEventListener("mousedown", handleClick);
        };
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [])

    useEffect(() => {
        let ignore = false;

        if (!ignore) {
            loadUserData(selected, setNewUser, setError);
        }

        return () => {
            ignore = true;
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, [selected]);

    return (<div id="add-user-dialog" ref={dialog} className="dialog">
        <div style={{ fontSize: "1.1em" }}>Dodaj użytkownika</div>
        {(newUser && newUser.isLoading) && <Loader message={"Ładowanie"} />}
        <form className="dialog-form" onSubmit={async (e) => {
            e.preventDefault();
            const requestOptions = {
                method: "POST",
                headers: { "Content-Type": "application/json", "Accept": "application/json" },
                body: JSON.stringify({
                    Imię: e.target.firstname.value.toString(),
                    Nazwisko: e.target.lastname.value.toString(),
                    Mail: e.target.email.value.toString(),
                    Hasło: e.target.password.value.toString(),
                    Grupa: e.target.group.value.toString(),
                    NumerTel: "",
                    IdBudynku: null,
                    IdRoli: newUser.rola === undefined ? null : newUser.rola.idRoli,
                    Rola: newUser.rola === undefined ? null : newUser.rola,
                    IdWydziału: newUser.wydział === undefined ? null : newUser.wydział.idWydziału,
                    Wydział: newUser.wydział === undefined ? null : newUser.wydział,
                    IdSpecjalizacji: newUser.specjalizacja === undefined ? null : newUser.specjalizacja.idSpecjalizacji,
                    Specjalizacja: newUser.specjalizacja === undefined ? null : newUser.specjalizacja,
                    IdGrupyStudenckiej: newUser.grupaStudencka === undefined ? null : newUser.grupaStudencka.idGrupy,
                    GrupaStudencka: newUser.grupaStudencka === undefined ? null : newUser.grupaStudencka,
                    IdKierunkuStudiów: newUser.kierunekStudiów === undefined ? null : newUser.kierunekStudiów.idKierunku,
                    KierunekStudiów: newUser.kierunekStudiów === undefined ? null : { ...newUser.kierunekStudiów, studenci: [] },
                    Profil: null
                })
            };

            try {
                console.log(requestOptions.body);
                console.log(newUser);
                const response = await fetch(`api/${selected === "Administrator" ? "Administratorzy" : selected === "Nauczyciel" ? "Nauczyciele" : "Studenci"}`, requestOptions);

                if (response.status !== 200 && response.status !== 201) {
                    setError({ code: response.status, message: JSON.stringify(await response.text(), null, "\t") || response.title });
                    return;
                } else if (response.status === 201) {
                    alert("Poprawnie stworzono użytkownika");
                }

                const data = await response.json();
                console.log(data);
            } catch (error) {
                console.log("Error from AddUser");
                setError({ code: error || "", message: error || "Błąd przy tworzeniu użytkownika" });
            }
        }}>
            <div id="form-content">
                <div id="user-part" className="form-part">
                    <div className="form-input">
                        <label htmlFor="firstname">Imię</label>
                        <input type="text" name="firstname" required />
                        <label htmlFor="lastname">Nazwisko</label>
                        <input type="text" name="lastname" required />
                    </div>
                    <div className="form-input">
                        <label htmlFor="email">E-Mail</label>
                        <input type="email" name="email" required />
                        <label htmlFor="password">Hasło</label>
                        <input type="password" name="password" required />
                    </div>
                    <div className="form-input">
                        <label htmlFor="group">Grupa</label>
                        <input type="text" name="group" />
                    </div>
                    <div className="form-input">
                        <label htmlFor="usertype">Typ użytkownika</label>
                        <select name="usertype" onChange={(e) => setSelected(e.target.value)} defaultValue={"Student"} required>
                            <option value={"Administrator"}>Administrator</option>
                            <option value={"Nauczyciel"}>Nauczyciel</option>
                            <option value={"Student"}>Student</option>
                        </select>
                    </div>
                </div>
                {selected === "Administrator" && <div id="admin-part" className="form-part">
                    <label htmlFor="rsearch">Role</label>
                    <div className="searchbox">
                        <input type="search" name="rsearch" value={(!newUser.isLoading && newUser.rola !== undefined) ? newUser.rola.nazwa : ""} onClick={() => setNewUser({ ...newUser, listClicked: "role" })} onChange={() => null} required />
                        {(!newUser.isLoading && newUser.listClicked === "role") && <div id="role-list">
                            {newUser.role.map((role, k) => <div key={k} className="searchbox-el user-role" onClick={() => setNewUser({ ...newUser, rola: role, listClicked: null })}>{role.nazwa}</div>)}
                        </div>}
                    </div>
                </div>}
                {selected === "Nauczyciel" && <div id="teacher-part" className="form-part">
                    <label htmlFor="wsearch">Wydziały</label>
                    <div className="searchbox">
                        <input type="search" name="wsearch" value={(!newUser.isLoading && newUser.wydział !== undefined) ? newUser.wydział.nazwa : ""} onClick={() => setNewUser({ ...newUser, listClicked: "wydzialy" })} onChange={() => null} required />
                        {(!newUser.isLoading && newUser.listClicked === "wydzialy") && <div id="faculty-list">
                            {newUser.wydzialy.map((faculty, k) => <div key={k} className="searchbox-el user-faculty" onClick={() => setNewUser({ ...newUser, wydział: faculty, listClicked: null })}>{faculty.nazwa}</div>)}
                        </div>}
                    </div>
                    <label htmlFor="msearch">Specjalizacje</label>
                    <div className="searchbox">
                        <input type="search" name="msearch" value={(!newUser.isLoading && newUser.specjalizacja !== undefined) ? newUser.specjalizacja.nazwa : ""} onClick={() => setNewUser({ ...newUser, listClicked: "specjalizacje" })} onChange={() => null} required />
                        {(!newUser.isLoading && newUser.listClicked === "specjalizacje") && <div id="major-list">
                            {newUser.specjalizacje.map((major, k) => <div key={k} className="searchbox-el user-major" onClick={() => setNewUser({ ...newUser, specjalizacja: major, listClicked: null })}>{major.nazwa}</div>)}
                        </div>}
                    </div>
                    <label htmlFor="ssearch">Przedmioty</label>
                    <div className="searchbox">
                        <input type="search" name="ssearch" value={(!newUser.isLoading && newUser.przedmiot !== undefined) ? newUser.przedmiot.nazwa : ""} onClick={() => setNewUser({ ...newUser, listClicked: "przedmioty" })} onChange={() => null} required />
                        {(!newUser.isLoading && newUser.listClicked === "przedmioty") && <div id="subject-list">
                            {newUser.przedmioty.map((subject, k) => <div key={k} className="searchbox-el user-subject" onClick={() => setNewUser({ ...newUser, przedmiot: subject, listClicked: null })}>{subject.nazwa}</div>)}
                        </div>}
                    </div>
                </div>}
                {selected === "Student" && <div id="student-part" className="form-part">
                    <label htmlFor="gsearch">Grupy studenckie</label>
                    <div className="searchbox">
                        <input type="search" name="gsearch" value={(!newUser.isLoading && newUser.grupaStudencka !== undefined) ? newUser.grupaStudencka.nazwa : ""} onChange={(e) => { }} onClick={() => setNewUser({ ...newUser, listClicked: "grupy studenckie" })} required />
                        {(!newUser.isLoading && newUser.listClicked === "grupy studenckie") && <div id="group-list">
                            {newUser.grupyStudenckie.map((group, k) => <div key={k} className="searchbox-el user-group" onClick={() => setNewUser({ ...newUser, grupaStudencka: group, listClicked: null })}>{group.nazwa}</div>)}
                        </div>}
                    </div>
                    <label htmlFor="ksearch">Kierunek studiów</label>
                    <div className="searchbox">
                        <input type="search" name="ksearch" value={(!newUser.isLoading && newUser.kierunekStudiów !== undefined) ? newUser.kierunekStudiów.nazwaKierunku : ""} onChange={() => null} onClick={() => setNewUser({ ...newUser, listClicked: "kierunki studiów" })} required />
                        {(!newUser.isLoading && newUser.listClicked === "kierunki studiów") && <div id="course-list">
                            {newUser.kierunkiStudiów.map((course, k) => <div key={k} className="searchbox-el user-course" onClick={() => setNewUser({ ...newUser, kierunekStudiów: course, listClicked: null })}>{course.nazwaKierunku}</div>)}
                        </div>}
                    </div>
                </div>}
            </div>
            <input type="submit" value="Dodaj" />
        </form>
    </div >);
}

const ModifyUser = ({ admin, setAdmin, setError }) => {
    //
    return (<div id="modify-user-dialog" className="dialog"></div>);
}

/**
 * 
 * @param {String} type Typ użytkownika, który chcemy dodać
 * @param {React.Dispatch<React.SetStateAction>} setNewUser Funkcja do załadowania wczytanych danych
 * @param {React.Dispatch<React.SetStateAction<{code: String, message: String}>>} setError Funkcja obsługująca błędy od części UI
 */
const loadUserData = async (type, setNewUser, setError) => {
    try {
        let user = {};
        setNewUser({ isLoading: true });

        if (type === "Administrator") {
            var response = await fetch("api/Role");

            if (response.status !== 200) {
                setError({ code: response.status, message: response.statusText });
            }

            var data = await response.json();

            user = { ...user, role: data, isLoading: false };

            setNewUser(user);
        } else if (type === "Nauczyciel") {
            response = await fetch("api/Wydziały");

            if (response.status !== 200) {
                setError({ code: response.status, message: response.statusText });
            }

            data = await response.json();

            user = { ...user, wydzialy: data, isLoading: false };

            response = await fetch("api/Specjalizacje");

            if (response.status !== 200) {
                setError({ code: response.status, message: response.statusText });
            }

            data = await response.json();

            user = { ...user, specjalizacje: data };

            response = await fetch("api/Przedmioty");

            if (response.status !== 200) {
                setError({ code: response.status, message: response.statusText });
            }

            data = await response.json();

            user = { ...user, przedmioty: data };
        } else {
            response = await fetch("api/GrupyStudenckie");

            if (response.status !== 200) {
                setError({ code: response.status, message: response.statusText });
            }

            data = await response.json();

            user = { ...user, grupyStudenckie: data, isLoading: false };

            response = await fetch("api/KierunkiStudiów");

            if (response.status !== 200) {
                setError({ code: response.status, message: response.statusText });
            }

            data = await response.json();

            user = { ...user, kierunkiStudiów: data };
        }

        if (Object.keys(user).length !== 0) setNewUser(user);
    }
    catch (error) {
        console.log("Error from loadUserData");
        console.log(error);
        setError({ code: "", message: "" });
    }

}

/**
 * Funkcja usuwająca użytkownika z systemu
 * 
 * @param {Number} id Id użytkownika do usunięcia
 * @param {React.Dispatch<React.SetStateAction>} setError Funkcja obsługująca błędy od części UI
 * @returns String
 */
const deleteUser = async (id, ids = id, setError) => {
    // TODO: dodać sprawdzanie czy użytkownik jest pewien, że chce usunąć i jakiś rodzaj potwierdzania
    try {
        const requestOptions = {
            method: "DELETE",
            headers: { "Content-Type": "application/json", "Accept": "application/json" },
            body: JSON.stringify(ids)
        };
        console.log(requestOptions);

        if (window.confirm("Jesteś pewien, że chcesz usunąć tego/tych użytkowników?")) {
            const response = await fetch(`api/Uzytkownicy/${id}`, requestOptions);

            if (response.status !== 200) {
                console.log(response);
                setError({ code: response.status, message: response.statusText });
            }
        } else return;

    } catch (error) {
        console.log("Error from deleteUser");
        console.log(error);
        setError({ code: error || "", message: error || "" })
    }
    return "Success";
}

const AddRole = ({ other, setOther, setError }) => {
    const dialog = useRef(null);

    useEffect(() => {
        const handleClick = (e) => {
            if (dialog.current && !dialog.current.contains(e.target)) {
                setOther({ ...other, popup: null });
            }
        };

        document.addEventListener("mousedown", handleClick);

        return () => {
            document.removeEventListener("mousedown", handleClick);
        };
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    useEffect(() => {
        let ignore = false;

        if (!ignore) {
            loadPermissions(-1, other, setOther, setError);
        }

        return () => {
            ignore = true;
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    useEffect(() => console.log(other), [other]);

    return (<div id="add-role-dialog" ref={dialog} className="role-dialog dialog">
        <form onSubmit={(e) => newRole(e, other, setError)}>
            <div className="form-input">
                <label htmlFor="roleName">Nazwa roli</label>
                <input type="text" name="roleName" />
            </div>
            <div className="form-input">
                <label htmlFor="permissions-list">Uprawnienia</label>
                <div className="permissions-list" name="permissions-list">
                    {other.permissions && other.permissions.map((perm, k) => <div key={k} className="permission-list">
                        <input type="checkbox" checked={k % 4 === 0 ? other.permissions.slice(k + 1, k + 4).every(e => e.selected) : perm.selected} name="perm-check" onChange={(e) => setOther({ ...other, permissions: other.permissions.map((p, l) => k % 4 === 0 && l < k + 4 ? { ...p, selected: e.target.checked } : l === k ? { ...p, selected: e.target.checked } : { ...p }) })} />
                        <label htmlFor="perm-check">{perm.value.nazwa}</label>
                    </div>)}
                </div>
            </div>
            <input type="submit" value="Dodaj rolę" />
        </form>
    </div>);
}

const ModifyRole = ({ selected, other, setOther, setError }) => {
    const dialog = useRef(null);

    useEffect(() => {
        const handleClick = (e) => {
            if (dialog.current && !dialog.current.contains(e.target)) {
                setOther({ ...other, popup: null, roles: other.roles.map((r, k) => k === selected ? { ...other.roles[selected], uprawnienia: [] } : { ...other.roles[k] }) });
            }
        };

        document.addEventListener("mousedown", handleClick);

        return () => {
            document.removeEventListener("mousedown", handleClick);
        };
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    useEffect(() => {
        let ignore = false;

        if (!ignore) {
            loadPermissions(selected, other, setOther, setError);
        }

        return () => {
            ignore = true;
        }
        // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (<div id="modify-role-dialog" ref={dialog} className="role-dialog dialog">
        <form onSubmit={(e) => { updateRole(e, other.roles[selected].value, setError); setOther({ ...other, popup: null }); }}>
            <div className="form-input">
                <label htmlFor="roleName">Nazwa roli</label>
                <input type="text" name="roleName" defaultValue={other.roles[selected].value.nazwa} />
            </div>
            <div className="form-input">
                <label htmlFor="permissions-list">Uprawnienia</label>
                <div className="permissions-list" name="permissions-list">
                    {other.permissions && other.permissions.map((perm, k) => <div key={k} className="permission-list">
                        <input type="checkbox" checked={k % 4 === 0 ? other.permissions.slice(k + 1, k + 4).every(e => e.selected) : perm.selected} name="perm-check" onChange={(e) => setOther({ ...other, roles: other.roles.map((r, l) => l === selected ? { ...other.roles[selected], value: { ...other.roles[selected].value, uprawnienia: other.roles[selected].value.uprawnienia.some(el => el.nazwa === perm.value.nazwa) ? [...other.roles[selected].value.uprawnienia] : k % 4 === 0 ? [...other.roles[selected].value.uprawnienia, ...other.permissions.map((p, m) => k % 4 === 0 && (k <= m && m < k + 4) && { ...p, selected: e.target.checked }).filter(e => e !== false)] : [...other.roles[selected].value.uprawnienia, { ...perm, selected: e.target.checked }] } } : { ...other.roles[l] }), permissions: other.permissions.map((p, l) => k % 4 === 0 && (k < l && l < k + 4) ? { ...p, selected: e.target.checked } : l === k ? { ...p, selected: e.target.checked } : { ...p }) })} />
                        <label htmlFor="perm-check">{perm.value.nazwa}</label>
                    </div>)}
                </div>
            </div>
            <input type="submit" value="Aktualizuj rolę" />
        </form>
    </div>);
}

const loadPermissions = async (selected, other, setOther, setError) => {
    try {
        const response = await fetch("api/Uprawnienia");

        if (response.status !== 200) {
            setError({ code: response.status || "", message: await response.text() || response.statusText });
            return;
        }
        // { ...other, permissions: other.permissions.map((el, k) => other.roles[selected].value.uprawnienia.some((up, l) => up.nazwa === el.value.nazwa) ? { ...el, selected: true } : { ...el }) }
        const data = await response.json();
        setOther({ ...other, permissions: data.map((perm, k) => { return { selected: selected > -1 ? other.roles[selected].value.uprawnienia.some((up, l) => up.nazwa === perm.nazwa) : false, value: perm }; }) });
    } catch (error) {
        console.log("Error from loadPermissions");
        console.log(error);
        setError({ code: error || "", message: error || "" })
    }
}

const loadRoles = async (id, other, setOther, setError) => {
    try {
        const response = id < 0 ? await fetch("api/Role") : await fetch(`api/Role/${id}`);

        if (response.status !== 200 && response.status !== 204) {
            setError({ code: response.status, message: await response.text() || response.statusText });
            return;
        }

        const data = await response.json();
        console.log(id);

        console.log(data);
        setOther(id < 0 ? { ...other, roles: data.map((el) => { return { selected: false, value: el }; }) } : { ...other, roles: [...other.roles.slice(0, id), { selected: false, value: data }, ...other.roles.slice(id)] });

    } catch (error) {
        console.log("Error from loadRole");
        console.log(error);
        setError({ code: error || "", message: error || "" });
    }
}

const newRole = async (e, role, setError) => {
    e.preventDefault();
    const requestOptions = {
        method: "POST",
        headers: { "Content-Type": "application/json", "Accept": "application/json" },
        body: JSON.stringify({
            idRoli: 0,
            nazwa: e.target.roleName.value.toString(),
            uprawnienia: role.permissions.filter(e => e.selected).map(p => { return { ...p.value }; })
        })
    };

    try {
        const response = await fetch("api/Role", requestOptions);

        if (response !== 200) {
            setError({ code: response.status, message: JSON.stringify(await response.text(), null, "\t") || response.title });
            return
        } else if (response.status === 201) {
            alert("Rola poprawnie dodana");
            return;
        }

    } catch (error) {
        console.log("Error from newRole");
        console.log(error);
        //
    }
    return;
}

const updateRole = async (e, role, setError) => {
    e.preventDefault();
    const requestOptions = {
        method: "PUT",
        headers: { "Content-Type": "application/json", "Accept": "application/json" },
        body: JSON.stringify({
            IdRoli: role.idRoli,
            Nazwa: e.target.roleName.value.toString(),
            Uprawnienia: role.uprawnienia.map((u, k) => u.value) //{ return { RoleIdRoli: u.value.idRoli, UprawnienieIdUprawnienia: k + 1, Rola: role, Uprawnienie: u.value }; 
        })
    };

    console.log(requestOptions);
    try {
        const response = await fetch(`api/Role/${role.idRoli}`, requestOptions);

        if (response.status !== 200 && response.status !== 204) {
            setError({ code: response.status, message: JSON.stringify(await response.text(), null, "\t") || response.title });
            return;
        } else if (response.status === 204) {
            alert("Poprawnie zaktualizowano rolę.");
            return;
        }

        const data = await response.json();
        console.log(data);
    } catch (error) {
        console.log("Error from updateRole");
        setError({ code: error || "", message: error || "" });
    }
    return;
}

const deleteRole = async (id, setError) => {
    const requestOptions = {
        method: "DELETE",
        headers: { "Content-Type": "application/json", "Accept": "application/json" }
    };
    try {
        const response = await fetch(`api/Role/${id}`, requestOptions);

        if (response !== 200) {
            setError({ code: response.status, message: await response.text() || response.title });
            return;
        }

    } catch (error) {
        console.log("Error from deleteRole");
        console.log(error);
        setError({ code: error || "", message: error || "" });
    }
    return;
}

export default AdminHub;