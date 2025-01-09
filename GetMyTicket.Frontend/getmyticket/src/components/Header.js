import logo from '../assets/images/logo.png';
import '../assets/style/css/style.css';

function Header() {
    return (
        <header className="header">

            <div className='LogoAndHeading'>
                <div className="logo-box">
                    <img src={logo} alt="Logo" className="logo">
                    </img>
                </div>
                <div className='heading-primary'>
                    <h1>Get my ticket</h1>
                </div>
            </div>
            <div className='navigation'>
                <div className="nav-elements">
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
                <div className='login'>
                    <a href="http://localhost:3000/">
                        Login | Sign up
                    </a>
                </div>
            </div>
        </header >
    );
}

export default Header;