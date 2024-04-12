import "./ErrorBox.css";

const ErrorBox = ({ error, className, toggleVis, vis }) => {

    return (<div className={className} id="error-box">
        <h2 id="error-title">{error.title}</h2>
        <div id="error-message">{error.message}</div>
        <div id="error-btn" onClick={() => toggleVis(vis.substring(0, vis.indexOf(" ErrorBox")))}>OK</div>
    </div>);
}

export default ErrorBox;