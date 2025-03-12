import { Link } from 'react-router-dom';
import logo from '../../assets/images/logo.png';
import '../../assets/style/css/style.css';
import NavBar from './NavBar';


function Header() {

    //TODO -> replace logo with something that can be more easily adjusted 
    return (
        <div className='header'>
            <div className='header__container'>
                <div className='header__logoBox'>
                    <Link to='/'>
                        <img src={logo} alt="Logo" className="header__logo">
                        </img>
                    </Link>
                </div>

                <NavBar />

            </div>
        </div >
    );
}

export default Header;