import { useState, useEffect } from 'react';
import { NavLink } from 'react-router-dom';

import voyago from '../../assets/images/voyago.png'

import Cookies from 'js-cookie';

import AccountMenuDropdown from '../account/AccountMenuDropdown';
import Login from '../account/Login';

import arrowdown from '../../assets/icons/arrowdown.png';
import arrowup from '../../assets/icons/arrowup.png';

function NavBar() {
    let isLoggedIn = !!Cookies.get('accessToken');

    const [showUserMenu, setShowUserMenu] = useState(false);
    const [LoginPopupVisibility, setLoginPopupVisibility] = useState(false);

    const handleLoginToggle = () => {
        setLoginPopupVisibility(!LoginPopupVisibility);
    };

    useEffect(() => {
        //disables back button on login popup
        if (LoginPopupVisibility) {
            window.history.pushState(null, "", window.location.href);

            const handleBackButton = (e) => {
                e.preventDefault()
                console.log('HANDLE BACK BUTTON')
                window.history.pushState(null, "", window.location.href);
            };

            window.addEventListener("popstate", handleBackButton);

        }
    }, [LoginPopupVisibility, setLoginPopupVisibility]);

    const NoUserNav =
        <ul className='navigation__user'>
            <li
                className='navigation__login'
                onClick={handleLoginToggle}
            >
                Login
            </li>
            <li>
                <NavLink to='/register'>
                    Register
                </NavLink>
            </li>
        </ul>

    const UserNav =
        <ul className='navigation__user'>
            <li onClick={() => (setShowUserMenu(!showUserMenu))} href='#' >
                <span>Account </span>
                <img src={showUserMenu ? arrowup : arrowdown} alt='arrow up/down' />
            </li>



        </ul>

    return (
        <>
            <nav className='navigation'>
                <div className='navigation__logo'>
                    <NavLink to="/">
                        <img src={voyago} alt='Site logo and name' />
                    </NavLink>
                </div>
                <ul className="navigation__browse">
                    <li>
                        <NavLink to='/trains'>
                            Trains
                        </NavLink>
                    </ li>
                    <li>
                        <NavLink to='/buses'>
                            Buses
                        </NavLink>
                    </li >
                    <li>
                        <NavLink to='/flights'>
                            Flights
                        </NavLink>
                    </li>
                </ul>

                {isLoggedIn ? UserNav : NoUserNav}

                {
                    LoginPopupVisibility ?
                        <div className='login blur-overlay'>
                            <Login setLoginPopupVisibility={setLoginPopupVisibility} />
                        </div> :
                        null
                }

            </nav>

            {showUserMenu ? <AccountMenuDropdown /> : null}</>
    )
}

export default NavBar;