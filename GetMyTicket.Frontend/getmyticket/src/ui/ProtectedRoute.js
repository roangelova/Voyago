import Cookies from "js-cookie";
import React, { useEffect } from "react";
import { Outlet, useNavigate } from "react-router-dom";
import { AccountProvider } from "../features/account/AccountContext";

function ProtectedRoute({children}) {
    const navigate = useNavigate();
    let isLoggedIn = !!Cookies.get('accessToken');

    useEffect(() => {
        if (!isLoggedIn) {
            //makes sure we update the navigation. There isn't a user anymore
            window.dispatchEvent(new Event("refreshUser"));
            navigate('/');
        }
    }, [isLoggedIn, navigate])

    return (
        <AccountProvider>
           <Outlet/>
        </AccountProvider>
    );
}

export default ProtectedRoute;