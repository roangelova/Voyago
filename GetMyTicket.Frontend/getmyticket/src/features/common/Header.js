import { Link } from 'react-router-dom';
import '../../assets/style/css/style.css';
import NavBar from './NavBar';

//TODO -> FIX TINY JUMP (NAV SHIFTS A BIT TO THE LEFT WHEN NOT IN HEADER)
function Header() {
    return (
        <div className='header'>
            <div className='header__container'>
              

                <NavBar />

            </div>
        </div >
    );
}

export default Header;