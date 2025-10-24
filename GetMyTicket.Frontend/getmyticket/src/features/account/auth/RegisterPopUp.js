import { toast } from "react-toastify";
import googleLogo from "../../../assets/icons/google.svg";

import register from "../../../assets/images/register.jpg";
import { Account } from "../../../services/accountService";

function RegisterPopUp({
  setLoginPopupVisibility,
  setRegisterPopupVisibility,
}) {
  const onLoginClick = () => {
    setLoginPopupVisibility(true);
    setRegisterPopupVisibility(false);
  };

  function registerUser(e) {
    e.preventDefault();
    const formData = new FormData(e.target);
    let email = formData.get("email").trim();
    let password = formData.get("password").trim();
    let confirmPassword = formData.get("confirmPassword").trim();
    let newsletterSubscribtion = formData.get("newsletterSubscription");

    if (email == "") {
      toast.error(
        "All fields are required for registration! Please, try again."
      );
      return;
    }

    if (password !== confirmPassword) {
      toast.error("Password and confirm password do not match!");
      return;
    }

    let checkboxValue = newsletterSubscribtion === "on" ? true : false;

    Account.register({ email, password, checkboxValue })
      .then((res) => {
        if (res.succeeded) {
          toast.success(
            "Registration successful.Taking you to login page in 3, 2, 1 .. "
          );

          setTimeout(() => {
            setLoginPopupVisibility(true);
            setRegisterPopupVisibility(false)
           // window.location.href = "/";
          }, 3000);
        }
      })
      .catch((err) => {
        console.log(err);
        let errorMessage = "";
        err.response.data.forEach((error) => {
          errorMessage += error.description;
          errorMessage += " ";
        });
        toast.error(err);
      });
  }

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
          <form className="register__box" onSubmit={registerUser}>
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
            <div className="register__checkbox">
              <input
                type="checkbox"
                defaultChecked
                name="newsletterSubscription"
              ></input>
              <label htmlFor="newsletterSubscription">
                Subscribe to newsletter
              </label>
            </div>
            <button type="submit">Join</button>
            <p className="register__agreement">
              By joining, you agree to our <a>Terms and Conditions</a> and{" "}
              <a>Privacy Policy</a>
            </p>
          </form>
        </div>
      </div>
    </div>
  );
}

export default RegisterPopUp;
