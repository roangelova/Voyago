import { useEffect } from "react";

import { toast } from "react-toastify";
import { Account } from "../../services/accountService";
import Cookies from "js-cookie";

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

        Cookies.set(
          "accessToken",
          JSON.stringify({
            accessToken: res.accessToken,
            tokenType: res.tokenType,
          }),
          {
            expires: new Date(res.accessTokenExpires),
            secure: true,
            sameSite: false,
          }
        );

        Cookies.set(
          "refreshToken",
          JSON.stringify({ refreshToken: res.refreshToken }),
          {
            expires: new Date(res.refreshTokenExpires),
            secure: true,
            sameSite: false,
          }
        );

        toast.success("Logged in! Taking you to the homepage in 3, 2, 1..");
        setTimeout(() => {
          window.location.href = "/";
        }, 3000);
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
