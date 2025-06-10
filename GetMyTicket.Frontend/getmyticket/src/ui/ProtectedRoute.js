import Cookies from "js-cookie";
import React, { useEffect } from "react";
import { Outlet, useNavigate } from "react-router-dom";
import { AccountProvider } from "../features/account/AccountContext";

function ProtectedRoute({children}) {
    const navigate = useNavigate();
    let isLoggedIn = !!Cookies.get('accessToken');

    useEffect(() => {
        if (!isLoggedIn) {
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