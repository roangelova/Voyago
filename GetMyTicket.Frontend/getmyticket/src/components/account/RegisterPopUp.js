import referee_program from "../../assets/images/referee_cta.webp"

import facebookLogo from "../../assets/icons/facebook.svg";
import googleLogo from "../../assets/icons/google.svg";
import appleLogo from "../../assets/icons/apple.svg";

function RegisterPopUp() {

    //TODO -> add as an actual popup and make background blurry

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
                        <div>
                            <img src={facebookLogo}
                                alt="Facebook logo"
                                className="register__options-logo" >
                            </img>
                            <a href="#" >
                                Continue with Facebook
                            </a>
                        </div>

                        <div>
                            <img src={googleLogo}
                                alt="Google logo"
                                className="register__options-logo">
                            </img>
                            <a href="#">
                                Continue with Google
                            </a>
                        </div>

                        <div>
                            <img src={appleLogo}
                                alt="Apple logo"
                                className="register__options-logo">
                            </img>
                            <a href="#">
                                Continue with Apple
                            </a>

                        </div>
                        <div>
                            <a href="/register-form">
                                Continue to registration form
                            </a>
                        </div>

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