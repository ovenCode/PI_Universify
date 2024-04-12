import React, { useState, useEffect, useRef } from 'react';
import "./Profile.css";



const Profile = (props) => {
    //const [loading, setLoading] = useState(true);
    const [profileInfo, setProfileInfo] = useState({ picture: "", info: { name: "", lastname: "", mail: "", group: "", curriculum: "" }, sidebar: { info: "", }, content: { calendar: { name: "Some calendar", currentMonth: "", months: [{ name: "", days: [{ number: 1, upcoming: [{name: "", date: ""}] }]}]}, feed: ["",] } });
    //const [img, setPicture] = useState({ img: "" });
    const id = props.userId;
    // ON MOUNT EFFECT
    useEffect(() => {
        let ignore = false;
        loadProfileInfo({ ignore, setProfileInfo, id });

        return () => {
            ignore = true;
        }
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []);

    return (
        <div id="profile-page">
            <div id="profile">
                <div id="profile-container">
                    <div id="profile-picture"><RenderPicture id="picture-image" img={profileInfo.picture} /></div>
                    <div id="profile-info"><RenderProfileInfo data={profileInfo.info} /></div>
                </div>
                <div id="profile-sidebar">TEST 3</div>
                <div id="profile-divider"></div>
                <div id="profile-content"><RenderProfileContent content={profileInfo.content} /></div>
            </div>
        </div>
    )
}

const RenderPicture = ({ id, img }) => {
    return <img id={id} src={img} alt="" />
}

const loadProfileInfo = async (props) => {
    try {
        const response = await fetch("/api/Profile/"+ props.id + "/");
        const data = await response.json();
        //const response_picture = await fetch("/api/Files/basic_temp.jpg");
        //const picture = await response_picture.url;

        console.log("NEXT");
        //console.log(response);
        //console.log(response.json());
        console.log("This is the response: ");
        console.log(data);
        console.log("loadProfileInfo::props: ");
        console.log(props);

        if (!props.ignore) {
            //props.setLoading(false);
            props.setProfileInfo(data);
            //props.setPicture(picture);
        }
    } catch (e) {
        console.log(e);
        return false;
    }


    return true;
}

const RenderProfileInfo = ({ data }) => {

    const ProfileInfo = (<div>
        <div id="profile-name">{data.name + " " + data.lastname}</div>
        <div className="profile-info-sub">
            <div id="profile-group">{data.group}</div>
            <div id="profile-curriculum">Some info</div>
        </div>
        <div className="profile-info-sub">
            <div id="profile-mail">{data.mail}</div>
            <div id="profile-additional">{data.curriculum}</div>
        </div>
    </div>);

    return ProfileInfo;
}

const RenderProfileContent = ({ content }) => {
    const [upcomingEvents, setEvents] = useState([{ name: '', date: '' }]);
    const [selectedDay, setDay] = useState('0');
    const calEvent = useRef(null);

    /**
     * Setting events
     */
    useEffect(() => {
        let ignore = false;
        let temp = [];
        if (!ignore) {
            content.calendar.months.forEach(month => month.days.forEach(day => day.upcoming.forEach(event => temp.push(event))));
            setEvents(temp);
        }

        if (calEvent.current !== null && calEvent.current.children.length === upcomingEvents.length) {
            //calEvent.current.children[upcomingEvents.findIndex(event => event.date === `${new Date(Date.now()).getDate()}/${new Date(Date.now()).getMonth() + 1}`) !== -1 ? upcomingEvents.findIndex(event => event.date === `${new Date(Date.now()).getDate()}/${new Date(Date.now()).getMonth() + 1}`) + 1 : 0].scrollIntoView(false);
        }

        return () => {
            ignore = true;
        };
    // eslint-disable-next-line react-hooks/exhaustive-deps
    }, []); // content.calendar, upcomingEvents

    /*useEffect(() => {
        if (calEvent.current != null) {
            console.log("Current latest event");
            console.log(calEvent.current);
            calEvent.current.scrollIntoView(false);
        }
    },[calEvent.current]);*/

    useEffect(() => {
        if (calEvent.current !== null && selectedDay !== 0) {
            //calEvent.current.children[upcomingEvents.findIndex(event => event.date === selectedDay) !== -1 ? upcomingEvents.findIndex(event => event.date === selectedDay) + 1 : 0].scrollIntoView(false);
        }
    },[selectedDay, upcomingEvents]);

    const ProfileContent = (<div>
        <div id="profile-calendar"><Calendar calendar={content.calendar} handleDay={setDay} /></div>
        <div id="profile-upcoming" ref={calEvent}>{upcomingEvents.map((event, id) => 
            <div key={id} className="profile-event"> {/*event.date.substring((`${event.date}` === `${selectedDay}` ? calEvent : event.date.indexOf('/') + 1)) === `${new Date(Date.now()).getMonth() + 1}` ? event.date.substring(0, event.date.indexOf('/')) === `${new Date(Date.now()).getDate()}` ? calEvent : null : null*/}
            <div className="profile-event-title">{event.name}</div>
            <div className="profile-event-date">{event.date}</div>
        </div>
        )}</div>
        <div id="profile-feed">Some feed {content.feed}</div>
    </div>);

    return ProfileContent;
}

const Calendar = ({ calendar, handleDay }) => {

    const [monthShown, setMonth] = useState(calendar.months[0]);
    useEffect(() => {
        if (calendar.months.length > 1) {
            setMonth(calendar.months[new Date(Date.now()).getMonth()]);
        }
    },[calendar.months]);

    return (<div className="calendar">
        <div id="calendar-name">{calendar.name}</div>
        <div id="calendar-month"><div className="button" onClick={() => setMonth(calendar.months[calendar.months.findIndex(month => month.name === monthShown.name) - 1])}>{'<|'}</div>{monthShown.name}<div className="button" onClick={() => setMonth(calendar.months[calendar.months.findIndex(month => month.name === monthShown.name) + 1])}>{'|>'}</div></div>
        <div id="calendar-days">{monthShown.days.map((day, id) => <div key={id} style={{
            gridRow: (day.number / 7) % 5 === 0 ? (day.number / 7) % 5 + 1 : (day.number / 7) % 5,
            gridColumn: day.number % 7 === 0 ? 7 : day.number % 7
        }} onClick={() => { console.log(`${day.number}/${calendar.months.findIndex(month => month.name === monthShown.name) + 1}`); handleDay(`${day.number}/${calendar.months.findIndex(month => month.name === monthShown.name) + 1}`); }}>{day.number}</div>)}</div>
    </div>);
}

export { Profile }