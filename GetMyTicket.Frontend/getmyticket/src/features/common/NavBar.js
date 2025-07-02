import { useState, useEffect } from "react";
import { NavLink } from "react-router-dom";

import voyago from "../../assets/images/voyago.png";

import Cookies from "js-cookie";

import LoginPopUp from "../account/LoginPopUp";
import RegisterPopUp from "../account/RegisterPopUp";

function NavBar() {
  const [isLoggedIn, setIsLoggedIn] = useState(!!Cookies.get("accessToken"));

  const [LoginPopupVisibility, setLoginPopupVisibility] = useState(false);
  const [RegisterPopupVisibility, setRegisterPopupVisibility] = useState(false);

  const handleLoginToggle = () => {
    setLoginPopupVisibility(!LoginPopupVisibility);
  };

  const handleRegisterToggle = () => {
    setRegisterPopupVisibility(!RegisterPopupVisibility);
  };

  function handleUpdateUserNav() {
    setIsLoggedIn(!!Cookies.get("accessToken"));
  }

  useEffect(() => {
    window.addEventListener("refreshUser", handleUpdateUserNav);

    return () => {
      window.removeEventListener("refreshUser", handleUpdateUserNav);
    };
  }, []);

  const NoUserNav = (
    <ul className="navigation__user">
      <li className="navigation__login" onClick={handleLoginToggle}>
        <a>Login</a>
      </li>
      <span></span>
      <li>
        <a onClick={handleRegisterToggle}>Register</a>
      </li>
    </ul>
  );

  return (
    <>
      <nav className="navigation">
        <div className="navigation__logo">
          <NavLink to="/">
            <img src={voyago} alt="Site logo and name" />
          </NavLink>
        </div>
        <ul className="navigation__browse">
          <li>
            <NavLink to="/trains">Trains</NavLink>
          </li>
          <li>
            <NavLink to="/buses">Buses</NavLink>
          </li>
          <li>
            <NavLink to="/flights">Flights</NavLink>
          </li>
        </ul>

        {isLoggedIn ? <NavLink to="account">Account</NavLink> : NoUserNav}

        {LoginPopupVisibility ? (
          <div className="login blur-overlay" onClick={handleLoginToggle}>
            <div onClick={(e) => e.stopPropagation()}>
              <LoginPopUp setLoginPopupVisibility={setLoginPopupVisibility} />
            </div>
          </div>
        ) : null}
      </nav>
      {RegisterPopupVisibility ? (
        <div className="blur-overlay" onClick={handleRegisterToggle}>
          <div onClick={(e) => e.stopPropagation()}>
            <RegisterPopUp
              setLoginPopupVisibility={setLoginPopupVisibility}
              setRegisterPopupVisibility={setRegisterPopupVisibility}
            />
          </div>
        </div>
      ) : null}
    </>
  );
}

export default NavBar;
