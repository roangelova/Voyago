import { toast } from "react-toastify";
import { Account } from "../../services/accountService";
import { setAccessAndRefreshTokenInCookies } from "../../helpers";

function Login({ setLoginPopupVisibility }) {
  const Login = (formData) => {
    let email = formData.get("email");
    let password = formData.get("password");

    if (email == "" || password == "") {
      toast.error("Email and password required for login.");
      return;
    }

    Account.login({ email, password })
      .then((res) => {
        sessionStorage.setItem("userId", res.userId);
        window.dispatchEvent(new Event("userIdSet"));
        setAccessAndRefreshTokenInCookies(res);
        toast.success("You have logged in successfully!");
        //NOTE: we do not navigate. User might want to login after clicking on a trip to book
        setLoginPopupVisibility(false);
      })
      .catch((err) => {
        toast.error(err.response?.data ?? "There was an error logging you in.");
      });
  };

  return (
    <div className="login">
      <div className="login__container">
        <form action={Login}>
          <div className="login__row">
            <label htmlFor="email">Email:</label>
            <input type="text" name="email" placeholder="Email"></input>
          </div>
          <div className="login__row">
            <label htmlFor="password">Password: </label>
            <input
              type="password"
              placeholder="Password"
              name="password"
            ></input>
          </div>
          <button className="login__btn" type="submit">
            Login
          </button>
        </form>
      </div>
    </div>
  );
}

export default Login;
