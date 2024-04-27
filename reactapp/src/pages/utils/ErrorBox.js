import "./ErrorBox.css";

const ErrorBox = ({ error, className, setError }) => {

    return (<div className={className} id="error-box">
        <div id="error-title">{error.code}</div>
        <div id="error-message">{error.message}</div>
        <div id="error-btn" onClick={() => {

            if (error.func) {
                error.func(error.funcData);
            }
            setError({ code: null, message: null });
        }}>OK</div>
    </div>);
}

export default ErrorBox;