import { NavLink, useNavigate } from "react-router-dom";

import logout from "../../assets/icons/logout.png";
import booking from "../../assets/icons/booking.png";
import creditcard from "../../assets/icons/creditcard.png";
import passenger from "../../assets/icons/passenger.png";
import help from "../../assets/icons/help.png";
import notification from "../../assets/icons/notification.png";

import { Account } from "../../services/accountService";
import { toast } from "react-toastify";
import Cookies from "js-cookie";

function AccountNav() {
  const navigate = useNavigate();

  function UserAfterLogoutCleanUp() {
    Cookies.remove("accessToken");
    Cookies.remove("refreshToken");
    sessionStorage.removeItem("userId");
    navigate("/");
  }

  function handleLogout() {
    Account.logout()
      .then((res) => {
        toast.info(res);
      })
      .catch((err) => {
        toast.error(
          err.response?.data ?? "There was an error logging you out."
        );
      })
      .finally(() => {
        setTimeout(UserAfterLogoutCleanUp, 1000);
      });
  }

  return (
    <aside className="account__nav">
      <nav>
        <ul className="account__nav--list">
          <NavLink to="/account/bookings" className="account__nav--item">
            <img src={booking} alt="Logout icon" />
            <span>Bookings</span>
          </NavLink>
          <NavLink to="/account/passengers" className="account__nav--item">
            <img src={passenger} alt="Passengers icon" />
            <span>Passengers</span>
          </NavLink>
          <NavLink to="/account/paymentMethods" className="account__nav--item">
            <img src={creditcard} alt="Payment methods icon" />
            <span>Payment Methods</span>
          </NavLink>
          <NavLink to="/account/notifications" className="account__nav--item">
            <img src={notification} alt="Notifications icon" />
            <span>Notifications</span>
          </NavLink>
          <NavLink to="/account/help" className="account__nav--item">
            <img src={help} alt="Help icon" />
            <span>Help</span>
          </NavLink>
          <li className="account__nav--item" onClick={handleLogout}>
            <img src={logout} alt="Logout icon" />
            <span>Sign out</span>
          </li>
        </ul>
      </nav>
    </aside>
  );
}

export default AccountNav;
