import referee_program from "../assets/images/referee_cta.webp"

function RegisterPopUp() {

    return (
        <div className="register">
            <div className="register__container">
                <div className="register__left-side">
                    <img src={referee_program}
                        alt="A promo for our referee program"
                        className="register__cta-img"
                    />
                    <h3>Get the full experience.</h3>
                    <p>Enjoy bonus points with our referee program now! </p>
                </div>

                <div className="register__right-side">
                    <div className="register__options">
                        <a href="#" >
                            Continue with Facebook
                        </a>
                        <a href="#">
                            Continue with Google
                        </a>
                        <a href="#">
                            Continue with Apple
                        </a>
                        <a href="#">
                            Continue to registration form
                        </a>
                    </div>
                    <p>
                        By creating an account you agree to our
                        Terms of Use and Privacy Policy
                    </p>
                </div>
            </div>
        </div>
    )

}

export default RegisterPopUp;