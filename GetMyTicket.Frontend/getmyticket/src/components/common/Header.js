import { useState, useEffect } from 'react';
import logo from '../../assets/images/logo.png';
import '../../assets/style/css/style.css';
import Login from '../account/Login';
import NavBar from './NavBar';


function Header() {
    const [LoginPopupVisibility, setLoginPopupVisibility] = useState(false);

    const handleLoginToggle = () => {
        setLoginPopupVisibility(true);
    };

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
                        <img src={logo} alt="Logo" className="header__logo">
                        </img>
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