import { useEffect } from 'react';
import { Link } from 'react-router-dom';
import logo from '../../assets/images/logo.png';
import '../../assets/style/css/style.css';
import Login from '../account/Login';
import NavBar from './NavBar';


function Header({ LoginPopupVisibility, handleLoginToggle, setLoginPopupVisibility }) {

    useEffect(() => {
        if (LoginPopupVisibility) {
            window.history.pushState(null, "", window.location.href);

            const handleBackButton = () => {
                window.history.pushState(null, "", window.location.href);
            };

            window.addEventListener("popstate", handleBackButton);

            return () => {
                window.removeEventListener("popstate", handleBackButton);
            };
        }
    }, [LoginPopupVisibility]);


    return (
        <div className='header__container'>
            <div className={`header ${LoginPopupVisibility ? 'blurred' : ''}`}>
                <div className='header__logoBox'>
                    <div>
                        <Link to='/'>
                            <img src={logo} alt="Logo" className="header__logo">
                            </img>
                        </Link>
                    </div>
                    <div className='header__heading'>
                        <h1>Tickify</h1>
                    </div>
                </div>

                <NavBar handleLoginToggle={handleLoginToggle} />

            </div>

            {
                LoginPopupVisibility ?
                    <div className='login'> <Login setLoginPopupVisibility={setLoginPopupVisibility} />
                    </div> :
                    null
            }

        </div >
    );
}

export default Header;