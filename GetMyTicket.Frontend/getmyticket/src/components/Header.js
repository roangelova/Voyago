import logo from '../assets/images/logo.png';
import '../assets/style/css/style.css';

function Header() {
    return (
        <header className='header'>
            <div className='header__logoBox'>
                <div>
                    <img src={logo} alt="Logo" className="header__logo">
                    </img>
                </div>
                <div className='header__heading'>
                    <h1>Tickify</h1>
                </div>
            </div>
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
                <div className='header__navigation-user'>
                    <a href='/login' className='header__navigation-login'>
                        Login
                    </a>
                    <a href='/register'>
                        Sign up
                    </a>
                </div>
            </div>
        </header >
    );
}

export default Header;