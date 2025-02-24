import Cookies from 'js-cookie';
import AccountMenuDropdown from '../account/AccountMenuDropdown';
import { useState } from 'react';

import arrowdown from '../../assets/icons/arrowdown.png';
import arrowup from '../../assets/icons/arrowup.png';

function NavBar({ handleLoginToggle }) {
    let isLoggedIn = !!Cookies.get('accessToken'); 

    const [showUserMenu, setShowUserMenu] = useState(false);

    const NoUserNav =
        <div className='header__navigation-user'>
            <a href='#'
                className='header__navigation-login'
                onClick={handleLoginToggle}
            >
                Login
            </a>
            <a href='/register'>
                Sign up
            </a>
        </div>

    return (
        <div className='header__navigation'>
            <div className="header__navigation-browse">
                <a href="http://localhost:3000/"  >
                    Trains
                </ a>
                <a href="http://localhost:3000/">
                    Buses
                </ a >
                <a href="http://localhost:3000/">
                    Flights
                </a>
            </div>

            {isLoggedIn ?
                <div className='header__navigation-user'>
                    <a onClick={() => (setShowUserMenu(!showUserMenu))} href='#' >
                        <span>Account   </span>
                        <img src={showUserMenu ? arrowup : arrowdown} alt='arrow up/down' />
                    </a>

                    {showUserMenu ? <AccountMenuDropdown /> : null}

                </div>
                :
                NoUserNav

            }

        </div>
    )
}

export default NavBar;