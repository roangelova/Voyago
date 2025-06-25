import { Outlet } from "react-router-dom";
import AccountNav from "../common/AccountNav";

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
