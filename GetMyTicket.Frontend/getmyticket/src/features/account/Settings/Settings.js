import { toast } from "react-toastify";
import {
  Account,
  cleanUpAfterUserLogout,
} from "../../../services/accountService";

function Settings() {
  function handleCloseAccount() {
    let confirm = window.confirm(
      "Are you sure you want to close your account? You wont't be able to reactivate it.",
    );

    //TODO --> trigger logout after account is deleted
    if (confirm) {
      let userId = localStorage.getItem("userId");

      console.log(userId);
      Account.delete(userId)
        .then(() => {
          toast.success("Your account was closed successfully!");
          setTimeout(cleanUpAfterUserLogout, 1000);
        })
        .catch((err) => {
          toast.error(err);
        });
    }

    return;
  }

  return <p onClick={handleCloseAccount}>Click here to close your account</p>;
}

export default Settings;
