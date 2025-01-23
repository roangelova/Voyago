
function Login() {

    return (

        <div className="login__form">
            <form >
                <div className="login__row">
                    <label htmlFor="email" >
                        Email:
                    </label>
                    <input
                        type="text"
                        name="email"
                    >
                    </input>
                </div>

                <div className="login__row">
                    <label htmlFor="password" >
                        Password:               </label>
                    <input
                        type="password"
                        name="password"
                    >
                    </input>
                </div>

                <button
                    className="login__btn"
                    type="submit">
                    Login
                </button>


            </form>

        </div>
    )
}

export default Login;