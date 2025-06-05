import { useState, useEffect } from "react";
import { NavLink } from "react-router-dom";

import voyago from "../../assets/images/voyago.png";

import Cookies from "js-cookie";

import LoginPopUp from "../account/LoginPopUp";
import RegisterPopUp from "../account/RegisterPopUp";

function NavBar() {
  let isLoggedIn = !!Cookies.get("accessToken");

  const [LoginPopupVisibility, setLoginPopupVisibility] = useState(false);
  const [RegisterPopupVisibility, setRegisterPopupVisibility] = useState(false);

  const handleLoginToggle = () => {
    setLoginPopupVisibility(!LoginPopupVisibility);
  };

  const handleRegisterToggle = () => {
    setRegisterPopupVisibility(!RegisterPopupVisibility);
  };

  useEffect(() => {
    //disables back button on login popup
    if (LoginPopupVisibility) {
      window.history.pushState(null, "", window.location.href);

      const handleBackButton = (e) => {
        e.preventDefault();
        console.log("HANDLE BACK BUTTON");
        window.history.pushState(null, "", window.location.href);
      };

      window.addEventListener("popstate", handleBackButton);
    }
  }, [LoginPopupVisibility, setLoginPopupVisibility]);

  //TODO -> THEY ARE EITHER GONNA BE NAVLINKS OR A -elements; we cant have it both ways;
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
