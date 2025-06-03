import { Outlet } from "react-router-dom";
import AccountNav from "../common/AccountNav";

//TODO -> after this is fully implemented, adjust AccountMenuDropdown.

function Account() {
  return (
    <div className="account">
      <AccountNav />
      <div className="account__content">
        <Outlet />
      </div>
    </div>
  );
}

export default Account;
