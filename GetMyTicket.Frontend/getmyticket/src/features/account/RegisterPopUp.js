import googleLogo from "../../assets/icons/google.svg";

import register from "../../assets/images/register.jpg";

function RegisterPopUp({
  setLoginPopupVisibility,
  setRegisterPopupVisibility,
}) {
  const onLoginClick = () => {
    setLoginPopupVisibility(true);
    setRegisterPopupVisibility(false);
  };

  return (
    <div className="register">
      <div className="register__container">
        <div className="register__left">
          <img src={register} />
        </div>
        <div className="register__right">
          <h3 className="heading--tertiary">Sign up</h3>
          <span className="register__row">
            Already have an account? <a onClick={onLoginClick}>Login</a>
          </span>
          <div className="register__row register__provider">
            <img src={googleLogo} />
            <span>Sign up with Google</span>
          </div>
          <div className="register__row">or</div>
          <div className="register__box">
            <div>
              <label htmlFor="email">Email</label>
              <input
                name="email"
                type="email"
                placeholder="email@example.com"
                autoComplete="on"
              ></input>
            </div>
            <div>
              <label htmlFor="password">Password</label>
              <input
                name="password"
                type="password"
                placeholder="********"
              ></input>
            </div>

            <div>
              <label htmlFor="confirmPassword">Confirm password</label>
              <input
                name="confirmPassword"
                type="password"
                placeholder="********"
              ></input>
            </div>
            <button>Join</button>
            <p className="register__agreement">
              By joining, you agree to our <a>Terms and Conditions</a> and{" "}
              <a>Privacy Policy</a>
            </p>
          </div>
        </div>
      </div>
    </div>
  );
}

export default RegisterPopUp;
