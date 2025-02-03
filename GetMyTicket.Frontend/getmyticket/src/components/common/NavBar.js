import Cookies from 'js-cookie';

function NavBar({ handleLoginToggle }) {
    let isLoggedIn = !!Cookies.get('accessToken');

    const UserNav =
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
                <div>
                    <a href='#' className='header__navigation-user'>
                        Account
                    </a>
                </div>
                :
                UserNav
            }

        </div>
    )
}

export default NavBar;