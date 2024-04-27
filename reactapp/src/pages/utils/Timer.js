import { useEffect, useState } from "react";
import "./Timer.css";

const Timer = ({ time }) => {
    const timer = useCalcTime(time);

    return (<div id="timer">
        {/*  */}
        {timer && timer.minutes}
        {" "}
        {timer && timer.seconds}
        {/* {count && count.value} */}
    </div>);
}

const calcTime = (count, setCount) => {

    const secCount = count.value % 60;
    const minCount = Math.floor(count.value / 60);
    const computeSec = String(secCount).length === 1 ? `0${secCount}` : secCount;
    const computeMin = String(minCount).length === 1 ? `0${minCount}` : minCount;

    setCount({ ...count, minutes: computeMin, seconds: computeSec, value: count.value - 1 });
}

export const useCalcTime = (time) => {
    const [count, setCount] = useState({ start: true, minutes: "00", seconds: "00", value: time });

    useEffect(() => {
        let interval;
        if (count.value === 0 && count.start) {
            console.log();
        } else if (count.value === 0) {
            // Resetuje czas
            setCount({ ...count, start: false });
        } else if (count.start) {
            interval = setInterval(() => {
                const secCount = count.value % 60;
                const minCount = Math.floor(count.value / 60);
                const computeSec = String(secCount).length === 1 ? `0${secCount}` : secCount;
                const computeMin = String(minCount).length === 1 ? `0${minCount}` : minCount;

                setCount({ ...count, minutes: computeMin, seconds: computeSec, value: count.value - 1 });
            }, 1000);
        }



        return () => {
            clearInterval(interval);
        }
    }, [count]);

    return count;
}

export default Timer;