import Cookies from 'js-cookie';
import AccountMenuDropdown from '../account/AccountMenuDropdown';
import { useState } from 'react';
import { NavLink } from 'react-router-dom';

import arrowdown from '../../assets/icons/arrowdown.png';
import arrowup from '../../assets/icons/arrowup.png';

function NavBar({ handleLoginToggle }) {
    let isLoggedIn = !!Cookies.get('accessToken');

    const [showUserMenu, setShowUserMenu] = useState(false);

    const NoUserNav =
        <ul className='header__navigation-user'>
            <li
                className='header__navigation-login'
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

    return (
        <nav className='header__navigation'>
            <ul className="header__navigation-browse">
            <li>
                    <NavLink to='/'>
                        Home
                    </NavLink>
                </ li>
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

            {isLoggedIn ?
                <ul className='header__navigation-user'>
                    <li onClick={() => (setShowUserMenu(!showUserMenu))} href='#' >
                        <span>Account </span>
                        <img src={showUserMenu ? arrowup : arrowdown} alt='arrow up/down' />
                    </li>

                    {showUserMenu ? <AccountMenuDropdown /> : null}

                </ul>
                :
                NoUserNav

            }

        </nav>
    )
}

export default NavBar;