import React, { useState, useEffect } from 'react';

import "./Profile.css";



const Profile = (props) => {
    const [isVisible, setVisible] = useState(props.isVisible);
    //const [loading, setLoading] = useState(true);
    const [profileInfo, setProfileInfo] = useState({ picture: "", info: { name: "", lastname: "", mail: "", group: "", curriculum: "" }, sidebar: { info: "", }, content: { calendar: { name: "Some calendar", currentMonth: "", months: [{ name: "", days: [{ number: 1 }]}]}, upcoming: [ "",], feed: ["",] } });
    //const [img, setPicture] = useState({ img: "" });
    const id = 2;
    // ON MOUNT EFFECT
    useEffect(() => {
        let ignore = false;
        loadProfileInfo({ ignore, setProfileInfo, id });

        return () => {
            ignore = true;
        }
    }, []);

    return (
        <div id="profile-page">
            {isVisible === "Profile" && <div id="profile">
                <div id="profile-container">
                    <div id="profile-picture"><RenderPicture id="picture-image" img={profileInfo.picture} /></div>
                    <div id="profile-info"><RenderProfileInfo data={profileInfo.info} /></div>
                </div>
                <div id="profile-sidebar">TEST 3</div>
                <div id="profile-divider"></div>
                <div id="profile-content"><RenderProfileContent content={profileInfo.content} /></div>
            </div>}
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
        console.log("This is the response: " + data);
        console.log("loadProfileInfo::props: " + props);

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
    //
    const ProfileContent = (<div>
        <div id="profile-calendar"><Calendar calendar={content.calendar}/></div>
        <div id="profile-upcoming">{content.upcoming}</div>
        <div id="profile-feed">Some feed {content.feed}</div>
    </div>);

    return ProfileContent;
}

const Calendar = ({ calendar }) => {
    var id = 0;
    return (<div className="calendar">
        <div id="calendar-name">{calendar.name}</div>
        <div id="calendar-month">{calendar.currentMonth}</div>
        <div id="calendar-days">{calendar.months[0].days.map(day => <div key={id++} style={{
            gridRow: (day.number/7)%5 === 0 ? (day.number/7)%5 + 1 : (day.number/7)%5,
            gridColumn: day.number%7 === 0 ? 7 : day.number%7
        }}>{day.number}</div>)}</div>
    </div>);
}

export { Profile }