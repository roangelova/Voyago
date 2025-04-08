import { toast } from 'react-toastify';

import bookings from '../../assets/icons/bookings.png';
import logout from '../../assets/icons/logout.png';
import profile from '../../assets/icons/profile.png';

import { Account } from '../../services/accountService';
import Cookies from "js-cookie";

function AccountMenuDropdown() {

    function handleLogout() {
        Account.logout()
            .then(res => {
                toast.info(res);
            })
            .catch(err => {
                toast.error(err.response?.data ?? 'There was an error logging you out.')
            })
            .finally(() => {

                Cookies.remove('accessToken');
                Cookies.remove('refreshToken');
                sessionStorage.removeItem('userId');

                setTimeout(() => {
                    window.location.href = "/";
                }
                    , 3000);
            })
    }

    return (
        <ul className="accountMenu">
            <a href='/account/profile'>
                <li className="accountMenu__item">
                    <img src={profile} alt='My profile icon' />
                    <span>Profile</span>
                </li>
            </a>

            <a href='/account/bookings'>
                <li className="accountMenu__item">
                    <img src={bookings} alt='My bookings icon' />
                    <span>My bookings</span>
                </li>
            </a>

            <a href='#' onClick={handleLogout}>
                <li className="accountMenu__item">
                    <img src={logout} alt='Log out icon' />
                    <span>Sign out</span>
                </li>
            </a >
        </ul>

    )
}

export default AccountMenuDropdown;