function Timer() {

    //TODO --> implement countdown

    return (
        <div className="timer">
            <p>Your seat is reserved for</p>
            <div className="timer_content">
                <div className="timer_part">
                    <span className="timer_part--time">15</span>
                    <span className="timer_part--text">minutes</span>
                </div>
                <span>:</span>
                <div className="timer_part">
                    <span className="timer_part--time">00</span>
                    <span className="timer_part--text">seconds</span>
                </div>
            </div>
        </div>
    )
}

export default Timer; 