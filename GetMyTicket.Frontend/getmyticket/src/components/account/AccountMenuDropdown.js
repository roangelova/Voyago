import { toast } from 'react-toastify';
import bookings from '../../assets/icons/bookings.png';
import logout from '../../assets/icons/logout.png';
import profile from '../../assets/icons/profile.png';
import { Account } from '../../services/accountService';

function AccountMenuDropdown() {

    function handleLogout() {
        Account.logout()
            .then(res => {
                toast.info(res);

                setTimeout(() => {
                    window.location.href = "/";
                }
                    , 3000);
            })
            .catch(err => {
            console.error(err)
            })
    }

    return (
        <ul className="accountMenu">
            <a href='/user/profile'>
                <li className="accountMenu__item">
                    <img src={profile} alt='My profile icon' />
                    <span>Profile</span>
                </li>
            </a>

            <a href='/user/bookings'>
                <li className="accountMenu__item">
                    <img src={bookings} alt='My bookings icon' />
                    <span>My bookings</span>
                </li>
            </a>

            <a href='#' onClick={handleLogout}>
                <li className="accountMenu__item">
                    <img src={logout} alt='Log out icon' />
                    <span>Log out</span>
                </li>
            </a >
        </ul>

    )
}

export default AccountMenuDropdown;