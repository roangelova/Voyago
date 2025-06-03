import { useState, useEffect } from "react";
import { NavLink } from "react-router-dom";

import voyago from "../../assets/images/voyago.png";

import Cookies from "js-cookie";

import Login from "../account/Login";
import RegisterPopUp from "../account/RegisterPopUp";

function NavBar() {
  let isLoggedIn = !!Cookies.get("accessToken");

  const [LoginPopupVisibility, setLoginPopupVisibility] = useState(false);

  const handleLoginToggle = () => {
    setLoginPopupVisibility(!LoginPopupVisibility);
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
        <a href="/register">Register</a>
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
          <div className="login blur-overlay">
            <Login setLoginPopupVisibility={setLoginPopupVisibility} />
          </div>
        ) : null}
      </nav>
      {/*TODO: FOR DEVELOPMENT PURPOSES ONLY */}
      <div className="blur-overlay">
        <RegisterPopUp />
      </div>
    </>
  );
}

export default NavBar;
